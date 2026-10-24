# Independent Integration CI

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

Use this guide when you own an integration repository and consume public WSSCore, but cannot dispatch workflows or write branches in the WSS repositories. You can detect upstream changes, verify a candidate, test your integration, and propose an update entirely within your own repository.

**Status:** the downstream monitor described here is an adapter design to implement in your repository. WSS's existing first-party App cascade is not a public subscription service.

## Decide What Counts as an Update

| Policy | Discovery | Candidate gate |
| --- | --- | --- |
| Published stable releases | Poll published releases, excluding drafts and prereleases according to your policy | Resolve the release tag to a full commit and verify the intended assets and producer evidence |
| Development commits | Poll the configured Core branch and its producer runs | Require the exact completed successful authoritative `artifacts.yml` producer; branch movement alone is insufficient |

Start with releases if your users need stable updates. Development monitoring provides earlier feedback but may encounter failed builds or expired artifacts. Explicitly decide whether to test every eligible revision or coalesce to the newest one. Record skipped/superseded candidates rather than silently claiming they were tested.

A `push` or `release` trigger in your repository observes your repository's events. GitHub Watch notifications do not connect upstream pushes to your Actions workflow. A downstream `workflow_run` is also not a cross-repository subscription. Use scheduled polling and a manual retry entry point, or an explicitly authorized external event service. `repository_dispatch` requires a sender authorized to dispatch to the receiving repository; it does not confer permission on the upstream project. See [GitHub event semantics](https://docs.github.com/en/actions/reference/workflows-and-actions/events-that-trigger-workflows).

## A Safe Discovery Starting Point

This illustrative workflow is **discovery only**. It queries public release metadata, writes no branches, downloads no executable dependencies, and creates no PR. Place it on your default branch. GitHub schedules can be delayed; do not treat the interval as a delivery guarantee.

```yaml
name: Inspect public Core release
on:
  schedule:
    - cron: '23 */6 * * *'
  workflow_dispatch:
permissions:
  contents: read
concurrency:
  group: inspect-core-release
  cancel-in-progress: false
jobs:
  discover:
    runs-on: ubuntu-latest
    steps:
      - name: Read current published stable release metadata
        env:
          GH_TOKEN: ${{ github.token }}
          UPSTREAM: cwru-non-academic/WSSCore
        shell: bash
        run: |
          set -euo pipefail
          gh api "repos/$UPSTREAM/releases/latest" > "$RUNNER_TEMP/release.json"
          jq -e '(.draft == false) and (.prerelease == false)' "$RUNNER_TEMP/release.json"
          jq '{id, tag_name, published_at, assets: [.assets[] | {id, name, size}]}' \
            "$RUNNER_TEMP/release.json"
```

If no release is published, authentication fails, or the API rate limit is reached, this example fails visibly. A production adapter should classify these outcomes and back off; it must not substitute a random older download. The release's `target_commitish` is not sufficient evidence of an immutable tag commit.

## Implement the Candidate-to-PR Adapter

Keep the monitor/controller on trusted downstream code. Its contract should be:

1. Read your committed dependency record and candidate policy. Resolve one upstream full commit, producer workflow/run, artifact ID, archive filename, and SHA-256. If using release assets, additionally retain release and asset IDs and resolve the tag; never trust a moving `latest` URL as a pin.
2. Verify repository, event, branch/tag, run conclusion and required jobs. Reject fork/PR merge-ref producers as accepted development candidates. Check the selected artifact belongs to that run and commit and is still available.
3. Download to a fresh directory. Validate the ZIP inventory, safe extraction paths, checksum, assembly/dependency identities, and platform layout. A checksum obtained from the same compromised source proves consistency, not independent authenticity; bind it to your trusted producer policy.
4. Check out exact integration source and stage candidate dependencies outside the committed fallback. Clear stale outputs. Verify the candidate DLL hashes reach build/publish outputs, then run behavioral and platform compatibility tests through your real integration entry points.
5. Record tested source, candidate identity, test results, and coverage limits. A failed or skipped required test stops promotion. Do not replace expected values merely to accept an upstream change.
6. If accepted dependency bytes already match, record a no-op without rewriting provenance. Otherwise construct a dependency-only diff on a deterministic branch keyed by candidate identity. Check existing PRs/branches before writing; reuse an exact match, stop on conflicts or closed/rejected candidates, and never force-push over unrelated work.
7. Use a downstream-only authoring credential to create the PR after validation. Include old/new pins, producer and test links, archive/hash identity, and failures or untested platforms. Run normal PR CI against the committed proposal and require human review.

The candidate record and workflow evidence are data, not executable instructions. Test upstream binaries in an isolated job without PR-writing secrets. A separate authoring phase must verify the evidence came from the expected trusted workflow and immutable source before acquiring write credentials. Do not run candidate checkout scripts in a privileged `pull_request_target` job.

## Reuse the Existing Validation, Adapt the Orchestration

The [workflow reference](ci-workflow-reference.md) identifies actual source files. In C#, `wss-preflight.yml` already demonstrates fresh artifact resolution, disabling `lib` fallback, clean builds, output-hash verification, behavioral tests, and a Windows/Ubuntu/macOS compatibility matrix. `Verify-WssOutput.ps1` and the resolver are useful contracts to study.

They are not unrestricted reusable workflows: the resolver allowlists WSSCore, the YAML mints WSS App tokens, and promotion requires evidence from the coordinated CLI. Replace authentication with public reads or a credential you own with necessary upstream read access. Implement your own source-bound approval policy if you do not control a paired application; do not manufacture first-party CLI approval.

Unity and Python provide pinned canonical-fixture validation with runtime-specific evidence. Their reusable workflows and scripts also encode repository roles and layouts. Port the production-path tests and integrity checks deliberately; changing only a repository name does not make a new integration conformant.

For a C# integration using the same project layout, this **validation-step excerpt** shows how candidate paths are passed explicitly after verified extraction and fallback removal:

```powershell
# Template: adapt project paths and tests to your repository.
$ErrorActionPreference = 'Stop'
$candidate = Join-Path $env:RUNNER_TEMP 'verified/WSS-Universal'
if (!(Test-Path -LiteralPath $candidate -PathType Container)) { throw 'Verified candidate missing.' }
dotnet test tests/Wss.CSharpImplementation.Tests/Wss.CSharpImplementation.Tests.csproj `
  -c Release "-p:WSS_ARTIFACT_DIR=$candidate" "-p:WSS_BLE_ARTIFACT_DIR=$candidate"
if ($LASTEXITCODE -ne 0) { throw 'Candidate tests failed.' }
```

This excerpt does not implement discovery, verification, fallback removal, PR creation, or all first-party gates. Complete those preceding steps before enabling automation.

## Credentials and Operation

You need no upstream write token. Keep metadata/download permissions separate from downstream `contents: write` and `pull-requests: write`. Use your own narrowly installed App or appropriately scoped downstream credential if automatic PR CI requires it. If choosing the repository `GITHUB_TOKEN`, verify the resulting PR-check behavior and repository policy; do not assume authoring a PR guarantees checks will execute unattended. See [GitHub workflow triggering](https://docs.github.com/actions/using-workflows/triggering-a-workflow).

For artifact expiry, retries, branch protection, and action pinning, follow [Evidence, permissions, and operations](ci-evidence-and-operations.md). An upstream outage should delay your update, not silently change the accepted dependency.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [Building a New Integration Library](start-here-building-a-new-integration-library.md)
- [Next: Application Dependency Updates](ci-application-updates.md)
