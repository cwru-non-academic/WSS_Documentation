# Shared First-party CI Implementation

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

This November 5, 2026 reference describes the shared CI implementation deployed to all nine receiver `dev` and `main` branches. Source adoption, default-branch registration and acceptance of an actual dependency rollout are separate facts. The adoption checks do not establish nine-target campaign acceptance. The [October workflow inventory](ci-workflow-reference.md) remains historical.

## Runtime and Source Ownership

GitHub Actions runs common orchestration as Node.js 24 ECMAScript modules, with PowerShell 7 archive, process and native-evidence bridges. Receivers copy actual source files rather than independently translating the same logic into each language.

The current receiver locks copy [Core `83d77fb`](https://github.com/cwru-non-academic/WSSCore/tree/83d77fb184b1de92164fa73272630d07749077d2/tools/ci) and [Python integration source `82e52b4`](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/tree/82e52b49af0224313231eaea50867bab1cd66e11/.github/scripts/wss-ci). A November 5 audit verified all eighteen receiver branch heads: their scripts, workflows and stage configuration match between dev and main, and every lock selects this source. Each branch's `.github/scripts/wss-ci.lock.json` remains authoritative for subsequent maintenance revisions. The [Core manifest](https://github.com/cwru-non-academic/WSSCore/blob/83d77fb184b1de92164fa73272630d07749077d2/tools/ci/sync-tooling.json) declares both owners and repository scopes. Integration-only logic originates in the integration reference rather than a second Core implementation.

| Integration | Deployed dev adoption | Applications |
| --- | --- | --- |
| C# | [Library #60](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/60) | [CLI #39](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/39) |
| Unity | [Library #43](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/43) | [GUI #28](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/28) |
| Python | [Library #41](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/41) | [CLI #30](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/30), [ROS #25](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/25) |
| MATLAB | [Library #40](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/40) | [CLI #24](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/24) |

Main deployment records are [C# library #61](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/61), [Unity library #44](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/44), [Python library #42](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/42), [MATLAB library #41](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/41), [C# CLI #40](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/40), [Unity GUI #29](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/29), [Python CLI #31](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/31), [ROS #26](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/26) and [MATLAB CLI #25](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/25). Python's main promotions copied only reviewed CI files, preserving main's product payloads and gitlinks; the other cohort promotions preserved the verified dev tree. CI parity does not imply that product branches or accepted candidate records are identical.

[Core #39](https://github.com/cwru-non-academic/WSSCore/pull/39) aligned trusted-main executable tooling and replaced the older inline archive reader with the same guarded shared reader used by dev, without promoting Core product changes.

## Shared Code and Required Adapters

Integrations receive 27 managed files; applications receive 25. The two integration-only files implement the family notifier and workflow. All nine receivers run hosted `Shared CI source identity` checks against canonical Git blobs.

| Owner | Source | Responsibility |
| --- | --- | --- |
| Core | `tools/release/verify.mjs`, `github.mjs`, `policy.mjs` | Candidate/plan/digest validation, bounded reads, exact attempts and frozen producer-policy provenance |
| Core | `producer-jobs.mjs`, `normal-ci.mjs` | The 17 Core gates, including three BLE aggregate jobs; unique committed-state CI selection |
| Core | `tools/ci/sync-tooling.mjs`, manifest, archive bridge and identity workflow | Copying registered files, source locks and drift rejection |
| Python integration | `.github/scripts/wss-ci/receipt.mjs`, `receipt-targets.json`, `verify-plan.mjs` | Actual merge receipts, the same Core plan validator, parent gitlinks and frozen tooling |
| Python integration | `promotion.mjs`, `normalize-candidate.mjs` | Constrained publication, exact PR reuse, non-force pushes and six-field candidate records |
| Python integration | `staging.mjs`, `dispatch-proof.mjs`, profiles and PowerShell bridges | Stage policy, durable intent, exact reuse and ambiguous/uncertain dispatch rejection |
| Python integration | `preflight.mjs`, `Preflight.ps1` | One UTF-8 request serializer, correlated-run discovery and exact-run waiting across all four integration families |
| Python integration | `Process.ps1`, `Evidence.ps1`, notifier module and workflow | Argument-safe processes, versioned evidence identity and guarded integration wake-ups |

The common publisher resets inherited multi-valued Git authorization headers before supplying one delayed, repository-scoped publication token. Hosted contracts exercise a real Git HTTP endpoint on Linux, Windows and macOS. This corrects duplicate authorization headers without relaxing credential scope or source checks.

The shared preflight engine writes literal UTF-8 JSON to a file and passes that path directly to `gh api`. It validates the registered target and exact source/run identity, rejects duplicate correlated runs, and does not automatically repeat a POST with an uncertain outcome. Language adapters retain only their wire inputs and rich runtime proof checks.

Core runtime fixture pins are separate from orchestration-tool versions. The C# native Mac fixture uses reviewed Core source `0ac18df22a1371e5ff94d5485c6d3fa4b3e6538f`; its native fixture script rejects a different source. Updating common CI tooling does not authorize replacing that test fixture.

Adapters retain C# packaged/native tests, Unity import rules, Python/pythonnet and ROS Humble tests, MATLAB native architecture/version checks, paired preflights, payload mappings and runtime gitlinks. Executable adapter pins are immutable revisions recorded in each branch's workflows; inspect the corresponding workflow rather than assuming that dev and main have already adopted the same maintenance revision. These pins are distinct from the common origin and runtime dependency gitlinks.

## Evidence Stages

Promotion PR-head checks test proposed dependency state before its actual merge exists. Later receipt acceptance requires the actual same-repository bot dependency PR, human merge, current accepted tree and subsequent successful committed-state CI. An ordinary human infrastructure merge cannot authorize dependency fanout.

Receipt selection requires exactly one eligible postmerge `push` or `workflow_dispatch` run. Multiple eligible runs are ambiguous even when the newest is green. Required jobs must be unique, completed and successful at the selected attempt. Full workflow reruns keep required jobs together; inspect partial reruns rather than combining unrelated attempts into proof.

The shared `wss-ci-evidence-identity/v1` schema binds repository, run ID, attempt, source SHA and canonical architecture. Native adapters compare it with authoritative metadata alongside runtime, coverage and file hashes. Python CLI and ROS worker outcomes call the same integration-owned helper as the reusable Python workflow. Older outcomes without this identity cannot satisfy the current validator.

Unity's current automation uses a .NET production-source harness; its `personal-hosted-unverified` license route does not establish a real Unity Editor run. Native BLE ABI/load checks do not establish physical Bluetooth or stimulation tests. MATLAB uses real native MATLAB lanes, including separate Intel and Apple Silicon checks. Preserve these coverage limits when reading green CI.

## Deployed Branch and Coordinator Behavior

Core pull-request checks build and test proposed changes; their routine downstream handoff is skipped. Core's producer is triggered by `dev` pushes. It creates the identified universal candidate and frozen evidence plans. The deployed producer does not start a main-to-main candidate campaign on a Core `main` push.

[Core #37](https://github.com/cwru-non-academic/WSSCore/pull/37) deployed the reviewed coordinator to trusted `main` without changing the producer workflow. `dev-propagation.yml` reconciles successful Core producers, scheduled checks and explicit requests. For frozen policies declaring `application_fanout: per-integration`, one integration can authorize its own applications after its reviewed dependency merge and unique successful committed-state CI. Other families do not delay that authorization. Older frozen policies retain their original all-integration barrier. Final acceptance still requires all applicable nine targets.

The four integration notifiers are registered on their default branches. They execute the immutable default-main workflow revision and independently validate the actual merged dev candidate. The workflow source SHA and merged dev SHA are different identities; equality between them is not required. They observe eligible dependency merges and successful committed integration CI, validate candidate/frozen-source identity, and request a fixed Core reconciliation wake-up. They do not grant acceptance or publish anything. Human infrastructure PRs are excluded; source-adoption merges were observed with application fanout skipped.

Unity and Python integration/application workflows run full normal CI on applicable `dev` and `main` pushes. C# and MATLAB full normal suites use PR or explicit dispatch triggers; their shared identity checks run on pushes. Exact postmerge validation therefore includes explicit normal-CI runs where the workflow has no push trigger.

`dev-release.yml` separates acceptance from publication. Acceptance is gated by `WSS_DEV_ACCEPTANCE_ENABLED`; publication additionally requires `WSS_DEV_RELEASE_ENABLED`, all live evidence and the reviewed tag guard. At this deployment's hosted inspection, acceptance was enabled and the release switch was unset. The deployed policy is dev/beta with stable publication disabled. Fingerprint tags use `dev-beta-<producer-run-id>-<plan-prefix>`; this is not semantic-version numbering. Source deployment or a green PR does not establish a published release.

### Workflow tree snapshots

[Core #40](https://github.com/cwru-non-academic/WSSCore/pull/40) deployed the Core-owned workflow guard to trusted main before [Core #38](https://github.com/cwru-non-academic/WSSCore/pull/38) activated it on dev. [Producer 42114110612](https://github.com/cwru-non-academic/WSSCore/actions/runs/42114110612) passed all seventeen gates and emitted the actual snapshot artifacts. This establishes producer and handoff behavior; it does not establish final nine-target acceptance.

The reviewed producer policies declare `workflow_snapshot_policy: wss-workflow-snapshots/v1`. The versioned `workflow_snapshots` envelope records Git directory-tree identities outside the receiver-owned target profiles. The stage artifact covers all four integration dev/main workflow trees; the acceptance artifact covers all nine receiver dev trees and all four integration main trees. The directory identity includes nested reusable workers whose entrypoint YAML can remain unchanged.

One Core-only module, `tools/ci/workflow-freeze.mjs`, verifies the declaration against policy at the actual producer commit and checks snapshot identities. The snapshots participate in the plan ID. Removing the declaration or metadata and recomputing hashes cannot select legacy behavior. Historical producers without the declaration retain their original frozen rules; their artifacts are not rewritten. Existing receiver target/check/parent/coverage profiles and their canonical source83 locks remain unchanged.

The guard checks readiness, all four initial handoffs, integration-stage barriers, proof dispatch, live acceptance and the existing publisher's mutation boundary. The initial C# handoff runs inside its still-active producer; receivers retain their separate wait for successful producer completion. Per-family checks allow an unchanged integration to advance while another family is blocked. Final acceptance still requires every applicable target. Read-only evidence credentials remain separate from delayed write credentials, and existing publication switches and tag protection remain enforced.

Editing a frozen local workflow, including a nested worker, invalidates its affected authorization. Preserve the old evidence and use a fresh verified campaign rather than changing frozen data or relaxing receiver profiles. This guard covers repository-local workflow definitions; existing third-party action pin policy is unchanged.
### C# receipt policy boundary

[C# library #63](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/63) and [#64](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/64) deployed the canonical receipt assertion to dev and trusted main. [C# CLI #42](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/42) copied the identical adapter and tests; [#43](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/43) promoted them to main. Actual postmerge platform checks passed on all four branches.

Both C# adapters import the existing frozen Node `targetPolicy` and plan validator. They check the full context plan and digests, then require exact agreement with its canonical target. The integration reference's `receipt-targets.json` supplies the required jobs, parents, coverage and trusted paths, including the wrapper notifier. The C# adapter retains its role whitelist and rich native, projection, merge, source and CI-freshness checks. It has no second policy table.

The PowerShell invocation is assertion-only: literal context/tool paths are separate native arguments, and a fixed argument sentinel prevents the imported module from entering its receipt CLI. Hosted Linux, Windows and macOS tests exercise the actual bridge with canonical profiles, malformed contexts, weakened policy, invalid digests, process failure and denied network/process/receipt-write effects. The shared Core/integration locks and receiver target profiles are unchanged.

Changing a frozen adapter requires newly frozen campaign evidence. A successful tooling adoption does not repair an earlier plan or authorize substituting its scripts or receipts.

## Updating the Implementation

1. Change the owning source once and verify its exact revision with hosted contracts. Core owns broadly shared validation; the integration reference owns receiver-only orchestration.
2. Commit and retain the exact source revisions declared by the manifest. From the reviewed Core checkout, copy the registered files into a workspace containing all nine receivers:

   ```text
   node tools/ci/sync-tooling.mjs copy <workspace>
   ```

3. Review receiver diffs and required adapters or executable pins. Do not independently edit generated common files.
4. Use receiver PRs and hosted identity/platform checks to verify current heads. Earlier green heads do not verify later changes. Ordinary deployment proceeds through `dev` then `main`; default-branch registration is verified separately.

The shared identity workflow also executes the Core-owned `tools/ci/adapter-pins.mjs` from the lock's immutable Core checkout. It checks fixed tool checkouts and complete reusable workflow trees, including nested workers, against the current integration dev/main sources. Matching common-file locks alone do not prove that a workflow executes the current adapter. The verified owner checkout must be clean and committed before an application pin refresh:

```text
node tools/ci/adapter-pins.mjs refresh <application> <checkout> <workspace> <verified-adapter-sha>
node tools/ci/adapter-pins.mjs check <application> <checkout> <workspace>
```

These commands update only registered executable pins; runtime product gitlinks and explicitly reviewed native fixture pins retain their separate meanings. Unity's committed and candidate Mac paths use `macos-15` for Apple Silicon and retain the separate Intel checks.
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
