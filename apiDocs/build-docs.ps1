param(
  [string]$ManifestPath,
  [switch]$Serve,
  [switch]$SkipPython,
  [switch]$Main
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
    $clean = $line.TrimStart([char]0xFEFF)
    if ($clean -match '^\s*#\s+(.+)$') {
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

function Get-CSharpSourceConfig {
  param([Parameter(Mandatory = $true)] [object]$Repo)

  $solutionValue = [string](Get-OptionalProperty -Object $Repo -Name 'solution')
  if (-not [string]::IsNullOrWhiteSpace($solutionValue)) {
    return [pscustomobject]@{
      PathValue = $solutionValue
      BaseValue = [string](Get-OptionalProperty -Object $Repo -Name 'solutionBase' -DefaultValue 'repo')
    }
  }

  $csprojValue = [string](Get-OptionalProperty -Object $Repo -Name 'csproj')
  if (-not [string]::IsNullOrWhiteSpace($csprojValue)) {
    return [pscustomobject]@{
      PathValue = $csprojValue
      BaseValue = [string](Get-OptionalProperty -Object $Repo -Name 'csprojBase' -DefaultValue 'repo')
    }
  }

  throw "C# repo '$([string](Get-OptionalProperty -Object $Repo -Name 'id'))' must define 'solution' or 'csproj'."
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
if ($Main) {
  $enabledRepos = @($enabledRepos | Where-Object { $_.id -eq 'core' })
}

if (Test-Path -LiteralPath (Join-Path $docsDir 'api')) {
  Remove-Item -LiteralPath (Join-Path $docsDir 'api') -Recurse -Force
}
if (Test-Path -LiteralPath (Join-Path $docsDir 'external')) {
  Remove-Item -LiteralPath (Join-Path $docsDir 'external') -Recurse -Force
}

$csharpRepos = @()
$pythonRepos = @()

foreach ($repo in $enabledRepos) {
  $repoId = [string](Get-OptionalProperty -Object $repo -Name 'id')
  $repoKind = [string](Get-OptionalProperty -Object $repo -Name 'kind')
  $repoRootValue = [string](Get-OptionalProperty -Object $repo -Name 'root')

  if ([string]::IsNullOrWhiteSpace($repoId)) { throw "Each repository entry must include 'id'." }
  if ([string]::IsNullOrWhiteSpace($repoKind)) { throw "Repository '$repoId' is missing 'kind'." }
  if ([string]::IsNullOrWhiteSpace($repoRootValue)) { throw "Repository '$repoId' is missing 'root'." }

  if ($SkipPython -and $repoKind -eq 'python') {
    continue
  }

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
  $csharpSource = Get-CSharpSourceConfig -Repo $repo
  $globalNamespaceId = [string](Get-OptionalProperty -Object $repo -Name 'globalNamespaceId')

  $csharpSourceResolveBase = if ($csharpSource.BaseValue -eq 'hub') { $repoRoot } else { $repo.resolvedRoot }
  $csharpSourcePath = Resolve-ConfiguredPath -BasePath $csharpSourceResolveBase -PathValue $csharpSource.PathValue
  if (-not (Test-Path -LiteralPath $csharpSourcePath)) {
    throw "C# project or solution file not found for '$repoId': $csharpSourcePath"
  }
  if (($csharpSourcePath -notlike '*.csproj') -and ($csharpSourcePath -notlike '*.sln')) {
    throw "C# repo '$repoId' must point to a .csproj or .sln file: $csharpSourcePath"
  }

  $csharpSourceDir = [System.IO.Path]::GetDirectoryName($csharpSourcePath)
  $csharpSourceFile = [System.IO.Path]::GetFileName($csharpSourcePath)

  $destValue = [string](Get-OptionalProperty -Object $repo -Name 'docfxDest')
  $dest = if ([string]::IsNullOrWhiteSpace($destValue)) { "api/$repoId" } else { $destValue }
  if (-not $dest.StartsWith('api/')) {
    $dest = "api/$dest"
  }

  $srcEntry = @{
    src = $csharpSourceDir
    files = @($csharpSourceFile)
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
    pageHref = "$dest/toc.html"
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

$tocLines = New-Object System.Collections.Generic.List[string]
$tocLines.Add('- name: Home')
$tocLines.Add('  href: index.md')
$tocLines.Add('- name: Start Here')
$tocLines.Add('  href: start-here.md')
$tocLines.Add('  items:')
$tocLines.Add('  - name: Start Here: Using an Application')
$tocLines.Add('    href: conceptual/start-here-using-an-application.md')
$tocLines.Add('  - name: Start Here: Developing an Application')
$tocLines.Add('    href: conceptual/start-here-developing-an-application.md')
$tocLines.Add('  - name: Start Here: Building a New Integration Library')
$tocLines.Add('    href: conceptual/start-here-building-a-new-integration-library.md')
$tocLines.Add('  - name: Start Here: Minor Core Modifications')
$tocLines.Add('    href: conceptual/start-here-minor-core-modifications.md')
$tocLines.Add('  - name: Start Here: Adding Layers or Core Functionality')
$tocLines.Add('    href: conceptual/start-here-adding-layers-or-core-functionality.md')
$tocLines.Add('  - name: Choosing a Runtime for an Integration Library')
$tocLines.Add('    href: conceptual/integration-library-runtime-selection.md')
$tocLines.Add('  - name: Repository and Kit Links')
$tocLines.Add('    href: conceptual/repository-and-kit-links.md')
$tocLines.Add('- name: Concepts')
$tocLines.Add('  href: concepts.md')
$tocLines.Add('  items:')
$tocLines.Add('  - name: Layering Guide (Modules)')
$tocLines.Add('    href: conceptual/layering-guide.md')
$tocLines.Add('  - name: Core Architecture (Transport, Codec, Core)')
$tocLines.Add('    href: conceptual/core-architecture.md')
$tocLines.Add('  - name: Setup Order and Modification')
$tocLines.Add('    href: conceptual/setup-order-and-modification.md')
$tocLines.Add('  - name: Firmware Compatibility Matrix')
$tocLines.Add('    href: conceptual/firmware-compatibility-matrix.md')
$tocLines.Add('  - name: Config Files Reference')
$tocLines.Add('    href: conceptual/config-files-reference.md')
$tocLines.Add('- name: Advanced')
$tocLines.Add('  href: advanced.md')
$tocLines.Add('  items:')
$tocLines.Add('  - name: WSS Commands Reference')
$tocLines.Add('    href: conceptual/wss-commands-reference.md')
$tocLines.Add('  - name: Simple Serial Communication with WSS')
$tocLines.Add('    href: conceptual/simple-serial-communication.md')

if ($apiNavItems.Count -gt 0) {
  $tocLines.Add('- name: C# API')
  $tocLines.Add('  items:')
  foreach ($item in $apiNavItems) {
    $tocLines.Add("  - name: $($item.title)")
    $tocLines.Add("    href: $($item.href)")
  }
}

$tocLines.Add('- name: Maintainers')
$tocLines.Add('  href: maintainers.md')
$tocLines.Add('  items:')
$tocLines.Add('  - name: Building Software API Docs')
$tocLines.Add('    href: conceptual/building-software-api-docs.md')

Set-Content -LiteralPath $tocPath -Value ($tocLines -join [Environment]::NewLine) -Encoding UTF8

$indexLines = New-Object System.Collections.Generic.List[string]
$indexLines.Add('# WSS Documentation Hub')
$indexLines.Add('')
$indexLines.Add('This site is organized first for people using WSS applications and for developers building applications or integrations on top of WSS.')
$indexLines.Add('')
$indexLines.Add('## Choose Your Path')
$indexLines.Add('')
$indexLines.Add('- [Start Here](start-here.md)')
$indexLines.Add('  - The best entry point if you are deciding whether you are using an existing application, building a new application, creating a new integration library, or making focused core changes.')
$indexLines.Add('- [Using an Application](conceptual/start-here-using-an-application.md)')
$indexLines.Add('  - Start here if you want to run WSS through an existing GUI, CLI, Unity, or Python workflow.')
$indexLines.Add('- [Developing an Application](conceptual/start-here-developing-an-application.md)')
$indexLines.Add('  - Start here if you are building a user-facing tool on top of an existing WSS integration library.')
$indexLines.Add('- [Building a New Integration Library](conceptual/start-here-building-a-new-integration-library.md)')
$indexLines.Add('  - Start here if you need to expose WSS to a new language, platform, or transport environment.')
$indexLines.Add('')
$indexLines.Add('## Repository And Kit Links')
$indexLines.Add('')
$indexLines.Add('- [Repository and Kit Links](conceptual/repository-and-kit-links.md)')
$indexLines.Add('  - One page for grouped application, integration library, and core repository and kit links.')
$indexLines.Add('')
$indexLines.Add('## Core Concepts')
$indexLines.Add('')
$indexLines.Add('- [Concepts](concepts.md)')
$indexLines.Add('  - Overview of the main architecture, layering, setup, firmware compatibility, and config references.')
$indexLines.Add('- [Layering Guide (Modules)](conceptual/layering-guide.md)')
$indexLines.Add('  - Explains how WSS grows from Core to Params to Model and where new reusable functionality should live.')
$indexLines.Add('- [Core Architecture (Transport, Codec, Core)](conceptual/core-architecture.md)')
$indexLines.Add('  - Explains transports, framing, lifecycle, setup sequencing, and streaming behavior.')
$indexLines.Add('- [Config Files Reference](conceptual/config-files-reference.md)')
$indexLines.Add('  - Describes the standard config files used by applications and integration libraries.')
$indexLines.Add('')

if ($apiNavItems.Count -gt 0) {
  $indexLines.Add('## API Reference')
  $indexLines.Add('')
  foreach ($item in $apiNavItems) {
    $indexLines.Add("- [$($item.title)]($($item.pageHref))")
  }
  if ($pythonNavItems.Count -gt 0) {
    $indexLines.Add('- Python Integration (Python)')
    $indexLines.Add('  - Published under `external/<repo>/` when Python docs are enabled in the docs build manifest.')
  }
  $indexLines.Add('')
}

$indexLines.Add('## Hardware Documentation')
$indexLines.Add('')
$indexLines.Add('- [Hardware Overview](../hardwareDocs/wsshardware.html)')
$indexLines.Add('  - Direct access to the hardware documentation.')
$indexLines.Add('')

$indexLines.Add('## Advanced Reference')
$indexLines.Add('')
$indexLines.Add('- [Advanced](advanced.md)')
$indexLines.Add('  - Lower-level protocol and raw communication material for debugging and direct device work.')
$indexLines.Add('- [WSS Commands Reference](conceptual/wss-commands-reference.md)')
$indexLines.Add('  - Byte-level command and protocol reference.')
$indexLines.Add('- [Simple Serial Communication with WSS](conceptual/simple-serial-communication.md)')
$indexLines.Add('  - Raw serial communication examples for macOS, Windows, and MATLAB.')
$indexLines.Add('')
$indexLines.Add('## Docs Maintenance')
$indexLines.Add('')
$indexLines.Add('- [Maintainers](maintainers.md)')
$indexLines.Add('  - Build and maintain the documentation hub itself.')
$indexLines.Add('- [Building Software API Docs](conceptual/building-software-api-docs.md)')
$indexLines.Add('  - Build workflow for the multi-repository DocFX site and generated API docs.')
$indexLines.Add('')

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

Write-Host "Done. Output at: $docsDir"
