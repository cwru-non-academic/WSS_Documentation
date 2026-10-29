# CI workflow reference

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

Use each repository's checked-out workflow files as the executable contract for that revision. The shared guides describe trust, evidence and review boundaries; application setup remains in the [repository READMEs](repository-and-kit-links.md).

## Workflow responsibilities

| Owner | Responsibilities | Shared guide |
| --- | --- | --- |
| Core | Build a complete candidate, validate it and identify its source/artifacts | [Core promotion](ci-core-promotion.md) |
| Integration | Test the exact Core candidate in the host wrapper and bind acceptance to evidence | [Evidence and operations](ci-evidence-and-operations.md) |
| Application | Test the exact integration pin through the application path | [Application updates](ci-application-updates.md) |
| Independent consumer | Select upstream revisions, validate trust and test within its own repository | [Independent integration CI](ci-independent-integrations.md) |

Keep workflow filenames, acceptance contexts, permissions and source refs tied to the reviewed revision. Code presence alone does not prove installed App scope, branch protection or successful changed-candidate propagation. Preserve source/runtime identity with test results, and distinguish hosted .NET checks from native Unity or MATLAB execution.

Repository-specific maintainer files explain local operation. The [central release checklist](release-readiness.md) retains historical inspection evidence and the acceptance checks required before publishing 1.0. It is the place for transient readiness findings.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [Previous: Evidence, Permissions, and Operations](ci-evidence-and-operations.md)
