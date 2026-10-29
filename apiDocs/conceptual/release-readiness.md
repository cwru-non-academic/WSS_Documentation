# Release 1.0 readiness

[Home](../index.md) | [Back to Maintainers](../maintainers.md)

This is the single maintainer record for remaining release verification and publication decisions. User onboarding describes the intended finished 1.0 release, including MATLAB and Core macOS BLE. Do not publish these documentation changes until maintainers review the items below against the final source and distribution revisions.

## Publication gate

- [ ] Record final application, wrapper, Core and backend commits, submodule pins and distribution checksums for all 11 repositories in the repository catalog.
- [ ] Complete native MATLAB setup/CLI acceptance and Core macOS BLE integration, then propagate the complete matching payload to each consumer that exposes that transport. MATLAB's current facade exposes serial/test/conformance options, not a BLE selector; do not infer MATLAB BLE from Core BLE.
- [ ] Confirm host/OS/runtime combinations promised by each README against final artifacts. Unity's baseline BLE selection accepts Windows only; validate any additional Unity BLE claim after backend integration.
- [ ] Promote the Python runtime loader and runtime-compatible complete payload together, then run the documented CoreCLR startup on Windows/Linux/macOS. Baseline Windows loader chooses .NET Framework before honoring CoreCLR; forcing CoreCLR with its old payload fails loading System.IO.Ports.Parity. The passing baseline simulator result is .NET Framework evidence only.
- [ ] Verify ROS source and installed-package startup with explicit simulator/config/log overrides, packaged managed dependencies and the final wrapper pin. Confirm canonical launch configuration avoids developer-machine absolute paths and unintended device selection.
- [ ] Run native MATLAB simulator/readiness/shutdown and conformance using the final managed build and recursive CLI checkout. Default-sandbox startup failed with a filesystem error; reviewed native MATLAB startup, config/help and syntax checks succeeded outside that restriction. Full managed emulator execution remains required.
- [ ] Run Unity Test scene and minimal integration example in the licensed Editor, and build the target desktop player. Review the baseline unguarded UnityEditor import in Assets/Scripts/CrabSerialCNTR.cs and keep unrelated serial/port-scanning components disabled during simulator checks.
- [ ] Build and run C# library/CLI documented simulator commands with final complete dependencies. Local execution was blocked by Windows Application Control (0x800711C7); static inspection does not satisfy runtime acceptance.

## Documentation and hosting

- [ ] Regenerate Core/C#/Unity API metadata from recorded final revisions. Current committed C# metadata uses HFI.Wss while source uses Wss.CSharpImplementation; reconcile all API links, provenance and outputs before publication. Preserve the prior API refresh audit worktree as evidence rather than treating it as the final build.
- [ ] Run full DocFX/API and RMarkdown builds, then rendered HTML link/fragment/navigation checks, including every repository README link. The dependency-free catalog tool restored offline here but its execution was blocked by Application Control; no passing site build is claimed.
- [ ] Confirm the docs manifest's C# path src/Wss.CSharpImplementation.csproj and each external repository/source root. Verify the Sphinx path separately before enabling Python API publication; it is not required to follow the Python README onboarding.
- [ ] Resolve hosting with maintainers: onboarding links use https://cwru-non-academic.github.io/WSS_Documentation/; the existing CNAME points at wss.cosmiic.org. Verify deployed base path and redirects without changing hosting as part of this local draft.
- [ ] Verify all current academic repository and README URLs on final target branches. Organization/name migration is a separate change; retain the current catalog owner.
- [ ] Review overlap: shared docs own architecture, lifecycle/configuration/platform/testing contracts; READMEs own prerequisites, copyable installation and startup. Recheck reciprocal links and bounded simulator cleanup after final source changes.

## Evidence interpretation

Record executed, statically checked and unavailable checks separately. Simulator results do not certify hardware or prove native MATLAB/Unity execution. Historical CI evidence below is a prior inspection, not release acceptance; reconcile it with final workflows before publication.

## CI release acceptance

- [ ] Review current branches, registered dispatch workflows, evidence schemas, approval/merge behavior, App scope and branch protections for every supported consumer, including ROS and MATLAB.
- [ ] Run a genuine changed-candidate Core-to-wrapper-to-application chain and retain exact producer/candidate evidence. An identical-payload no-op does not prove propagation.
- [ ] Review requested dev-to-dev/main-to-main channel routing separately with the CI implementation owner; this documentation pass changes no CI behavior.

## Preserved historical CI inspection

The following 2026-10-24 snapshot is retained for comparison only. Resolve its open findings in the checks above rather than using it as current operational status.

# CI Workflow Reference and Verified Status

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

This is a source inspection snapshot from **2026-10-24**, not a continuous operational status report. Links below pin reviewed default-branch revisions. Routine first-party promotions target `dev`; default-branch registration and the workflow/script versions on the dispatched ref must both be ready.

## Inspected Repository Revisions

| Repository | Inspected `main` | Observed `dev` |
| --- | --- | --- |
| [WSSCore](https://github.com/cwru-non-academic/WSSCore/tree/28bdded0f14773f5ef27a38a0016578703d9fca2) | `28bdded0f14773f5ef27a38a0016578703d9fca2` | `5ec22900587587e9e9465ffa53d04d7d1ce13545` |
| [C# Integration](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/tree/109d6629a8890158dc501c8c42c66ef3121d2664) | `109d6629a8890158dc501c8c42c66ef3121d2664` | `7145e2307f997f703748530bf74311ef94455e98` |
| [C# CLI](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/tree/22db07e7921442d16a6fe62cfa22b0fc5bf467c8) | `22db07e7921442d16a6fe62cfa22b0fc5bf467c8` | `ec1a6d682ecb0d62718cbcad7acc276d84d9772d` |
| [Unity Integration](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/tree/724396545a2ce0b939fdf07353d110d053b3759d) | `724396545a2ce0b939fdf07353d110d053b3759d` | Same revision |
| [Unity GUI](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/tree/5d449d14db987e8888d7d9ec6c34976b80bfdc97) | `5d449d14db987e8888d7d9ec6c34976b80bfdc97` | Same revision |
| [Python Integration](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/tree/b527733d0c3fb213a515cd5a3e7b4a0d66b461c0) | `b527733d0c3fb213a515cd5a3e7b4a0d66b461c0` | Restored at the same revision |
| [Python CLI](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/tree/22139ec606140f900068b8f5fae469aab00f86e7) | `22139ec606140f900068b8f5fae469aab00f86e7` | Restored at the same revision |

## Workflow and Reuse Map

| Audience | Actual source | Reuse boundary |
| --- | --- | --- |
| Core maintainers | [artifacts.yml](https://github.com/cwru-non-academic/WSSCore/blob/28bdded0f14773f5ef27a38a0016578703d9fca2/.github/workflows/artifacts.yml) | Producer validation, one archive/checksum, in-run C# handoff, separate tag release job |
| Core maintainers | [propagate-unity.yml](https://github.com/cwru-non-academic/WSSCore/blob/28bdded0f14773f5ef27a38a0016578703d9fca2/.github/workflows/propagate-unity.yml), [propagate-python.yml](https://github.com/cwru-non-academic/WSSCore/blob/28bdded0f14773f5ef27a38a0016578703d9fca2/.github/workflows/propagate-python.yml) | Completed-producer trust checks and authorized dispatch; not a public polling adapter |
| C# integration developers | [wss-preflight.yml](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/blob/109d6629a8890158dc501c8c42c66ef3121d2664/.github/workflows/wss-preflight.yml), [wss-compatibility.yml](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/blob/109d6629a8890158dc501c8c42c66ef3121d2664/.github/workflows/wss-compatibility.yml) | Fresh-candidate versus committed-state behavioral/platform testing; adapt authentication and project layout |
| C# integration maintainers | [Resolve-WssRelease.ps1](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/blob/109d6629a8890158dc501c8c42c66ef3121d2664/.github/scripts/Resolve-WssRelease.ps1), [WssPromotion.ps1](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/blob/109d6629a8890158dc501c8c42c66ef3121d2664/.github/scripts/WssPromotion.ps1) | Producer verification and paired schema-2 approval; repository-specific trust and source restrictions |
| C# app maintainers | [cli-ci.yml](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/blob/22db07e7921442d16a6fe62cfa22b0fc5bf467c8/.github/workflows/cli-ci.yml), [wss-preflight.yml](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/blob/22db07e7921442d16a6fe62cfa22b0fc5bf467c8/.github/workflows/wss-preflight.yml), [promote-integration.yml](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/blob/22db07e7921442d16a6fe62cfa22b0fc5bf467c8/.github/workflows/promote-integration.yml) | Exact library checkout, output propagation and CLI tests; authoring additionally requires first-party approval |
| Unity developers | [UNITY_AUTOMATION.md](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/blob/724396545a2ce0b939fdf07353d110d053b3759d/.github/UNITY_AUTOMATION.md), [_unity-validation.yml](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/blob/724396545a2ce0b939fdf07353d110d053b3759d/.github/workflows/_unity-validation.yml) | Shared production .NET conformance, fixture/projection/evidence contract; manual Editor route is distinct |
| Unity app maintainers | [unity-ci.yml](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/blob/5d449d14db987e8888d7d9ec6c34976b80bfdc97/.github/workflows/unity-ci.yml), [promote-integration.yml](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/blob/5d449d14db987e8888d7d9ec6c34976b80bfdc97/.github/workflows/promote-integration.yml) | Committed gitlink validation and fresh merged-wrapper candidate preflight before constrained PR |
| Python developers | [PYTHON_AUTOMATION.md](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/blob/b527733d0c3fb213a515cd5a3e7b4a0d66b461c0/.github/PYTHON_AUTOMATION.md), [_python-validation.yml](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/blob/b527733d0c3fb213a515cd5a3e7b4a0d66b461c0/.github/workflows/_python-validation.yml) | Real Python/pythonnet canonical fixture execution and full candidate inventory evidence |
| Python app maintainers | [cli-ci.yml](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/blob/22139ec606140f900068b8f5fae469aab00f86e7/.github/workflows/cli-ci.yml), [promote-integration.yml](https://github.com/cwru-non-academic/CLI_Python_WSS_Application/blob/22139ec606140f900068b8f5fae469aab00f86e7/.github/workflows/promote-integration.yml) | Production CLI factory and exact wrapper tests; fresh merged-wrapper preflight before gitlink PR |

The implementation uses `promote-wss.yml` in integration repositories, `dispatch-cli-promotion.yml` for C#/Python merged-integration handoffs, `dispatch-unity-promotion.yml` for the Unity handoff, and `promote-integration.yml` in applications. These are coordinated workflows, not generic independent subscription endpoints.

## Deployment and Evidence Limits

GitHub reported Unity Integration PR4, Unity GUI PR3, Core handoff PR12, Python Integration PR1, Python CLI PR1, and Core Python handoff PR13 merged. The **2026-10-24 readiness follow-up** restored Python Integration `dev` at `b527733d0c3fb213a515cd5a3e7b4a0d66b461c0` and Python CLI `dev` at `22139ec606140f900068b8f5fae469aab00f86e7`; the earlier missing-branch finding is resolved. Fresh Python normal and candidate preflights each passed all ten cases. [Promotion readiness run 40058662944](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/actions/runs/40058662944) passed as an identical-payload no-op, not a changed-dependency PR.

Unity Integration [PR5](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/5) and Unity GUI [PR4](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/4) are draft fixes for missing normal `main` PR triggers. Their checks passed ten cases each, but they were **not merged** in this follow-up; do not describe those trigger additions as deployed behavior.

All four Unity/Python consumers have `main` and `dev`, default to `main`, and have automatic branch deletion disabled. Their readiness audit found no rulesets. Required-check/review enforcement must therefore not be inferred from passing workflow code. The exact acceptance contexts to use when configuring protection are **Integration behavioral tests** in both libraries, **GUI general conformance** in the Unity application, and **CLI general conformance** in the Python CLI. Repository protection configuration remains separate work; this documentation changes none of it.

Code presence does not confirm App installation scope, inherited secret availability, successful dispatch, or changed-payload propagation. Existing Core-to-C# App use is documented in the implementation; access to Unity/Python must not be inferred from it. A verified no-op and standalone passing candidate tests do not prove a complete changed-candidate Core-to-wrapper-to-application chain. Changed-candidate App dispatch/write and such an end-to-end chain remain untested after the readiness follow-up. A genuine changed-Core validation is still future work after readiness, ROS coverage, and protection setup.

The committed Unity automation notes describe older manual Editor evidence as predating the source extraction. Any newer local EditMode result must be retained with its exact source/runtime and evaluated separately; this inspection did not independently verify it. Hosted .NET evidence must continue to report `unity_executed: false`.

## Additional Applications

The verified additional application is [ROS_Python_WSS_Application](xref:repo.python-ros), package `hfi_ros_wss_driver`. Its `HFI_WSS_Python_Interface` dependency pins the older Python integration revision abbreviated `60f29a8`. Its CI/promotion pipeline remains untouched. The Python CLI evidence and dispatch target do not establish ROS coverage. Resolve and record its full dependency SHA, production-path tests, evidence contract, and authorized promotion route separately before claiming full downstream propagation.

## Independent Adapter Work Still Required

No independent polling/release-monitor implementation is installed by these docs. Its owner must implement discovery/state tracking, credential selection, trust verification, isolated tests, evidence-bound PR authoring, retry/idempotency behavior, and receiving-repository review policy. In particular, a persistent release-assets resolver is not supplied by copying the existing Actions-artifact resolver.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [Previous: Evidence, Permissions, and Operations](ci-evidence-and-operations.md)
