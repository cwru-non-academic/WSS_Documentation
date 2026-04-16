# Start Here: Minor Core Modifications

[Home](../index.md) | [Back to Start Here](../start-here.md)

## Who This Is For

This guide is for contributors making focused changes inside the existing WSS core without introducing a new layer or redesigning the overall architecture.

Use this path when you need to make a targeted change such as:

- adding a new supported firmware version
- updating an existing message definition
- changing the numeric limits or bounds for an existing message
- updating how module settings are decoded from a device reply
- changing the order or content of the initial setup sequence

## What Counts As a Minor Core Modification

Minor core modifications usually keep the current architecture intact and change one well-defined part of the shared core behavior.

Common examples are:

- extending `WSSVersionHandler` for a new firmware version
- updating `WSSMessageIDs` or the payload expectations for an existing command
- adjusting `WSSLimits` when firmware or protocol bounds change
- updating `ModuleSettings` decoding when a `ModuleQuery` payload changes
- changing `NormalSetup()` so startup sends a different ordered set of startup setup steps

## What Does Not Count

This is not the right path when you are:

- adding a new optional layer
- changing the transport abstraction or creating a new transport for another platform
- redesigning lifecycle ownership or streaming ownership
- exposing WSS to a new language or runtime
- making a large architectural change that affects how capabilities are composed

If your change is broader than a targeted core update, use [Start Here: Adding Layers or Core Functionality](start-here-adding-layers-or-core-functionality.md) instead.

## Start With These Questions

Before editing the core, answer these questions first:

1. Is this change firmware-specific or should it apply to all supported devices?
2. Does it change message layout, version gating, runtime limits, decoded settings, or setup order?
3. Does it affect startup only, or also mid-stream edits and runtime behavior?
4. Does the change require documentation updates in firmware compatibility, config docs, command docs, or troubleshooting?

Those questions usually tell you which core class should own the change.

## Which Part Owns the Change

The most important thing in a minor modification is putting the change in the correct place.

- `WSSVersionHandler`
  - Owns firmware parsing, supported-version checks, and feature-gating behavior.
  - Use this when a feature becomes available only in a newer firmware version.
- `WSSMessageIDs`
  - Owns protocol command identifiers.
  - Use this when the on-wire command code itself changes or a new message ID is added.
- `WSSLimits`
  - Owns shared protocol and device numeric bounds.
  - Use this when maximum values such as pulse width, IPD, waveform amplitude, or field sizes change.
- `WssFrameCodec`
  - Owns framing, escaping, deframing, and checksum validation.
  - Use this only when the wire format itself changes.
- `WssClient`
  - Owns low-level command construction, sending, and reply correlation.
  - Use this when an existing command payload or reply handling changes.
- `ModuleSettings`
  - Owns decoding and exposing the meaning of a `ModuleQuery` settings payload.
  - Use this when the device reports settings differently or exposes new settings fields.
  - That decoded snapshot includes hardware configuration flags, PA and PW limits, thresholds for battery and impedance, and a derived amplitude-curve choice such as `10mA` vs `72mA`.
- `WssStimulationCore`
  - Owns setup orchestration, state transitions, queued setup changes, and streaming behavior.
  - Use this when you need to change startup setup order, scheduling behavior, or lifecycle handling.

## Common Change Types

### Adding a New Firmware Version

Use this path when the protocol is mostly the same, but the core needs to recognize a new firmware string and gate features correctly.

Typical areas involved:

- `WSSVersionHandler`
- possibly `WssClient` if a command is only valid in that version
- firmware compatibility documentation

### Updating an Existing Message

Use this when the command already exists, but one of these changes:

- payload fields
- reply expectations
- a selector or subcommand meaning
- a version-gated feature for that message

Typical areas involved:

- `WSSMessageIDs` if the command code changed
- `WssClient` if framing of the payload or reply handling changed
- `wssCommands.Rmd` so the reference matches the code
- `WSSVersionHandler` if the change is firmware-specific

### Changing Limits or Bounds

Use this when an existing command or parameter keeps the same meaning but the valid numeric range changes.

Typical areas involved:

- `WSSLimits`
- any validation paths that rely on those limits
- config reference and config examples docs
- troubleshooting notes if old values now fail validation

### Updating Module Settings Decoding

Use this when queried device settings need to be interpreted differently.

Typical areas involved:

- `ModuleSettings`
- `WssClient.ModuleQuery(...)` only if the request or reply handling changed
- firmware compatibility docs if the payload meaning varies by firmware

### Changing Setup

Use this when startup needs to send a different ordered set of setup actions or when a new step must be added to initialization.

Typical areas involved:

- `WssStimulationCore.NormalSetup()`
- the queued setup runner behavior in the core
- command docs if the startup sequence now depends on a different command order
- troubleshooting docs if setup failures or readiness conditions change

See [Setup Order and Modification](setup-order-and-modification.md) for the reusable setup-order guide and for the distinction between initial core setup and later setup replacement from an integration library or application.

## Changing Setup: What Owns It

Setup is owned by `WssStimulationCore`, not by `ModuleSettings`.

`NormalSetup()` is the main entry point for the initial startup sequence. It seeds the per-target setup step lists for a full initial configuration and starts the setup runner. This happens after connection succeeds and before the system becomes `Ready`.

That does not mean every setup change belongs in the core. The core only owns the default initial setup path. If an integration library or application exposes the necessary setup-related methods, it can also replace or override setup later by sending the right command sequence after startup.

That means setup changes belong in the core orchestration logic whenever you need to:

- insert a new setup step
- remove a step
- reorder setup steps
- make setup version-dependent
- change which commands must complete before `Ready`

The setup order, dependency rules, and the distinction between initial setup and later setup replacement are documented in [Setup Order and Modification](setup-order-and-modification.md).

## Quickstart for a Minor Core Modification

If you are making a small targeted change, a good order is:

1. Decide whether the change is about versioning, message shape, limits, settings decoding, or setup.
2. Identify the owning class before editing anything.
3. Change the smallest relevant code path.
4. Check whether the change also needs a version gate.
5. Update the matching documentation page.
6. Verify that initialization, readiness, and shutdown semantics still make sense.

## Documentation to Update Alongside the Code

Depending on the change type, update the matching docs as part of the same work:

- firmware changes
  - firmware compatibility matrix
- message changes
  - `WSS Commands Reference`
- limit changes
  - config reference and config examples/ranges
- setup changes
  - lifecycle/state docs, troubleshooting docs, and this page if the documented setup order changes
- module settings changes
  - firmware compatibility notes and any diagnostics-oriented docs

## Related Guides

- [Core Architecture (Transport, Codec, Core)](core-architecture.md)
- [Layering Guide (Modules)](layering-guide.md)
- [Firmware Compatibility Matrix](firmware-compatibility-matrix.md)
- [Config Files Reference](config-files-reference.md)
- [Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md)
- [Start Here: Adding Layers or Core Functionality](start-here-adding-layers-or-core-functionality.md)

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Previous: Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md)
- [Next: Start Here: Adding Layers or Core Functionality](start-here-adding-layers-or-core-functionality.md)
