param(
  [string]$SourceDir,
  [string]$OutDir,
  [string]$Namespace = 'WSSInterfacing'
)

$ErrorActionPreference = 'Stop'

if (-not $PSCommandPath) { $PSCommandPath = $MyInvocation.MyCommand.Path }
$here = Split-Path -Parent $PSCommandPath
if (-not $SourceDir) {
  if ($env:WSS_CODE_ROOT -and (Test-Path -LiteralPath $env:WSS_CODE_ROOT)) {
    $SourceDir = $env:WSS_CODE_ROOT
  } else {
    # Fallbacks: try sibling repo layout or original relative path
    $siblingGuess = Join-Path (Split-Path (Split-Path $here -Parent) -Parent) 'SimpleWSSStimConsole\Assets\SubModules\WSSInterfacingModule'
    if (Test-Path -LiteralPath $siblingGuess) {
      $SourceDir = $siblingGuess
    } else {
      $SourceDir = Join-Path $here '..\..\Assets\SubModules\WSSInterfacingModule'
    }
  }
}
if (-not $OutDir) { $OutDir = Join-Path $here '..\ApiProject\Transformed' }

function Ensure-Dir { param([string]$Path) if (-not (Test-Path -LiteralPath $Path)) { New-Item -ItemType Directory -Path $Path | Out-Null } }

Ensure-Dir -Path $OutDir

Add-Type -AssemblyName 'System.Text.RegularExpressions' | Out-Null
$regexOptions = [System.Text.RegularExpressions.RegexOptions]::Multiline

$files = Get-ChildItem -Path $SourceDir -Recurse -File -Filter *.cs
foreach ($f in $files) {
  $srcPath = (Resolve-Path -LiteralPath $f.FullName).Path
  $rel = $srcPath.Substring((Resolve-Path -LiteralPath $SourceDir).Path.Length).TrimStart([char]92,'/')
  $dest = Join-Path $OutDir $rel
  Ensure-Dir -Path (Split-Path $dest)
  $content = Get-Content -LiteralPath $srcPath -Raw
  $hasNs = [System.Text.RegularExpressions.Regex]::IsMatch($content, '^\s*namespace\s+\w', $regexOptions)
  if ($hasNs) {
    Set-Content -LiteralPath $dest -Value $content -Encoding UTF8
  } else {
    $wrapped = "namespace $Namespace {`r`n" + $content + "`r`n}"
    Set-Content -LiteralPath $dest -Value $wrapped -Encoding UTF8
  }
}

Write-Host "Transformed $($files.Count) files into $OutDir under namespace $Namespace"
