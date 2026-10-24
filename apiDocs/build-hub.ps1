Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
  & dotnet run --project tools/RepositoryLinks -- hub @args
  if ($LASTEXITCODE -ne 0) { throw "Repository-links hub failed (exit $LASTEXITCODE)." }
} finally { Pop-Location }