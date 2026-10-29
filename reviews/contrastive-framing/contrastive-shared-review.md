# Shared documentation contrastive-framing review

Snapshot: `DocsNavigation` HEAD `c6eb5894ba64cb518ca51a04706ee6ff0fd3d2a6` (requested branch baseline c6eb5894). Read-only inventory; only review reports in the task workspace were written; repository sources remain untouched.

Scope: authored Markdown under `apiDocs/` including conceptual pages and classes, root `README.md` and `AGENTS.md`, and authored RMarkdown build guidance under `howtoCompileAPIDocs/`. Entire source files were considered for context. Excluded `core-architecture.md` and `layering-guide.md` because another reviewer owns their branch variants; generated HTML, API YAML, external/generated/shim/site trees, repository implementation and workflows. RMarkdown build guidance is included once as source, never duplicated from generated HTML.

Method: enumerate contrast cues (not/no/never/rather/instead/unlike/only/just/without/separate/cannot/avoid/except/versus/vs/but and em dashes), then distinguish rhetorical framing from factual constraints. One entry per source line retains the full exact quote, including multiple cues on that line. KEEP does not certify factual currency: stale CI/Sphinx statements require the separate content-maintenance phase. Code-only syntax is reviewed for context and listed in the excluded appendix; actual mode-selection and prerequisite distinctions remain semantic KEEP findings. Lines without a contrast cue and pages with zero candidates are listed in coverage; they are not silently omitted.

Results: 323 initial candidate lines; 319 semantic constraints/framing findings and 4 excluded neutral/code/search candidates. Dispositions: KEEP 298, REWRITE 19, REMOVE 2.

## Priority edits for a later authorized prose pass

- Consolidate the repeated optionality/rebuttal passages in the adding-layers guide; retain the actual Core/layer ownership and optional-composition rules.
- Replace the setup guide’s Common Mistake rebuttal with one clear startup-versus-session decision rule. Retain setup dependencies and the automatic StartStim/Ready timing caveat.
- Make cross-language API guidance concrete about the selected integration’s actual signatures and namespace.
- Keep CI trust, permissions, artifact identity, human acceptance, test-coverage and release-readiness boundaries explicit. These are genuine constraints, not removable rhetorical contrast.
- Treat Sphinx promises and stale AGENTS tooling status as accuracy follow-ups, not reasons to erase qualifications during a framing-only edit.


The initial scan count is not a count of rhetorical contrasts. Source IDs remain stable for consolidation; gaps refer to the excluded-candidate appendix. Technical restrictions are retained even when they contain contrast words. Questions below have been refined against the actual operation, evidence or ownership distinction.


## Semantic findings

### 1. DocsNavigation/apiDocs/advanced.md:24 - REWRITE

> - If you need architecture and config guidance rather than protocol details, go to [Concepts](concepts.md).

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: The destination already identifies the content; express the navigation positively.

Optional rewrite/scope: For architecture and configuration guidance, go to Concepts.

### 2. DocsNavigation/apiDocs/ci.md:5 - KEEP

> WSS updates move through Core, an integration library, and an application. Each repository accepts a specific dependency revision through tests and a reviewable pull request. A notification starts evaluation; it does not authorize merging or releasing.

Reader question: What does a dependency notification authorize, and which update path fits my repository? Section: CI and Dependency Updates.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 3. DocsNavigation/apiDocs/ci.md:11 - KEEP

> | Your own integration library, consuming public Core | [Independent integration CI](conceptual/ci-independent-integrations.md) | Read public upstream evidence; write only your own repository |

Reader question: What does a dependency notification authorize, and which update path fits my repository? Section: Choose the Repository You Control.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 4. DocsNavigation/apiDocs/ci.md:15 - KEEP

> The independent paths describe a **proposed downstream-owned monitoring adapter**. The coordinated first-party path describes workflow code already present in the WSS repositories. This documentation does not install a monitor or grant access to the WSS GitHub App.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 5. DocsNavigation/apiDocs/ci.md:23 - KEEP

> Passing an integration preflight does not prove an application works. Passing an application preflight does not publish a release. Identical accepted dependency bytes can produce a verified no-op, with no PR.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 6. DocsNavigation/apiDocs/ci.md:30 - KEEP

> The implementation snapshot was inspected on **2026-10-24**. Workflow definitions, branch existence, credentials, and successful end-to-end execution are separate facts; see the dated status before relying on automatic promotion.

Reader question: Which credentials may this actor use, and when must they be isolated?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 7. DocsNavigation/apiDocs/conceptual/api-reference.md:21 - REWRITE

> Use the public namespaces and signatures declared by the selected integration. A documentation grouping label does not itself declare a production namespace. Keep the source revision and bundled dependency distribution together when comparing APIs across repositories.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Genuine API distinction, but the grouping/production-namespace wording is abstract for a cross-language reader.

Optional rewrite/scope: Use the namespace and signatures declared by the selected integration; documentation group names are navigation labels.

### 8. DocsNavigation/apiDocs/conceptual/api-reference.md:23 - KEEP

> See [Choosing a Runtime](integration-library-runtime-selection.md) for host requirements and [Verify a Developer Checkout](developer-verification.md) for reproducible software-only validation.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 9. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:5 - KEEP

> Use this path when you maintain an application and consume an integration library. Your update boundary is the library's public release, package, or exact repository commit. You do not need write access to Core or to the integration repository.

Reader question: Which wrapper revision may my application accept, and what evidence or permissions does that require? Section: Application Dependency Updates.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 10. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:17 - KEEP

> A branch name or floating package range is discovery configuration, not an accepted pin. An application consuming a wrapper should normally advance the wrapper dependency rather than independently replace hidden Core DLLs underneath it.

Reader question: Should this update select the wrapper pin or replace its internal Core payload?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 11. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:21 - KEEP

> 1. Poll your chosen library release channel or allowed branch using the [independent monitoring pattern](ci-independent-integrations.md). A Core push is not automatically an application update: the wrapper may still be validating or may reject it.

Reader question: Is branch discovery an accepted immutable pin or merely an update signal?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 12. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:23 - KEEP

> 3. Check out your application at an immutable source revision and stage only the proposed library change. Verify the resulting submodule/package/binary contents. Use fresh build outputs to prevent committed or cached dependencies masking failures.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 13. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:24 - KEEP

> 4. Run application construction/lifecycle tests, the integration's applicable conformance tests, and application-specific regression tests. Check deployment output contains the intended dependency. Emulation does not substitute for required runtime or device testing.

Reader question: Which wrapper revision may my application accept, and what evidence or permissions does that require? Section: From Library Update to Application PR.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 14. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:25 - KEEP

> 5. Only after successful validation, open or reuse a deterministic application dependency PR. Include the old/new library pin, upstream evidence, application test evidence, and coverage limits. Normal PR CI checks the committed candidate; a human decides whether to merge.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 15. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:27 - KEEP

> Use a read-only validation job and a separately gated application-only writer. Preserve branch protection, required review, and check requirements. If an upstream change requires source adaptation, make that work explicit and rerun tests; do not hide unrelated code changes in a gitlink-only automation PR.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 16. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:37 - KEEP

> The shared suites exercise real production command paths and Core emulation. Unity's automated .NET GUI path covers diagnostic forwarding, not scenes or UI managers. Python CLI conformance covers its production controller factory and Python wrapper, not interactive parsing. Add application-specific tests for the behavior your users depend on.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 17. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:43 - KEEP

> Unity GUI and Python CLI promotion then run a **new preflight against the actual merged wrapper and current application source** before authoring a gitlink-only PR. The inspected C# CLI promotion instead validates the original paired approval, tested sources, actual merged Integration and payload before its gitlink PR; do not assume it implements that additional fresh post-merge preflight.

Reader question: Does this preflight test the exact application/wrapper revision being accepted?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 18. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:45 - KEEP

> An independent app cannot claim the first-party dual approval merely because upstream CI is green. It can use upstream evidence as one input and issue its own source-bound application acceptance record. You control only the guarantees tested in your repository.

Reader question: What authority does this approval grant, and may it be reused for another candidate?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 19. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:49 - KEEP

> Key requests by the immutable library revision and relevant candidate identity. If the exact pin is already accepted, report a no-op. Reuse an open matching PR only after checking its base/head, diff, provenance, and tested source. Stop for conflicting state, stale sources, a closed/rejected PR, or an uncertain prior write. Refresh validation when application code or the library revision changes.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 20. DocsNavigation/apiDocs/conceptual/ci-application-updates.md:51 - KEEP

> Do not automatically release your application after a dependency merge. Release packaging, manual runtime checks, and deployment remain separate policies. See [Evidence, permissions, and operations](ci-evidence-and-operations.md) for retention and recovery.

Reader question: How long will the exact archive/evidence remain available for promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 21. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:7 - KEEP

> ## Producer Validation and Release Are Separate

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 22. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:11 - KEEP

> The required validation includes **Core conformance**, **Build and validate artifacts**, **BLE consumer (Linux/Windows)**, and **Serial consumer** and **Core consumer** on Linux, Windows, and macOS. Packaging builds production native inputs; consumer jobs download and verify the same ZIP rather than repackaging their own substitute. Emulated tests and dependency/native activation checks do not establish stimulation over real hardware. Missing-controller probes may explicitly remain unexecuted; read actual job reports rather than equating a green workflow with every hardware path running.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 23. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:13 - KEEP

> For a canonical successful Core `dev` push, **Start routine downstream validation** hands the exact producer tuple to C# Integration. That is the eleventh required producer job checked by the separate Unity/Python handoff validators. C# waits for the entire producer run to finish successfully before accepting it.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 24. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:15 - KEEP

> `propagate-unity.yml` and `propagate-python.yml` are separate default-branch `workflow_run` completion handlers. They require a successful `artifacts.yml` push to canonical Core `dev`, validate dev ancestry and all required producer jobs, select the unique unexpired artifact and verify its archive checksum. They also check destination workflow readiness and prior correlated requests before obtaining a target-scoped dispatch token. Their failure is separate from the original producer result.

Reader question: Does this checksum establish trusted provenance or just payload consistency?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 25. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:17 - KEEP

> PR builds, failed producers, fork producers, tags, and manual producers do not automatically take those routine Unity/Python paths. Manual candidate resolution and routine automatic eligibility are separate policies.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 26. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:19 - KEEP

> Only the gated `v*` tag-push publication job creates the GitHub release from tested archive/checksum assets. A successful `dev` build does not publish a release; merging a downstream dependency PR does not release an application.

Reader question: Does this checksum establish trusted provenance or just payload consistency?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 27. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:23 - KEEP

> The handoff carries the producer repository, full commit, run ID, artifact ID, archive name, and SHA-256. Consumers revalidate it; a dispatch payload alone is not authority.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 28. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:29 - KEEP

> 3. Execute **both** integration and application candidate preflights against the same producer and exact sources. Reuse only validated matching correlated evidence.

Reader question: Does this preflight test the exact application/wrapper revision being accepted?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 29. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:31 - KEEP

> 5. Only then obtain integration-only contents/PR write credentials and create or reuse a constrained dependency PR targeting `dev`.

Reader question: Which credentials may this actor use, and when must they be isolated?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 30. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:34 - KEEP

> The C# approval is schema 2 with `candidate`, `integration.source_sha/preflight_run_id`, and `cli.source_sha/preflight_run_id`. Unity and Python have their own approval schemas and markers, including their source/fixture/payload evidence. Their records are not interchangeable. Never copy a historical bootstrap approval into a new candidate request.

Reader question: Does this preflight test the exact application/wrapper revision being accepted?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 31. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:36 - KEEP

> Identical payload bytes/projection produce a verified no-op rather than a metadata-only dependency PR. Current committed provenance is preserved. C# retains the newly verified no-op approval in `wss-automatic-approval`; Unity/Python can exit at their earlier validated identical-payload check, before dispatching new preflights. A no-op proves that path's comparison, not a changed-payload promotion chain.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 32. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:40 - KEEP

> Unity and Python pin the C# fixture at `7145e2307f997f703748530bf74311ef94455e98` and verify its configured SHA-256. Only the controller factory/type alias is adapted. The ten canonical cases and eighteen `Assert.That` source expressions, values, finite waits, evaluator calls, and grouped assertions remain unchanged.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 33. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:42 - KEEP

> The cases cover initialization, direct analog stimulation, six normalized scenarios, runtime event editing/resumption, and stopping without unintended resumed streaming. Core's emulator/evaluator owns the protocol/state expectations. Eighteen is a count of source expressions; parameterized execution and setup can yield different runtime assertion counts.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 34. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:44 - KEEP

> | Runtime path | What actual acceptance demonstrates | What it does not demonstrate |

Reader question: What do producer, candidate and application checks establish before promotion or publication? Section: Canonical Conformance Across Languages.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 35. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:50 - KEEP

> Unity automated evidence explicitly records `execution: dotnet` and `unity_executed: false`. Python records its Python execution contract. Manual Unity EditMode evidence is separate and must identify its exact source/runtime. Neither an artifact name containing `unity` nor a local Editor result changes what hosted .NET evidence proves.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 36. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:54 - KEEP

> The integration dispatcher uses a merged, same-repository canonical bot dependency PR targeting `dev`. Its `pull_request_target: closed` handler runs trusted base tooling rather than executing the caller-nominated PR source with privileges. It validates unique approval markers, deterministic branch identity, the allowed dependency-only diff, the actual merge and committed payload, and successful normal PR CI for the final reviewed head.

Reader question: Which trusted source may execute with privileged PR-event credentials?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 37. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:56 - KEEP

> For Unity/Python, the application promotion revalidates the original paired evidence and the actual merged wrapper, then executes another candidate preflight against that wrapper merge and current application. A successful exact match permits an application-only contents/PR token and a gitlink-only PR. C# validates its original paired proof, source restrictions, reviewed Integration merge, and payload before its CLI gitlink PR. Normal application PR CI and a human merge remain required in every path.

Reader question: Does this preflight test the exact application/wrapper revision being accepted?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 38. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:58 - KEEP

> Normal CI names are deliberately consistent: **WSS integration compatibility / Integration behavioral tests**, **WSS downloaded-artifact preflight / Preflight behavioral tests**, and **CLI CI / CLI general conformance**; Unity GUI uses **GUI CI / GUI general conformance**. These names are acceptance surfaces, not interchangeable evidence. Inspect the repository-specific workflow and runtime record.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 39. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:62 - KEEP

> Existing approved `WSS_GITHUB_APP_ID` and `WSS_GITHUB_APP_PRIVATE_KEY` configuration is required for coordinated promotion. Core's token has Actions-write only for the target integration; consumer evidence tokens are read-only; preflight tokens write Actions only on necessary consumers; authoring tokens write contents/PRs only on the destination after validation. Default `GITHUB_TOKEN` permissions remain read-only except the release publication job's required contents write.

Reader question: Does the repository token grant the cross-repository authority required here?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 40. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:64 - KEEP

> Normal Unity/Python conformance uses public read-only checkouts without an App secret. A maintainer's personal ability to read repositories does not establish that the WSS App is installed or permitted on them. Workflow code cannot create installation access or organization-secret grants. An owner must resolve a missing approved capability explicitly.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 41. DocsNavigation/apiDocs/conceptual/ci-core-promotion.md:66 - KEEP

> Default-branch workflow registration, target `dev` branch availability, approved baselines, artifact access, and configured credentials are separate prerequisites. The **2026-10-24** readiness follow-up restored the Python target `dev` branches and verified fresh normal/candidate conformance plus an identical-payload promotion no-op. Changed-candidate App dispatch/write and end-to-end propagation remain untested. See [the dated source/status table](ci-workflow-reference.md), including pending Unity trigger fixes and repository protection status, before attempting promotion. This documentation does not change branches or permissions, or fabricate an upstream push to trigger a demonstration.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 42. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:9 - KEEP

> Keep producer repository, full commit, workflow path/event, run and attempt context, artifact ID, archive filename/hash, consumer source SHAs, test-run IDs, runtime, fixture identity, and payload/projection hashes together. Where an implementation's JSON omits a field, retain the corresponding authoritative run metadata rather than inventing an accepted schema extension.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 43. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:11 - KEEP

> Verify that evidence refers to the expected workflow, exact source, latest applicable execution, and actual successful required jobs. Validate real test XML, expected case identities, zero failures/skips, and log/XML hashes where required. An unrelated green job, manually edited PR body, or matching artifact name is not enough. Source changes affecting tests, references, behavior, or workflow trust require fresh evidence; use each validator's narrow source-difference rules rather than assuming all changed files are harmless.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 44. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:13 - KEEP

> Do not fetch executable promotion tooling from a caller-nominated commit. Use reviewed trusted tooling, treat dispatch JSON/PR markers/artifacts as untrusted inputs, constrain URLs/repositories and archive paths, and validate before using them. Pin third-party actions and adopted reusable workflows to reviewed full commit SHAs in your own deployment, with a deliberate update process. Existing WSS YAML still uses some moving version/branch references; this is guidance for adoption, not a claim that every current workflow is SHA-pinned. [GitHub secure use guidance](https://docs.github.com/en/actions/reference/security/secure-use)

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 45. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:21 - KEEP

> | Run candidate tests | Read-only source/dependency access, isolated from authoring credentials | Consumer repository |

Reader question: Which credentials may this actor use, and when must they be isolated?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 46. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:26 - KEEP

> An independent adopter does not receive WSS App credentials or upstream dispatch permission. Do not broaden an account token merely because a first-party script expects it. Keep private keys in approved secret storage, never workflow examples, artifacts, PR bodies, or logs. Credentials must be unavailable to untrusted candidate code; use separate jobs and validate the transition to authoring.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 47. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:28 - KEEP

> Repository `GITHUB_TOKEN` permissions are repository-scoped; permission declarations do not grant cross-repository authority. Also verify how the chosen authoring token triggers checks. Current GitHub documentation describes exceptions and approval requirements for token-created PR events; App tokens can have different behavior. A PR existing is not evidence that its required checks ran. [Triggering workflows](https://docs.github.com/actions/using-workflows/triggering-a-workflow)

Reader question: Does the repository token grant the cross-repository authority required here?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 48. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:32 - KEEP

> Public source visibility does not guarantee anonymous access to Actions archives. GitHub documents signed-in repository read access for artifact download. Cross-repository API/CLI retrieval must use credentials authorized for the requested resource. Artifacts and logs can expire or be deleted. [Downloading workflow artifacts](https://docs.github.com/actions/how-tos/manage-workflow-runs/download-workflow-artifacts)

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 49. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:36 - KEEP

> | Core `universal-release-assets` | No explicit duration; effective repository policy applies |

Reader question: What evidence, authority and retry conditions make this automation action valid? Section: Artifact Availability and Retention.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 50. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:39 - KEEP

> | C# Integration candidate provenance and `wss-automatic-approval` | No explicit duration; repository policy applies |

Reader question: What authority does this approval grant, and may it be reused for another candidate?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 51. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:42 - KEEP

> Do not assume defaults guarantee a particular retention period. The inspected C# resolver's release-tag mode resolves an exact successful tag producer, then downloads its **Actions** archive. It does not download manually attached release assets as a fallback. A published release therefore does not make this resolver independent of Actions artifact retention. A downstream release-assets adapter is separate work and needs its own producer/authenticity policy.

Reader question: How long will the exact archive/evidence remain available for promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 52. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:44 - KEEP

> Store durable dependency provenance with accepted dependency state, plus the audit records required by your organization. Historical bootstrap evidence in the C# repository is narrowly allowlisted by its validator; it is not permission to bypass missing evidence for future candidates. If proof expires before promotion, obtain a new explicitly identified authoritative candidate/preflight or stop for maintainer resolution. Do not substitute a rebuilt archive under the old run/artifact/hash identity.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 53. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:48 - KEEP

> Configure the receiving repository to require relevant normal PR checks and review, and protect CI/promotion tooling changes with appropriate ownership. Ensure automation cannot bypass the intended merge gates. This is an adoption requirement; repository rulesets were not audited or changed for this documentation.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 54. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:50 - KEEP

> Use stable acceptance checks that fail when prerequisites are failed or skipped. Include any additional application/runtime/platform checks required by your support policy. Require the final proposed head to be checked after changes. An App token able to open PRs must not be treated as approval to merge, tag, release, or deploy.

Reader question: What authority does this approval grant, and may it be reused for another candidate?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 55. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:62 - KEEP

> | Conflicting/orphan branch, closed/rejected candidate, ambiguous runs | Stop for inspection; no force push or silent replacement |

Reader question: Is branch discovery an accepted immutable pin or merely an update signal?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 56. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:64 - KEEP

> | Accepted payload/gitlink already matches | Record a verified no-op and preserve existing provenance |

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 57. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:66 - KEEP

> First-party workflows use deterministic correlation and constrained PR identities; concurrency reduces overlaps but does not alone establish idempotency. Validate existing state before every retry. Do not repeatedly dispatch a failed candidate to search for a green result while retaining the old approval. Keep failures visible, and never manufacture a Core code/metadata change just to produce a demonstration PR.

Reader question: What authority does this approval grant, and may it be reused for another candidate?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 58. DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md:70 - KEEP

> The current implementation and a proposed independent adapter do not automatically establish physical-device safety, every application UX path, a published release, or successful delivery through every downstream repository. Report the actual runtime, cases, platforms, and skipped work. See [workflow status and sources](ci-workflow-reference.md) for the inspected implementation's limits.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 59. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:5 - KEEP

> Use this guide when you own an integration repository and consume public WSSCore, but cannot dispatch workflows or write branches in the WSS repositories. You can detect upstream changes, verify a candidate, test your integration, and propose an update entirely within your own repository.

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 60. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:7 - KEEP

> **Status:** the downstream monitor described here is an adapter design to implement in your repository. WSS's existing first-party App cascade is not a public subscription service.

Reader question: How can my own downstream repository detect and validate updates with its own credentials? Section: Independent Integration CI.

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 61. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:16 - KEEP

> Start with releases if your users need stable updates. Development monitoring provides earlier feedback but may encounter failed builds or expired artifacts. Explicitly decide whether to test every eligible revision or coalesce to the newest one. Record skipped/superseded candidates rather than silently claiming they were tested.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 62. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:18 - KEEP

> A `push` or `release` trigger in your repository observes your repository's events. GitHub Watch notifications do not connect upstream pushes to your Actions workflow. A downstream `workflow_run` is also not a cross-repository subscription. Use scheduled polling and a manual retry entry point, or an explicitly authorized external event service. `repository_dispatch` requires a sender authorized to dispatch to the receiving repository; it does not confer permission on the upstream project. See [GitHub event semantics](https://docs.github.com/en/actions/reference/workflows-and-actions/events-that-trigger-workflows).

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 63. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:22 - KEEP

> This illustrative workflow is **discovery only**. It queries public release metadata, writes no branches, downloads no executable dependencies, and creates no PR. Place it on your default branch. GitHub schedules can be delayed; do not treat the interval as a delivery guarantee.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 64. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:52 - KEEP

> If no release is published, authentication fails, or the API rate limit is reached, this example fails visibly. A production adapter should classify these outcomes and back off; it must not substitute a random older download. The release's `target_commitish` is not sufficient evidence of an immutable tag commit.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 65. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:58 - KEEP

> 1. Read your committed dependency record and candidate policy. Resolve one upstream full commit, producer workflow/run, artifact ID, archive filename, and SHA-256. If using release assets, additionally retain release and asset IDs and resolve the tag; never trust a moving `latest` URL as a pin.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 66. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:60 - KEEP

> 3. Download to a fresh directory. Validate the ZIP inventory, safe extraction paths, checksum, assembly/dependency identities, and platform layout. A checksum obtained from the same compromised source proves consistency, not independent authenticity; bind it to your trusted producer policy.

Reader question: Does this checksum establish trusted provenance or just payload consistency?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 67. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:62 - KEEP

> 5. Record tested source, candidate identity, test results, and coverage limits. A failed or skipped required test stops promotion. Do not replace expected values merely to accept an upstream change.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 68. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:63 - KEEP

> 6. If accepted dependency bytes already match, record a no-op without rewriting provenance. Otherwise construct a dependency-only diff on a deterministic branch keyed by candidate identity. Check existing PRs/branches before writing; reuse an exact match, stop on conflicts or closed/rejected candidates, and never force-push over unrelated work.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 69. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:64 - KEEP

> 7. Use a downstream-only authoring credential to create the PR after validation. Include old/new pins, producer and test links, archive/hash identity, and failures or untested platforms. Run normal PR CI against the committed proposal and require human review.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 70. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:66 - KEEP

> The candidate record and workflow evidence are data, not executable instructions. Test upstream binaries in an isolated job without PR-writing secrets. A separate authoring phase must verify the evidence came from the expected trusted workflow and immutable source before acquiring write credentials. Do not run candidate checkout scripts in a privileged `pull_request_target` job.

Reader question: Which trusted source may execute with privileged PR-event credentials?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 71. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:72 - KEEP

> They are not unrestricted reusable workflows: the resolver allowlists WSSCore, the YAML mints WSS App tokens, and promotion requires evidence from the coordinated CLI. Replace authentication with public reads or a credential you own with necessary upstream read access. Implement your own source-bound approval policy if you do not control a paired application; do not manufacture first-party CLI approval.

Reader question: What authority does this approval grant, and may it be reused for another candidate?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 72. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:74 - KEEP

> Unity and Python provide pinned canonical-fixture validation with runtime-specific evidence. Their reusable workflows and scripts also encode repository roles and layouts. Port the production-path tests and integrity checks deliberately; changing only a repository name does not make a new integration conformant.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 73. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:88 - KEEP

> This excerpt does not implement discovery, verification, fallback removal, PR creation, or all first-party gates. Complete those preceding steps before enabling automation.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 74. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:92 - KEEP

> You need no upstream write token. Keep metadata/download permissions separate from downstream `contents: write` and `pull-requests: write`. Use your own narrowly installed App or appropriately scoped downstream credential if automatic PR CI requires it. If choosing the repository `GITHUB_TOKEN`, verify the resulting PR-check behavior and repository policy; do not assume authoring a PR guarantees checks will execute unattended. See [GitHub workflow triggering](https://docs.github.com/actions/using-workflows/triggering-a-workflow).

Reader question: Does the repository token grant the cross-repository authority required here?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 75. DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md:94 - KEEP

> For artifact expiry, retries, branch protection, and action pinning, follow [Evidence, permissions, and operations](ci-evidence-and-operations.md). An upstream outage should delay your update, not silently change the accepted dependency.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 76. DocsNavigation/apiDocs/conceptual/ci-workflow-reference.md:16 - KEEP

> Keep workflow filenames, acceptance contexts, permissions and source refs tied to the reviewed revision. Code presence alone does not prove installed App scope, branch protection or successful changed-candidate propagation. Preserve source/runtime identity with test results, and distinguish hosted .NET checks from native Unity or MATLAB execution.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Identifies a permission, trust, evidence, scope, eligibility or failure boundary; removing the qualification would overstate automation guarantees.

### 77. DocsNavigation/apiDocs/conceptual/config-files-reference.md:30 - KEEP

>   - These are on-wire receiver addresses, not serial port names or BLE device identifiers. Use the configuration schema supplied with the application's pinned dependency distribution.

Reader question: What does this configuration field identify or control? Section: `stimConfig.json`.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 78. DocsNavigation/apiDocs/conceptual/developer-verification.md:5 - KEEP

> Start with the setup and software-only example in the selected [repository README](repository-and-kit-links.md). An application README owns its run commands and configuration paths; an integration README owns installation and API examples. This guide describes the shared verification contract.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 79. DocsNavigation/apiDocs/conceptual/developer-verification.md:9 - KEEP

> Clone applications with their pinned submodules, using `git clone --recurse-submodules` or `git submodule update --init --recursive`. For a repeatable checkout, avoid `git submodule update --remote`, which selects a different dependency revision.

Reader question: What can this checkout test prove, and who owns ticking and cleanup? Section: Preserve Dependency Identity.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 80. DocsNavigation/apiDocs/conceptual/developer-verification.md:11 - KEEP

> Keep each candidate in a separate checkout. Record application and integration commits, gitlinks, and distribution hashes. Source commits, CI artifacts and tagged releases identify different inputs; select them explicitly. Keep the complete managed/native dependency distribution together instead of overlaying individual DLLs.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 81. DocsNavigation/apiDocs/conceptual/developer-verification.md:13 - KEEP

> ## Start Without Hardware

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 82. DocsNavigation/apiDocs/conceptual/developer-verification.md:17 - KEEP

> A useful software-only check should establish that:

Reader question: What can this checkout test prove, and who owns ticking and cleanup? Section: Start Without Hardware.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 83. DocsNavigation/apiDocs/conceptual/developer-verification.md:25 - KEEP

> Simulator and conformance results cover the behaviors they exercise. They do not establish device behavior, Unity Editor compatibility, or an application's coverage merely because a related wrapper passed.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 84. DocsNavigation/apiDocs/conceptual/developer-verification.md:29 - KEEP

> C# and Python controllers own their background ticking. Unity advances through its component lifecycle. An application composing Core/layers directly supplies the tick loop, waits for a usable state, and performs shutdown. The following helper assumes the caller already selected and configured the Core/layer stack; it is not a standalone application.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 85. DocsNavigation/apiDocs/conceptual/developer-verification.md:31 - KEEP

> Use an emulator for software-only checks: initialization itself queues device setup and `StartStim`.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 86. DocsNavigation/apiDocs/conceptual/developer-verification.md:35 - KEEP

> `Shutdown()` retains the transport for reinitialization. The caller owns final `Dispose()`, after which the Core instance cannot be reused.

Reader question: What can this checkout test prove, and who owns ticking and cleanup? Section: Own the Lifecycle Once.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 87. DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md:17 - KEEP

> It only lists version-gated behavior that is explicitly visible in the Core source.

Reader question: Which commands/features does the selected firmware actually support? Section: What This Page Covers.

Reason: Firmware-dependent support or fallback behavior is a real protocol constraint; preserve the distinction.

### 88. DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md:42 - KEEP

> | `H03` | Yes | Yes | Not a current gate | No | No | baseline/default documented behavior |

Reader question: Which commands/features does the selected firmware actually support? Section: Compatibility Matrix.

Reason: Firmware-dependent support or fallback behavior is a real protocol constraint; preserve the distinction.

### 89. DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md:43 - KEEP

> | `J03` | Yes | No | Not a current gate | Yes | Yes | adds LED-related behavior and ModuleQuery support |

Reader question: Which commands/features does the selected firmware actually support? Section: Compatibility Matrix.

Reason: Firmware-dependent support or fallback behavior is a real protocol constraint; preserve the distinction.

### 90. DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md:49 - KEEP

> Core registers `LEDSettings` and `ModuleQuery` in `FeatureMinimumVersions`. Unknown feature names return false from `IsFeatureAvailable(...)`. Amplitude checking is not a registered feature gate.

Reader question: Which commands/features does the selected firmware actually support? Section: `AmplitudeCheck`.

Reason: Firmware-dependent support or fallback behavior is a real protocol constraint; preserve the distinction.

### 91. DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md:56 - KEEP

>   - `CreateContactConfig(...)` accepts and sends `ledByte` only when firmware supports LED settings

Reader question: Which commands/features does the selected firmware actually support? Section: LED Settings.

Reason: Firmware-dependent support or fallback behavior is a real protocol constraint; preserve the distinction.

### 92. DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md:67 - KEEP

>   - on older firmware, no command is sent and default cached settings are seeded instead

Reader question: Which commands/features does the selected firmware actually support? Section: `ModuleQuery`.

Reason: Firmware-dependent support or fallback behavior is a real protocol constraint; preserve the distinction.

### 93. DocsNavigation/apiDocs/conceptual/integration-library-runtime-selection.md:11 - KEEP

> | BLE transport abstraction and backends | `netstandard2.0` | Deploy the matching platform backend and native assets. A portable assembly target does not make a native backend portable. |

Reader question: Which managed target, host runtime and native assets are required? Section: Choosing a Runtime for an Integration Library.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 94. DocsNavigation/apiDocs/conceptual/integration-library-runtime-selection.md:13 - KEEP

> | Unity integration | Unity project | Follow its supported Editor version and plugin import settings. A successful standalone .NET build does not establish Unity compatibility. |

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 95. DocsNavigation/apiDocs/conceptual/integration-library-runtime-selection.md:17 - KEEP

> The documentation toolchain has its own pinned SDK and DocFX versions. Those build tools do not define the application's runtime requirement.

Reader question: Which managed target, host runtime and native assets are required? Section: Choosing a Runtime for an Integration Library.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 96. DocsNavigation/apiDocs/conceptual/platform-support.md:5 - KEEP

> WSS 1.0 supports desktop workflows on Windows, Linux and macOS through the integrations below. Choose both a host runtime and a transport: support in Core does not mean every wrapper exposes the same transport-selection API.

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 97. DocsNavigation/apiDocs/conceptual/platform-support.md:17 - KEEP

> Use the linked README for exact runtime versions and available transport options. ROS additionally requires a supported ROS 2 installation for its host; do not infer a native Windows/macOS ROS package from the Python library's portability.

Reader question: Which host/transport combinations and platform prerequisites apply? Section: Choose a host.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 98. DocsNavigation/apiDocs/conceptual/platform-support.md:21 - KEEP

> - **Simulator/test mode:** exercises software setup and lifecycle without serial or BLE devices. Use the application README's explicit simulator option before selecting hardware.

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 99. DocsNavigation/apiDocs/conceptual/platform-support.md:25 - KEEP

> macOS BLE uses the system Bluetooth stack. Desktop permissions, adapter availability and runtime packaging are separate from the shared protocol. Transport availability is not a statement of device certification or equivalent behavior across arbitrary adapters.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 100. DocsNavigation/apiDocs/conceptual/publishing-site.md:18 - KEEP

> The command refuses to overwrite a populated directory and never deletes the live site.

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Build the same site on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 101. DocsNavigation/apiDocs/conceptual/publishing-site.md:24 - KEEP

> It does not regenerate external API metadata or rerender RMarkdown. Those remain

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Build the same site on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 102. DocsNavigation/apiDocs/conceptual/publishing-site.md:25 - KEEP

> separate release tasks with additional pinned upstream checkouts and dependencies.

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Build the same site on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 103. DocsNavigation/apiDocs/conceptual/publishing-site.md:30 - KEEP

> same-page links are repaired only from uniquely matching heading labels.

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Build the same site on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 104. DocsNavigation/apiDocs/conceptual/publishing-site.md:34 - KEEP

> and footer routes back to the hub, without requiring JavaScript. The report lists

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Build the same site on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 105. DocsNavigation/apiDocs/conceptual/publishing-site.md:37 - KEEP

> API body-link freshness remains part of the separate API-reference review.

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Build the same site on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 106. DocsNavigation/apiDocs/conceptual/publishing-site.md:42 - KEEP

> 2022 and Ubuntu 24.04. Pull requests only upload `complete-site-<OS>` review artifacts

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 107. DocsNavigation/apiDocs/conceptual/publishing-site.md:43 - KEEP

> and their reports. They cannot run the deployment job or upload the main-only Pages

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Review builds and deployment permissions.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 108. DocsNavigation/apiDocs/conceptual/publishing-site.md:44 - KEEP

> artifact. The existing hub-only workflow remains a smaller conceptual preview check.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 109. DocsNavigation/apiDocs/conceptual/publishing-site.md:46 - KEEP

> Build jobs have only `contents: read`. The separate deployment job has only

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Review builds and deployment permissions.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 110. DocsNavigation/apiDocs/conceptual/publishing-site.md:47 - KEEP

> `pages: write` and `id-token: write`, uses the `github-pages` environment, and only

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Review builds and deployment permissions.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 111. DocsNavigation/apiDocs/conceptual/publishing-site.md:50 - KEEP

> in legacy branch mode. No custom token or repository secret is needed.

Reader question: Is branch discovery an accepted immutable pin or merely an update signal?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 112. DocsNavigation/apiDocs/conceptual/publishing-site.md:54 - KEEP

> Merge the workflow only after both complete-site checks pass and its artifact is

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 113. DocsNavigation/apiDocs/conceptual/publishing-site.md:58 - KEEP

> This switch is separate from code review and requires explicit authorization.

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Initial activation and later releases.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 114. DocsNavigation/apiDocs/conceptual/publishing-site.md:63 - KEEP

> only validates and packages a review build. Never dispatch a deployment from a PR

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 115. DocsNavigation/apiDocs/conceptual/publishing-site.md:64 - KEEP

> branch or relax the main-only job gate.

Reader question: Is branch discovery an accepted immutable pin or merely an update signal?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 116. DocsNavigation/apiDocs/conceptual/publishing-site.md:66 - KEEP

> The staged artifact deliberately omits `CNAME`; it does not assume a custom domain.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 117. DocsNavigation/apiDocs/conceptual/publishing-site.md:68 - KEEP

> HTTPS changes as separate administration decisions. The legacy setup previously

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 118. DocsNavigation/apiDocs/conceptual/publishing-site.md:75 - KEEP

> a successful upload alone is not proof of correct browser rendering.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 119. DocsNavigation/apiDocs/conceptual/publishing-site.md:81 - KEEP

> complete-site command consumes this TOC without generating a reduced replacement,

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Navigation and API maintenance.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 120. DocsNavigation/apiDocs/conceptual/publishing-site.md:86 - KEEP

> repositories is a separate full-build operation. Review its resulting API changes

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Navigation and API maintenance.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 121. DocsNavigation/apiDocs/conceptual/publishing-site.md:87 - KEEP

> before publishing, rather than silently dropping old API sections or standalone content.

Reader question: What does this build preserve or regenerate, and when may it deploy? Section: Navigation and API maintenance.

Reason: This is an actual preservation or composition-ownership requirement. Retain the named boundary so a reader does not choose an invalid operation.


### 122. DocsNavigation/apiDocs/conceptual/release-readiness.md:5 - KEEP

> This is the single maintainer record for remaining release verification and publication decisions. User onboarding describes the intended finished 1.0 release, including MATLAB and Core macOS BLE. Do not publish these documentation changes until maintainers review the items below against the final source and distribution revisions.

Reader question: What evidence is still required before accepting the release? Section: Release 1.0 readiness.

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 123. DocsNavigation/apiDocs/conceptual/release-readiness.md:10 - KEEP

> - [ ] Complete native MATLAB setup/CLI acceptance and Core macOS BLE integration, then propagate the complete matching payload to each consumer that exposes that transport. MATLAB's current facade exposes serial/test/conformance options, not a BLE selector; do not infer MATLAB BLE from Core BLE.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 124. DocsNavigation/apiDocs/conceptual/release-readiness.md:11 - KEEP

> - [ ] Confirm host/OS/runtime combinations promised by each README against final artifacts. Unity's baseline BLE selection accepts Windows only; validate any additional Unity BLE claim after backend integration.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 125. DocsNavigation/apiDocs/conceptual/release-readiness.md:12 - KEEP

> - [ ] Promote the Python runtime loader and runtime-compatible complete payload together, then run the documented CoreCLR startup on Windows/Linux/macOS. Baseline Windows loader chooses .NET Framework before honoring CoreCLR; forcing CoreCLR with its old payload fails loading System.IO.Ports.Parity. The passing baseline simulator result is .NET Framework evidence only.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 126. DocsNavigation/apiDocs/conceptual/release-readiness.md:16 - KEEP

> - [ ] Build and run C# library/CLI documented simulator commands with final complete dependencies. Local execution was blocked by Windows Application Control (0x800711C7); static inspection does not satisfy runtime acceptance.

Reader question: What evidence is still required before accepting the release? Section: Publication gate.

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 127. DocsNavigation/apiDocs/conceptual/release-readiness.md:20 - KEEP

> - [ ] Regenerate Core/C#/Unity API metadata from recorded final revisions. Current committed C# metadata uses HFI.Wss while source uses Wss.CSharpImplementation; reconcile all API links, provenance and outputs before publication. Preserve the prior API refresh audit worktree as evidence rather than treating it as the final build.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 128. DocsNavigation/apiDocs/conceptual/release-readiness.md:21 - KEEP

> - [ ] Run full DocFX/API and RMarkdown builds, then rendered HTML link/fragment/navigation checks, including every repository README link. The dependency-free catalog tool restored offline here but its execution was blocked by Application Control; no passing site build is claimed.

Reader question: What evidence is still required before accepting the release? Section: Documentation and hosting.

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 129. DocsNavigation/apiDocs/conceptual/release-readiness.md:22 - KEEP

> - [ ] Confirm the docs manifest's C# path src/Wss.CSharpImplementation.csproj and each external repository/source root. Verify the Sphinx path separately before enabling Python API publication; it is not required to follow the Python README onboarding.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 130. DocsNavigation/apiDocs/conceptual/release-readiness.md:23 - KEEP

> - [ ] Resolve hosting with maintainers: onboarding links use https://cwru-non-academic.github.io/WSS_Documentation/; the existing CNAME points at wss.cosmiic.org. Verify deployed base path and redirects without changing hosting as part of this local draft.

Reader question: What evidence is still required before accepting the release? Section: Documentation and hosting.

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 131. DocsNavigation/apiDocs/conceptual/release-readiness.md:24 - KEEP

> - [ ] Verify all current academic repository and README URLs on final target branches. Organization/name migration is a separate change; retain the current catalog owner.

Reader question: Is branch discovery an accepted immutable pin or merely an update signal?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 132. DocsNavigation/apiDocs/conceptual/release-readiness.md:29 - KEEP

> Record executed, statically checked and unavailable checks separately. Simulator results do not certify hardware or prove native MATLAB/Unity execution. Historical CI evidence below is a prior inspection, not release acceptance; reconcile it with final workflows before publication.

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 133. DocsNavigation/apiDocs/conceptual/release-readiness.md:34 - KEEP

> - [ ] Run a genuine changed-candidate Core-to-wrapper-to-application chain and retain exact producer/candidate evidence. An identical-payload no-op does not prove propagation.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 134. DocsNavigation/apiDocs/conceptual/release-readiness.md:35 - KEEP

> - [ ] Review requested dev-to-dev/main-to-main channel routing separately with the CI implementation owner; this documentation pass changes no CI behavior.

Reader question: What evidence is still required before accepting the release? Section: CI release acceptance.

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 135. DocsNavigation/apiDocs/conceptual/release-readiness.md:39 - KEEP

> The following 2026-10-24 snapshot is retained for comparison only. Resolve its open findings in the checks above rather than using it as current operational status.

Reader question: What evidence is still required before accepting the release? Section: Preserved historical CI inspection.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 136. DocsNavigation/apiDocs/conceptual/release-readiness.md:45 - KEEP

> This is a source inspection snapshot from **2026-10-24**, not a continuous operational status report. Links below pin reviewed default-branch revisions. Routine first-party promotions target `dev`; default-branch registration and the workflow/script versions on the dispatched ref must both be ready.

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 137. DocsNavigation/apiDocs/conceptual/release-readiness.md:63 - KEEP

> | Core maintainers | [artifacts.yml](https://github.com/cwru-non-academic/WSSCore/blob/28bdded0f14773f5ef27a38a0016578703d9fca2/.github/workflows/artifacts.yml) | Producer validation, one archive/checksum, in-run C# handoff, separate tag release job |

Reader question: Does this checksum establish trusted provenance or just payload consistency?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 138. DocsNavigation/apiDocs/conceptual/release-readiness.md:64 - KEEP

> | Core maintainers | [propagate-unity.yml](https://github.com/cwru-non-academic/WSSCore/blob/28bdded0f14773f5ef27a38a0016578703d9fca2/.github/workflows/propagate-unity.yml), [propagate-python.yml](https://github.com/cwru-non-academic/WSSCore/blob/28bdded0f14773f5ef27a38a0016578703d9fca2/.github/workflows/propagate-python.yml) | Completed-producer trust checks and authorized dispatch; not a public polling adapter |

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 139. DocsNavigation/apiDocs/conceptual/release-readiness.md:65 - KEEP

> | C# integration developers | [wss-preflight.yml](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/blob/109d6629a8890158dc501c8c42c66ef3121d2664/.github/workflows/wss-preflight.yml), [wss-compatibility.yml](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib/blob/109d6629a8890158dc501c8c42c66ef3121d2664/.github/workflows/wss-compatibility.yml) | Fresh-candidate versus committed-state behavioral/platform testing; adapt authentication and project layout |

Reader question: Does this preflight test the exact application/wrapper revision being accepted?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 140. DocsNavigation/apiDocs/conceptual/release-readiness.md:73 - KEEP

> The implementation uses `promote-wss.yml` in integration repositories, `dispatch-cli-promotion.yml` for C#/Python merged-integration handoffs, `dispatch-unity-promotion.yml` for the Unity handoff, and `promote-integration.yml` in applications. These are coordinated workflows, not generic independent subscription endpoints.

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 141. DocsNavigation/apiDocs/conceptual/release-readiness.md:77 - KEEP

> GitHub reported Unity Integration PR4, Unity GUI PR3, Core handoff PR12, Python Integration PR1, Python CLI PR1, and Core Python handoff PR13 merged. The **2026-10-24 readiness follow-up** restored Python Integration `dev` at `b527733d0c3fb213a515cd5a3e7b4a0d66b461c0` and Python CLI `dev` at `22139ec606140f900068b8f5fae469aab00f86e7`; the earlier missing-branch finding is resolved. Fresh Python normal and candidate preflights each passed all ten cases. [Promotion readiness run 40058662944](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib/actions/runs/40058662944) passed as an identical-payload no-op, not a changed-dependency PR.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 142. DocsNavigation/apiDocs/conceptual/release-readiness.md:79 - KEEP

> Unity Integration [PR5](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib/pull/5) and Unity GUI [PR4](https://github.com/cwru-non-academic/GUI_Unity_WSS_Application/pull/4) are draft fixes for missing normal `main` PR triggers. Their checks passed ten cases each, but they were **not merged** in this follow-up; do not describe those trigger additions as deployed behavior.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 143. DocsNavigation/apiDocs/conceptual/release-readiness.md:81 - KEEP

> All four Unity/Python consumers have `main` and `dev`, default to `main`, and have automatic branch deletion disabled. Their readiness audit found no rulesets. Required-check/review enforcement must therefore not be inferred from passing workflow code. The exact acceptance contexts to use when configuring protection are **Integration behavioral tests** in both libraries, **GUI general conformance** in the Unity application, and **CLI general conformance** in the Python CLI. Repository protection configuration remains separate work; this documentation changes none of it.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 144. DocsNavigation/apiDocs/conceptual/release-readiness.md:83 - KEEP

> Code presence does not confirm App installation scope, inherited secret availability, successful dispatch, or changed-payload propagation. Existing Core-to-C# App use is documented in the implementation; access to Unity/Python must not be inferred from it. A verified no-op and standalone passing candidate tests do not prove a complete changed-candidate Core-to-wrapper-to-application chain. Changed-candidate App dispatch/write and such an end-to-end chain remain untested after the readiness follow-up. A genuine changed-Core validation is still future work after readiness, ROS coverage, and protection setup.

Reader question: What does an identical-payload no-op prove, and does it authorize a dependency PR?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 145. DocsNavigation/apiDocs/conceptual/release-readiness.md:85 - KEEP

> The committed Unity automation notes describe older manual Editor evidence as predating the source extraction. Any newer local EditMode result must be retained with its exact source/runtime and evaluated separately; this inspection did not independently verify it. Hosted .NET evidence must continue to report `unity_executed: false`.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 146. DocsNavigation/apiDocs/conceptual/release-readiness.md:89 - KEEP

> The verified additional application is [ROS_Python_WSS_Application](xref:repo.python-ros), package `hfi_ros_wss_driver`. Its `HFI_WSS_Python_Interface` dependency pins the older Python integration revision abbreviated `60f29a8`. Its CI/promotion pipeline remains untouched. The Python CLI evidence and dispatch target do not establish ROS coverage. Resolve and record its full dependency SHA, production-path tests, evidence contract, and authorized promotion route separately before claiming full downstream propagation.

Reader question: Who may dispatch this workflow, and what does receiving a dispatch authorize?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 147. DocsNavigation/apiDocs/conceptual/release-readiness.md:93 - KEEP

> No independent polling/release-monitor implementation is installed by these docs. Its owner must implement discovery/state tracking, credential selection, trust verification, isolated tests, evidence-bound PR authoring, retry/idempotency behavior, and receiving-repository review policy. In particular, a persistent release-assets resolver is not supplied by copying the existing Actions-artifact resolver.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Release-evidence status or acceptance boundary is material to publication; negative evidence must remain explicit.

### 148. DocsNavigation/apiDocs/conceptual/repository-links.md:38 - KEEP

> links must not open a bare `toc.html` fragment.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Adding links and copyable commands.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 149. DocsNavigation/apiDocs/conceptual/repository-links.md:44 - KEEP

> staged standalone RMarkdown pages; only generic navigation is replaced there,

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Adding links and copyable commands.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 150. DocsNavigation/apiDocs/conceptual/repository-links.md:46 - KEEP

> repaired only when its label uniquely matches an actual heading; ambiguous links

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Adding links and copyable commands.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 151. DocsNavigation/apiDocs/conceptual/repository-links.md:48 - KEEP

> sources for guide content, rather than editing generated navigation in HTML.

Reader question: Should I edit source inputs or generated output, and which files will this command regenerate?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 152. DocsNavigation/apiDocs/conceptual/repository-links.md:51 - KEEP

> directives belong only in the shared documentation inputs.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Adding links and copyable commands.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 153. DocsNavigation/apiDocs/conceptual/repository-links.md:61 - KEEP

> Give each deep destination a named entry; do not append `/blob/...` to an xref UID.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Adding links and copyable commands.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 154. DocsNavigation/apiDocs/conceptual/repository-links.md:64 - KEEP

> DocFX does not expand xrefs inside code fences. Include a generated clone snippet

Reader question: Should I edit source inputs or generated output, and which files will this command regenerate?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 155. DocsNavigation/apiDocs/conceptual/repository-links.md:65 - KEEP

> on a line by itself instead, using a path relative to the current Markdown page:

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Adding links and copyable commands.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 156. DocsNavigation/apiDocs/conceptual/repository-links.md:76 - KEEP

> The catalog provides link references, not general text/code templating.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 157. DocsNavigation/apiDocs/conceptual/repository-links.md:82 - KEEP

> hub-only preview. Thin PowerShell and Bash entrypoints call this same code; there

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 158. DocsNavigation/apiDocs/conceptual/repository-links.md:83 - KEEP

> is no second URL-resolution implementation. .NET is already required for DocFX.

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 159. DocsNavigation/apiDocs/conceptual/repository-links.md:97 - KEEP

> NuGet; subsequent builds use the restored local tool. No global DocFX installation

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Same implementation on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 160. DocsNavigation/apiDocs/conceptual/repository-links.md:98 - KEEP

> or PATH modification is required. The generator itself has no package dependencies.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Same implementation on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 161. DocsNavigation/apiDocs/conceptual/repository-links.md:99 - KEEP

> `test` without `--render` runs catalog checks without restoring DocFX.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Same implementation on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 162. DocsNavigation/apiDocs/conceptual/repository-links.md:116 - KEEP

> Generated inputs are ignored under `apiDocs/generated/repository-links/`; never

Reader question: Should I edit source inputs or generated output, and which files will this command regenerate?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 163. DocsNavigation/apiDocs/conceptual/repository-links.md:117 - KEEP

> edit them. Rebuild after changing the catalog; serving alone does not regenerate

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Same implementation on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 164. DocsNavigation/apiDocs/conceptual/repository-links.md:120 - KEEP

> They never modify the live catalog. `--docfx <executable>` can reuse an existing

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Same implementation on Windows and Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 165. DocsNavigation/apiDocs/conceptual/repository-links.md:127 - KEEP

> tool, runs the same rendered fixture tests, builds the hub-only preview and uploads

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 166. DocsNavigation/apiDocs/conceptual/repository-links.md:128 - KEEP

> one review artifact per OS. Action versions are fixed too. The workflow has read-only

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 167. DocsNavigation/apiDocs/conceptual/repository-links.md:129 - KEEP

> repository permission and does not publish a site or require deployment secrets.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: CI validation and staged releases.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 168. DocsNavigation/apiDocs/conceptual/repository-links.md:130 - KEEP

> Authoring the workflow does not mean it has run: inspect both job results after pushing.

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 169. DocsNavigation/apiDocs/conceptual/repository-links.md:132 - KEEP

> Hub-only validation excludes API metadata and imported Sphinx content, does not

Reader question: Does this build mode include Sphinx output, and what extra prerequisites apply?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 170. DocsNavigation/apiDocs/conceptual/repository-links.md:135 - KEEP

> they do not make the preview a complete release. Fixture builds treat warnings as

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 171. DocsNavigation/apiDocs/conceptual/repository-links.md:138 - KEEP

> A complete release should be added in separate stages:

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: CI validation and staged releases.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 172. DocsNavigation/apiDocs/conceptual/repository-links.md:142 - KEEP

>    Project/solution paths in the manifest are relative to those roots, not a maintainer's

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 173. DocsNavigation/apiDocs/conceptual/repository-links.md:146 - KEEP

>    Windows uses PowerShell 7. These extra tools are unnecessary for hub-only checks.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: CI validation and staged releases.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 174. DocsNavigation/apiDocs/conceptual/repository-links.md:150 - KEEP

>    destination. Publication and repository transfers are not part of this workflow.

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 175. DocsNavigation/apiDocs/conceptual/repository-links.md:152 - KEEP

> For the complete staged site and the gated deployment workflow, see [Publishing the Complete Site](publishing-site.md). The hub preview remains deliberately smaller and is never deployed by itself.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 176. DocsNavigation/apiDocs/conceptual/repository-links.md:157 - KEEP

> Published pages contain ordinary URLs. Raw GitHub Markdown does not resolve xrefs

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Which links stay literal.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 177. DocsNavigation/apiDocs/conceptual/repository-links.md:160 - KEEP

> Hosting URLs are deployment settings, separate from repository URLs.

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Which links stay literal.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 178. DocsNavigation/apiDocs/conceptual/repository-links.md:164 - KEEP

> kit placeholders, generated API source links and SourceLink data are not migrated.

Reader question: Should I edit source inputs or generated output, and which files will this command regenerate?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 179. DocsNavigation/apiDocs/conceptual/repository-links.md:168 - KEEP

> The catalog does not rewrite generated HTML, RMarkdown, imported Sphinx output,

Reader question: Does this build mode include Sphinx output, and what extra prerequisites apply?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 180. DocsNavigation/apiDocs/conceptual/repository-links.md:169 - KEEP

> navigation YAML, or Git remotes. A valid rendered URL cannot guarantee the remote

Reader question: Which source owns a link, and what does catalog/build validation actually check? Section: Which links stay literal.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 181. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:10 - REWRITE

> That distinction matters because you do not always need to modify the core just to change setup behavior.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Just minimizes or pads the instruction without adding a prerequisite or behavioral boundary.

Optional rewrite/scope: Session-specific setup changes can use the integration's exposed commands; reserve shared startup changes for Core.

### 182. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:24 - KEEP

> This is the right place to make a change only when you want to modify the default setup that should happen every time the core starts.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 183. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:30 - KEEP

> That means an integration library or application can often replace or override setup behavior without modifying the core itself, as long as it exposes the necessary methods.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 184. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:43 - KEEP

> - use integration-library or application-level setup commands when you only want to change the setup that is active during a particular workflow or session

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 185. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:56 - KEEP

> `StartStim` is already in the automatic setup queue; it is not a separate application action that must happen after the queue reaches `Ready`. Initialization alone can advance into `Started`/`Streaming`. `Ready()` is therefore not a status to wait for exclusively. The command-intent examples below explain available operations; they are not a literal list of every command sent during `NormalSetup()`.

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Essential startup behavior prevents duplicate start assumptions; retain Ready/Started qualification.

### 186. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:79 - KEEP

> Plain English: remove old programmed state so the new setup does not inherit unexpected leftover definitions from a previous run.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 187. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:148 - KEEP

>   - removes an event from a schedule when the relationship should no longer exist

Reader question: Should this setup change alter Core startup or use exposed session operations, and which command dependencies must hold? Section: 4. Add Relationships.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 188. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:173 - KEEP

>   - stops stimulation when needed; this is not part of setup creation, but it is often part of safely replacing or changing setup later

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 189. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:181 - KEEP

> - Do not edit something before it exists.

Reader question: Should this setup change alter Core startup or use exposed session operations, and which command dependencies must hold? Section: Why the Order Matters.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 190. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:182 - KEEP

> - Do not add an event to a schedule before both the event and schedule exist.

Reader question: Should this setup change alter Core startup or use exposed session operations, and which command dependencies must hold? Section: Why the Order Matters.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 191. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:183 - KEEP

> - Do not sync or start before the device has the intended setup.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 192. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:184 - KEEP

> - Do not move `StartStim(...)` earlier unless you are sure the remaining setup actions are no longer required before stimulation begins.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 193. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:191 - KEEP

> - start only after setup is complete

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 194. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:202 - KEEP

> - `Ready` should only be reached after a different or expanded set of setup steps completes

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 195. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:206 - REWRITE

> ## When You Do Not Need To Modify the Core

Reader question: Should this setup change alter Core startup or use exposed session operations, and which command dependencies must hold? Section: When You Do Not Need To Modify the Core.

Reason: Useful decision boundary; make the heading state the supported action.

Optional rewrite/scope: ## Change setup through the integration

### 196. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:208 - KEEP

> You do not need to modify the core if the integration library or application already exposes enough setup-related methods to recreate the desired setup after startup.

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 197. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:215 - REWRITE

> - you want the application to own setup changes instead of baking them into every startup

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: The underlying choice is useful; state the selected action or ownership directly while retaining the condition and technical constraint.

Optional rewrite/scope: Lead with the recommended action already present in this sentence; retain its named alternative only when needed to choose a valid command, owner or validation scope.

### 198. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:227 - KEEP

> 5. decide whether it belongs only in initial setup or should also be possible after startup

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 199. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:234 - REMOVE

> That is not true.

Reader question: Should this setup change alter Core startup or use exposed session operations, and which command dependencies must hold? Section: Common Mistake.

Reason: Standalone rebuttal repeats the preceding misconception and the following constructive explanation.

Optional rewrite/scope: Delete this sentence; retain the explanation of exposed session-level setup operations.

### 200. DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md:236 - REWRITE

> The core only owns the default initial setup path. Once the application is running, setup can also be changed from higher layers if those methods are exposed.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: The narrow only claim can be mistaken for absence of Core runtime scheduling responsibilities.

Optional rewrite/scope: Core defines the default startup sequence; exposed Core operations also coordinate later session edits.

### 201. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:9 - REWRITE

> This guide is for contributors extending WSS itself rather than only consuming existing APIs.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Who This Is For.

Reason: Audience definition can be direct while retaining the route boundary.

Optional rewrite/scope: This guide is for contributors adding reusable WSS functionality.

### 202. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:13 - KEEP

> If you only need to make a targeted core change such as adding a firmware version, updating a message, changing a limit, updating `ModuleSettings`, or changing setup order, use [Start Here: Minor Core Modifications](start-here-minor-core-modifications.md) instead.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 203. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:20 - KEEP

> - Core only

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Start With the Layering Guide.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 204. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:26 - REWRITE

> The main idea is that functionality should be optional and composable. WSS should not force every consumer to adopt every feature that anyone has ever needed.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Start With the Layering Guide.

Reason: Optional composition is useful, but a hypothetical coercive alternative is unnecessary.

Optional rewrite/scope: Each integration selects the capabilities its consumers need.

### 205. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:28 - REMOVE

> Instead of putting all possible behavior directly into the core:

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Start With the Layering Guide.

Reason: The following ownership list already states the architecture; the rejected alternative is rhetorical.

Optional rewrite/scope: Introduce the following list with: Keep responsibilities focused:

### 206. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:38 - KEEP

> Layers exist so that new functionality can be added without making all integrations carry that functionality by default.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Why Layers Exist.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 207. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:42 - KEEP

> - one integration may only need the base core

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Why Layers Exist.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 208. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:43 - KEEP

> - another may need Params but not Model

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Why Layers Exist.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 209. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:45 - REWRITE

> - another may need a completely different feature stack and should not be forced to include unrelated layers

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Why Layers Exist.

Reason: Keep the concrete alternative stack, remove the hypothetical coercion.

Optional rewrite/scope: - another may use a different set of optional layers

### 210. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:51 - REWRITE

> - you do not need to implement everything in the core just because one project needed it

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Repeat of the ownership/composition rule; state the actionable placement criterion.

Optional rewrite/scope: Place reusable optional behavior in a focused layer.

### 211. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:52 - REWRITE

> - you do not need to expose every possible feature in every integration library

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Why Layers Exist.

Reason: Repeat of optional API exposure, best expressed directly.

Optional rewrite/scope: Expose the capabilities supported by the integration.

### 212. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:53 - KEEP

> - you can build higher-level functionality on top of lower-level layers only when it is needed

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Why Layers Exist.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 213. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:65 - KEEP

> This allows a higher-level layer to reuse the behavior below it instead of re-implementing it.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Layer Dependencies and Composition.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 214. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:69 - KEEP

> For example, if one future layer sits three layers above Model, you can include that path and get all of its dependencies automatically. But if another feature path also sits three layers above Model and depends on three different intermediate layers that your project does not need, you do not have to include that path at all.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 215. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:71 - KEEP

> That is one of the main benefits of the layering approach: feature stacks can grow in different directions without forcing every consumer to adopt every branch.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 216. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:73 - KEEP

> ## When To Add a Layer vs. Change the Core

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: When To Add a Layer vs. Change the Core.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 217. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:77 - KEEP

> - optional rather than universally required

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: When To Add a Layer vs. Change the Core.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 218. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:78 - KEEP

> - domain-specific rather than transport/protocol-specific

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 219. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:90 - KEEP

> If a feature can reasonably be absent in some integrations, that is a strong sign it should be a layer rather than a core change.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: When To Add a Layer vs. Change the Core.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 220. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:92 - KEEP

> This is especially important for stimulator-specific firmware features. An optional layer is a good fit when a feature is supported by a stimulator's firmware, but it is not part of the minimum functionality required for the stimulator to work through the shared core.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: When To Add a Layer vs. Change the Core.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 221. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:97 - KEEP

> - an optional layer should contain additional stimulator functionality that is useful, but not required for baseline operation

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: When To Add a Layer vs. Change the Core.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 222. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:99 - KEEP

> This separation becomes more important as WSS expands to support more stimulators. Different stimulators may all support the shared core concepts, but each one may have different advanced firmware features. Optional layers make it possible for each stimulator family to expose its own advanced capabilities without forcing those features into the shared core for every device.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: When To Add a Layer vs. Change the Core.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 223. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:112 - KEEP

> Changes to layers should respect these core behaviors rather than bypass them.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Core Architecture.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 224. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:114 - REWRITE

> The core is the shared foundation that layers build on top of. A good layer extends the system by using the core correctly, not by replacing the core's responsibilities.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Core Architecture.

Reason: Useful ownership rule is already expressed by the following responsibility list.

Optional rewrite/scope: Layers extend the shared stack through its Core contracts.

### 225. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:116 - KEEP

> For example, layers should not take over:

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Core Architecture.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 226. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:134 - KEEP

> - expose only the API surface that a consumer actually needs

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Development Philosophy.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 227. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:136 - REWRITE

> The goal is not to make one giant stack that contains every feature by default. The goal is to make a system where capabilities can be combined in useful ways.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Development Philosophy.

Reason: The positive goal answers the design question; the imagined undesirable alternative adds no criterion.

Optional rewrite/scope: Compose the capabilities each integration needs.

### 228. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:142 - KEEP

> - a layer can be left out when a project does not need it

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 229. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:143 - KEEP

> - higher-level layers reuse lower-level layers instead of duplicating their logic

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Development Philosophy.

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 230. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:144 - KEEP

> - integration libraries can expose only the layers they actually support

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: Development Philosophy.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 231. DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md:158 - KEEP

> - Optional stimulator-specific layers can add advanced firmware-supported behavior that is not required for all devices.

Reader question: Does this behavior belong in Core or an optional layer, and which stack should my integration expose? Section: A Good Mental Model.

Reason: Layer selection or ownership constraint answers which capabilities belong in this composition. Retain the distinction; consolidate repeated optionality examples during a later edit.

### 232. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:7 - KEEP

> For the shared checkout and software-only validation contract, see [Verify a Developer Checkout](developer-verification.md).

Reader question: What should my integration own, expose and delegate to Core? Section: Start Here: Building a New Integration Library.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 233. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:13 - KEEP

> Use this path when an existing integration library does not fit your target environment and you need to build a new reusable integration layer.

Reader question: What should my integration own, expose and delegate to Core? Section: Who This Is For.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 234. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:21 - KEEP

> When you are building an integration library, the application should not need to understand the internal WSS stack in detail. The integration library usually owns:

Reader question: What should my integration own, expose and delegate to Core? Section: What an Integration Library Usually Owns.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 235. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:34 - KEEP

> An application built on top of the integration library should mostly focus on workflow, UX, automation, and timing rather than stack composition.

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: This is an actual preservation or composition-ownership requirement. Retain the named boundary so a reader does not choose an invalid operation.


### 236. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:53 - KEEP

> Integration libraries should usually accept a config location from the application rather than hard-code where the config files live.

Reader question: What should my integration own, expose and delegate to Core? Section: Config Files in an Integration Library.

Reason: This is an actual preservation or composition-ownership requirement. Retain the named boundary so a reader does not choose an invalid operation.


### 237. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:59 - KEEP

> - if the application does not provide a location, the integration library can fall back to a default root-level config directory

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 238. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:60 - KEEP

> - if the files do not exist yet, the library stack will usually auto-generate them with default values

Reader question: What should my integration own, expose and delegate to Core? Section: Config Files in an Integration Library.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 239. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:80 - REWRITE

> Most integration libraries should expose capability groups instead of forcing applications to compose the raw stack themselves.

Reader question: What should my integration own, expose and delegate to Core? Section: Which Capabilities Should Usually Be Surfaced.

Reason: The underlying choice is useful; state the selected action or ownership directly while retaining the condition and technical constraint.

Optional rewrite/scope: Lead with the recommended action already present in this sentence; retain its named alternative only when needed to choose a valid command, owner or validation scope.

### 240. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:111 - KEEP

> `ITransport` is separated from the rest of the stack because transport support is often platform-specific. A transport that works well in one environment may not be portable to another, and trying to force one transport implementation across every language and platform is usually not realistic.

Reader question: What should my integration own, expose and delegate to Core? Section: Transport First.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 241. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:115 - KEEP

> For example, a Bluetooth transport written for one C# environment may not transfer cleanly into Unity on Android. In that case, the integration library can provide its own `ITransport` implementation using a Bluetooth library that is specific to Unity and Android. Once that implementation satisfies `ITransport`, it can be plugged into the existing WSS core and layers without changing the rest of the stack.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 242. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:133 - KEEP

> Future layers should follow the same pattern: add focused reusable behavior without mixing transport mechanics into the layer itself.

Reader question: What should my integration own, expose and delegate to Core? Section: Attach Optional Layers.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 243. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:150 - KEEP

> - report ready/not-ready state

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 244. DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md:160 - KEEP

> If the integration library exposes enough setup-related methods, an application can clear, recreate, and reconfigure the active setup later without modifying the core itself. That is different from changing the core's default startup setup.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 245. DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md:16 - KEEP

> Keep one tested set of pinned integration files and runtime dependencies. A Git submodule records a revision; initialize it recursively after cloning, and review dependency updates alongside their tests. Do not replace one DLL in a runtime bundle without checking the matching dependencies.

Reader question: How do I compose an application without duplicating lifecycle ownership or losing dependency identity? Section: Responsibilities.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 246. DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md:22 - KEEP

> 3. Advance its update loop as required by that host. Unity, MATLAB and CLI applications schedule updates differently; do not add a second update loop around a controller that already owns one.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 247. DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md:24 - KEEP

> 5. Expose actions only when the corresponding state permits them. Readiness and output enablement are separate.

Reader question: How do I compose an application without duplicating lifecycle ownership or losing dependency identity? Section: Build the lifecycle first.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 248. DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md:27 - KEEP

> Start with status inspection and clean shutdown. Add stimulation controls after the lifecycle works. Method spellings, units, return values and disposal rules differ by integration; use that library's README and generated API instead of translating a C# signature literally into another language.

Reader question: Should I edit source inputs or generated output, and which files will this command regenerate?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 249. DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md:31 - KEEP

> Choose the configuration directory before initialization. Keep Core settings, per-channel limits and model settings together as an experiment configuration. See [configuration fields](config-files-reference.md) and [setup dependencies](setup-order-and-modification.md). Missing-file generation is an integration-specific convenience; do not rely on it to supply an experiment's intended settings.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 250. DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md:37 - KEEP

> Use the [testing and deployment guide](testing-and-deployment.md) to separate simulator, host-runtime and hardware checks. See [application dependency updates](ci-application-updates.md) for reviewing pinned upstream changes. If no existing integration fits your runtime, continue to [building an integration library](start-here-building-a-new-integration-library.md).

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 251. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:7 - KEEP

> For the shared checkout and software-only validation contract, see [Verify a Developer Checkout](developer-verification.md).

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Start Here: Minor Core Modifications.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 252. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:11 - KEEP

> This guide is for contributors making focused changes inside the existing WSS core without introducing a new layer or redesigning the overall architecture.

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Who This Is For.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 253. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:33 - REWRITE

> ## What Does Not Count

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: What Does Not Count.

Reason: Boundary helps routing but heading can point directly to alternatives.

Optional rewrite/scope: ## Choose another guide for broader changes

### 254. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:35 - KEEP

> This is not the right path when you are:

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: What Does Not Count.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 255. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:43 - KEEP

> If your change is broader than a targeted core update, use [Start Here: Adding Layers or Core Functionality](start-here-adding-layers-or-core-functionality.md) instead.

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: What Does Not Count.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 256. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:51 - KEEP

> 3. Does it affect startup only, or also mid-stream edits and runtime behavior?

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Start With These Questions.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 257. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:62 - KEEP

>   - Use this when a feature becomes available only in a newer firmware version.

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Which Part Owns the Change.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 258. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:71 - KEEP

>   - Use this only when the wire format itself changes.

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Which Part Owns the Change.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 259. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:78 - KEEP

>   - That decoded snapshot includes hardware configuration flags, PA and PW limits, thresholds for battery and impedance, and a derived amplitude-curve choice such as `10mA` vs `72mA`.

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 260. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:87 - KEEP

> Use this path when the protocol is mostly the same, but the core needs to recognize a new firmware string and gate features correctly.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 261. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:92 - KEEP

> - possibly `WssClient` if a command is only valid in that version

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Adding a New Firmware Version.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 262. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:97 - KEEP

> Use this when the command already exists, but one of these changes:

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 263. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:113 - KEEP

> Use this when an existing command or parameter keeps the same meaning but the valid numeric range changes.

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Changing Limits or Bounds.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 264. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:129 - KEEP

> - `WssClient.ModuleQuery(...)` only if the request or reply handling changed

Reader question: Which Core class owns my focused change, and when is this guide the wrong route? Section: Updating Module Settings Decoding.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 265. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:147 - KEEP

> Setup is owned by `WssStimulationCore`, not by `ModuleSettings`.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: Specific ownership distinction answers where setup edits belong and prevents editing a decoder as an orchestrator.

### 266. DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md:151 - REWRITE

> That does not mean every setup change belongs in the core. The core only owns the default initial setup path. If an integration library or application exposes the necessary setup-related methods, it can also replace or override setup later by sending the right command sequence after startup.

Reader question: Does the change alter shared startup setup or a session-level setup through exposed operations?

Reason: The narrow only claim can be mistaken for absence of Core runtime scheduling responsibilities.

Optional rewrite/scope: Core defines the default startup sequence; exposed Core operations also coordinate later session edits.

### 267. DocsNavigation/apiDocs/conceptual/start-here-using-an-application.md:23 - KEEP

> A simulator exercises the software lifecycle without connecting a device. It does not establish hardware behavior. Runtime test mode and CI conformance mode serve different purposes; use the startup mode named by the README.

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 268. DocsNavigation/apiDocs/conceptual/start-here-using-an-application.md:29 - KEEP

> Initialization performs connection and setup; it does not mean the device is ready immediately. Readiness, output enablement and a requested stimulation value are distinct states. Core can advance through Ready into Started/Streaming during automatic setup; use the operational predicate documented by the host API. Follow the application's status and control flow rather than assuming that initialization alone enables output.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 269. DocsNavigation/apiDocs/conceptual/testing-and-deployment.md:9 - KEEP

> | Check | Establishes | Does not establish |

Reader question: What does this source require or establish for the reader? Section: What each check establishes.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 270. DocsNavigation/apiDocs/conceptual/testing-and-deployment.md:12 - KEEP

> | Simulator startup | Initialization, update scheduling, readiness handling and cleanup without hardware | Radio/serial behavior or physical stimulation |

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 271. DocsNavigation/apiDocs/conceptual/testing-and-deployment.md:14 - KEEP

> | Native host check | The same candidate loads and runs in MATLAB, Unity, Python or .NET as recorded | Hardware/adapter behavior without a device test |

Reader question: Does this test execute hardware, and what device claims may follow?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 272. DocsNavigation/apiDocs/conceptual/testing-and-deployment.md:26 - KEEP

> A passing .NET test for a Unity-facing assembly does not establish Unity Editor or player execution. A MATLAB wrapper build does not establish a native MATLAB session. Preserve this distinction in evidence.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 273. DocsNavigation/apiDocs/conceptual/testing-and-deployment.md:32 - KEEP

> For automated dependency changes, see [CI and dependency updates](../ci.md). That process evaluates candidate revisions and produces reviewable changes; its trust and approval contracts are separate from application startup. Documentation and software releases require maintainer review of the [1.0 readiness checklist](release-readiness.md).

Reader question: What authority does this approval grant, and may it be reused for another candidate?

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 274. DocsNavigation/apiDocs/maintainers.md:3 - REWRITE

> Use this section when you are maintaining the documentation hub itself rather than using or extending WSS.

Reader question: Is this guide for documentation maintenance or WSS use? Section: Maintainers.

Reason: Audience can be stated directly.

Optional rewrite/scope: Use this section to maintain the documentation hub.

### 275. DocsNavigation/apiDocs/start-here.md:14 - KEEP

>   - Choose this if no existing integration library fits your language, platform, or transport environment.

Reader question: Which entry point fits running, extending, or integrating WSS? Section: Choose The Closest Path.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 276. DocsNavigation/apiDocs/start-here.md:19 - REWRITE

>   - Choose this if you are extending WSS itself with new reusable capabilities rather than making a focused internal fix.

Reader question: Which entry point fits running, extending, or integrating WSS? Section: Choose The Closest Path.

Reason: Route distinction is useful; positive routing is shorter.

Optional rewrite/scope: Choose this for new reusable layers or shared Core capabilities.

### 277. DocsNavigation/apiDocs/start-here.md:23 - REWRITE

> - I already have an app and just need to run WSS.

Reader question: Does this operation reach Ready or a later operational state, and which predicate should I observe?

Reason: Just adds no routing information.

Optional rewrite/scope: I already have an app and want to run WSS.

### 278. DocsNavigation/apiDocs/start-here.md:29 - KEEP

> - I need to change one core behavior without redesigning the stack.

Reader question: Which entry point fits running, extending, or integrating WSS? Section: Fast Decision Guide.

Reason: Defines an actual lifecycle, configuration, composition, support or test-coverage boundary relevant to the stated reader question.

### 279. DocsNavigation/README.md:34 - KEEP

> - Main/core only: `./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json --main`

Reader question: Which documentation build mode and prerequisite set should I use? Section: Docs Build Modes.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 280. DocsNavigation/README.md:38 - KEEP

> Without `--main`, the docs build includes all enabled external repositories from `apiDocs/repos.manifest.json`. In the default manifest, `--main` builds the root WSS core solution, which includes the shared core and any transport projects linked into that solution.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 281. DocsNavigation/README.md:44 - KEEP

> For a hub/C# build without Sphinx output, use `--skip-python` in Bash or `-SkipPython` in PowerShell.

Reader question: Does this build mode include Sphinx output, and what extra prerequisites apply?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 282. DocsNavigation/README.md:63 - KEEP

> ### Needed only when Python docs are enabled

Reader question: Which documentation build mode and prerequisite set should I use? Section: Needed only when Python docs are enabled.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 283. DocsNavigation/README.md:84 - KEEP

> DocFX 2.81.0 is pinned in `.config/dotnet-tools.json`. The hub-only preview is written to `artifacts/hub`; add `--serve` to preview locally.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 284. DocsNavigation/README.md:85 - KEEP

> The Windows/Linux CI workflow runs these same commands and uploads review artifacts without deploying them.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 285. DocsNavigation/README.md:86 - KEEP

> This path requires no jq, Python, R, Pandoc, external checkout, or private machine paths.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 286. DocsNavigation/README.md:87 - KEEP

> Full API/RMarkdown builds still require their additional dependencies and configured external checkout roots; they are a separate release stage.

Reader question: Which documentation build mode and prerequisite set should I use? Section: Repository Links And Windows/Linux Validation.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 287. DocsNavigation/README.md:94 - KEEP

> Run `dotnet run --project tools/RepositoryLinks -- site` for the complete staged site, preserving API pages, root redirect and standalone guides. See [Publishing the Complete Site](./apiDocs/conceptual/publishing-site.md) for validation artifacts and the separate authorized Pages Actions activation step.

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 288. DocsNavigation/AGENTS.md:11 - KEEP

> - Prefer minimal diffs; avoid broad “format-only” changes.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 289. DocsNavigation/AGENTS.md:28 - KEEP

> ## Generated vs Source (Edit the Right Thing)

Reader question: Should I edit source inputs or generated output, and which files will this command regenerate?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 290. DocsNavigation/AGENTS.md:31 - KEEP

>   - `*.Rmd` (source) instead of the corresponding `*.html` output.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: Generated vs Source (Edit the Right Thing).

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 291. DocsNavigation/AGENTS.md:32 - KEEP

>   - `apiDocs/conceptual/*.md`, `apiDocs/docfx.json`, and `apiDocs/repos.manifest.json` (inputs) instead of built site output.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: Generated vs Source (Edit the Right Thing).

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 292. DocsNavigation/AGENTS.md:33 - KEEP

> - Avoid hand-editing generated artifacts unless explicitly requested:

Reader question: Which exact producer archive and evidence must be available before promotion?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 293. DocsNavigation/AGENTS.md:45 - KEEP

> - R + `rmarkdown`/`knitr` only if regenerating `.Rmd` outputs.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: Prerequisites.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 294. DocsNavigation/AGENTS.md:78 - KEEP

> - No repo-wide linter/formatter is configured (no `.editorconfig`, no CI workflows detected).

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: Factual tooling inventory, not rhetorical contrast. Recheck accuracy separately because CI/tooling has evolved.

### 295. DocsNavigation/AGENTS.md:79 - KEEP

> - Do not introduce large reformatting churn; keep edits focused.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: Lint / Formatting.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 296. DocsNavigation/AGENTS.md:84 - KEEP

> - There is no first-class automated test suite in this repository.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: Tests.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 297. DocsNavigation/AGENTS.md:105 - KEEP

> - Keep existing conventions; avoid drive-by cleanups.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: General.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 298. DocsNavigation/AGENTS.md:110 - KEEP

> - If Unity projects fail metadata resolution, use doc-only shim projects (linked sources + local refs) instead of source duplication.

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 299. DocsNavigation/AGENTS.md:113 - KEEP

>   - Do not churn import ordering unless necessary; remove unused imports only when touching the file.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: C#.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 300. DocsNavigation/AGENTS.md:122 - KEEP

>   - Some legacy members use `getX` style; do not rename public APIs without a strong reason.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: C#.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 301. DocsNavigation/AGENTS.md:126 - KEEP

>   - Only swallow exceptions in cleanup/shutdown paths.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: C#.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 302. DocsNavigation/AGENTS.md:128 - KEEP

>   - Prefer `Task`-based async; avoid blocking waits.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 303. DocsNavigation/AGENTS.md:137 - KEEP

> - Quote paths (especially on Windows) and avoid assumptions about current working directory.

Reader question: Which source, generated output, editing rule or verification command should an agent use? Section: PowerShell / Bash.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 304. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:32 - KEEP

> ### Needed only when Python docs are enabled

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: Needed only when Python docs are enabled.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 305. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:88 - KEEP

> If `docfx` is not found after installing it as a .NET global tool, add `~/.dotnet/tools` to your shell `PATH`.

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: macOS.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 306. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:115 - KEEP

> If `docfx` is not found after installing it as a .NET global tool, add `~/.dotnet/tools` to your shell `PATH`.

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 307. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:119 - KEEP

> If R package installation fails because the system library is not writable, install to a user library instead:

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: Linux.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 308. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:138 - KEEP

> If you only want the main/core docs, add the main-only flag:

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: Configure linked repositories.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 309. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:143 - KEEP

> When `--main` or `-Main` is set, the build only includes the `core` manifest entry. In the default manifest, that `core` entry points at the root `WSS_Core_Interface.sln`, so main/core docs include the shared core plus any transport projects linked into that solution. Without that flag, the scripts build all enabled repositories.

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 310. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:152 - KEEP

> Python integration docs for the library are not currently supported in this local build workflow. For now, use `--skip-python` in Bash or `-SkipPython` in PowerShell.

Reader question: Does workflow presence establish successful execution or the required permission?

Reason: Build-mode limitation is operational, not filler. User removed Sphinx, so update/remove this stale promise in the later authorized README/build-guidance phase.

### 311. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:204 - KEEP

> index link will use `xref:<entryUid>` (a stable link to a namespace/type) instead of linking to a

Reader question: Should the API index link use a stable entry UID or a generated TOC fragment?

Reason: Concrete compatibility, schema, capability, trust, evidence, editing-ownership or command-selection boundary. The contrast answers a real choice or prevents an invalid inference; retain its substance explicitly.

### 312. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:240 - KEEP

> Build only the main/core docs:

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: Windows (PowerShell).

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 313. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:270 - KEEP

> Build only the main/core docs:

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: macOS/Linux or Git Bash (Windows).

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 314. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:276 - KEEP

> Build the docs hub without Python and without rerendering standalone RMarkdown docs:

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: macOS/Linux or Git Bash (Windows).

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 315. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:299 - KEEP

> If execute permission cannot be changed (for example, restricted mount/ACL), run through Bash directly:

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: macOS/Linux or Git Bash (Windows).

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 316. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:308 - KEEP

> - If `--main` / `-Main` is set, keeps only the `core` entry and skips all optional external integrations

Reader question: Which documentation-generation mode, external checkout and tool dependencies apply? Section: What the scripts do.

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 317. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:330 - KEEP

> If a Unity repository contains a UI/app `.csproj` but the integration code you want to document is a

Reader question: Does the recorded runtime execute Unity or only managed code?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 318. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:331 - KEEP

> submodule folder (not a standalone project), use the provided shim project:

Reader question: What validation and human acceptance must precede this dependency PR action?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

### 319. DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:358 - KEEP

>   - It must be the integration submodule root inside the Unity project (not the Unity repo root).

Reader question: Must DocsSourceRoot point to the Unity integration submodule or the enclosing application repository?

Reason: Defines an editing/build/deployment mode, source/generated ownership, prerequisite or validation limit needed to select the correct operation.

## Excluded candidate appendix

These retain complete initial search coverage without counting neutral labels or code syntax as rhetorical findings.

- ID 320; `DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:367`

  > - DocFX not found

  Exclusion reason: Troubleshooting label names a missing dependency, without rhetorical comparison.

- ID 321; `DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:390`

  > - `apiDocs/` — DocFX project (docfx.json, templates, tools)

  Exclusion reason: Neutral label/list/source-path description or ordinary separator; no rhetorical contrast.

- ID 322; `DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:391`

  > - `apiDocs/build-docs.ps1` — PowerShell build script (Windows)

  Exclusion reason: Neutral label/list/source-path description or ordinary separator; no rhetorical contrast.

- ID 323; `DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd:392`

  > - `apiDocs/build-docs.sh` — Bash build script (macOS/Linux/Git Bash)

  Exclusion reason: Neutral label/list/source-path description or ordinary separator; no rhetorical contrast.

## CI constraint groups for consolidation

Authority and trust: source IDs 6, 20, 24, 27, 29, 31, 36, 37, 39, 41, 44, 45, 46, 47, 48, 52, 54, 57, 59, 62, 69, 70, 71, 74, 75, 76. These can be consolidated editorially, but each distinct condition must survive.

Evidence identity and coverage: source IDs 3, 5, 13, 14, 16, 17, 18, 20, 21, 22, 23, 24, 25, 26, 27, 28, 30, 31, 35, 37, 38, 39, 40, 41, 42, 43, 44, 46, 47, 48, 51, 52, 61, 64, 65, 66, 69, 70, 71, 72, 75. These can be consolidated editorially, but each distinct condition must survive.

Promotion/release state: source IDs 5, 6, 9, 10, 11, 14, 15, 17, 19, 20, 21, 24, 26, 31, 36, 37, 39, 41, 44, 49, 51, 53, 54, 55, 56, 58, 59, 61, 62, 63, 64, 65, 68, 75, 76. These can be consolidated editorially, but each distinct condition must survive.

## File coverage

| Source | Lines | Candidate lines |
| --- | ---: | ---: |
| `DocsNavigation/apiDocs/advanced.md` | 28 | 1 |
| `DocsNavigation/apiDocs/ci.md` | 37 | 5 |
| `DocsNavigation/apiDocs/classes/WssStimulationCore.md` | 23 | 0 |
| `DocsNavigation/apiDocs/concepts.md` | 40 | 0 |
| `DocsNavigation/apiDocs/conceptual/api-reference.md` | 28 | 2 |
| `DocsNavigation/apiDocs/conceptual/building-software-api-docs.md` | 14 | 0 |
| `DocsNavigation/apiDocs/conceptual/ci-application-updates.md` | 58 | 12 |
| `DocsNavigation/apiDocs/conceptual/ci-core-promotion.md` | 73 | 21 |
| `DocsNavigation/apiDocs/conceptual/ci-evidence-and-operations.md` | 76 | 17 |
| `DocsNavigation/apiDocs/conceptual/ci-independent-integrations.md` | 100 | 17 |
| `DocsNavigation/apiDocs/conceptual/ci-workflow-reference.md` | 23 | 1 |
| `DocsNavigation/apiDocs/conceptual/config-files-reference.md` | 94 | 1 |
| `DocsNavigation/apiDocs/conceptual/developer-verification.md` | 46 | 9 |
| `DocsNavigation/apiDocs/conceptual/firmware-compatibility-matrix.md` | 122 | 6 |
| `DocsNavigation/apiDocs/conceptual/integration-library-runtime-selection.md` | 31 | 3 |
| `DocsNavigation/apiDocs/conceptual/platform-support.md` | 35 | 4 |
| `DocsNavigation/apiDocs/conceptual/publishing-site.md` | 92 | 22 |
| `DocsNavigation/apiDocs/conceptual/release-readiness.md` | 98 | 26 |
| `DocsNavigation/apiDocs/conceptual/repository-and-kit-links.md` | 38 | 0 |
| `DocsNavigation/apiDocs/conceptual/repository-links.md` | 175 | 33 |
| `DocsNavigation/apiDocs/conceptual/setup-order-and-modification.md` | 250 | 20 |
| `DocsNavigation/apiDocs/conceptual/simple-serial-communication.md` | 14 | 0 |
| `DocsNavigation/apiDocs/conceptual/start-here-adding-layers-or-core-functionality.md` | 176 | 31 |
| `DocsNavigation/apiDocs/conceptual/start-here-building-a-new-integration-library.md` | 207 | 13 |
| `DocsNavigation/apiDocs/conceptual/start-here-developing-an-application.md` | 44 | 6 |
| `DocsNavigation/apiDocs/conceptual/start-here-minor-core-modifications.md` | 203 | 16 |
| `DocsNavigation/apiDocs/conceptual/start-here-using-an-application.md` | 37 | 2 |
| `DocsNavigation/apiDocs/conceptual/testing-and-deployment.md` | 38 | 5 |
| `DocsNavigation/apiDocs/conceptual/wss-commands-reference.md` | 14 | 0 |
| `DocsNavigation/apiDocs/index.md` | 63 | 0 |
| `DocsNavigation/apiDocs/maintainers.md` | 32 | 1 |
| `DocsNavigation/apiDocs/start-here.md` | 47 | 4 |
| `DocsNavigation/README.md` | 94 | 9 |
| `DocsNavigation/AGENTS.md` | 153 | 16 |
| `DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd` | 399 | 20 |
