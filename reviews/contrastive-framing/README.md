# Contrastive framing: review inventory

This is a read-only editorial review for individual selection. No broader wording proposals in these reports have been applied. The separately requested layering revision is already on `docs/clear-layering` at `94bb4f1ea4d12907317a0690a31f0c40859e343a`; its Windows/Linux build and rendered link checks passed in run 40814314674, and the rebuilt page's diagram rendered successfully in the browser.

The audit lives on the separate local branch `docs/contrastive-framing-review`, based on approved navigation `c6eb5894ba64cb518ca51a04706ee6ff0fd3d2a6`. Architecture, layering and API variants were read in their own worktrees; they were not merged into this branch. The later approval to integrate architecture/navigation into dev does not approve these wording recommendations.

## Read the inventory

Every finding records its source version, path/line, exact passage or explicitly identified excerpt, likely reader question, recommendation and reason. Stable IDs let you accept or reject individual suggestions. Use the report name as an ID prefix: `Shared:199`, `README:21`, `Variants:A09`, `API:A026`, or `Main-delta:C1`.

Relative Markdown links inside quoted source passages retain their original text and resolve relative to that source page, not this report directory. Use the accompanying source path/line when following those quoted links. The report navigation links in this index resolve within the audit folder.

| Report | Coverage and findings |
| --- | --- |
| [Shared Markdown](contrastive-shared-review.md) | Complete authored shared Markdown and build guidance at navigation c6: 319 framing/constraint findings, with 298 KEEP, 19 REWRITE and 2 REMOVE; four neutral candidates excluded separately. Includes maintainer/CI material, where many explicit boundaries are useful. |
| [Ten software READMEs](contrastive-readmes-review.md) | All ten preservation-branch READMEs: 57 framing/boundary entries plus 12 neutral exclusions. Separate whole-file review of all ten locally available origin/main snapshots: 72 boundary/contrast groups plus eight neutral groups. Exact local hashes are recorded. |
| [Independent README cross-review](contrastive-cross-review.md) | Reconsiders all ten initial rewrite recommendations against actual setup questions. Four bounded rewrites are reasonable; six distinctions should remain explicit. Read this before selecting README edits. |
| [Standalone guides and branch variants](contrastive-variants-review.md) | All four RMarkdown guides; architecture a1d46548, layering 94bb4f1, and new/modified uncommitted API guidance/comments. 81 KEEP distinctions, 33 rewrite/remove/shorten suggestions, and ten non-rhetorical candidates listed separately. |
| [Core/C#/Unity API prose](contrastive-api-reference-review.md) | 61 managed-reference YAML files, 1,763 prose fields. Main inventory: 119 distinct texts (110 KEEP, five REWRITE, one REWRITE/VERIFY, three VERIFY-before-rewrite). Another 86 neutral candidates/constraints are in an appendix. All 324 candidate occurrences and owning UIDs are recorded across 205 deduplicated texts. |
| [Main-to-draft and legacy content](contrastive-main-delta-review.md) | 35 grouped decisions from changed main-source passages, 15 unique legacy HTML findings and three manual class-page findings. Records relocated constraints so shortening a page is not mistaken for losing the underlying contract. |

These counts are not additive. BuildSoftwareDocs.Rmd appears in shared and standalone review; main-to-draft entries may point to the same text's new owner; API overloads with identical prose are grouped. HTML generated from an identified authored source is represented by that source. A repeated fact in separate authored pages remains visible because repetition itself can be a maintenance concern.

## Editorial judgement

The strongest candidates are unnecessary chart-reading instructions, presumed misconceptions, repeated optional-layer rebuttals, and meta commentary explaining what the documentation is not trying to do. The shared adding-layers guide and setup guide contain several examples; see Shared:199–211 and the variants report's selection notes.

Explicit contrasts often answer real questions. Preserve byte-chunk versus message boundaries, transient Ready versus operational state, caller versus controller tick ownership, error handling versus final disposal, reset versus shutdown, incompatible YAML schemas, simulator versus device I/O, runtime selection, firmware support, and build-versus-publication evidence. A positive rewrite is acceptable only when it retains the facts that make the distinction useful.

The independent README review changes the initial recommendation for six items:

| README ID | Consolidated recommendation |
| --- | --- |
| 11 | KEEP the explicit background-tick ownership and Test/Conformance distinction; optional tightening only. |
| 34 | KEEP the ROS schema distinction; both supported formats must remain clear. |
| 49 | KEEP the saved Unity scene/default-state warning unless the adjacent setup steps already make the requirement explicit. |
| 53 | KEEP caller-owned CLR selection and persistence; avoid substituting vague ownership jargon. |
| 54 | KEEP the answer that a separate Core checkout is unnecessary. |
| 67 | KEEP application-root versus MATLAB working-directory path behavior; direct wording can retain both facts. |

The four bounded README candidates are 21 (path tail only), 23 and 29 (retain the historical SSH URL reason and invocation-local HTTPS substitution), and 27 (last path sentence only). Other filenames, API spellings, links, lifecycle explanations and commands should remain intact. The primary reviewer and independent judgement are both retained so you can disagree with either.

In the API reference, several suggestions require source or behavior verification before a wording decision. Baseline YAML lacks generation source provenance. Candidate XML owners are inferred and labelled accordingly; approved future changes belong in owning XML comments followed by regeneration, rather than hand-edited YAML or HTML.

## Coverage and limits

The complete prepared/shared-source pass covers navigation c6, all ten README preservation branches, the independent architecture/layering variants and uncommitted API guidance. The main-source supplement uses documentation main `573f5b2f0e925706311c69b3e70eb0208c569809` plus the exact local software origin/main hashes in the README report. Local software refs were not fetched or claimed current. The live deployed site revision was not established or exhaustively inspected for this wording audit.

`readme.html` is a special exception to generated-copy deduplication: it is a Pandoc-generated legacy assembled guide with unique material, not an output of the current hub README.md. Its original producer invocation/revision is unestablished. The main-delta addendum records that evidence and separates unique legacy prose from material duplicated in current RMarkdown sources.

Search included not/no/never, rather than, instead, unlike, only, just, without, but/however, conditional alternatives, same/different, optionality and related forms. Whole authored sources were read in context. The full API prose-field population was extracted and scanned, with candidate passages evaluated in context. Raw token hits are not findings by themselves. Code negation, shell flags, enum/type names, vendor scripts and ordinary neutral labels are excluded or logged as neutral candidates. No finite lexical scan proves the absence of every unmarked implicit contrast; the inventory records the reviewed population and those limits rather than claiming such a guarantee.

[Source coverage and file hashes](source-coverage.json) and [raw lexical candidates](search-candidates.json) support the snapshot. Raw candidates include code/neutral matches and must not be used as a defect count. Main-snapshot lines and additional script comments are documented in the relevant reports, not all duplicated in this raw search file.

This review does not certify API behavior, hardware support or the currency of historical CI/Sphinx prose. Existing accuracy issues are flagged for their owning review phases. The Core API remains part of shared documentation; Python Sphinx is not being rebuilt. No Cosmic naming migration is proposed.

## Selecting changes

Reply with report-prefixed IDs and keep/rewrite/remove decisions, or identify a group whose recommendation you accept. The next implementation can preserve those decisions on this separate branch. No broad prose edit, source regeneration, main merge or publication follows automatically from this inventory.
