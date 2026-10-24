Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
Push-Location (Split-Path $PSScriptRoot -Parent)
try {
  & dotnet run --project tools/RepositoryLinks -- generate @args
  if ($LASTEXITCODE -ne 0) { throw "Repository-links generate failed (exit $LASTEXITCODE)." }
} finally { Pop-Location }