# Contrastive wording: standalone and branch variants

Read-only editorial inventory, 2026-10-29. Repository wording is unchanged. This report records suggestions for user selection. Keep preserves meaningful constraints; Rewrite retains the fact with direct language; Remove identifies dispensable framing. No proposal is approved or applied.

## Snapshots and scope

| Key | Worktree / branch | Reviewed snapshot |
| --- | --- | --- |
| NAV | `DocsNavigation`, `docs/reliable-navigation-links` | HEAD `c6eb5894ba64cb518ca51a04706ee6ff0fd3d2a6`; all four authored standalone RMarkdown guides. |
| ARCH | `DocsArchitecture`, `docs/accurate-architecture` | `a1d46548a24feaf3cd49f056ed8a5895710a94ec`; architecture snapshot under review. No additional edits or user approval are claimed. |
| LAYER | `DocsLayering`, `docs/clear-layering` | `94bb4f1ea4d12907317a0690a31f0c40859e343a`; the user's layering wording revision is already handled here. |
| API | `DocsAPI`, `docs/core-api-presentation` | HEAD `c6eb5894ba64cb518ca51a04706ee6ff0fd3d2a6` plus uncommitted API presentation draft, compared with that base. |

Each section gives an exact workspace-relative source path; each row supplies the exact source line. Branch/commit are inherited from its key above. Quotations are exact excerpts, grouped when a sentence spans lines. All paths are relative to `C:\Users\Owner\Documents\Codex\2026-10-28\task-6`.

Search covered not/rather than/instead/unlike/only/just/but/while/without/never/no/unless/except/whereas/versus/cannot/separate/still and related forms, followed by semantic reading. No rhetorical “just” or “unlike” occurs in the owned prose. Genuine technical constraints remain in the inventory.

Deduplication/exclusions: generated HTML/YAML duplicates, binary assets, Boolean syntax, protocol enum names, and unchanged inherited API prose are excluded. Other shared Markdown/READMEs belong to other reviewers. API source comments and user-visible build/provenance text are listed separately. The new `maintainers.md:18` link contains no contrast; its inherited line 5 “rather than” sentence is outside the API diff. Core and Model namespace overwrite prose contains no contrast. No local runtime, build, device or publication action was performed.

## NAV: `DocsNavigation/hardwareDocs/wsshardware.rmd`

| ID / line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| H01 / 50 | “Charging requires a 5V power adapter and only functions when the mode switch is set to Charging.” | Which charger and mode? | **Keep**: electrical/mode condition. Optional: “Use a 5V adapter and set the switch to Charging.” |
| H02 / 64 | “The device will not charge unless the switch is in Charging mode.” | Why is charging inactive? | **Rewrite/merge H01**: repeated condition. “Set the switch to Charging to charge the battery.” |
| H03 / 65 | “The device will not run any programs in Charging mode.” | Can programs run during charging? | **Keep**: operational constraint. Optional: “Charging mode disables program execution.” |
| H04 / 113 | “The other wire, without a marker, is the negative (-) terminal” | Which wire is negative? | **Keep**: physical identification. Optional: “The unmarked wire is the negative (-) terminal.” |
| H05 / 123 | “Dry Active Electrode and ACE Ring  (HFI only)” | Does this apply to my kit? | **Keep pending kit-scope verification**: removing “only” broadens applicability. Optional: “HFI kit: Dry Active Electrode and ACE Ring.” |

Neutral candidates reviewed: line 95 “both ends” describes the figure; line 102 “separate 2mm touchproof connector” describes wiring.

## NAV: `DocsNavigation/howtoCompileAPIDocs/BuildSoftwareDocs.Rmd`

| ID / line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| B01 / 11 | “generate the WSS documentation hub while keeping code in external repositories.” | Must source code be copied? | **NOT RHETORICAL**: concurrent goals/build description. Optional clarity wording: “The hub builds documentation from external code repositories.” |
| B02 / 32 | “Needed only when Python docs are enabled” | Is Sphinx always required? | **Keep**: dependency condition. |
| B03 / 37 | “If DocFX isn't installed” | What if the CLI is missing? | **Keep; verify separately**: actionable predicate; align with pinned-tool instructions before changing facts. |
| B04 / 88 | “If `docfx` is not found after installing it as a .NET global tool” | Why is the installed command unavailable? | **Keep**: PATH troubleshooting. Optional: “For a global-tool install, add ~/.dotnet/tools to PATH.” |
| B05 / 115 | “If `docfx` is not found after installing it as a .NET global tool” | Same question on another platform. | **Keep**: platform-local duplicate can be useful. |
| B06 / 119 | “If R package installation fails because the system library is not writable, install to a user library instead:” | How do I install with restricted access? | **Rewrite**: “For a read-only system library, install R packages to a user library.” |
| B07 / 138 | “If you only want the main/core docs, add the main-only flag:” | How do I select a smaller build? | **Keep/direct rewrite**: “To build the main/core section, add --main or -Main.” |
| B08 / 143 | “the build only includes the `core` manifest entry” | What exactly does --main select? | **Keep**: precise flag semantics. |
| B09 / 143 | “Without that flag, the scripts build all enabled repositories.” | What is the default? | **Keep/direct rewrite**: “The default build includes every enabled repository.” |
| B10 / 150 | “while still building the DocFX hub and any enabled C# API sections.” | Does skipping Python skip the hub? | **Rewrite**: “This flag skips Sphinx entries and builds the hub and enabled C# API sections.” |
| B11 / 152 | “Python integration docs for the library are not currently supported in this local build workflow. For now, use” | Which mode should I use? | **Rewrite/remove after scope decision**: temporary status conflicts with finished-release prose. State supported scope directly; do not invent Sphinx support. |
| B12 / 159 | “while still building the DocFX hub and any enabled API sections.” | Can APIs build without RMarkdown? | **Rewrite**: “This flag reuses standalone HTML and builds the hub and enabled API sections.” |
| B13 / 169 | “This must be the subfolder that contains the integration code you want documented.” | Which Unity directory? | **Keep**: path contract. |
| B14 / 172 | “This must be the Python project root that contains a `docs/` folder” | Which Python directory? | **Keep**: filesystem contract. |
| B15 / 204 | “instead of linking to a” / 205 “generated TOC file.” | What does entryUid change? | **Rewrite**: “With entryUid, the index links directly to the selected namespace/type.” |
| B16 / 224 | “`root` must be the Python project root” | Where is Sphinx resolved? | **Keep**: root semantics. |
| B17 / 240 | “Build only the main/core docs:” | Which narrow-mode command? | **Keep**: command scope. Optional: “Build the main/core section:”. |
| B18 / 270 | “Build only the main/core docs:” | Same command in another shell. | **Keep**: shell-local caption. |
| B19 / 276 | “without Python and without rerendering standalone RMarkdown docs:” | Which tools can I skip? | **Keep**: explains actual flags. Optional caption names --skip-python/--skip-rmd directly. |
| B20 / 299 | “If execute permission cannot be changed” | How do I run a readable script on a restricted mount? | **Keep**: operational fallback; presumes Bash execution itself is allowed. |
| B21 / 308 | “keeps only the `core` entry and skips all optional external integrations” | What does manifest filtering retain? | **Keep**, but duplicates B08. Consider one authoritative explanation. |
| B22 / 309 | “unless `--skip-rmd` / `-SkipRmd` is used:” | Is RMarkdown rendered by default? | **Keep**: default/opt-out contract. |
| B23 / 330–331 | “contains a UI/app `.csproj` but the integration code” / “submodule folder (not a standalone project)” | Which Unity project should metadata use? | **Rewrite**: “For integration sources in a Unity submodule, use the documentation shim project.” |
| B24 / 358 | “(not the Unity repo root).” | Which nearby directory is correct? | **Keep/direct rewrite**: “Set this to the integration submodule root inside the Unity project.” |
| B25 / 367 | “DocFX not found” | Why did the command fail? | **Keep**: troubleshooting heading. |
| B26 / 384 | “nullability warnings that don't affect documentation output.” | Why are warnings suppressed? | **Rewrite/verify**: “The docs shim's NoWarn list records the suppressed nullability diagnostics.” Avoid claiming all suppressed warnings are harmless. |

Neutral candidates reviewed: adding the same file to both platform scripts (line 324); csproj/solution alternatives; the actual fallback command blocks. Stale tool/path advice is a separate factual audit and has not been changed.

## NAV: `DocsNavigation/simpleSerialPortDocs/SimpleSerial.Rmd`

| ID / line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| S01 / 20 | “USB-Serial devices only” | Which glob filters receivers? | **Keep**: shell-comment scope. |
| S02 / 78 | “No data available.” | What does an empty read mean? | **NOT RHETORICAL**: literal status output. |
| S03 / 87 | “raw bytes (not ASCII) and disable line endings.” | Why does text-terminal input fail? | **Keep/direct rewrite**: “Send binary frame bytes with line endings disabled.” |
| S04 / 114 | “Only one program can have the serial port open at a time.” | Why is the port busy? | **Keep**: concrete resource constraint; platform breadth can be verified separately. |
| S05 / 115 | “if the device does not appear.” | When are vendor drivers needed? | **Keep**: conditional troubleshooting. |
| S06 / 116 | “If you only see ASCII” / “sending text not bytes” | Why is the response wrong? | **Rewrite**: “For binary communication, send the byte arrays shown above and disable line endings.” Avoid treating all visible ASCII as failure. |

Line 49 “same payload” is an Echo contract, not rhetorical framing.

## NAV: `DocsNavigation/wssCommandsDocs/wsscommands.Rmd`

| ID / line | Exact excerpt | Reader question | Decision and reason |
| --- | --- | --- | --- |
| W01 / 49 | “except the final END terminator.” | Is the delimiter escaped? | **Keep**: framing boundary. |
| W02 / 52 | “except the final END.” | What enters the checksum? | **Keep**: checksum boundary. |
| W03 / 85 | “Stream PA+PW only” | What is in 0x31? | **Keep**: exact field subset. |
| W04 / 86 | “Stream PA+IPI only” | What is in 0x32? | **Keep**: exact field subset. |
| W05 / 87 | “Stream PW+IPI only” | What is in 0x33? | **Keep**: exact field subset. |
| W06 / 109 | “number of data bytes only.” | Does length include framing? | **Keep**: protocol definition. |
| W07 / 110 | “0-255 unless noted.” | What is the default field range? | **Keep**: numeric default and exceptions. |
| W08 / 235 | “present only if firmware supports LED settings (J03+)” | Does this firmware accept ledByte? | **Keep**: version-gated layout. |
| W09 / 237 | “00 = not used” | What does 00 mean? | **Keep**: literal enum meaning. |
| W10 / 248 | “Optional LED bitmask (J03+ only)” | Which versions accept this field? | **Keep**: table-local version restriction. |

## ARCH: `DocsArchitecture/apiDocs/conceptual/core-architecture.md`

This section audits architecture snapshot a1d46548. It claims neither user approval of that wording nor additional architecture edits. Entries include actual ownership, scheduling and state distinctions; neutral search candidates are explicitly separated from rhetorical framing.

| ID / line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| A01 / 5 | “as separate collaborators.” | How do client, codec and transport relate? | **NOT RHETORICAL**: neutral composition statement. |
| A02 / 13 | “It does not own or connect the transport.” | Does codec setup open a connection? | **Keep/direct rewrite**: “Client/Core owns connection and transport lifetime.” |
| A03 / 14 | “Chunks need not align with message boundaries.” | Is one callback one message? | **Keep**: essential byte-stream contract. |
| A04 / 29 | “An integration can own that loop instead” | Who calls Tick? | **Rewrite**: state directly that C#/Python own background ticks and Unity uses its lifecycle. |
| A05 / 29 | “instead of adding a second loop.” | Should my host tick again? | **Keep/direct rewrite**: “Use the integration's existing update loop.” |
| A06 / 35 | “while ticks advance the public lifecycle” | How do worker and Tick interact? | **NOT RHETORICAL**: temporal/concurrent mechanisms. |
| A07 / 35 | “rather than waiting exclusively for `Ready()`.” | Why can a Ready-only wait miss success? | **Rewrite**: “Observe the integration's operational predicate, which can include Started/Streaming.” |
| A08 / 37 | “successful path only.” | Which outcomes does the sequence cover? | **Keep/direct rewrite**: “The sequence shows successful initialization.” |
| A09 / 37 | “neither failure cleanup nor host shutdown is an implied step after success.” | Does success immediately trigger shutdown? | **Remove if diagram clear**: defensive explanation of a previously misleading sequence. |
| A10 / 59 | “A status-only host example still needs an explicit emulator/test transport to avoid device I/O.” | Can initialization connect hardware during a status query? | **Keep/direct rewrite**: “Select an emulator/test transport before initialization for a hardware-free status example.” |
| A11 / 63 | “while the configured retry budget permits” | When do retries stop? | **Keep**: bounded retry condition. |
| A12 / 63 | “not a fresh connection or a replay of the entire initial setup on every attempt.” | Does each retry restart everything? | **Rewrite**: “Each retry resumes from the stored per-target setup cursor.” |
| A13 / 65 | “does not perform final disposal or change the state to `Disconnected`.” | Is Error full shutdown? | **Keep**: precise state/ownership limit. |
| A14 / 71 | “no” | What if retry budget remains? | **NOT RHETORICAL**: literal diagram decision edge. |
| A15 / 80 | “A passing setup result does not establish later radio availability or hardware behavior” | What does setup success establish? | **Rewrite**: “Handle later disconnections and failures with bounded waits at the integration boundary.” Preserve action, shorten assurance disclaimer. |
| A16 / 84 | “while the system is running” / “while setting up” | Can operations be queued concurrently? | **NOT RHETORICAL**: temporal behavior. |
| A17 / 86 | “a separate decision from finishing the commands.” | Does command completion resume streaming? | **Keep**: meaningful scheduler distinction. |
| A18 / 86 | “Completion alone does not imply that streaming starts again.” | Is queue drain sufficient? | **Rewrite/merge A17**: “Streaming resumes when the batch requests it; StopStim clears that request.” |
| A19 / 92 | “no” | What if streaming is stopped? | **NOT RHETORICAL**: literal diagram edge. |
| A20 / 98 | “no” | What if the queue is empty? | **NOT RHETORICAL**: literal diagram edge. |
| A21 / 100 | “no” | What if resume is disabled? | **NOT RHETORICAL**: literal diagram edge. |
| A22 / 103 | “rather than treating concurrent edits as a safety guarantee.” | How should the host coordinate calls? | **Rewrite**: retain “Serialize lifecycle and setup operations in the host.” The safety comparison adds no instruction. |
| A23 / 105 | “does not wait for a fresh application request for every packet.” | Why does streaming continue? | **Rewrite**: “Streaming periodically transmits cached channel values.” |
| A24 / 105 | “different response behavior.” | Do stream packets receive command-style replies? | **Keep**: real protocol distinction between reply-bearing edits and one-way packets. |
| A25 / 109 | “whereas host shutdown releases a running session. They are not interchangeable.” | Is MCU reset equivalent to host shutdown? | **Rewrite**: “MCU reset sends a protocol command. Shutdown releases the host session.” |
| A26 / 109 | “does not automatically apply Core's scheduler policy.” | Can I directly call low-level reset? | **Keep/direct rewrite**: “Expose and coordinate reset through the integration when needed.” Verify the integration's actual support; this wording does not imply a public Core reset API exists. |
| A27 / 114 | “Does not dispose the client/transport or reset Core to `Disconnected`.” | What remains after Error handling? | **Keep**: table-local lifecycle contract. |
| A28 / 118 | “Cleanup is not a promise that the device acknowledged a stop” | Can host cleanup confirm remote stop? | **Keep/direct rewrite**: “Cleanup attempts zeroing and disconnect; device acknowledgement requires separate observation.” |
| A29 / 118 | “forced process termination cannot run normal cleanup.” | Does force termination execute callbacks? | **Keep**: concrete host limitation. |
| A30 / 122 | “a capability of this concrete Core, not an independently attached decorator.” | Must I construct a separate Basic layer? | **Rewrite**: “WssStimulationCore exposes Basic Stimulation directly through IBasicStimulation.” |
| A31 / 124 | “implementation, not methods an application can call” | May an application call the named helpers? | **Rewrite**: “These private helpers explain the implementation; application entry points use public interfaces.” |

## LAYER: `DocsLayering/apiDocs/conceptual/layering-guide.md`

Recent wording already handled at 94bb4f1. The direct introductory explanation and responsibilities table already replace the earlier broad rhetorical framing.

| ID / line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| L01 / 29 | “without those transformations.” | Are direct analog/lifecycle calls transformed? | **Keep/direct rewrite**: “Lifecycle and direct analog commands pass through unchanged.” |
| L02 / 42 | “Direct `StimulateNormalized` calls bypass this model calculation.” | Which entry point applies P/PD? | **Keep**: meaningful semantic contrast with no negative keyword. |
| L03 / 56 | “not an experiment configuration or a recommendation for a device.” | Are the example values ready for hardware? | **Rewrite/merge**: “The following simulator values illustrate the mapping.” The preceding sentence already identifies simulation. |
| L04 / 70 | “default pulse width instead.” | What varies in amplitude mode? | **Rewrite**: “Params varies amplitude and uses the configured default pulse width.” |
| L05 / 71 | “zero-output branch rather than the positive-value minimum.” | Does zero map to positive minimum? | **Keep/direct rewrite**: “A normalized input at or below zero selects the zero-output branch.” |
| L06 / 83 | “a separate concern from object construction.” | Is decorator construction device setup? | **Rewrite**: “After object construction, Core connects and queues the device setup sequence.” |
| L07 / 87 | “do not add another tick loop” | Should my host tick again? | **Keep/direct rewrite**: “Use the controller's existing update loop.” |
| L08 / 91 | “initial setup versus later edits” | Where are runtime configuration changes explained? | **Keep**: useful navigation distinction. |
| L09 / 93–94 | “rather” / “than sending a second stream of protocol commands from a layer.” | Where should a layer send device edits? | **Rewrite**: retain “Keep device edits in Core's scheduling path.” |

Neutral meaningful alternatives retained: Core-only/Params/Model compositions (lines 31–33, 109–110), and normalized versus model entry points (58–62). “same directory” source references are not rhetorical contrasts.

## API: `DocsAPI/apiDocs/conceptual/api-regeneration.md`

| ID / line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| P01 / 5 | “It does not use moving branches or generate Python/Sphinx APIs.” | Which source/generator scope? | **Rewrite**: “This route generates Core/C#/Unity metadata from the locked commits.” The preceding sentence already provides most scope. |
| P02 / 18 | “Generation never applies output, deletes a prior artifact, commits changes or publishes.” | What side effects does the command have? | **Keep/direct rewrite**: “Generation writes a new review artifact; adoption and publication are separate steps.” Preserve fresh-output requirement. |
| P03 / 18 | “a successful generation is not release or hardware acceptance.” | Does metadata success approve release? | **Rewrite**: “Review the artifact and complete release-readiness checks before adoption/publication.” |
| P04 / 24 | “they do not claim every old member has an equivalent signature.” | Do compatibility routes prove API equivalence? | **Keep/direct rewrite**: “Compatibility pages retain navigation to the current type; compare signatures against the pinned API.” |
| P05 / 26 | “Only then proceed” | When can publication begin? | **Keep**: review prerequisite. |
| P06 / 30 | “No production signatures or Remarks semantics are rewritten.” | What can presentation change? | **Keep/direct rewrite**: “The transform changes labels and ordering while preserving signatures and Remarks.” |
| P07 / 32 | “still accept manifest-selected source checkouts.” | Is that build route available? | **Rewrite**: “Manifest-driven scripts accept selected source checkouts.” Remove temporal filler. |
| P08 / 32 | “They do not supply the locked provenance of the release regeneration route.” | Which route supplies release evidence? | **Keep/direct rewrite**: “Use the locked regeneration route for release provenance.” |
| P09 / 34 | “rather than emitting untransformed metadata.” | What if pwsh is missing? | **Rewrite**: “The full Bash API build requires PowerShell 7 and exits with a dependency message when pwsh is absent.” |
| P10 / 34 | “This is separate from hub-only and complete-site builds” | Do previews regenerate source? | **Keep/direct rewrite**: “Hub-only and complete-site builds render reviewed metadata.” |
| P11 / 34 | “without external source regeneration.” | Why do those builds need fewer tools? | **Keep/merge P10**: actual build-mode distinction. |
| P12 / 34 | “Existing Sphinx modes remain separate and unchanged.” | Did this work change Sphinx? | **Remove from durable guide**: change-report/history language; retain only if explaining a present command choice. |
| P13 / 36 | “source-only documentation shim” | What does the harness execute? | **Keep**: harness scope. |
| P14 / 36 | “It does not execute the Unity Editor, load a native Unity player, connect hardware or verify stimulation.” | What does metadata generation validate? | **Keep/direct rewrite**: describe compilation and link the separate Editor/player/device acceptance checks. The evidence boundary is real. |

## API: namespace overwrite prose

| ID / exact path:line | Exact excerpt | Reader question | Decision and reason / suggested wording |
| --- | --- | --- | --- |
| P15 / `DocsAPI/apiDocs/overwrites/WSS.Unity.md:5` | “It is not a production namespace and must not be added to application `using` statements.” | Should I write using WSS.Unity? | **Keep**: directly prevents the actual namespace trap. Optional: “Use the globally declared Unity types directly.” |
| P16 / `DocsAPI/apiDocs/overwrites/WSS.Unity.md:7` | “documentation-only shim; this does not execute the Unity Editor.” | Does the API page establish scene compatibility? | **Shorten API overview; keep detailed evidence in maintainer guide**: “Signatures are generated from the locked Unity source.” Link to generation scope. |
| P17 / `DocsAPI/apiDocs/overwrites/Wss.CalibrationModule.md:7` | “does not rename that namespace.” | Is Params a new namespace? | **Rewrite**: “The parameter layer uses the production namespace Wss.CalibrationModule.” Consider dropping “historical,” which can imply obsolete code. |
| P18 / `DocsAPI/apiDocs/overwrites/Wss.Transports.md:7` | “does not establish physical-device test coverage.” | Is every backend hardware-tested? | **Move/remove from API summary**: place concrete test coverage in testing/platform/release-checklist guidance. It does not help locate a transport API. |

Core/Model overwrite descriptions and the new maintainer-task link have no rhetorical contrasts.

## API: implementation comments and user-visible build/provenance text

These entries should not be mechanically rewritten with onboarding prose. Diagnostics and reproducibility policies can require explicit exclusions.

| ID / exact path:line | Exact excerpt | Reader question | Decision / reason |
| --- | --- | --- | --- |
| T01 / `DocsAPI/apiDocs/api-sources.lock.json:6` | “generation never resolves moving branches.” | Is source selection immutable? | **Keep**: provenance invariant. Optional direct statement: checks out recorded SHAs. |
| T02 / `DocsAPI/apiDocs/api-sources.lock.json:12` | “exclude tests and executable tools” | Which Core projects? | **Keep**: project-selection boundary. Production public emulator APIs remain included. |
| T03 / `DocsAPI/apiDocs/refresh-api.ps1:20` | “OutputRoot must be new or empty; no output is deleted.” | Will rerun delete artifacts? | **Keep**: precise rejection/side-effect contract. |
| T04 / `DocsAPI/apiDocs/refresh-api.ps1:52` | “determined only inside the immutable checkout” | Where is project selection resolved? | **Keep**: ties inventory to pin. |
| T05 / `DocsAPI/apiDocs/refresh-api.ps1:121` | “no member equivalence inferred.” | What do compatibility routes guarantee? | **Keep**: machine-readable policy. |
| T06 / `DocsAPI/apiDocs/refresh-api.ps1:124` | “no UID/namespace changes” | What does sorting preserve? | **Keep**: transformation invariant. |
| T07 / `DocsAPI/apiDocs/refresh-api.ps1:124` | “display label only; no production type names or signatures changed” | What changes for Unity? | **Keep**: narrow transformation scope. |
| T08 / `DocsAPI/apiDocs/refresh-api.ps1:127` | “No repository metadata replaced or site published.” | Was output applied/published? | **Keep**: execution result/side-effect evidence. |
| T09 / `DocsAPI/apiDocs/shims/PinnedUnity/PinnedUnity.Docs.csproj:2` | “Production source and runtime targets are unchanged.” | Does the shim retarget production? | **Keep**: implementation boundary. |
| T10 / `DocsAPI/apiDocs/shims/PinnedUnity/PinnedUnity.Docs.csproj:10` | “CS0649 does not indicate a metadata defect.” | Why suppress that diagnostic? | **Rewrite**: “Unity populates serialized fields at runtime; suppress CS0649 for metadata compilation.” |
| T11 / `DocsAPI/apiDocs/shims/PinnedUnity/PinnedUnity.Docs.csproj:20` | “native backend, not a compiler metadata reference.” | Why exclude WssBleWindows.dll? | **Keep**: managed/native compiler distinction. |
| T12 / `DocsAPI/apiDocs/tools/ApiInputHash.ps1:4` | “UTF-8 without BOM.” | Which exact bytes are hashed? | **NOT RHETORICAL**: literal hash representation definition. |
| T13 / `DocsAPI/apiDocs/tools/ApiInputHash.ps1:5` | “No Unicode, whitespace, or Git working-tree normalization is performed.” | What normalization is excluded? | **Keep**: reproducibility contract. |
| T14 / `DocsAPI/apiDocs/tools/ApiPresentation.ps1:20` | “without moving a type to another namespace.” | Does sorting move types? | **Keep**: algorithm invariant. |
| T15 / `DocsAPI/apiDocs/tools/ApiPresentation.ps1:56` | “Only the synthetic namespace UID's display fields change.” | Which fields can mutate? | **Keep**: mutation scope. |
| T16 / `DocsAPI/apiDocs/tools/ApiPresentation.ps1:78` | “without type or signature changes.” | What do the fixtures verify? | **Keep**: test-result scope. |
| T17 / `DocsAPI/.github/workflows/docs-pages.yml:11` | “artifact review only” | Can this dispatch publish? | **Keep**: consequential input description. |
| T18 / `DocsAPI/.github/workflows/docs-pages.yml:56` | “main-only Pages artifact” | Which refs package deployment? | **Keep, inherited context**: real restriction; line itself is not new to API diff. |
| T19 / `DocsAPI/.github/workflows/docs-pages.yml:75` | “without applying output” | Is metadata adopted automatically? | **Keep**: workflow side-effect boundary. |

Excluded implementation literals: Git --no-checkout, MSBuild Exclude, YAML if-no-files-found, invalid fixture text “not a toc,” Boolean operators and test assertion failure text. These are syntax or deliberately failing fixtures rather than rhetorical prose.

## Selection notes

There are **124 stable-ID inventory rows**, not 124 rhetorical contrasts:

| Primary recommendation category | Count | Meaning |
| --- | --- | --- |
| NOT RHETORICAL | 10 | Neutral composition/time, literal output/diagram edges or representation definitions found by search. Excluded from rhetorical/constraint totals. |
| Keep constraint/distinction | 81 | Substantive operational, protocol, ownership, scope or evidence statements. Some offer an optional direct rephrasing without recommending removal of the fact. |
| Rewrite/remove/shorten | 33 | Primary editorial change suggestions; factual verification or scope decisions are identified in the rows. |

NOT RHETORICAL IDs: **B01, S02, A01, A06, A14, A16, A19, A20, A21, T12**.

Primary rewrite/remove/shorten IDs: **H02; B06, B10, B11, B12, B15, B23, B26; S06; A04, A07, A09, A12, A15, A18, A22, A23, A25, A30, A31; L03, L04, L06, L09; P01, P03, P07, P09, P12, P16, P17, P18; T10**. All other stable IDs are in the Keep category.

Strong low-risk prose candidates: B06/B10/B12/B15/B23/B26; A04/A07/A09/A12/A15/A18/A22/A23/A25/A30/A31; L03/L04/L06/L09; P01/P03/P07/P09/P12/P17/P18. Protocol/hardware/ownership/publication constraints should survive any rewrite. A global negation replacement would damage useful contracts.

The architecture snapshot is a1d46548; no approval or additional edits are claimed. The user's layering revision is already handled at 94bb4f1. API remains an uncommitted draft at the c6 base. This inventory is for wording selection; source behavior validation and publication review remain separate work.
