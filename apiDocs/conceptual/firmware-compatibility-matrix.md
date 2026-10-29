# Firmware Compatibility Matrix

[Home](../index.md) | [Back to Concepts](../concepts.md)

This page summarizes the firmware-version behavior currently documented in the WSS core.

It is meant to be the human-readable companion to `WSSVersionHandler` and other version-gated behavior in the core client.

## What This Page Covers

This matrix is based on Core source behavior from:

- `WSSVersionHandler`
- `WssClient`
- the command reference where payload shape changes by firmware

It only lists version-gated behavior that is explicitly visible in the Core source.

## How Firmware Selection Works

The core reads the firmware version string from `stimConfig.json` and uses it to create `WSSVersionHandler`.

- supported version strings currently include `H03` and `J03`
- unknown version strings currently fall back to `H03`
- feature-gated behavior is then decided from the selected version

That means the `firmware` field in `stimConfig.json` directly affects which optional behaviors the core will use.

See [Config Files Reference](config-files-reference.md) for where that value is configured.

## Supported Firmware Versions

The currently documented supported firmware versions are:

- `H03`
- `J03`

## Compatibility Matrix

| Firmware | Recognized by Core | Fallback Target for Unknown String | AmplitudeCheck | LED Settings | ModuleQuery | Notes |
|---|---|---|---|---|---|---|
| `H03` | Yes | Yes | Not a current gate | No | No | baseline/default documented behavior |
| `J03` | Yes | No | Not a current gate | Yes | Yes | adds LED-related behavior and ModuleQuery support |

## Feature Notes

### `AmplitudeCheck`

Core registers `LEDSettings` and `ModuleQuery` in `FeatureMinimumVersions`. Unknown feature names return false from `IsFeatureAvailable(...)`. Amplitude checking is not a registered feature gate.

### LED Settings

- Version source: `WSSVersionHandler` and `WssClient`
- Current minimum version: `J03`
- Current documented effect:
  - `CreateContactConfig(...)` accepts and sends `ledByte` only when firmware supports LED settings
  - otherwise the LED value is ignored

This matches the command reference, which documents `ledByte` as `J03+` behavior.

### `ModuleQuery`

- Version source: `WssClient` behavior and generated API docs
- Current availability: `J` or later in the documented client behavior
- Current documented effect:
  - on supported firmware, `ModuleQuery` is sent and the reply is cached
  - on older firmware, no command is sent and default cached settings are seeded instead

This means runtime settings inspection and `ModuleSettings` decoding behave differently depending on firmware.

## What Changes Across Versions

In the currently documented codebase, firmware version affects at least these areas:

- whether certain named features are enabled
- whether LED configuration bytes are included in contact configuration commands
- whether `ModuleQuery` is actually sent or replaced by default seeded settings
- whether higher-level docs should treat a capability as available or unavailable

## Where This Matters

This matrix should be consulted when you are working on:

- `stimConfig.json.firmware`
- setup changes that may become firmware-dependent
- command payloads that vary by version
- `ModuleSettings` behavior and ModuleQuery decoding
- adding support for a new firmware version in `WSSVersionHandler`

## Notes for Contributors

If you add or change firmware support:

1. update `WSSVersionHandler`
2. update this matrix
3. update command docs where payload shape or optional fields change
4. update setup docs if startup behavior becomes version-dependent
5. update troubleshooting docs if the visible behavior changes for users

## Source Basis

This page is currently based on these documented behaviors:

- `WSSVersionHandler.SupportedVersions`: `H03`, `J03`
- `WSSVersionHandler` unknown-string fallback to `H03`
- `WSSVersionHandler` feature gates for `LEDSettings` and `ModuleQuery`
- `WssClient.CreateContactConfig(...)` LED behavior
- `WssClient.ModuleQuery(...)` version-gated behavior

## Related Guides

- [Config Files Reference](config-files-reference.md)
- [Start Here: Minor Core Modifications](start-here-minor-core-modifications.md)
- [Setup Order and Modification](setup-order-and-modification.md)
- [WSS Commands Reference](wss-commands-reference.md)

Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
- [Previous: Setup Order and Modification](setup-order-and-modification.md)
- [Next: Config Files Reference](config-files-reference.md)
