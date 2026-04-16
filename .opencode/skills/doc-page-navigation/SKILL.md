---
name: doc-page-navigation
description: Scan documentation pages discoverable from the built docs site for missing top and bottom navigation links, add them using existing repo patterns, and report which pages already had them.
---

## What I do

I standardize page-level navigation for this repository's built documentation site.

I inspect documentation source files that are discoverable through the docs website once built, determine which pages already have top and bottom navigation, and add missing navigation using the existing patterns in this repo.

I exclude repository meta files by default, including root `README.md`, `AGENTS.md`, shim `README.md` files, generated site output, and other non-site-supporting repo docs unless the user explicitly asks to include them.

## When to use me

Use this skill when you need to:

- add missing top and bottom navigation to docs pages
- audit docs pages for navigation consistency
- identify which built-site pages already have navigation and which do not
- update newly added docs pages so they match the repo's existing navigation style

Do not use this skill for:

- repo meta documentation not intended to be browsed through the built docs website
- generated output under `apiDocs/_site/**`, `apiDocs/api/**`, or `apiDocs/external/**`
- unrelated markdown cleanup or broad formatting-only changes

## How to use me

When loaded, follow this workflow.

1. Determine in-scope pages.

Treat as in scope only source files that are discoverable through the built docs website. In this repository, that normally means:

- `apiDocs/**/*.md`
- `howtoCompileAPIDocs/*.Rmd`
- `simpleSerialPortDocs/*.Rmd`
- `wssCommandsDocs/*.Rmd`
- `hardwareDocs/*.rmd`

Treat as out of scope by default:

- `README.md`
- `AGENTS.md`
- `apiDocs/shims/**`
- `.opencode/**`
- generated output such as `apiDocs/_site/**`, `apiDocs/api/**`, `apiDocs/external/**`, and rendered HTML files

2. Inspect existing examples before editing.

Use nearby pages that already follow the established pattern as the source of truth. In this repo, the standard pattern is:

- top navigation near the title, usually a compact breadcrumb line such as:
  - `[Home](../index.md) | [Back to Concepts](../concepts.md)`
  - `[Home](../README.md) | [Docs Hub](../apiDocs/index.md) | [Back to Advanced](../apiDocs/advanced.md)`
- bottom navigation block in this format:

```md
Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
```

3. Audit each in-scope page.

Classify each page as one of:

- already has both top and bottom navigation
- missing top navigation
- missing bottom navigation
- missing both

Report the findings clearly, especially when the user asked for an audit.

4. Infer the right links from section context.

Do not force one universal template across all pages. Use the page's location and nearby examples to choose the appropriate links.

Common repo patterns:

- `apiDocs/index.md`
  - top links to main section landing pages
  - bottom `Navigation:` block links to the same sections
- `apiDocs/start-here.md`, `apiDocs/concepts.md`, `apiDocs/advanced.md`, `apiDocs/maintainers.md`
  - top navigation is usually `[Home](index.md)`
  - bottom navigation usually includes `- [Back to Home](index.md)`
- `apiDocs/conceptual/*.md`
  - top navigation usually links back to `../index.md` and the relevant section landing page
  - bottom navigation uses a `Navigation:` block and may include previous/next links where the page series already uses them
- standalone docs like `howtoCompileAPIDocs/*.Rmd`, `simpleSerialPortDocs/*.Rmd`, `wssCommandsDocs/*.Rmd`, and `hardwareDocs/*.rmd`
  - top navigation should reflect how the page is reached from the docs hub, often including `Home`, `Docs Hub`, and a relevant section link when applicable
  - bottom navigation should mirror that context with a `Navigation:` block

5. Make minimal source-only edits.

Edit the source files, not generated output. Keep diffs small and avoid unrelated cleanup.

Prefer:

- adding one breadcrumb line immediately after the title
- adding one `Navigation:` block at the end when missing
- preserving any existing previous/next links already established on nearby pages

Avoid:

- rewording unrelated content
- changing link labels across the repo without need
- adding navigation to excluded repo/meta files unless the user explicitly asks for it

6. Verify after editing.

Re-scan the same in-scope files and confirm which pages:

- already had compliant navigation
- were updated
- remain intentionally excluded

7. Report results concisely.

Summarize:

- pages changed
- pages already compliant
- pages excluded by scope

If any page has ambiguous placement or section membership, stop and ask one short clarifying question instead of guessing.
