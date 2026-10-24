# First-party Core Promotion

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

This guide is for maintainers authorized to coordinate Core, integrations, and applications. It describes the inspected implementation, including its gates and current readiness limits. Independent developers should use [their downstream-owned adapter](ci-independent-integrations.md).

## Producer Validation and Release Are Separate

Core's `artifacts.yml`, **Build universal WSS release**, runs for PRs targeting `dev`, `main`, and `net2_BLE`, pushes to `dev`, `v*` tag pushes, and manual dispatch. It builds one authoritative universal archive and checksum, validates inventory and identities, and exercises source Core conformance and packaged consumers.

The required validation includes **Core conformance**, **Build and validate artifacts**, **BLE consumer (Linux/Windows)**, and **Serial consumer** and **Core consumer** on Linux, Windows, and macOS. Packaging builds production native inputs; consumer jobs download and verify the same ZIP rather than repackaging their own substitute. Emulated tests and dependency/native activation checks do not establish stimulation over real hardware. Missing-controller probes may explicitly remain unexecuted; read actual job reports rather than equating a green workflow with every hardware path running.

For a canonical successful Core `dev` push, **Start routine downstream validation** hands the exact producer tuple to C# Integration. That is the eleventh required producer job checked by the separate Unity/Python handoff validators. C# waits for the entire producer run to finish successfully before accepting it.

`propagate-unity.yml` and `propagate-python.yml` are separate default-branch `workflow_run` completion handlers. They require a successful `artifacts.yml` push to canonical Core `dev`, validate dev ancestry and all required producer jobs, select the unique unexpired artifact and verify its archive checksum. They also check destination workflow readiness and prior correlated requests before obtaining a target-scoped dispatch token. Their failure is separate from the original producer result.

PR builds, failed producers, fork producers, tags, and manual producers do not automatically take those routine Unity/Python paths. Manual candidate resolution and routine automatic eligibility are separate policies.

Only the gated `v*` tag-push publication job creates the GitHub release from tested archive/checksum assets. A successful `dev` build does not publish a release; merging a downstream dependency PR does not release an application.

## Changed Candidate to Integration PR

The handoff carries the producer repository, full commit, run ID, artifact ID, archive name, and SHA-256. Consumers revalidate it; a dispatch payload alone is not authority.

For a changed candidate:

1. Select immutable integration and application source revisions; reject source races.
2. Resolve a fresh verified candidate. C# disables committed `lib` fallback and checks output propagation. Unity validates its complete DLL/importer projection; Python verifies its complete universal inventory.
3. Execute **both** integration and application candidate preflights against the same producer and exact sources. Reuse only validated matching correlated evidence.
4. Validate actual test results, source identity, candidate provenance, and required acceptance jobs. Construct the implementation-specific approval record.
5. Only then obtain integration-only contents/PR write credentials and create or reuse a constrained dependency PR targeting `dev`.
6. Run normal Integration PR CI on the committed proposal and require human review/merge.

The C# approval is schema 2 with `candidate`, `integration.source_sha/preflight_run_id`, and `cli.source_sha/preflight_run_id`. Unity and Python have their own approval schemas and markers, including their source/fixture/payload evidence. Their records are not interchangeable. Never copy a historical bootstrap approval into a new candidate request.

Identical payload bytes/projection produce a verified no-op rather than a metadata-only dependency PR. Current committed provenance is preserved. C# retains the newly verified no-op approval in `wss-automatic-approval`; Unity/Python can exit at their earlier validated identical-payload check, before dispatching new preflights. A no-op proves that path's comparison, not a changed-payload promotion chain.

## Canonical Conformance Across Languages

Unity and Python pin the C# fixture at `7145e2307f997f703748530bf74311ef94455e98` and verify its configured SHA-256. Only the controller factory/type alias is adapted. The ten canonical cases and eighteen `Assert.That` source expressions, values, finite waits, evaluator calls, and grouped assertions remain unchanged.

The cases cover initialization, direct analog stimulation, six normalized scenarios, runtime event editing/resumption, and stopping without unintended resumed streaming. Core's emulator/evaluator owns the protocol/state expectations. Eighteen is a count of source expressions; parameterized execution and setup can yield different runtime assertion counts.

| Runtime path | What actual acceptance demonstrates | What it does not demonstrate |
| --- | --- | --- |
| C# | Integration/CLI behavior and platform packaging checks specified by their jobs | Physical-device behavior or all application workflows |
| Unity repositories, automated .NET | Shared production commands/layer composition; GUI diagnostic forwarding; exact ten-case XML evidence | Unity Editor/import/runtime, scenes, UI, player builds, device I/O |
| Python/pythonnet on Windows | Real production Python wrapper/tick thread and Core stack; CLI production factory | All Python/.NET versions, interactive CLI parsing, physical transports |

Unity automated evidence explicitly records `execution: dotnet` and `unity_executed: false`. Python records its Python execution contract. Manual Unity EditMode evidence is separate and must identify its exact source/runtime. Neither an artifact name containing `unity` nor a local Editor result changes what hosted .NET evidence proves.

## Human Integration Merge to Application PR

The integration dispatcher uses a merged, same-repository canonical bot dependency PR targeting `dev`. Its `pull_request_target: closed` handler runs trusted base tooling rather than executing the caller-nominated PR source with privileges. It validates unique approval markers, deterministic branch identity, the allowed dependency-only diff, the actual merge and committed payload, and successful normal PR CI for the final reviewed head.

For Unity/Python, the application promotion revalidates the original paired evidence and the actual merged wrapper, then executes another candidate preflight against that wrapper merge and current application. A successful exact match permits an application-only contents/PR token and a gitlink-only PR. C# validates its original paired proof, source restrictions, reviewed Integration merge, and payload before its CLI gitlink PR. Normal application PR CI and a human merge remain required in every path.

Normal CI names are deliberately consistent: **WSS integration compatibility / Integration behavioral tests**, **WSS downloaded-artifact preflight / Preflight behavioral tests**, and **CLI CI / CLI general conformance**; Unity GUI uses **GUI CI / GUI general conformance**. These names are acceptance surfaces, not interchangeable evidence. Inspect the repository-specific workflow and runtime record.

## Access and Deployment Readiness

Existing approved `WSS_GITHUB_APP_ID` and `WSS_GITHUB_APP_PRIVATE_KEY` configuration is required for coordinated promotion. Core's token has Actions-write only for the target integration; consumer evidence tokens are read-only; preflight tokens write Actions only on necessary consumers; authoring tokens write contents/PRs only on the destination after validation. Default `GITHUB_TOKEN` permissions remain read-only except the release publication job's required contents write.

Normal Unity/Python conformance uses public read-only checkouts without an App secret. A maintainer's personal ability to read repositories does not establish that the WSS App is installed or permitted on them. Workflow code cannot create installation access or organization-secret grants. An owner must resolve a missing approved capability explicitly.

Default-branch workflow registration, target `dev` branch availability, approved baselines, artifact access, and configured credentials are separate prerequisites. The **2026-10-24** readiness follow-up restored the Python target `dev` branches and verified fresh normal/candidate conformance plus an identical-payload promotion no-op. Changed-candidate App dispatch/write and end-to-end propagation remain untested. See [the dated source/status table](ci-workflow-reference.md), including pending Unity trigger fixes and repository protection status, before attempting promotion. This documentation does not change branches or permissions, or fabricate an upstream push to trigger a demonstration.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [Minor Core Modifications](start-here-minor-core-modifications.md)
- [Adding Layers or Core Functionality](start-here-adding-layers-or-core-functionality.md)
- [Next: Evidence, Permissions, and Operations](ci-evidence-and-operations.md)
