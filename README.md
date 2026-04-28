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

- [Hardware Overview](./hardwareDocs/wsshardware.html)
- [WSS Commands Reference](./wssCommandsDocs/wsscommands.html)
- [Building Software API Docs (DocFX)](./howtoCompileAPIDocs/BuildSoftwareDocs.html)

## Docs Build Modes

- Full build: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json`
- Main/core only: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --main`
- Skip Python docs: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --skip-python`
- Skip standalone RMarkdown rerender: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --skip-rmd`

Without `--main`, the docs build includes all enabled external repositories from `apiDocs/repos.manifest.json`. In the default manifest, `--main` builds the root WSS core solution, which includes the shared core and any transport projects linked into that solution.

Use `--skip-python` in Bash or `-SkipPython` in PowerShell to skip building Python/Sphinx documentation while still building the DocFX hub and any enabled C# API sections.

Use `--skip-rmd` in Bash or `-SkipRmd` in PowerShell to skip regenerating the standalone RMarkdown-based HTML guides while still building the DocFX hub and any enabled API sections.

Python integration docs for the library are not currently supported in this local build workflow. For now, use `--skip-python` in Bash or `-SkipPython` in PowerShell.

The docs build scripts also render the standalone RMarkdown guides before running DocFX. This requires both `Rscript` and `pandoc` to be installed.

## Docs Build Dependencies

### Always needed for DocFX/API builds

- .NET SDK 7 or later
- DocFX CLI on `PATH`
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
