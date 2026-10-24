# CI Evidence, Permissions, and Operations

[Home](../index.md) | [CI and Dependency Updates](../ci.md)

Use these rules with either an independent downstream adapter or the coordinated first-party flow. Implementation-specific validators remain the source of truth for their accepted schemas.

## Pin Identity and Preserve Evidence

Keep producer repository, full commit, workflow path/event, run and attempt context, artifact ID, archive filename/hash, consumer source SHAs, test-run IDs, runtime, fixture identity, and payload/projection hashes together. Where an implementation's JSON omits a field, retain the corresponding authoritative run metadata rather than inventing an accepted schema extension.

Verify that evidence refers to the expected workflow, exact source, latest applicable execution, and actual successful required jobs. Validate real test XML, expected case identities, zero failures/skips, and log/XML hashes where required. An unrelated green job, manually edited PR body, or matching artifact name is not enough. Source changes affecting tests, references, behavior, or workflow trust require fresh evidence; use each validator's narrow source-difference rules rather than assuming all changed files are harmless.

Do not fetch executable promotion tooling from a caller-nominated commit. Use reviewed trusted tooling, treat dispatch JSON/PR markers/artifacts as untrusted inputs, constrain URLs/repositories and archive paths, and validate before using them. Pin third-party actions and adopted reusable workflows to reviewed full commit SHAs in your own deployment, with a deliberate update process. Existing WSS YAML still uses some moving version/branch references; this is guidance for adoption, not a claim that every current workflow is SHA-pinned. [GitHub secure use guidance](https://docs.github.com/en/actions/reference/security/secure-use)

## Permission Boundaries

| Phase | Needed authority | Owner |
| --- | --- | --- |
| Discover public updates | Public metadata reads, with rate-limit authentication if needed | Downstream maintainer |
| Read producer artifacts/evidence | Read access to the named upstream resources and required Actions APIs | Credential owner authorized for that upstream |
| Run candidate tests | Read-only source/dependency access, isolated from authoring credentials | Consumer repository |
| Dispatch coordinated preflights | Actions-write on explicitly selected destinations | Approved first-party App installation |
| Open dependency PR | Contents/PR-write on that one destination | Downstream-owned credential or approved scoped App |
| Publish release | Explicit release-job contents write | Producer repository |

An independent adopter does not receive WSS App credentials or upstream dispatch permission. Do not broaden an account token merely because a first-party script expects it. Keep private keys in approved secret storage, never workflow examples, artifacts, PR bodies, or logs. Credentials must be unavailable to untrusted candidate code; use separate jobs and validate the transition to authoring.

Repository `GITHUB_TOKEN` permissions are repository-scoped; permission declarations do not grant cross-repository authority. Also verify how the chosen authoring token triggers checks. Current GitHub documentation describes exceptions and approval requirements for token-created PR events; App tokens can have different behavior. A PR existing is not evidence that its required checks ran. [Triggering workflows](https://docs.github.com/actions/using-workflows/triggering-a-workflow)

## Artifact Availability and Retention

Public source visibility does not guarantee anonymous access to Actions archives. GitHub documents signed-in repository read access for artifact download. Cross-repository API/CLI retrieval must use credentials authorized for the requested resource. Artifacts and logs can expire or be deleted. [Downloading workflow artifacts](https://docs.github.com/actions/how-tos/manage-workflow-runs/download-workflow-artifacts)

| Inspected artifact | Retention in YAML |
| --- | --- |
| Core `universal-release-assets` | No explicit duration; effective repository policy applies |
| C# preflight `verified-wss-universal` | 1 day |
| C# CLI preflight `wss-candidate-provenance` | 1 day |
| C# Integration candidate provenance and `wss-automatic-approval` | No explicit duration; repository policy applies |
| Unity/Python conformance evidence, diagnostics, promotion records, and Core handoff records | 14 days where explicitly configured |

Do not assume defaults guarantee a particular retention period. The inspected C# resolver's release-tag mode resolves an exact successful tag producer, then downloads its **Actions** archive. It does not download manually attached release assets as a fallback. A published release therefore does not make this resolver independent of Actions artifact retention. A downstream release-assets adapter is separate work and needs its own producer/authenticity policy.

Store durable dependency provenance with accepted dependency state, plus the audit records required by your organization. Historical bootstrap evidence in the C# repository is narrowly allowlisted by its validator; it is not permission to bypass missing evidence for future candidates. If proof expires before promotion, obtain a new explicitly identified authoritative candidate/preflight or stop for maintainer resolution. Do not substitute a rebuilt archive under the old run/artifact/hash identity.

## Branch Protection and Human Review

Configure the receiving repository to require relevant normal PR checks and review, and protect CI/promotion tooling changes with appropriate ownership. Ensure automation cannot bypass the intended merge gates. This is an adoption requirement; repository rulesets were not audited or changed for this documentation.

Use stable acceptance checks that fail when prerequisites are failed or skipped. Include any additional application/runtime/platform checks required by your support policy. Require the final proposed head to be checked after changes. An App token able to open PRs must not be treated as approval to merge, tag, release, or deploy.

## Failure and Retry Decisions

| Observation | Response |
| --- | --- |
| Producer still running, discovery rate limit, temporary network failure | Back off with a bounded retry; keep the same immutable request |
| Producer failed, required job skipped, wrong event/repository | Stop candidate promotion; inspect producer failure |
| Missing branch, unregistered workflow, unavailable App capability | Identify the exact destination/setup need; owner resolves it explicitly |
| Expired/missing artifact or evidence | Stop; request new authoritative evidence under its new identity |
| Consumer source advanced | Re-resolve sources and run fresh matching preflights |
| Exact open PR already exists | Revalidate branch/diff/provenance and reuse it |
| Conflicting/orphan branch, closed/rejected candidate, ambiguous runs | Stop for inspection; no force push or silent replacement |
| Dispatch/push/PR creation response uncertain | Inspect correlated runs, remote branch, and PR state before retrying |
| Accepted payload/gitlink already matches | Record a verified no-op and preserve existing provenance |

First-party workflows use deterministic correlation and constrained PR identities; concurrency reduces overlaps but does not alone establish idempotency. Validate existing state before every retry. Do not repeatedly dispatch a failed candidate to search for a green result while retaining the old approval. Keep failures visible, and never manufacture a Core code/metadata change just to produce a demonstration PR.

## What Remains Outside These Gates

The current implementation and a proposed independent adapter do not automatically establish physical-device safety, every application UX path, a published release, or successful delivery through every downstream repository. Report the actual runtime, cases, platforms, and skipped work. See [workflow status and sources](ci-workflow-reference.md) for the inspected implementation's limits.

Navigation:

- [Back to CI and Dependency Updates](../ci.md)
- [Previous: First-party Core Promotion](ci-core-promotion.md)
- [Next: Workflow Reference and Verified Status](ci-workflow-reference.md)
