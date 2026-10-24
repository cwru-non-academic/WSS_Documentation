Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
  & dotnet run --project tools/RepositoryLinks -- test @args
  if ($LASTEXITCODE -ne 0) { throw "Repository-links test failed (exit $LASTEXITCODE)." }
} finally { Pop-Location }