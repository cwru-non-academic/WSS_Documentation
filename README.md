# WSS Documentation

This repository contains documentation for the Wearable Stimulation System (WSS): application guidance, software concepts, API references, protocol details, hardware material, and docs-maintainer workflows.

## Main Docs

- [GitHub Pages Documentation Hub](https://cwru-non-academic.github.io/WSS_Documentation/)
  - The main entry point for application users and developers.
- [Local Documentation Hub Source](./apiDocs/)
  - DocFX content for the landing page, section pages, conceptual guides, and API navigation.

## Start Here

- [Using an Application](./apiDocs/conceptual/start-here-using-an-application.md)
- [Developing an Application](./apiDocs/conceptual/start-here-developing-an-application.md)
- [Building a New Integration Library](./apiDocs/conceptual/start-here-building-a-new-integration-library.md)

## Core Concepts

- [Documentation Hub Home](./apiDocs/)
- [Concepts](./apiDocs/concepts.md)
- [Config Files Reference](./apiDocs/conceptual/config-files-reference.md)
- [Core Architecture](./apiDocs/conceptual/core-architecture.md)

## Advanced And Maintenance

- [Hardware Overview source](./hardwareDocs/wsshardware.rmd)
- [WSS Commands Reference source](./wssCommandsDocs/wsscommands.Rmd)
- [Building Software API Docs source](./howtoCompileAPIDocs/BuildSoftwareDocs.Rmd)

## Docs Build Modes

- Full build: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json`
- Main/core only: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --main`
- Skip Python docs: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --skip-python`
- Skip standalone RMarkdown rerender: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --skip-rmd`

Without `--main`, the docs build includes all enabled external repositories from `apiDocs/repos.manifest.json`. In the default manifest, `--main` builds the root WSS core solution, which includes the shared core and any transport projects linked into that solution.

Use `--skip-python` in Bash or `-SkipPython` in PowerShell to skip building Python/Sphinx documentation while still building the DocFX hub and any enabled C# API sections.

Use `--skip-rmd` in Bash or `-SkipRmd` in PowerShell to skip regenerating the standalone RMarkdown-based HTML guides while still building the DocFX hub and any enabled API sections.

For a hub/C# build without Sphinx output, use `--skip-python` in Bash or `-SkipPython` in PowerShell.

The docs build scripts also render the standalone RMarkdown guides before running DocFX. This requires both `Rscript` and `pandoc` to be installed.

## Docs Build Dependencies

### Always needed for DocFX/API builds

- .NET SDK pinned by `global.json` (currently 10.0.300)
- pinned DocFX CLI restored with `dotnet tool restore`
- `jq` for `apiDocs/build-docs.sh`

### Needed when rerendering standalone `.Rmd` guides

- R
- `Rscript`
- `pandoc`
- R packages: `rmarkdown`, `knitr`

### Needed only when Python docs are enabled

- Python 3
- Sphinx / `sphinx-build`
- a valid `WSS_PYTHON_REPO_ROOT`

For OS-specific setup steps, including installing prerequisites on macOS/Linux and `Rscript` / `pandoc`, see `howtoCompileAPIDocs/BuildSoftwareDocs.Rmd` (rendered as `BuildSoftwareDocs.html`).

## Repository Links And Windows/Linux Validation

Portable links in the DocFX hub use one [repository catalog](./apiDocs/repository-links.json).
See [Maintaining Repository Links](./apiDocs/conceptual/repository-links.md) for organization moves, renames, deep links, copyable commands and CI/release stages.

Install the .NET SDK pinned in `global.json` (10.0.300), then run the same commands on Windows or Linux:

```text
dotnet tool restore
dotnet run --project tools/RepositoryLinks -- test --render
dotnet run --project tools/RepositoryLinks -- hub
```

DocFX 2.81.0 is pinned in `.config/dotnet-tools.json`. The hub-only preview is written to `artifacts/hub`; add `--serve` to preview locally.
The Windows/Linux CI workflow runs these same commands and uploads review artifacts without deploying them.
This path requires no jq, Python, R, Pandoc, external checkout, or private machine paths.
Full API/RMarkdown builds still require their additional dependencies and configured external checkout roots; they are a separate release stage.

## Release review

Start application or integration setup from the [repository guide](apiDocs/conceptual/repository-and-kit-links.md). Shared concepts live in this hub; installation and runnable examples live in each repository README. Review the [central 1.0 readiness checklist](apiDocs/conceptual/release-readiness.md) before publishing this documentation or the release.
## Complete-site Publication

Run `dotnet run --project tools/RepositoryLinks -- site` for the complete staged site, preserving API pages, root redirect and standalone guides. See [Publishing the Complete Site](./apiDocs/conceptual/publishing-site.md) for validation artifacts and the separate authorized Pages Actions activation step.
