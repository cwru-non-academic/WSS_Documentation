# Shared First-party CI Implementation

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

This November 5, 2026 reference describes the shared CI implementation deployed to all nine receiver `dev` and `main` branches. Source adoption, default-branch registration and acceptance of an actual dependency rollout are separate facts. The adoption checks do not establish nine-target campaign acceptance. The [October workflow inventory](ci-workflow-reference.md) remains historical.

## Runtime and Source Ownership

GitHub Actions runs common orchestration as Node.js 24 ECMAScript modules, with PowerShell 7 archive, process and native-evidence bridges. Receivers copy actual source files rather than independently translating the same logic into each language.

The deployed locks use [Core `815479c`](https://github.com/cwru-non-academic/WSSCore/tree/815479cd2b7d7c3eb5aa3e499080163e53d09a57/tools/ci) and [Python integration source `21ea1f7`](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/tree/21ea1f7eaf2b0b812eabf429163eb9234df3f195/.github/scripts/wss-ci). The [Core manifest](https://github.com/cwru-non-academic/WSSCore/blob/815479cd2b7d7c3eb5aa3e499080163e53d09a57/tools/ci/sync-tooling.json) declares both owners and repository scopes. Integration-only logic originates in the integration reference rather than a second Core implementation.

| Integration | Deployed dev adoption | Applications |
| --- | --- | --- |
| C# | [Library #54](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/54) | [CLI #33](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/33) |
| Unity | [Library #37](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/37) | [GUI #24](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/24) |
| Python | [Library #34](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/34) | [CLI #24](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/24), [ROS #19](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/19) |
| MATLAB | [Library #34](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/34) | [CLI #20](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/20) |

Main deployment records are [C# library #55](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/55), [Unity library #38](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/38), [Python library #35](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/35), [MATLAB library #35](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/35), [C# CLI #34](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/34), [Unity GUI #25](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/25), [Python CLI #25](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/25), [ROS #20](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/20) and [MATLAB CLI #21](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/21). Each main merge preserved the exact verified dev tree.

## Shared Code and Required Adapters

Integrations receive 25 managed files; applications receive 23. The two integration-only files implement the family notifier and workflow. All nine receivers run hosted `Shared CI source identity` checks against canonical Git blobs.

| Owner | Source | Responsibility |
| --- | --- | --- |
| Core | `tools/release/verify.mjs`, `github.mjs`, `policy.mjs` | Candidate/plan/digest validation, bounded reads, exact attempts and frozen producer-policy provenance |
| Core | `producer-jobs.mjs`, `normal-ci.mjs` | The 17 Core gates, including three BLE aggregate jobs; unique committed-state CI selection |
| Core | `tools/ci/sync-tooling.mjs`, manifest, archive bridge and identity workflow | Copying registered files, source locks and drift rejection |
| Python integration | `.github/scripts/wss-ci/receipt.mjs`, `receipt-targets.json`, `verify-plan.mjs` | Actual merge receipts, the same Core plan validator, parent gitlinks and frozen tooling |
| Python integration | `promotion.mjs`, `normalize-candidate.mjs` | Constrained publication, exact PR reuse, non-force pushes and six-field candidate records |
| Python integration | `staging.mjs`, `dispatch-proof.mjs`, profiles and PowerShell bridges | Stage policy, durable intent, exact reuse and ambiguous/uncertain dispatch rejection |
| Python integration | `Process.ps1`, `Evidence.ps1`, notifier module and workflow | Argument-safe processes, versioned evidence identity and guarded integration wake-ups |

The common publisher resets inherited multi-valued Git authorization headers before supplying one delayed, repository-scoped publication token. Hosted contracts exercise a real Git HTTP endpoint on Linux, Windows and macOS. This corrects duplicate authorization headers without relaxing credential scope or source checks.

Adapters retain C# packaged/native tests, Unity import rules, Python/pythonnet and ROS Humble tests, MATLAB native architecture/version checks, paired preflights, payload mappings and runtime gitlinks. Python application executable pins use source `4521613359b88e440c022ae0116832eabe353b8a`; MATLAB CLI uses adapter source `584e2a8fa93e528a2ffef9b0faa263e41cee7f21`. These pins are distinct from the common origin and runtime dependency gitlinks.

## Evidence Stages

Promotion PR-head checks test proposed dependency state before its actual merge exists. Later receipt acceptance requires the actual same-repository bot dependency PR, human merge, current accepted tree and subsequent successful committed-state CI. An ordinary human infrastructure merge cannot authorize dependency fanout.

Receipt selection requires exactly one eligible postmerge `push` or `workflow_dispatch` run. Multiple eligible runs are ambiguous even when the newest is green. Required jobs must be unique, completed and successful at the selected attempt. Full workflow reruns keep required jobs together; inspect partial reruns rather than combining unrelated attempts into proof.

The shared `wss-ci-evidence-identity/v1` schema binds repository, run ID, attempt, source SHA and canonical architecture. Native adapters compare it with authoritative metadata alongside runtime, coverage and file hashes. Python CLI and ROS worker outcomes call the same integration-owned helper as the reusable Python workflow. Older outcomes without this identity cannot satisfy the current validator.

Unity's current automation uses a .NET production-source harness; its `personal-hosted-unverified` license route does not establish a real Unity Editor run. Native BLE ABI/load checks do not establish physical Bluetooth or stimulation tests. MATLAB uses real native MATLAB lanes, including separate Intel and Apple Silicon checks. Preserve these coverage limits when reading green CI.

## Deployed Branch and Coordinator Behavior

Core pull-request checks build and test proposed changes; their routine downstream handoff is skipped. Core's producer is triggered by `dev` pushes. It creates the identified universal candidate and frozen evidence plans. The deployed producer does not start a main-to-main candidate campaign on a Core `main` push.

[Core #37](https://github.com/cwru-non-academic/WSSCore/pull/37) deployed the reviewed coordinator to trusted `main` without changing the producer workflow. `dev-propagation.yml` reconciles successful Core producers, scheduled checks and explicit requests. For frozen policies declaring `application_fanout: per-integration`, one integration can authorize its own applications after its reviewed dependency merge and unique successful committed-state CI. Other families do not delay that authorization. Older frozen policies retain their original all-integration barrier. Final acceptance still requires all applicable nine targets.

The four integration notifiers are registered on their default branches. They observe eligible dependency merges and successful committed integration CI, validate candidate/frozen-source identity, and request a fixed Core reconciliation wake-up. They do not grant acceptance or publish anything. Human infrastructure PRs are excluded; source-adoption merges were observed with application fanout skipped.

Unity and Python integration/application workflows run full normal CI on applicable `dev` and `main` pushes. C# and MATLAB full normal suites use PR or explicit dispatch triggers; their shared identity checks run on pushes. Exact postmerge validation therefore includes explicit normal-CI runs where the workflow has no push trigger.

`dev-release.yml` separates acceptance from publication. Acceptance is gated by `WSS_DEV_ACCEPTANCE_ENABLED`; publication additionally requires `WSS_DEV_RELEASE_ENABLED`, all live evidence and the reviewed tag guard. At this deployment's hosted inspection, acceptance was enabled and the release switch was unset. The deployed policy is dev/beta with stable publication disabled. Fingerprint tags use `dev-beta-<producer-run-id>-<plan-prefix>`; this is not semantic-version numbering. Source deployment or a green PR does not establish a published release.

## Updating the Implementation

1. Change the owning source once and verify its exact revision with hosted contracts. Core owns broadly shared validation; the integration reference owns receiver-only orchestration.
2. Commit and retain the exact source revisions declared by the manifest. From the reviewed Core checkout, copy the registered files into a workspace containing all nine receivers:

   ```text
   node tools/ci/sync-tooling.mjs copy <workspace>
   ```

3. Review receiver diffs and required adapters or executable pins. Do not independently edit generated common files.
4. Use receiver PRs and hosted identity/platform checks to verify current heads. Earlier green heads do not verify later changes. Ordinary deployment proceeds through `dev` then `main`; default-branch registration is verified separately.

The copier updates files and locks, not just a drift report. Branch pushes, PRs and merges remain separate reviewed operations. A common update is adopted only when affected receivers contain and verify the copied source.

`.github/scripts/wss-ci.lock.json` records exact Core/integration SHAs, retained refs, origin paths and per-file SHA-256. Hosted checks fetch literal revisions. Retain `ci/tooling/<commit>` refs and pinned adapter sources so squash merges or feature deletion cannot remove the intended source reference. Git canonical blobs accommodate Windows/Unix line endings.

## Trust and Deployment

Read-only evidence credentials remain separate from delayed target-only authoring/dispatch credentials. Source freshness, frozen tooling, durable intent and uncertain-outcome rejection remain enforced. Changing frozen scripts invalidates old evidence; a later green adoption PR does not repair an earlier candidate's proof.

Default-branch notification registration, trusted-main coordinator code and receiver baselines are separate prerequisites from dev adoption. The notifier requests a wake-up only for a verified dependency merge and canonical producer; Core still revalidates authorization. Final acceptance requires all applicable nine targets. No receipt or build by itself is a published beta or stable release.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [First-party Core Promotion](ci-core-promotion.md)
- [Evidence, Permissions, and Operations](ci-evidence-and-operations.md)
- [Historical Workflow Reference](ci-workflow-reference.md)
