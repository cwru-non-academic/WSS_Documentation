# Publishing the Complete Documentation Site

[Home](../index.md) | [Back to Maintainers](../maintainers.md)

## Build the same site on Windows and Linux

From the repository root, with the SDK pinned in `global.json` installed:

```text
dotnet tool restore
dotnet run --project tools/RepositoryLinks -- test --render
dotnet run --project tools/RepositoryLinks -- site
```

The complete site is staged at `artifacts/site`, with a preservation report at
`artifacts/site-validation.json`. Use `--output <new-directory>` to select another
empty directory under this checkout's `artifacts/` or the OS temporary directory.
The command refuses to overwrite a populated directory and never deletes the live site.

This command copies existing tracked public HTML/assets, then runs pinned DocFX
to render current conceptual Markdown, the authoritative `apiDocs/toc.yml`, and
checked-in API metadata. Generated pages keep their `apiDocs/` URLs. The root
redirect, standalone guides and their assets remain in their original locations.
It does not regenerate external API metadata or rerender RMarkdown. Those remain
separate release tasks with additional pinned upstream checkouts and dependencies.

Validation requires all inventoried public paths to survive. Root files and assets
remain byte-identical. Standalone guides receive the centrally defined hub links;
the build checks that their other content and section links remain unchanged.
Fresh pages must be DocFX output, with resolved repository links and clone snippets,
working local paths and fragments, and all three C# API sections in navigation.
Conceptual pages, generated API pages and standalone guides have explicit header
and footer routes back to the hub, without requiring JavaScript. The report lists
preserved paths, navigation-adapted guides, freshly generated pages, the pages whose
local paths/fragments were checked, and API pages whose hub backlinks were checked.
API body-link freshness remains part of the separate API-reference review.

## Review builds and deployment permissions

`Complete docs site` runs fixture tests and complete-site staging on Windows Server
2022 and Ubuntu 24.04. Pull requests only upload `complete-site-<OS>` review artifacts
and their reports. They cannot run the deployment job or upload the main-only Pages
artifact. The existing hub-only workflow remains a smaller conceptual preview check.

Build jobs have only `contents: read`. The separate deployment job has only
`pages: write` and `id-token: write`, uses the `github-pages` environment, and only
runs for `main` pushes or an explicit `main` manual run with `publish` enabled.
It checks the current Pages build type and skips deployment while Pages is still
in legacy branch mode. No custom token or repository secret is needed.

## Initial activation and later releases

Merge the workflow only after both complete-site checks pass and its artifact is
reviewed. Keep Pages in its existing mode until the workflow exists on `main` and
the merged commit's build is validated. Then an authorized repository administrator
can set Settings > Pages > Build and deployment > Source to **GitHub Actions**.
This switch is separate from code review and requires explicit authorization.

Once Actions mode is active, manually run `Complete docs site` on `main` with
`publish` enabled for the first deployment. Future matching `main` pushes build and
deploy after both operating-system checks pass. A manual run with `publish` disabled
only validates and packages a review build. Never dispatch a deployment from a PR
branch or relax the main-only job gate.

The staged artifact deliberately omits `CNAME`; it does not assume a custom domain.
Use the current Pages URL reported by GitHub settings/API, and treat any domain or
HTTPS changes as separate administration decisions. The legacy setup previously
ran Jekyll over source Markdown, leaving DocFX code directives unresolved even when
the Pages deployment was green. Actions publishes the validated artifact directly.

After deployment, verify the root redirect, this guide, the repository-link guide's
expanded clone command, CI guides when merged, representative API pages and standalone
hardware/protocol pages. Compare against the deployed commit and preservation report;
a successful upload alone is not proof of correct browser rendering.

## Navigation and API maintenance

Keep `apiDocs/toc.yml` as the authoritative complete-site navigation source. Quote
YAML names containing `: `; both full-build wrappers emit quoted names too. The
complete-site command consumes this TOC without generating a reduced replacement,
so later CI-guide navigation additions are included automatically after merging.

Regenerated API HTML uses the currently checked-in API metadata; retained legacy
public HTML paths are also copied into the site. Updating API metadata from upstream
repositories is a separate full-build operation. Review its resulting API changes
before publishing, rather than silently dropping old API sections or standalone content.

Navigation:

- [Back to Home](../index.md)
- [Back to Maintainers](../maintainers.md)
