# Start Here: Building a New Integration Library

[Home](../index.md) | [Back to Start Here](../start-here.md)

## Who This Is For

This guide is for people exposing WSS functionality to a new language or platform.

Use this path when an existing integration library does not fit your target environment and you need to build a new reusable integration layer.

Before choosing a transport or host-specific implementation, see [Choosing a Runtime for an Integration Library](integration-library-runtime-selection.md) to decide whether `.NET Standard 2.0`, `.NET Framework 4.8`, or `.NET 8` is the right fit.

Examples include a new Python wrapper, a Unity-facing library, a C#-native integration layer, or a future library for another runtime.

## What an Integration Library Usually Owns

When you are building an integration library, the application should not need to understand the internal WSS stack in detail. The integration library usually owns:

- language or platform adaptation
  - exposing WSS behavior in a way that feels native to the target environment
- composition of the WSS stack
  - choosing the transport, core, optional layers, and optional capabilities to attach
- lifecycle forwarding
  - surfacing `Initialize()`, `Tick()`, `Shutdown()`, readiness, and stimulation entry points
- configuration plumbing
  - accepting a config location from the application and wiring it into the underlying stack
- safe API exposure
  - deciding which WSS capabilities should become public in the target environment

An application built on top of the integration library should mostly focus on workflow, UX, automation, and timing rather than stack composition.

## Minimum Building Blocks

The smallest useful integration library is usually built from these parts:

- `ITransport`
  - Moves bytes between your environment and the device.
- `WssStimulationCore`
  - Coordinates connection state, setup, queued edits, and streaming.
- optional layers
  - Add reusable capability sets such as Params or Model.
- optional `IBasicStimulation`
  - Adds higher-level stimulation features when the target firmware and hardware support them.

The integration library decides how much of this stack should be exposed directly and how much should stay hidden behind a smaller public API.

## Config Files in an Integration Library

Integration libraries should usually accept a config location from the application rather than hard-code where the config files live.

In most cases:

- the application decides the config location
- the integration library passes that path into the WSS stack
- if the application does not provide a location, the integration library can fall back to a default root-level config directory
- if the files do not exist yet, the library stack will usually auto-generate them with default values

That means a new integration library should support both:

- first-run startup where config files are created automatically
- later reload/update workflows where the application edits and reloads those files

If your integration library uses the standard WSS config directory layout, the main files are:

- `stimConfig.json`
  - core-level runtime configuration
- `stimParams.json`
  - per-channel stimulation parameter configuration
- `modelConfig.json`
  - model-layer configuration

For a full reference of each config file and every section inside it, see [Config Files Reference](config-files-reference.md).

## Which Capabilities Should Usually Be Surfaced

Most integration libraries should expose capability groups instead of forcing applications to compose the raw stack themselves.

Common capability groups are:

- lifecycle and status
  - initialize, tick, ready, started, shutdown
- direct stimulation
  - start, stop, direct analog stimulation, zeroing behavior
- stimulation-parameter access
  - channel defaults, limits, parameter reads, parameter writes, normalized stimulation
- model-driven stimulation
  - model-layer operations when the library includes the Model layer
- optional higher-level stimulation features
  - waveform, event, save/load, or query functionality when `IBasicStimulation` is supported

Ideally, the integration library exposes all public functionality that is valid for the selected combination of core, layers, and optional capabilities.

## Suggested Order

1. Pick a transport.
2. Create the core.
3. Attach optional layers.
4. Decide which configuration path the application will provide and how the library should default it.
5. Decide which functionality your integration library should expose.
6. Surface a clean language- or platform-specific API.
7. Validate the lifecycle with `Initialize()`, `Tick()`, and `Shutdown()`.

## Transport First

Your transport choice determines how the rest of the stack will reach the device. Keep transport responsibilities narrow: connect, disconnect, send bytes, and surface received bytes.

`ITransport` is separated from the rest of the stack because transport support is often platform-specific. A transport that works well in one environment may not be portable to another, and trying to force one transport implementation across every language and platform is usually not realistic.

This is one of the main jobs of an integration library. The core and layers can stay reusable while the integration library provides the transport implementation that makes sense for its target environment.

For example, a Bluetooth transport written for one C# environment may not transfer cleanly into Unity on Android. In that case, the integration library can provide its own `ITransport` implementation using a Bluetooth library that is specific to Unity and Android. Once that implementation satisfies `ITransport`, it can be plugged into the existing WSS core and layers without changing the rest of the stack.

That means a new integration library can introduce a completely new transport protocol or platform-specific transport path while still reusing the same core, params layer, and model layer behavior above it.

## Create the Core

`WssStimulationCore` is the main orchestration point. Once the transport is wired in, the core can manage setup sequencing, device state, and streaming behavior.

## Attach Optional Layers

Layers add reusable higher-level capabilities on top of the core.

Current examples:
- Params layer
  - central parameter definitions, defaults, and validation
- Model layer
  - model-specific configuration and constraints

Future layers should follow the same pattern: add focused reusable behavior without mixing transport mechanics into the layer itself.

## Build the Lifecycle First

Before widening the public API surface, confirm that the basic lifecycle is correct:

1. `Initialize()` starts connection and setup.
2. `Tick()` advances state when your integration uses a polling or update loop.
3. Wait for readiness and confirm the selected layers are loaded correctly.
4. Verify a safe stimulation path.
5. `Shutdown()` stops stimulation activity and disconnects cleanly.

In practice, a good first milestone for a new integration library is a minimal wrapper that can:

- create the stack from a provided config location
- initialize
- tick on a stable interval
- report ready/not-ready state
- perform one safe stimulation call
- shut down cleanly

Once this works reliably, expose the rest of the functionality your target environment needs.

## Setup Exposure Matters

An integration library should decide deliberately whether applications are allowed to change setup after the initial core setup completes.

If the integration library exposes enough setup-related methods, an application can clear, recreate, and reconfigure the active setup later without modifying the core itself. That is different from changing the core's default startup setup.

The important distinction is:

- `WssStimulationCore.NormalSetup()` owns the automatic initial setup path
- an integration library can also expose setup commands so applications can build a new setup later during a session

See [Setup Order and Modification](setup-order-and-modification.md) for the current initial setup order and the dependency rules your exposed setup methods should preserve.

## Important API Areas

The most useful thing in this guide is to point you to the core API areas that a new integration library will usually wrap or surface.

### Core lifecycle and stimulation

- [IStimulationCore.LoadConfigFile()](xref:Wss.CoreModule.IStimulationCore.LoadConfigFile)
- [IStimulationCore.IsChannelInRange(int)](xref:Wss.CoreModule.IStimulationCore.IsChannelInRange(System.Int32))
- [IStimulationCore.StimulateAnalog(int, int, float, int)](xref:Wss.CoreModule.IStimulationCore.StimulateAnalog(System.Int32,System.Int32,System.Single,System.Int32))
- [IStimulationCore.StartStim(WssTarget)](xref:Wss.CoreModule.IStimulationCore.StartStim(Wss.CoreModule.WssTarget))

### Params-layer APIs

- [GetChannelAmpMode(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelAmpMode(System.Int32))
- [GetChannelDefault(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelDefault(System.Int32))
- [GetChannelMax(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelMax(System.Int32))
- [GetChannelMin(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelMin(System.Int32))
- [StimParamsLayer.ZeroOutStim(WssTarget)](xref:Wss.CalibrationModule.StimParamsLayer.ZeroOutStim(Wss.CoreModule.WssTarget))

### Model-layer APIs

- [ModelParamsLayer.GetChannelAmpMode(int)](xref:Wss.ModelModule.ModelParamsLayer.GetChannelAmpMode(System.Int32))

### Optional higher-level stimulation features

If your integration library also exposes `IBasicStimulation` behavior, use the layering guide and core architecture guide to decide how waveform, event, and config-query operations should appear in the target language or platform.

## Related Guides

- [Start Here: Developing an Application](start-here-developing-an-application.md)
- [Core Architecture (Transport, Codec, Core)](core-architecture.md)
- [Layering Guide (Modules)](layering-guide.md)

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Previous: Start Here: Developing an Application](start-here-developing-an-application.md)
- [Next: Start Here: Minor Core Modifications](start-here-minor-core-modifications.md)
