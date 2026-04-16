# Choosing a Runtime for an Integration Library

[Home](../index.md) | [Back to Start Here](../start-here.md) | [Back to Building a New Integration Library](start-here-building-a-new-integration-library.md)

## Why This Page Exists

When you are choosing or building a WSS integration library, the target runtime affects compatibility, transport support, and whether the result can still be used from Unity.

Use this page to pick the most appropriate runtime before you commit to a transport or host environment.

## Quick Selection Guide

| Target | Best for | Notes |
|---|---|---|
| `.NET Standard 2.0` | Broad compatibility and the base WSS core | Best when you want the most portable option and do not need a transport-specific integration bundled into the base library. |
| `.NET Framework 4.8` | Serial transport and Unity-compatible workflows | Use this path when you need the current serial transport support and need to stay compatible with Unity-based integrations. |
| `.NET 8` | Newer host environments and the BLE-oriented integration path | This is the newer integration target. Do not choose this path if Unity compatibility is required. |

## Runtime Choices

### `.NET Standard 2.0`

This is the most compatible starting point for the shared WSS core.

- The current core project targets `.NET Standard 2.0`.
- This is the best choice when you want a base library that works across older and newer .NET environments.
- It is a good fit when you want the reusable WSS logic without committing to a specific transport-specific integration package.

In practice, choose this when portability matters more than bundled transport support.

### `.NET Framework 4.8`

This is the current transport-specific choice for the serial path.

- The serial transport project currently targets `.NET Framework 4.8`.
- This is the safest choice when serial transport support is required and Unity compatibility still matters.
- The current Unity-facing integration path uses the serial transport.

If your priority is Unity compatibility, avoid jumping to the newer runtime path unless you are sure Unity support is no longer needed.

### `.NET 8`

This is the newer integration target.

- The current C# integration library targets `.NET 8`.
- This is the direction to use for newer integration work, including the BLE-oriented path.
- This path should not be treated as Unity-compatible.

If you choose this path for BLE or other newer host environments, document clearly that Unity is no longer a supported target for that integration.

## Recommended Decision Pattern

- Choose `.NET Standard 2.0` for the most reusable and broadly compatible base library.
- Choose `.NET Framework 4.8` when you need serial transport support and Unity compatibility.
- Choose `.NET 8` when you need the newer integration path and do not need Unity compatibility.

## Verified Current Targets

These statements match the projects currently referenced by this documentation workspace:

- `WSS_Core_Interface.csproj` targets `.NET Standard 2.0`.
- `WSS.Transport.Serial.csproj` targets `.NET Framework 4.8`.
- `HFI_WSS_Csharp_Implementation.csproj` targets `.NET 8`.

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Back to Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md)
