# Shared First-party CI Implementation

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

This November 5, 2026 source reference describes the coordinated shared-CI migration under review. It does not claim that the drafts have been merged, deployed to trusted `main`, or exercised by a fresh propagation plan. Inspect exact PR heads and checks before approving deployment. The [October workflow inventory](ci-workflow-reference.md) remains historical.

## One Source and Runtime

GitHub Actions runs common orchestration as Node.js 24 ECMAScript modules. Existing PowerShell 7 archive, process and native-evidence bridges are maintained once. [WSSCore owns the reference](https://github.com/cwru-non-academic/WSSCore/tree/11ad9dda62ce5566367d1f46b4b23be07ab911dd/tools/ci). Its [manifest](https://github.com/cwru-non-academic/WSSCore/blob/11ad9dda62ce5566367d1f46b4b23be07ab911dd/tools/ci/sync-tooling.json) copies actual source files to receivers, rather than translating their behavior into each language.

| Integration | Applications | Draft review |
| --- | --- | --- |
| C# | C# CLI | [Wrapper #52](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/52), [CLI #32](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/32) |
| Unity | Unity GUI | [Wrapper #34](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/34), [GUI #22](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/22) |
| Python | Python CLI and ROS | [Wrapper #32](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/32), [CLI #22](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/22), [ROS #17](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/17) |
| MATLAB | MATLAB CLI | [Wrapper #31](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/31), [CLI #18](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/18) |

[Core #34](https://github.com/cwru-non-academic/WSSCore/pull/34) supplies the common implementation. Draft links show review state, not immutable verification records or permission to merge.

## Shared Code and Required Adapters

| Shared source in Core | Responsibility |
| --- | --- |
| `tools/release/verify.mjs`, `github.mjs` | Candidate/plan/digest policy, bounded reads, exact run-attempt jobs and frozen producer-policy provenance |
| `receipt.mjs`, `receipt-targets.json`, `normal-ci.mjs` | Read-only receipt orchestration, actual merge/source, unique postmerge CI, parent gitlinks, frozen tooling and late revalidation |
| `producer-jobs.mjs` | The same 17 current Core gates, including the three BLE aggregate jobs |
| `normalize-candidate.mjs` | Exactly six candidate fields; diagnostic release-tag selection stays separate |
| `promotion.mjs` | Constrained staged changes, source/parent freshness, exact PR reuse, non-force publication and draft PR creation |
| `staging.mjs`, `dispatch-proof.mjs` | Verified staging policy, durable dispatch intent, exact successful reuse and rejection of ambiguous/uncertain retries |
| `tools/ci/shared/Assert-UniversalArchive.ps1`, `Process.ps1`, `Evidence.ps1` | ZIP path/type/size/hash checks, argument-safe process calls, evidence/hash checks and versioned CI identity |

Adapters retain C# packaged/native tests; Unity importer rules and real Editor versus .NET harness distinctions; Python/pythonnet and ROS Humble tests; MATLAB native architecture/version tests; rich paired preflights, payload mappings, source pins and runtime gitlinks. A six-field candidate record cannot replace those proofs.

## Evidence Stages

Promotion's PR-head checks prove a proposed dependency change before its actual merge exists. Receipt acceptance is later: it requires the actual same-repository dependency PR, human merge, current accepted tree and subsequent successful committed-state CI. Neither stage replaces the other.

Receipt selection requires exactly one eligible postmerge `push` or `workflow_dispatch` run. Multiple eligible runs are ambiguous even when the newest is green. Required jobs must be unique, completed and successful at the selected attempt. Inspect ambiguity instead of choosing an older success.

The shared nested schema `wss-ci-evidence-identity/v1` binds repository, run ID, attempt, source SHA and canonical architecture. Native adapters compare it with authoritative run metadata alongside existing runtime, coverage and file-hash checks. Another attempt must fail. Old committed-payload diagnostics may predate this schema; that does not authorize them as current candidate proof. Historical legacy inventory also cannot satisfy the current `macos-ble-v1` producer policy. ABI/load checks do not claim physical Bluetooth, stimulation or Unity Editor execution.

## Updating the Implementation

1. Change and commit common source in Core. Run its hosted contract matrix on Linux, Windows and macOS.
2. From the Core checkout, copy registered files into a workspace containing all nine receiver checkouts:

   ```text
   node tools/ci/sync-tooling.mjs copy <workspace>
   ```

3. Review each receiver diff. Adapt required call sites, language-specific tests and explicit tooling pins. Do not hand-edit generated common files.
4. Update draft PRs into `dev`; verify each current head with hosted normal CI and `Shared CI source identity`. Earlier green heads do not verify later changes.
5. Obtain approval before each merge. Review Core, wrappers, then applications. Coordinate ordinary `dev`-to-`main` promotion, separately required workflow registration, validator pins and fresh frozen baselines before generating guarded candidate proof.

`.github/scripts/wss-ci.lock.json` records the immutable Core source SHA, durable ref and per-file SHA-256. Hosted identity checks execute the literal SHA. `ci/tooling/<commit>` preserves reachability after feature deletion or squash merge. Retain that ref and pinned wrapper adapter sources. Git canonical blobs accommodate ordinary Windows/Unix line endings.

The copier updates all receivers; external branch/PR publication is separately authorized and reviewed. `publish-tooling.mjs` remains proposed bulk publication tooling and has not been successfully demonstrated. Its presence is not an activated or validated service. A common change is complete after receiver adoption and verification, not merely after drift detection.

## Trust and Deployment

Preserve separate read-only and target-only authoring/dispatch credentials, the four-wrapper barrier, late source rechecks, durable intent and uncertain-outcome inspection. Changing frozen scripts invalidates old plans. Installation on `dev` does not establish default-branch workflow registration. Hosted PR checks prove proposed code; full reconciliation needs reviewed deployment and fresh evidence. No merge, dispatch, producer activation or access expansion is authorized by this guide.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [First-party Core Promotion](ci-core-promotion.md)
- [Evidence, Permissions, and Operations](ci-evidence-and-operations.md)
- [Historical Workflow Reference](ci-workflow-reference.md)
