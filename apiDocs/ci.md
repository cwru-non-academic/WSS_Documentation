# CI and Dependency Updates

[Home](index.md) | [Start Here](start-here.md)

WSS updates move through Core, an integration library, and an application. Each repository accepts a specific dependency revision through tests and a reviewable pull request. A notification starts evaluation; it does not authorize merging or releasing.

## Choose the Repository You Control

| Your responsibility | Start here | Access model |
| --- | --- | --- |
| Your own integration library, consuming public Core | [Independent integration CI](conceptual/ci-independent-integrations.md) | Read public upstream evidence; write only your own repository |
| Your own application, consuming an integration library | [Application dependency updates](conceptual/ci-application-updates.md) | Read the library; update your application's pin and run its tests |
| Core and the coordinated WSS integrations/applications | [First-party Core promotion](conceptual/ci-core-promotion.md) | Existing approved App installation with explicitly scoped cross-repository access |

The independent paths describe a **proposed downstream-owned monitoring adapter**. The coordinated first-party path describes workflow code already present in the WSS repositories. This documentation does not install a monitor or grant access to the WSS GitHub App.

## Understand the Three Gates

1. **Producer validation:** Core builds and checks an exact candidate archive.
2. **Candidate preflight:** a consumer tests fresh downloaded dependencies against exact consumer source, before updating its committed dependency.
3. **Normal pull-request CI:** the proposed committed dependency state is checked again before a human merge.

Passing an integration preflight does not prove an application works. Passing an application preflight does not publish a release. Identical accepted dependency bytes can produce a verified no-op, with no PR.

## Shared Reference

- [Evidence, permissions, and operations](conceptual/ci-evidence-and-operations.md): pinning, credentials, retention, review gates, and safe retries.
- [Workflow reference and verified status](conceptual/ci-workflow-reference.md): actual file names, reusable parts, source revisions, and operational caveats.

The implementation snapshot was inspected on **2026-10-24**. Workflow definitions, branch existence, credentials, and successful end-to-end execution are separate facts; see the dated status before relying on automatic promotion.

Navigation:

- [Back to Home](index.md)
- [Independent integration CI](conceptual/ci-independent-integrations.md)
- [Application dependency updates](conceptual/ci-application-updates.md)
- [First-party Core promotion](conceptual/ci-core-promotion.md)
