# Testing and deployment

[Home](../index.md) | [Back to Concepts](../concepts.md)

A WSS application combines an application revision, an integration revision, Core assemblies, transport backends and configuration. Validate that combination before distributing it. Exact build and startup commands live in each [repository README](repository-and-kit-links.md).

## What each check establishes

| Check | Establishes | Does not establish |
| --- | --- | --- |
| Source and API checks | Commands, signatures and configuration paths match the selected source | Runtime startup |
| Simulator startup | Initialization, update scheduling, readiness handling and cleanup without hardware | Radio/serial behavior or physical stimulation |
| Canonical conformance | The tested implementation meets the fixture assertions for the tested candidate | Execution in another host runtime |
| Native host check | The same candidate loads and runs in MATLAB, Unity, Python or .NET as recorded | Hardware/adapter behavior without a device test |
| Device validation | The specific hardware, firmware, host and transport work together in the recorded setup | Certification or coverage of untested combinations |

## Application test sequence

1. Start from a recursive clone at pinned revisions and follow the README prerequisites.
2. Run the explicit simulator example with an isolated configuration directory.
3. Check a bounded readiness wait, useful error reporting and clean shutdown.
4. Run that repository's automated tests and retain the candidate revision, runtime and results.
5. Validate native runtime loading and packaging on each distribution target.
6. Validate the required physical setup under the project's device-testing procedure before making hardware claims.

A passing .NET test for a Unity-facing assembly does not establish Unity Editor or player execution. A MATLAB wrapper build does not establish a native MATLAB session. Preserve this distinction in evidence.

## Distribution

Ship matching integration, Core and backend dependencies as a complete bundle, including native runtime assets where required. Follow the host README's deployment layout. Keep configuration location explicit, record firmware selection, and exercise shutdown in the packaged application.

For automated dependency changes, see [CI and dependency updates](../ci.md). That process evaluates candidate revisions and produces reviewable changes; its trust and approval contracts are separate from application startup. Documentation and software releases require maintainer review of the [1.0 readiness checklist](release-readiness.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
- [Platform support](platform-support.md)
