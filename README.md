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
The same catalog owns named external HTTPS destinations and the values displayed
in the [Verification Pin Reference source](./apiDocs/conceptual/verification-pin-reference.md)
page. These are documentation values; executable security pins remain in their
owning repositories. Site pages reference destinations using `[label](xref:repo.core)`
or `[label](xref:link.docfx)`. The build generates the pin table from this catalog;
edit the JSON and rebuild rather than editing generated files. Ordinary GitHub
READMEs keep normal Markdown links because GitHub does not resolve DocFX xrefs.
Catalog `externalLinks` entries have `name` and `href` fields. `verificationPins`
entries have `name`, `kind` (`commit` or `sha256`), full lowercase `value`, `purpose`
and a commit-pinned catalog xref `source`. `verificationSnapshotDate` records the
inspection date. This is a manually reconciled historical snapshot: update the
value, immutable source URL and date together after the receiver change is reviewed.
The build does not inspect external checkouts or query moving remote branches.
The generated table is included by the published reference page. Rendered tests change URLs and pins in a temporary catalog and
reject stale output, invalid URLs and floating revisions. This mechanism applies
to authored DocFX references; historical immutable citations, RMarkdown content,
imported API pages, hosting settings and Git remotes retain their own ownership.
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

## Complete-site Publication

Run `dotnet run --project tools/RepositoryLinks -- site` for the complete staged site, preserving API pages, root redirect and standalone guides. See [Publishing the Complete Site](./apiDocs/conceptual/publishing-site.md) for validation artifacts and the separate authorized Pages Actions activation step.

## Repository Maintenance

README is the home for checkout, build and contributor instructions. The files under
`apiDocs/` and the standalone RMarkdown directories are inputs to the published
website. Keep these sources and their public URLs intact when cleaning the checkout.
Do not commit planning notes, local build logs or MSBuild caches.

Edit Markdown/RMarkdown sources, templates and configuration rather than generated
HTML, API YAML or vendor assets. Full API/RMarkdown regeneration is a separate
operation from the self-contained `hub` and `site` commands above. Keep generated
API metadata, published HTML and `apiDocs/ref/*.dll`: site staging and documentation
shim projects consume them. The legacy transformed-source project and `.RData` are
retained until their historical value and replacement coverage have been established.

Use the SDK in `global.json` and restore the pinned DocFX tool. Both existing CI
workflows run `dotnet run --project tools/RepositoryLinks -- test --render` on Windows
and Linux, then validate their respective `hub` or `site` output. Review artifacts and
site-preservation reports before publication. Deployment settings and activation are
separate authorized operations; a documentation cleanup must not activate them.

Keep changes focused and preserve existing source conventions. PowerShell scripts use
strict mode and stop on errors; Bash scripts use `set -euo pipefail`. Quote paths,
validate inputs and document dependencies. Keep protocol examples explicit about
bytes and units. In C#, preserve public APIs and use the existing null validation,
exception, locking and cancellation conventions.

For website pages, preserve top breadcrumbs and bottom navigation using neighboring
pages and `apiDocs/toc.yml` as the guide. Repository metadata and shim instructions
belong here rather than in the website navigation. Shared agent instructions and the
navigation skill remain tracked as tooling entry points; they are not planning files.

### Verification references

See the [Verification Pin Reference source](./apiDocs/conceptual/verification-pin-reference.md)
and its [dated snapshot catalog](./apiDocs/repository-links.json) for recorded values,
purposes and immutable evidence links. These are inspection records; the owning
repositories retain the authoritative operational pins. The public page becomes
available after reviewed dev-to-main promotion and site deployment.

## Unity Submodule DocFX Shim

Use this shim project when the Unity repository you want to document is a full Unity app/UI project,
but the code you actually want API docs for is a submodule folder (or a subset of the repo).

DocFX requires a C# project (`.csproj`) to generate metadata. Unity projects often pull in
UnityEditor/UI/build-time dependencies that are not needed (or not available) for API docs.
This shim:

- Compiles only the files you select (defaults to all `*.cs` under `DocsSourceRoot`)
- References Unity assemblies from `apiDocs/ref/*.dll`
- Produces XML docs so DocFX can generate API pages

### How To Use

1) Decide what folder contains the integration code you want documented.

Example (submodule checkout):

- `.../MyUnityApp/Assets/SubModules/WSSInterfacingModule`

2) Add a repo entry in `apiDocs/repos.manifest.json`.

Example:

```jsonc
{
  "id": "unity-integration",
  "title": "API: Unity Integration (C#)",
  "kind": "csharp",
  "enabled": true,
  "root": "${WSS_UNITY_REPO_ROOT}",

  // Use the shim .csproj stored in this docs hub
  "csprojBase": "hub",
  "csproj": "apiDocs/shims/UnitySubmodule/UnitySubmodule.Docs.csproj",

  // Separate API section
  "docfxDest": "api/unity",

  // Tell the shim which source tree to include
  "msbuildProperties": {
    "DocsSourceRoot": "${WSS_UNITY_INTEGRATION_SRC}",

    // Optional: override includes/excludes
    // "DocsInclude": "${WSS_UNITY_INTEGRATION_SRC}/UnityImplementation/**/*.cs",
    // "DocsExclude": "${WSS_UNITY_INTEGRATION_SRC}/**/Tests/**",

    // Optional: Unity compilation symbols (semicolon-separated)
    "DefineConstants": "UNITY_2021_3_OR_NEWER;UNITY_EDITOR"
  }
}
```

3) Set environment variables used in the manifest.

Example (bash):

```bash
export WSS_UNITY_REPO_ROOT="/abs/path/to/MyUnityApp"
export WSS_UNITY_INTEGRATION_SRC="$WSS_UNITY_REPO_ROOT/Assets/SubModules/WSSInterfacingModule"
```

Note: the shim also supports providing the source root *without* MSBuild properties by setting
`WSS_UNITY_INTEGRATION_SRC` (or `DOCS_SOURCE_ROOT`) in the environment.

4) Build:

```bash
./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json
```

### Notes

- `DocsSourceRoot` is required. If not set, the shim fails with a clear error.
- `DocsSourceRoot` must not be `/` (or a drive root like `C:\\`), otherwise MSBuild globbing becomes extremely slow and fails.
- By default, `Assets/**/Editor/**` is excluded to avoid `UnityEditor`-only code.
- If you need Editor APIs documented, remove that exclude and add `UnityEditor` references.
- If Unity types are missing, ensure `apiDocs/ref/*.dll` contains the correct Unity assemblies.
- If the submodule includes dependency assemblies under `Plugins/`, the shim automatically references `Plugins/**/*.dll` (excluding common Unity/Newtonsoft/system DLLs that are already referenced).
