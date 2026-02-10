# AGENTS Guide for WSS_Documentation

Use this as repo-specific guidance for agentic coding/documentation tools.

## Scope

- This repo primarily builds documentation (Markdown/RMarkdown + generated HTML) and a DocFX-hosted docs hub.
- The docs hub can aggregate multiple linked repositories:
  - C# API docs generated with DocFX metadata (one API section per repo)
  - Python API docs generated with Sphinx and hosted as static content
- Prefer minimal diffs; avoid broad “format-only” changes.

## Key Paths

- `README.md`
- `apiDocs/docfx.json`
- `apiDocs/repos.manifest.json`
- `apiDocs/build-docs.sh`
- `apiDocs/build-docs.ps1`
- `apiDocs/index.md`
- `apiDocs/toc.yml`
- RMarkdown sources:
  - `wssCommandsDocs/wsscommands.Rmd`
  - `simpleSerialPortDocs/SimpleSerial.Rmd`
  - `hardwareDocs/wsshardware.rmd`
  - `howtoCompileAPIDocs/BuildSoftwareDocs.Rmd`

## Generated vs Source (Edit the Right Thing)

- Prefer editing:
  - `*.Rmd` (source) instead of the corresponding `*.html` output.
  - `apiDocs/conceptual/*.md`, `apiDocs/docfx.json`, and `apiDocs/repos.manifest.json` (inputs) instead of built site output.
- Avoid hand-editing generated artifacts unless explicitly requested:
  - `apiDocs/_site/**` (DocFX output)
  - `apiDocs/api/**` (DocFX metadata yml)
  - `apiDocs/external/**` (Sphinx output copied into site)
  - `apiDocs/styles/**` (vendor/minified assets)

## Prerequisites

- .NET SDK 7+ for C# API projects.
- DocFX CLI on PATH (`docfx`) to generate the hub site.
- Python + Sphinx (`sphinx-build`) for Python API projects.
- PowerShell for `.ps1` scripts; Bash for `.sh` scripts.
- R + `rmarkdown`/`knitr` only if regenerating `.Rmd` outputs.

## Build / Verify Commands

### Build the docs hub (recommended)

- macOS/Linux/Git Bash:
  - `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json`
- Windows PowerShell:
  - `./apiDocs/build-docs.ps1 -ManifestPath ./apiDocs/repos.manifest.json`
  - `./apiDocs/build-docs.ps1 -ManifestPath ./apiDocs/repos.manifest.json -Serve`

Manifest notes:

- Add one entry per linked repo with `kind: csharp` or `kind: python`.
- Use `docfxDest` (C#) to keep API sections separated (example: `api/core`, `api/unity`).
- Use `publishDir` (Python) to publish Sphinx HTML under `external/<name>`.

### Step-by-step (useful for debugging)

- Build Python docs for a repo manually:
  - `sphinx-build -b html <repo-root>/docs ./apiDocs/external/<repo-id>`
- Run DocFX metadata/build manually (using generated config):
  - `docfx metadata ./apiDocs/docfx.generated.json`
  - `docfx build ./apiDocs/docfx.generated.json`

### Render a single RMarkdown doc

- `Rscript -e "rmarkdown::render('wssCommandsDocs/wsscommands.Rmd')"`
- `Rscript -e "rmarkdown::render('simpleSerialPortDocs/SimpleSerial.Rmd')"`

## Lint / Formatting

- No repo-wide linter/formatter is configured (no `.editorconfig`, no CI workflows detected).
- Do not introduce large reformatting churn; keep edits focused.
- If you add a lint/format tool, document the exact commands here and in `README.md`.

## Tests

- There is no first-class automated test suite in this repository.
- Treat “tests” as build/verification:
  - `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json`
  - `docfx metadata ./apiDocs/docfx.generated.json && docfx build ./apiDocs/docfx.generated.json`
  - Re-render changed `.Rmd` and open the produced HTML.

### Running a single test (template for future .NET test projects)

- List tests:
  - `dotnet test <test.csproj> --list-tests`
- Run one test by full name (contains match):
  - `dotnet test <test.csproj> --filter "FullyQualifiedName~Namespace.Class.Method"`
- Run by exact name or substring:
  - `dotnet test <test.csproj> --filter "Name=Method"`
  - `dotnet test <test.csproj> --filter "Name~Method"`

## Code Style Guidelines

### General

- Keep ASCII unless a file already uses Unicode.
- Keep existing conventions; avoid drive-by cleanups.

### C#

- Prefer documenting directly from external repo `*.csproj` entries via `repos.manifest.json`.
- If Unity projects fail metadata resolution, use doc-only shim projects (linked sources + local refs) instead of source duplication.
- Namespaces/usings:
  - Preserve the existing pattern (block namespace; `using` directives often inside the namespace).
  - Do not churn import ordering unless necessary; remove unused imports only when touching the file.
- Formatting:
  - 4-space indentation; braces on their own line; keep `#region` structure if present.
- Types/nullability:
  - Nullability is enabled; prefer explicit checks over null-forgiving (`!`).
  - Throw `ArgumentNullException`/`ArgumentException`/`ArgumentOutOfRangeException` for invalid inputs.
- Naming:
  - Public APIs: PascalCase.
  - Private fields: `_camelCase`.
  - Some legacy members use `getX` style; do not rename public APIs without a strong reason.
- Error handling:
  - Fail fast for invalid input.
  - Log actionable errors where the code already logs (e.g., `Log.Error(...)`).
  - Only swallow exceptions in cleanup/shutdown paths.
- Async/threading:
  - Prefer `Task`-based async; avoid blocking waits.
  - Respect existing locking/semaphore patterns in configuration/core classes.

### PowerShell / Bash

- Keep existing strict modes:
  - PowerShell: `Set-StrictMode -Version Latest`, `$ErrorActionPreference = 'Stop'`.
  - Bash: `set -euo pipefail`.
- Validate inputs early (paths, env vars, tool presence) and print remediation steps.
- Quote paths (especially on Windows) and avoid assumptions about current working directory.

### Markdown / RMarkdown

- Make examples copy-pasteable; use fenced blocks with language tags (`bash`, `powershell`, `csharp`, `matlab`).
- Prefer editing `.Rmd` sources and regenerating `.html`.
- For protocol-critical content, include explicit hex/byte examples.

## Cursor / Copilot Rules

Checked for (none found at time of writing):

- `.cursorrules`
- `.cursor/rules/`
- `.github/copilot-instructions.md`

If any are added later, follow them and update this file.
