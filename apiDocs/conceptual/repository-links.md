# Maintaining Repository Links

[Home](../index.md) | [Back to Maintainers](../maintainers.md)

## One source for repository destinations

`apiDocs/repository-links.json` is the master catalog for portable repository URLs.
Change `defaultOwner` once when repositories move together. A repository may set
`owner` to override that default during a staggered move. Change its `name` when
renamed; retain its catalog key so existing page references keep working. Remove
an owner override when that repository should follow the default again.

The catalog preserves current destinations. Local checkout paths and API settings
belong in `repos.manifest.json`, separately from public URLs. For example:

```json
{
  "name": "WSSCore",
  "owner": "cwru-non-academic",
  "links": {
    "readme": "/blob/main/README.md",
    "releases": "/releases",
    "workflow": "/blob/main/.github/workflows/build.yml#L10"
  }
}
```

The deep links above illustrate syntax; verify the branch and file before adding
them. Named suffixes support paths, queries and fragments and retain URL escaping.
Roots are GitHub HTTPS URLs; arbitrary hosts and full URL overrides are outside
this catalog. Owner overrides take precedence over the default owner.

## Adding links and copyable commands

The authored `apiDocs/toc.yml` and `apiDocs/index.md` own the site hierarchy and
entry points. Full API regeneration preserves those files. A nested API TOC uses
`tocHref` for its structure and `topicHref` for a styled landing page; primary
links must not open a bare `toc.html` fragment.

The active `templates/wss` overlay owns the visible hub links in the header and
footer. Their label and default destination are defined once in
`apiDocs/docfx.json` (`_wssHubTitle` and `_wssHubPath`). Complete-site staging adjusts
the destination for its `apiDocs/` output prefix. The same label supplies the
staged standalone RMarkdown pages; only generic navigation is replaced there,
while content, images and scripts are preserved. A missing same-page fragment is
repaired only when its label uniquely matches an actual heading; ambiguous links
fail validation. Edit RMarkdown
sources for guide content, rather than editing generated navigation in HTML.

GitHub READMEs retain normal Markdown links. DocFX xrefs and code-inclusion
directives belong only in the shared documentation inputs.

Add a stable repository key to the catalog, then use native DocFX xrefs:

```markdown
[Core repository](xref:repo.core)
[Core README](xref:repo.core.readme)
```

The README example requires a `readme` entry under that repository's `links`.
Give each deep destination a named entry; do not append `/blob/...` to an xref UID.
The shared generator resolves the complete URL before DocFX renders it.

DocFX does not expand xrefs inside code fences. Include a generated clone snippet
on a line by itself instead, using a path relative to the current Markdown page:

```markdown
[!code-bash[Clone core](../generated/repository-links/core-clone.sh)]
```

The directive renders this copyable command:

[!code-bash[Clone core](../generated/repository-links/core-clone.sh)]

Other code examples should use a real shell variable or an ordinary code snippet.
The catalog provides link references, not general text/code templating.

## Same implementation on Windows and Linux

`tools/RepositoryLinks` is a dependency-free .NET console utility. It generates
the DocFX xref map and clone snippets, validates fixture moves, and builds a
hub-only preview. Thin PowerShell and Bash entrypoints call this same code; there
is no second URL-resolution implementation. .NET is already required for DocFX.
`global.json` pins SDK 10.0.300, and `.config/dotnet-tools.json` pins DocFX 2.81.0.
Upgrading either pin is an explicit maintenance change validated by both CI jobs.

From the repository root on either platform:

```text
dotnet tool restore
dotnet run --project tools/RepositoryLinks -- test
dotnet run --project tools/RepositoryLinks -- test --render
dotnet run --project tools/RepositoryLinks -- hub
```

Install the exact pinned SDK first. The first restore needs network access to
NuGet; subsequent builds use the restored local tool. No global DocFX installation
or PATH modification is required. The generator itself has no package dependencies.
`test` without `--render` runs catalog checks without restoring DocFX.

The hub command writes to `artifacts/hub`; pass `--output <directory>` to change it
or `--serve` to start DocFX's local server after validation. Equivalent wrappers are
`./apiDocs/build-hub.ps1` on PowerShell and `bash ./apiDocs/build-hub.sh` on Bash.
Use `./apiDocs/test-repository-links.ps1 --render` or
`bash ./apiDocs/test-repository-links.sh --render` for the same tests.
Wrappers resolve the repository root, so the caller's working directory is immaterial.

Both full build scripts regenerate link inputs too, including skip-RMarkdown and
serve modes. Those scripts use the pinned local DocFX tool. If calling DocFX directly:

```text
dotnet run --project tools/RepositoryLinks -- generate
dotnet tool run docfx -- build apiDocs/docfx.json
```

Generated inputs are ignored under `apiDocs/generated/repository-links/`; never
edit them. Rebuild after changing the catalog; serving alone does not regenerate
it. Tests use temporary fixtures with an alternative owner, retained owner, renamed
repository and deep suffix, then inspect links and clone commands in rendered HTML.
They never modify the live catalog. `--docfx <executable>` can reuse an existing
DocFX executable in restricted environments; normal builds use the pinned tool.

## CI validation and staged releases

`.github/workflows/docs-hub-validation.yml` runs on pull requests, main pushes and
manual dispatch, using Windows Server 2022 and Ubuntu 24.04. It restores the pinned
tool, runs the same rendered fixture tests, builds the hub-only preview and uploads
one review artifact per OS. Action versions are fixed too. The workflow has read-only
repository permission and does not publish a site or require deployment secrets.
Authoring the workflow does not mean it has run: inspect both job results after pushing.

Hub-only validation excludes API metadata and imported Sphinx content, does not
rerender standalone RMarkdown, and avoids the remote Microsoft xref service.
Existing placeholders and links to absent API/standalone outputs can still warn;
they do not make the preview a complete release. Fixture builds treat warnings as
errors, and hub checks require the repository index links to resolve correctly.

A complete release should be added in separate stages:

1. Check out external repositories at explicit reviewed commits/tags into predictable
   workspace folders; set the manifest root environment variables to those folders.
   Project/solution paths in the manifest are relative to those roots, not a maintainer's
   home directory. Unity's shim remains relative to this documentation repository.
2. Pin and install R/Pandoc/R packages and Python/Sphinx dependencies, then run the
   existing full build wrapper. Bash also requires jq for manifest processing;
   Windows uses PowerShell 7. These extra tools are unnecessary for hub-only checks.
3. Validate the complete generated site and archive it as a release artifact. Resolve
   the outstanding placeholders/missing outputs before calling it a complete release.
4. Add a separately authorized publish job with an explicit release trigger and
   destination. Publication and repository transfers are not part of this workflow.

For the complete staged site and the gated deployment workflow, see [Publishing the Complete Site](publishing-site.md). The hub preview remains deliberately smaller and is never deployed by itself.

## Which links stay literal

These are [native DocFX cross-references](https://dotnet.github.io/docfx/docs/links-and-cross-references.html).
Published pages contain ordinary URLs. Raw GitHub Markdown does not resolve xrefs
or code-inclusion directives; use the built preview. The root README retains normal
relative links and its current GitHub Pages hosting URL so it stays usable on GitHub.
Hosting URLs are deployment settings, separate from repository URLs.

Historical commit-pinned evidence links should remain literal, including workflow
snapshots used by CI documentation. Unrelated external documentation and licenses,
kit placeholders, generated API source links and SourceLink data are not migrated.
The old checked-in HTML contains copies of authored links and DocFX edit-source
links derived from Git metadata: regenerate it through the publishing workflow,
and verify contribution/Git configuration when an actual move is authorized.
The catalog does not rewrite generated HTML, RMarkdown, imported Sphinx output,
navigation YAML, or Git remotes. A valid rendered URL cannot guarantee the remote
repository, branch, file, or release exists.

Navigation:

- [Back to Home](../index.md)
- [Back to Maintainers](../maintainers.md)
