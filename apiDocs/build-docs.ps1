param(
  [string]$CodeRoot,
  [switch]$Serve
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$here = Split-Path -Parent $PSCommandPath
$toolsDir = Join-Path $here 'tools'
$wrap = Join-Path $toolsDir 'WrapForDocs.ps1'
$apiProj = Join-Path $here 'ApiProject\ApiProject.csproj'

if (-not $CodeRoot) {
  if ($env:WSS_CODE_ROOT) { $CodeRoot = $env:WSS_CODE_ROOT }
  else {
    $guess = Join-Path (Split-Path $here -Parent) 'SimpleWSSStimConsole\Assets\SubModules\WSSInterfacingModule'
    if (Test-Path -LiteralPath $guess) { $CodeRoot = $guess }
  }
}

if (-not $CodeRoot -or -not (Test-Path -LiteralPath $CodeRoot)) {
  Write-Host "ERROR: Could not resolve code root. Pass -CodeRoot, or set WSS_CODE_ROOT, or place this repo next to SimpleWSSStimConsole." -ForegroundColor Red
  exit 1
}

Write-Host "Using code root: $CodeRoot"

# 1) Transform/copy source into ApiProject/Transformed
& $wrap -SourceDir $CodeRoot -OutDir (Join-Path $here 'ApiProject\Transformed')

# 2) Build the API project to produce XML docs (DocFX metadata also works without build if configured, but this ensures references are valid)
dotnet build $apiProj -c Debug

# 3) Generate DocFX metadata and site
if (Get-Command docfx -ErrorAction SilentlyContinue) {
  Push-Location $here
  try {
    docfx metadata
    if ($Serve) { docfx build --serve }
    else { docfx build }
  } finally { Pop-Location }
} else {
  Write-Host "DocFX CLI not found in PATH. Run: 'docfx metadata' then 'docfx build' in $here" -ForegroundColor Yellow
}

Write-Host "Done. Output at: $(Join-Path $here '_site')"

