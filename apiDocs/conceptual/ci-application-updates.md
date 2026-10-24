# Application Dependency Updates

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

Use this path when you maintain an application and consume an integration library. Your update boundary is the library's public release, package, or exact repository commit. You do not need write access to Core or to the integration repository.

**Status:** independent monitoring is a proposed downstream adapter. Existing WSS application promotion workflows implement a coordinated first-party policy and require its authorized credentials and evidence.

## Pin What the Application Actually Uses

| Dependency form | Commit in the application | Validate |
| --- | --- | --- |
| Git submodule | Exact mode-160000 gitlink and approved `.gitmodules` URL | Exact checkout SHA, origin, clean state, recursive dependencies as needed |
| Package | Exact package version and lock/integrity record | Trusted registry/release, resolved transitive dependencies, runtime compatibility |
| Vendored binary bundle | Complete verified inventory and provenance | Source release/commit, archive hash, payload hashes, native/runtime layout |

A branch name or floating package range is discovery configuration, not an accepted pin. An application consuming a wrapper should normally advance the wrapper dependency rather than independently replace hidden Core DLLs underneath it.

## From Library Update to Application PR

1. Poll your chosen library release channel or allowed branch using the [independent monitoring pattern](ci-independent-integrations.md). A Core push is not automatically an application update: the wrapper may still be validating or may reject it.
2. Resolve the exact library revision and verify your acceptance policy: release publication or successful canonical integration CI, expected repository, compatible runtime, and available provenance. Record which Core candidate is included when the library publishes that information.
3. Check out your application at an immutable source revision and stage only the proposed library change. Verify the resulting submodule/package/binary contents. Use fresh build outputs to prevent committed or cached dependencies masking failures.
4. Run application construction/lifecycle tests, the integration's applicable conformance tests, and application-specific regression tests. Check deployment output contains the intended dependency. Emulation does not substitute for required runtime or device testing.
5. Only after successful validation, open or reuse a deterministic application dependency PR. Include the old/new library pin, upstream evidence, application test evidence, and coverage limits. Normal PR CI checks the committed candidate; a human decides whether to merge.

Use a read-only validation job and a separately gated application-only writer. Preserve branch protection, required review, and check requirements. If an upstream change requires source adaptation, make that work explicit and rerun tests; do not hide unrelated code changes in a gitlink-only automation PR.

## What the WSS Application Workflows Already Do

The [C# CLI normal CI](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/blob/22db07e7921442d16a6fe62cfa22b0fc5bf467c8/.github/workflows/cli-ci.yml) resolves and verifies the committed Integration gitlink, cleans outputs, runs CLI conformance and shared Integration scenarios, and checks platform build/publish propagation. Its [candidate preflight](https://github.com/cwru-non-academic/CLI_CSharp_WSS_Application/blob/22db07e7921442d16a6fe62cfa22b0fc5bf467c8/.github/workflows/wss-preflight.yml) additionally selects exact Integration source, downloads an exact Core candidate, disables the checked-in fallback, and verifies candidate output hashes.

This separation is reusable: **normal CI tests the pin in the PR; preflight tests an explicitly staged proposed dependency**. The App-token creation, repository allowlists, and first-party approval checks must be adapted for an independently owned app.

In the [Unity GUI workflow](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/blob/5d449d14db987e8888d7d9ec6c34976b80bfdc97/.github/workflows/unity-ci.yml), committed validation derives the wrapper from the application's gitlink and calls the wrapper validation workflow. The Python CLI follows the same role-based pattern through `_python-validation.yml`. Their current cross-repository workflow references use `@dev`; an independent adopter should select and review an immutable workflow revision and maintain compatibility between that workflow and its scripts/configuration.

The shared suites exercise real production command paths and Core emulation. Unity's automated .NET GUI path covers diagnostic forwarding, not scenes or UI managers. Python CLI conformance covers its production controller factory and Python wrapper, not interactive parsing. Add application-specific tests for the behavior your users depend on.

## Coordinated First-party Promotion Is a Stronger, Different Contract

In the first-party chain, both the integration and application have already preflighted the same Core candidate. A human then merges the integration dependency PR. Its trusted dispatcher verifies the actual merge, constrained diff, original approval, and final-head normal CI before requesting the application update.

Unity GUI and Python CLI promotion then run a **new preflight against the actual merged wrapper and current application source** before authoring a gitlink-only PR. The inspected C# CLI promotion instead validates the original paired approval, tested sources, actual merged Integration and payload before its gitlink PR; do not assume it implements that additional fresh post-merge preflight.

An independent app cannot claim the first-party dual approval merely because upstream CI is green. It can use upstream evidence as one input and issue its own source-bound application acceptance record. You control only the guarantees tested in your repository.

## Repeated and Failed Updates

Key requests by the immutable library revision and relevant candidate identity. If the exact pin is already accepted, report a no-op. Reuse an open matching PR only after checking its base/head, diff, provenance, and tested source. Stop for conflicting state, stale sources, a closed/rejected PR, or an uncertain prior write. Refresh validation when application code or the library revision changes.

Do not automatically release your application after a dependency merge. Release packaging, manual runtime checks, and deployment remain separate policies. See [Evidence, permissions, and operations](ci-evidence-and-operations.md) for retention and recovery.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [Developing an Application](start-here-developing-an-application.md)
- [Previous: Independent Integration CI](ci-independent-integrations.md)
- [Next: First-party Core Promotion](ci-core-promotion.md)
