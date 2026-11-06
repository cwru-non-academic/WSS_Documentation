# Shared First-party CI Implementation

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

This November 6, 2026 reference describes the shared CI implementation deployed to all nine receiver `dev` and `main` branches. Source adoption, default-branch registration and acceptance of an actual dependency rollout are separate facts. The adoption checks do not establish nine-target campaign acceptance. The [October workflow inventory](ci-workflow-reference.md) remains historical.

## Runtime and Source Ownership

GitHub Actions runs common orchestration as Node.js 24 ECMAScript modules, with PowerShell 7 archive, process and native-evidence bridges. Receivers copy actual source files rather than independently translating the same logic into each language.

The receiver locks now copy [Core `d965f6a`](https://github.com/cwru-non-academic/WSSCore/tree/d965f6abda5dcad8ddda374ac00b67f5e5ddea91/tools/ci) and [Python integration source `5caa943`](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/tree/5caa943d5472a4709e4eb77176650acbef52d8a4/.github/scripts/wss-ci). A November 6 audit of all eighteen live branch heads verified these exact locks and identical scripts and complete workflow trees between each receiver's dev and main. Each branch's `.github/scripts/wss-ci.lock.json` remains authoritative for later maintenance revisions. The [Core manifest](https://github.com/cwru-non-academic/WSSCore/blob/d965f6abda5dcad8ddda374ac00b67f5e5ddea91/tools/ci/sync-tooling.json) declares both owners and repository scopes. Integration-only logic originates in the integration reference rather than a second Core implementation.

| Integration | Library dev / main adoption | Application dev / main adoption |
| --- | --- | --- |
| C# | [#66](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/66) / [#67](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/67) | CLI [#45](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/45) / [#46](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/46) |
| Unity | [#47](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/47) / [#48](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/48) | GUI [#32](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/32) / [#33](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/33) |
| Python | [#45](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/45) / [#46](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/46) | CLI [#34](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/34) / [#35](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/35); ROS [#29](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/29) / [#30](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/30) |
| MATLAB | [#44](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/44) / [#45](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/45) | CLI [#28](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/28) / [#29](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/29) |

These are CI-only adoptions: product payloads and runtime dependency gitlinks were preserved. CI parity does not imply identical product branches or accepted candidate records. Application executable pins select verified owner adoption revisions: C# `3673dbfef7b5fc00d19dd0a8d39a4762c7ff33bc`, Unity `555b6c217303d9e76fa4f3c88c5bd5467c5024df`, Python `7df83b26db1b6a89293f9c37a74ec6491433ceb4` and MATLAB `99099a98809fb87181f942c01ecd7c0b65724347`. Before pinning, the owner scripts, configuration, capability metadata and complete worker workflow trees were verified against both owner branches.

[Core #39](https://github.com/cwru-non-academic/WSSCore/pull/39) previously aligned trusted-main executable tooling and replaced the older inline archive reader with the same guarded shared reader used by dev, without promoting Core product changes. Receiver adoption of the newer immutable source did not itself deploy a new Core producer or coordinator. Subsequently, [Core #43](https://github.com/cwru-non-academic/WSSCore/pull/43) deployed the matching trusted-main bootstrap, followed by [Core #42](https://github.com/cwru-non-academic/WSSCore/pull/42) on dev. The registered main-channel controllers remain inactive, and the producer still starts automatic candidates only on dev pushes.

## Shared Code and Required Adapters

Integrations receive 30 managed files; applications receive 28. The two integration-only files implement the family notifier and workflow. The copied import closure includes the closed channel module, PowerShell channel bridge and assertion-safe entrypoint helper. Receiver capability files and adapters now accept explicit dev/main contexts while retaining producer, executing event/ref/source, frozen-policy and runtime-proof checks. This receiver support is deployed; Core main-to-main campaign activation has not been deployed. All nine receivers run hosted `Shared CI source identity` checks against canonical Git blobs.

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

One Core-only module, `tools/ci/workflow-freeze.mjs`, verifies the declaration against policy at the actual producer commit and checks snapshot identities. The snapshots participate in the plan ID. Removing the declaration or metadata and recomputing hashes cannot select legacy behavior. Historical producers without the declaration retain their original frozen rules; their artifacts are not rewritten. That deployment preserved receiver-owned target/check/parent/coverage profiles and the then-current source83 locks. The subsequent receiver source update above preserves historical plan semantics; it does not rewrite those frozen plans.

The guard checks readiness, all four initial handoffs, integration-stage barriers, proof dispatch, live acceptance and the existing publisher's mutation boundary. The initial C# handoff runs inside its still-active producer; receivers retain their separate wait for successful producer completion. Per-family checks allow an unchanged integration to advance while another family is blocked. Final acceptance still requires every applicable target. Read-only evidence credentials remain separate from delayed write credentials, and existing publication switches and tag protection remain enforced.

Editing a frozen local workflow, including a nested worker, invalidates its affected authorization. Preserve the old evidence and use a fresh verified campaign rather than changing frozen data or relaxing receiver profiles. This guard covers repository-local workflow definitions; existing third-party action pin policy is unchanged.
### C# receipt policy boundary

[C# library #63](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/63) and [#64](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/64) deployed the canonical receipt assertion to dev and trusted main. [C# CLI #42](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/42) copied the identical adapter and tests; [#43](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/43) promoted them to main. Actual postmerge platform checks passed on all four branches.

Both C# adapters import the existing frozen Node `targetPolicy` and plan validator. They check the full context plan and digests, then require exact agreement with its canonical target. The integration reference's `receipt-targets.json` supplies the required jobs, parents, coverage and trusted paths, including the wrapper notifier. The C# adapter retains its role whitelist and rich native, projection, merge, source and CI-freshness checks. It has no second policy table.

The PowerShell invocation is assertion-only: literal context/tool paths are separate native arguments, and a fixed argument sentinel prevents the imported module from entering its receipt CLI. Hosted Linux, Windows and macOS tests exercise the actual bridge with canonical profiles, malformed contexts, weakened policy, invalid digests, process failure and denied network/process/receipt-write effects. The shared Core/integration locks and receiver target profiles are unchanged.

Changing a frozen adapter requires newly frozen campaign evidence. A successful tooling adoption does not repair an earlier plan or authorize substituting its scripts or receipts.

### ROS candidate source boundary

[ROS #31](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/31) deployed the candidate-context repair to dev; [#32](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/32) deployed the identical CI tree to main. All three candidate resolver jobs share one workflow guard. Before loading any helper from the selected checkout, it verifies the canonical repository, dispatch event, closed dev/main ref, registered workflow identity, clean source and exact agreement between the selected source commit, executing commit and workflow commit. It then loads the shared channel/plan helpers and verifies the Core checkout against the ROS-owned immutable lock before acquiring application-read credentials.

The hosted boundary tests execute the production workflow guard on Windows, Linux and macOS for both channels. They reject incorrect caller/source identities, mismatched source and tooling roots, wrong or dirty guard checkouts and absent context. Alternate-source helpers write a marker if executed; the tests require rejection with the marker absent and detect a deliberately restored early-import ordering. These tests establish the source boundary, not a live main campaign or downstream receipt.

The preceding [Core producer 42170422551](https://github.com/cwru-non-academic/WSSCore/actions/runs/42170422551) generated real C#, Unity and MATLAB dependency merges and six corresponding integration/application receipts. Its Python promotion stopped at ROS's missing explicit candidate context. The ROS workflow repair changes a frozen workflow, so that campaign remains incomplete; its receipts do not establish acceptance of a later plan. A new guarded candidate requires its own actual downstream proof.

### Verified nine-target dev campaign

[Producer 42126584560](https://github.com/cwru-non-academic/WSSCore/actions/runs/42126584560), from Core dev commit `853d25c1e9e177f4478744e9eb7a7e4ea08adfa3`, passed the seventeen producer gates. [Core acceptance 42140895678](https://github.com/cwru-non-academic/WSSCore/actions/runs/42140895678) subsequently passed `Verify nine merged targets without publication`. It accepted all four integration libraries and all five linked applications using their actual dependency merges, successful committed-state CI and emitted receipts. Each family's application rollout proceeded independently; final acceptance required all nine targets.

The acceptance artifact `dev-release-acceptance-42126584560` (artifact ID `13467619226`) records plan `dcc96fc6b057fc6d0eb2cbd4b786633e416bc81d8388e97f693290a728096e3b`. Its candidate is `WSS-Universal-ci.zip`, artifact ID `13462250767`, SHA-256 `1e216b41625c6fdec861e695c8afe3370d389b4d2b7c856a1e2b3f0aaeb7ee28`. It binds each target's dependency merge, selected CI run and attempt, receipt emitter/source, receipt artifact and byte hash, and required coverage.

| Target | Accepted dependency PR | Committed-state CI | Receipt emitter |
| --- | --- | --- | --- |
| C# integration | [#65](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/65) | `42128756636`, attempt 1 | `42130567921`, attempt 1 |
| Unity integration | [#46](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/46) | `42130151880`, attempt 1 | `42130578687`, attempt 1 |
| Python integration | [#44](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/44) | `42129786024`, attempt 1 | `42130590118`, attempt 1 |
| MATLAB integration | [#43](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/43) | `42132329663`, attempt 1 | `42133838231`, attempt 1 |
| C# CLI | [#44](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/44) | `42132285756`, attempt 1 | `42133802464`, attempt 1 |
| Unity GUI | [#31](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/31) | `42132467232`, attempt 1 | `42133811711`, attempt 1 |
| Python CLI | [#33](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/33) | `42132479077`, attempt 1 | `42133820161`, attempt 1 |
| Python ROS | [#28](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/28) | `42132787573`, attempt 1 | `42133828487`, attempt 1 |
| MATLAB CLI | [#27](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/27) | `42138984269`, attempt 1 | `42140164023`, attempt 2 |

The MATLAB CLI receipt's first attempt failed on a GitHub read with HTTP 502 before emitting a receipt. Inspection found no receipt artifact from that attempt; a full rerun of the same unchanged run succeeded. The acceptance artifact records emitter attempt 2 separately from committed-state CI attempt 1. Earlier failed campaigns and their frozen evidence were not rewritten or substituted.

The publication job was skipped. This result proves dev-chain acceptance, including the repaired C# receipt boundary in both the library and CLI; it does not establish a published beta, main-to-main rollout, stable release, real Unity Editor execution or physical BLE tests.

### Verified dev campaign with dual-channel tooling

The later [producer 42185421813](https://github.com/cwru-non-academic/WSSCore/actions/runs/42185421813), from Core dev commit `13c1f1701dd9713f90466858fe490f3c25acf289`, completed the fresh rollout after the ROS candidate-context repair. [Core acceptance 42197608572](https://github.com/cwru-non-academic/WSSCore/actions/runs/42197608572) passed `Verify nine merged targets without publication` at 09:51 UTC on November 6, 2026. It verified all four actual integration dependency merges and all five application dependency merges, their successful committed-state CI and their emitted receipts. The C# application advanced before the MATLAB integration merged, demonstrating independent family progress.

The acceptance artifact `dev-release-acceptance-42185421813` (artifact ID `13492992152`) records plan `3f4543d5b2998d4295ca0b3fb6a7153234c765820e38c61aa1a0c3d5b2cea891`. Its original candidate is `WSS-Universal-ci.zip`, artifact ID `13487676036`, SHA-256 `194da08b6891e464e3e4cc875f51182e4a1a7de0012eabfbea478ea09c601f21`. All nine downloaded receipt byte hashes match the acceptance record, including their exact merge, selected CI attempt, emitter source, coverage and parent gitlinks. Earlier campaign receipts were not substituted.

| Target | Accepted dependency PR | Committed-state CI | Receipt emitter |
| --- | --- | --- | --- |
| csharp-wrapper | [#69](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/pull/69) | `42187475960`, attempt 1 | `42188081109`, attempt 1 |
| csharp-cli | [#48](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/pull/48) | `42189356521`, attempt 1 | `42191448581`, attempt 1 |
| unity-wrapper | [#50](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/50) | `42187815533`, attempt 1 | `42188740610`, attempt 1 |
| unity-gui | [#35](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/35) | `42190144443`, attempt 1 | `42191456847`, attempt 1 |
| python-wrapper | [#47](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/pull/47) | `42187495481`, attempt 1 | `42188090669`, attempt 1 |
| python-cli | [#36](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/pull/36) | `42190105283`, attempt 1 | `42191465233`, attempt 1 |
| python-ros | [#33](https://github.com/cwru-non-academic/ROS_Python_WSS_Application/pull/33) | `42190429163`, attempt 1 | `42191473547`, attempt 1 |
| matlab-wrapper | [#47](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib/pull/47) | `42191208763`, attempt 1 | `42192259723`, attempt 1 |
| matlab-cli | [#31](https://github.com/cwru-non-academic/CLI_Matlab_WSS_Application/pull/31) | `42196372988`, attempt 1 | `42197817047`, attempt 1 |

The publication job was skipped. This verifies the deployed dual-channel tooling on the dev chain, including the repaired live ROS candidate path; it does not establish an activated main campaign, a published release, Unity Editor execution or physical BLE tests.

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

The MATLAB native composite action is byte-identical in the integration and CLI but is not currently a registered copier entry; its updates still require coordinated copying and review.

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
