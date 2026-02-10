param(
  [string]$ManifestPath,
  [switch]$Serve,
  [switch]$SkipPython
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$docsDir = Split-Path -Parent $PSCommandPath
$repoRoot = Split-Path $docsDir -Parent
$docfxBaseConfig = Join-Path $docsDir 'docfx.json'
$docfxGeneratedConfig = Join-Path $docsDir 'docfx.generated.json'
$tocPath = Join-Path $docsDir 'toc.yml'
$indexPath = Join-Path $docsDir 'index.md'

function Ensure-Dir {
  param([string]$Path)
  if (-not (Test-Path -LiteralPath $Path)) {
    New-Item -ItemType Directory -Path $Path | Out-Null
  }
}

function Resolve-EnvTokens {
  param([string]$Value)
  if ([string]::IsNullOrWhiteSpace($Value)) { return $Value }

  $expanded = [Environment]::ExpandEnvironmentVariables($Value)

  $expanded = [System.Text.RegularExpressions.Regex]::Replace(
    $expanded,
    '\$\{([A-Za-z_][A-Za-z0-9_]*)\}',
    {
      param($m)
      $v = [Environment]::GetEnvironmentVariable($m.Groups[1].Value)
      if ([string]::IsNullOrEmpty($v)) { return $m.Value }
      return $v
    }
  )

  return $expanded
}

function Resolve-ConfiguredPath {
  param(
    [string]$BasePath,
    [string]$PathValue
  )

  if ([string]::IsNullOrWhiteSpace($PathValue)) { return $null }

  $expanded = Resolve-EnvTokens -Value $PathValue
  if ([System.IO.Path]::IsPathRooted($expanded)) {
    return [System.IO.Path]::GetFullPath($expanded)
  }

  return [System.IO.Path]::GetFullPath((Join-Path $BasePath $expanded))
}

function Get-MarkdownTitle {
  param([string]$Path)

  foreach ($line in Get-Content -LiteralPath $Path -TotalCount 80) {
    if ($line -match '^\s*#\s+(.+)$') {
      return $Matches[1].Trim()
    }
  }

  return [System.IO.Path]::GetFileNameWithoutExtension($Path)
}

function Get-OptionalProperty {
  param(
    [Parameter(Mandatory = $true)] [object]$Object,
    [Parameter(Mandatory = $true)] [string]$Name,
    [object]$DefaultValue = $null
  )

  $prop = $Object.PSObject.Properties[$Name]
  if ($null -ne $prop) {
    return $prop.Value
  }

  return $DefaultValue
}

function Test-PythonHasSphinx {
  param([string]$PythonCommand)

  try {
    & $PythonCommand -c "import sphinx" *> $null
    return ($LASTEXITCODE -eq 0)
  } catch {
    return $false
  }
}

if (-not $ManifestPath) {
  if ($env:DOCS_MANIFEST_PATH) {
    $ManifestPath = $env:DOCS_MANIFEST_PATH
  } else {
    $ManifestPath = Join-Path $docsDir 'repos.manifest.json'
  }
}

$ManifestPath = Resolve-ConfiguredPath -BasePath $repoRoot -PathValue $ManifestPath
if (-not (Test-Path -LiteralPath $ManifestPath)) {
  throw "Manifest file not found: $ManifestPath"
}

if (-not (Test-Path -LiteralPath $docfxBaseConfig)) {
  throw "DocFX config not found: $docfxBaseConfig"
}

if (-not (Get-Command docfx -ErrorAction SilentlyContinue)) {
  throw "DocFX CLI not found in PATH. Install docfx and retry."
}

Write-Host "Using manifest: $ManifestPath"

$manifest = Get-Content -LiteralPath $ManifestPath -Raw | ConvertFrom-Json -Depth 100
if (-not $manifest.repositories) {
  throw "Manifest must include a 'repositories' array."
}

$enabledRepos = @($manifest.repositories | Where-Object { $_.enabled -ne $false })
$csharpRepos = @()
$pythonRepos = @()

foreach ($repo in $enabledRepos) {
  $repoId = [string](Get-OptionalProperty -Object $repo -Name 'id')
  $repoKind = [string](Get-OptionalProperty -Object $repo -Name 'kind')
  $repoRootValue = [string](Get-OptionalProperty -Object $repo -Name 'root')

  if ([string]::IsNullOrWhiteSpace($repoId)) { throw "Each repository entry must include 'id'." }
  if ([string]::IsNullOrWhiteSpace($repoKind)) { throw "Repository '$repoId' is missing 'kind'." }
  if ([string]::IsNullOrWhiteSpace($repoRootValue)) { throw "Repository '$repoId' is missing 'root'." }

  $resolvedRoot = Resolve-ConfiguredPath -BasePath $repoRoot -PathValue $repoRootValue
  if (-not (Test-Path -LiteralPath $resolvedRoot)) {
    throw "Repository root not found for '$repoId': $resolvedRoot"
  }

  $repo | Add-Member -NotePropertyName resolvedRoot -NotePropertyValue $resolvedRoot -Force

  if ($repoKind -eq 'csharp') {
    $csharpRepos += $repo
  } elseif ($repoKind -eq 'python') {
    $pythonRepos += $repo
  } else {
    throw "Unsupported kind '$repoKind' for repo '$repoId'. Supported: csharp, python"
  }
}

if (-not $SkipPython) {
  foreach ($repo in $pythonRepos) {
    $repoId = [string](Get-OptionalProperty -Object $repo -Name 'id')
    $sphinxSourceValue = Get-OptionalProperty -Object $repo -Name 'sphinxSource'
    $publishValue = Get-OptionalProperty -Object $repo -Name 'publishDir'

    $sphinxSourceRel = if (-not [string]::IsNullOrWhiteSpace([string]$sphinxSourceValue)) { [string]$sphinxSourceValue } else { 'docs' }
    $publishRel = if (-not [string]::IsNullOrWhiteSpace([string]$publishValue)) { [string]$publishValue } else { "external/$repoId" }

    $sphinxSource = Resolve-ConfiguredPath -BasePath $repo.resolvedRoot -PathValue $sphinxSourceRel
    $publishDir = Resolve-ConfiguredPath -BasePath $docsDir -PathValue $publishRel

    if (-not (Test-Path -LiteralPath $sphinxSource)) {
      throw "Sphinx source dir not found for '$repoId': $sphinxSource"
    }

    if (Test-Path -LiteralPath $publishDir) {
      Remove-Item -LiteralPath $publishDir -Recurse -Force
    }
    Ensure-Dir -Path $publishDir

    Write-Host "Building Sphinx docs for '$repoId'..."
    $venvSphinx = Join-Path $repo.resolvedRoot '.venv/bin/sphinx-build'
    $venvPython = Join-Path $repo.resolvedRoot '.venv/bin/python'

    if (Get-Command sphinx-build -ErrorAction SilentlyContinue) {
      & sphinx-build -b html $sphinxSource $publishDir
    } elseif (Test-Path -LiteralPath $venvSphinx) {
      & $venvSphinx -b html $sphinxSource $publishDir
    } elseif ((Test-Path -LiteralPath $venvPython) -and (Test-PythonHasSphinx -PythonCommand $venvPython)) {
      & $venvPython -m sphinx -b html $sphinxSource $publishDir
    } elseif ((Get-Command python3 -ErrorAction SilentlyContinue) -and (Test-PythonHasSphinx -PythonCommand 'python3')) {
      & python3 -m sphinx -b html $sphinxSource $publishDir
    } elseif ((Get-Command python -ErrorAction SilentlyContinue) -and (Test-PythonHasSphinx -PythonCommand 'python')) {
      & python -m sphinx -b html $sphinxSource $publishDir
    } elseif (Get-Command py -ErrorAction SilentlyContinue) {
      & py -m sphinx -b html $sphinxSource $publishDir
    } else {
      throw "Could not find a usable Sphinx runner for repo '$repoId'. Install docs dependencies in that repo (for example: python -m pip install -e '.[docs]')."
    }
  }
}

$metadataEntries = @()
$apiNavItems = @()

foreach ($repo in $csharpRepos) {
  $repoId = [string](Get-OptionalProperty -Object $repo -Name 'id')
  $csprojValue = [string](Get-OptionalProperty -Object $repo -Name 'csproj')
  $csprojBase = [string](Get-OptionalProperty -Object $repo -Name 'csprojBase')
  $globalNamespaceId = [string](Get-OptionalProperty -Object $repo -Name 'globalNamespaceId')
  if ([string]::IsNullOrWhiteSpace($csprojValue)) {
    throw "C# repo '$repoId' must define 'csproj'."
  }

  $csprojResolveBase = if ($csprojBase -eq 'hub') { $repoRoot } else { $repo.resolvedRoot }
  $csprojPath = Resolve-ConfiguredPath -BasePath $csprojResolveBase -PathValue $csprojValue
  if (-not (Test-Path -LiteralPath $csprojPath)) {
    throw "C# project file not found for '$repoId': $csprojPath"
  }

  $csprojDir = [System.IO.Path]::GetDirectoryName($csprojPath)
  $csprojFile = [System.IO.Path]::GetFileName($csprojPath)

  $destValue = [string](Get-OptionalProperty -Object $repo -Name 'docfxDest')
  $entryUid = [string](Get-OptionalProperty -Object $repo -Name 'entryUid')
  $dest = if ([string]::IsNullOrWhiteSpace($destValue)) { "api/$repoId" } else { $destValue }
  if (-not $dest.StartsWith('api/')) {
    $dest = "api/$dest"
  }

  $srcEntry = @{
    src = $csprojDir
    files = @($csprojFile)
  }

  $msbuildProperties = Get-OptionalProperty -Object $repo -Name 'msbuildProperties'
  $topLevelProps = $null
  if ($null -ne $msbuildProperties) {
    $props = @{}
    foreach ($prop in $msbuildProperties.PSObject.Properties) {
      $val = [string]$prop.Value
      if (-not [string]::IsNullOrWhiteSpace($val)) {
        $val = Resolve-EnvTokens -Value $val
      }
      $props[$prop.Name] = $val
    }
    if ($props.Count -gt 0) { $topLevelProps = $props }
  }

  $entry = @{
    dest = $dest
    filter = 'filterConfig.yml'
    disableGitFeatures = $true
    src = @($srcEntry)
  }
  if (-not [string]::IsNullOrWhiteSpace($globalNamespaceId)) {
    $entry.globalNamespaceId = $globalNamespaceId
  }
  if ($null -ne $topLevelProps) {
    $entry.properties = $topLevelProps
  }
  $metadataEntries += $entry

  $apiNavItems += [pscustomobject]@{
    title = if (-not [string]::IsNullOrWhiteSpace([string](Get-OptionalProperty -Object $repo -Name 'title'))) { [string](Get-OptionalProperty -Object $repo -Name 'title') } else { "API: $repoId" }
    href = "$dest/toc.yml"
    pageHref = if (-not [string]::IsNullOrWhiteSpace($entryUid)) { "xref:$entryUid" } else { "$dest/toc.html" }
  }
}

$pythonNavItems = @()
foreach ($repo in $pythonRepos) {
  $repoId = [string](Get-OptionalProperty -Object $repo -Name 'id')
  $titleValue = [string](Get-OptionalProperty -Object $repo -Name 'title')
  $publishValue = [string](Get-OptionalProperty -Object $repo -Name 'publishDir')
  $publishRel = if ([string]::IsNullOrWhiteSpace($publishValue)) { "external/$repoId" } else { $publishValue }
  $pythonNavItems += [pscustomobject]@{
    title = if ([string]::IsNullOrWhiteSpace($titleValue)) { "Python: $repoId" } else { $titleValue }
    href = "$publishRel/index.html"
  }
}

$guideFiles = @()
$conceptualDir = Join-Path $docsDir 'conceptual'
if (Test-Path -LiteralPath $conceptualDir) {
  $guideFiles = @(Get-ChildItem -Path $conceptualDir -Filter *.md -File | Sort-Object Name)
}

$tocLines = New-Object System.Collections.Generic.List[string]
$tocLines.Add('- name: Home')
$tocLines.Add('  href: index.md')

if ($guideFiles.Count -gt 0) {
  $tocLines.Add('- name: Guides')
  $tocLines.Add('  items:')
  foreach ($file in $guideFiles) {
    $title = Get-MarkdownTitle -Path $file.FullName
    $tocLines.Add("  - name: $title")
    $tocLines.Add("    href: conceptual/$($file.Name)")
  }
}

if ($apiNavItems.Count -gt 0) {
  $tocLines.Add('- name: C# API')
  $tocLines.Add('  items:')
  foreach ($item in $apiNavItems) {
    $tocLines.Add("  - name: $($item.title)")
    $tocLines.Add("    href: $($item.href)")
  }
}

if ($pythonNavItems.Count -gt 0) {
  $tocLines.Add('- name: Python API')
  $tocLines.Add('  items:')
  foreach ($item in $pythonNavItems) {
    $tocLines.Add("  - name: $($item.title)")
    $tocLines.Add("    href: $($item.href)")
  }
}

Set-Content -LiteralPath $tocPath -Value ($tocLines -join [Environment]::NewLine) -Encoding UTF8

$indexLines = New-Object System.Collections.Generic.List[string]
$indexLines.Add('# WSS Documentation Hub')
$indexLines.Add('')
$indexLines.Add('This site combines conceptual documentation with API references from multiple repositories.')
$indexLines.Add('')

if ($guideFiles.Count -gt 0) {
  $indexLines.Add('## Guides')
  foreach ($file in $guideFiles) {
    $title = Get-MarkdownTitle -Path $file.FullName
    $indexLines.Add("- [$title](conceptual/$($file.Name))")
  }
  $indexLines.Add('')
}

if ($apiNavItems.Count -gt 0) {
  $indexLines.Add('## C# API')
  foreach ($item in $apiNavItems) {
    $indexLines.Add("- [$($item.title)]($($item.pageHref))")
  }
  $indexLines.Add('')
}

if ($pythonNavItems.Count -gt 0) {
  $indexLines.Add('## Python API')
  foreach ($item in $pythonNavItems) {
    $indexLines.Add("- [$($item.title)]($($item.href))")
  }
  $indexLines.Add('')
}

Set-Content -LiteralPath $indexPath -Value ($indexLines -join [Environment]::NewLine) -Encoding UTF8

$docfx = Get-Content -LiteralPath $docfxBaseConfig -Raw | ConvertFrom-Json -Depth 100
$docfx.metadata = @($metadataEntries)

$json = $docfx | ConvertTo-Json -Depth 100
Set-Content -LiteralPath $docfxGeneratedConfig -Value $json -Encoding UTF8

Push-Location $docsDir
try {
  if ($metadataEntries.Count -gt 0) {
    Write-Host "Running DocFX metadata for $($metadataEntries.Count) C# repo(s)..."
    docfx metadata $docfxGeneratedConfig
  } else {
    Write-Host "No enabled C# repositories in manifest; skipping docfx metadata."
  }

  if ($Serve) {
    docfx build $docfxGeneratedConfig --serve
  } else {
    docfx build $docfxGeneratedConfig
  }
} finally {
  Pop-Location
}

Write-Host "Done. Output at: $(Join-Path $docsDir '_site')"
