# Start Here: Developing an Application

[Home](../index.md) | [Back to Start Here](../start-here.md)

## Who This Is For

This guide is for people building a new application on top of an existing WSS integration library.

Examples include a GUI, CLI, ROS node, experiment runner, or other user-facing tool that uses an existing Python, C#, Unity, or future integration library.

## Which Integration Libraries Are Available

- Unity integration library
  - Used when your application lives inside a Unity project or Unity-centered workflow.
- C# integration library
  - Used when your application is a .NET-native tool or service.
- Python integration library
  - Used when your application is built around Python workflows, scripting, or robotics tooling.

An integration library selects which WSS functionality to surface from the core and optional layers, then adapts it to a language or platform.

An application uses that integration library to provide a concrete user experience.

## What an Application Usually Owns

When you are developing an application, the integration library should already handle the language/platform adaptation. Your application usually owns:

- the user interaction model
  - GUI, CLI, ROS topics/services, scripts, or experiment automation
- lifecycle timing
  - when initialization happens, how often updates occur, and when shutdown happens
- workflow logic
  - what actions happen in what order for your experiment, session, or tool
- configuration management
  - which config set gets loaded and when it gets reloaded or saved
- guardrails for users
  - validation, status display, mode checks, and safe shutdown behavior

The integration library owns the WSS-facing API surface. The application decides how that API is presented and used.

## Which APIs Are Usually Exposed

An integration library typically exposes:
- lifecycle control such as `Initialize()`, `Tick()`, and `Shutdown()`
- stimulation control such as `StartStim(...)` and `StopStim(...)`
- selected parameter or waveform operations when optional functionality is included
- configuration loading or saving when the chosen layers support it
- integration libraries also decide which transport protocols are available

The exact API surface depends on which WSS layers the integration library attaches.

## Config Files in an Application

Most applications should treat configuration as part of the application workflow, not as a one-time setup detail.

The application usually decides where the config files live and passes that location down to the integration library.

Integration libraries usually default to using a root-level config location when the application does not provide one explicitly.

If the application points to a config location and the files do not exist yet, the library stack will usually auto-generate the config files there with default values. The application can then modify those files and reload them later.

Common config responsibilities in an application are:

- selecting the active config set for the current device or experiment
- loading configuration before initialization
- allowing safe reload when settings change
- keeping hardware setup, stimulation parameters, and model behavior consistent with each other

If your application uses the standard WSS config directory layout, the main files are:

- `stimConfig.json`
  - core-level runtime configuration
- `stimParams.json`
  - per-channel stimulation parameter configuration
- `modelConfig.json`
  - model-layer configuration

For a full reference of each config file and every section inside it, see [Config Files Reference](config-files-reference.md).

## Which Capabilities Are Usually Surfaced

When reading or extending an existing integration library, the important application-facing capability groups are usually:

- core lifecycle and connection state
  - initialize, tick, ready, started, shutdown
- direct stimulation control
  - start, stop, direct analog stimulation, zeroing/stopping behavior
- stimulation-parameter access
  - reading limits/defaults and updating per-channel parameters
- model-driven stimulation
  - using model-layer logic when the integration library includes it
- optional higher-level stimulation features
  - waveform, event, or config helpers when a BASIC-style capability is attached

The integration library decides which of these become public API, and your application decides which of them become part of the user workflow. Ideally, an integration library exposes all available public methods for the supported layers.

## Setup Changes After Startup

Applications do not always need to accept the setup created by the core during initialization.

If the integration library exposes enough setup-related methods, the application can issue setup commands after startup to replace or override the active setup. That means a whole new setup can be created at the application level without modifying the core itself.

The important distinction is:

- the core owns the default initial setup that happens automatically during startup
- the application can still change setup later if the integration layer exposes the required methods

See [Setup Order and Modification](setup-order-and-modification.md) for the current initial setup order and the dependency rules that should still be respected when changing setup later.

## Build the Lifecycle First

Before building UI, command parsing, ROS plumbing, or experiment automation, get the application lifecycle working end to end.

Most applications should be understood in terms of this flow:

1. `Initialize()`
   - Begin connection and device setup through the integration library.
2. `Tick()`
   - Advance the state machine when the integration uses a polling or update model.
3. wait for readiness
   - Confirm the stack reaches a usable state before allowing stimulation actions.
4. perform application actions
   - start stimulation, stop stimulation, load config, edit parameters, or query state.
5. `Shutdown()`
   - stop activity and release resources safely.

In practice, a good first milestone is a small application that can:

- initialize
- tick on a stable interval
- report ready/not-ready state
- trigger one safe stimulation action
- shut down cleanly

Once that works, add the rest of your application-specific UX or workflow logic.

## Important API Areas

The most useful thing in this guide is to point you to the API areas you will likely wire into your application rather than restating full signatures here.

### Integration-library entry points

- Lifecycle and status
  - [Initialize()](xref:HFI.Wss.StimulationController.Initialize)
  - [Ready()](xref:HFI.Wss.StimulationController.Ready)
  - [Shutdown()](xref:HFI.Wss.StimulationController.Shutdown)
- Application-driven stimulation
  - [StartStimulation()](xref:HFI.Wss.StimulationController.StartStimulation)
  - [StimulateAnalog(string, int, int, int)](xref:HFI.Wss.StimulationController.StimulateAnalog(System.String,System.Int32,System.Int32,System.Int32))
  - [IsFingerValid(string)](xref:HFI.Wss.StimulationController.IsFingerValid(System.String))
- Config reload and parameter refresh
  - [LoadCoreConfigFile()](xref:HFI.Wss.StimulationController.LoadCoreConfigFile)
  - [LoadParamsJson()](xref:HFI.Wss.StimulationController.LoadParamsJson)
  - [LoadParamsJson(string)](xref:HFI.Wss.StimulationController.LoadParamsJson(System.String))

### Stimulation-parameter APIs

These are useful when your application exposes editing, presets, inspection, or calibration-oriented tooling:

- [AddOrUpdateStimParam(string, float)](xref:HFI.Wss.StimulationController.AddOrUpdateStimParam(System.String,System.Single))
- [GetAllStimParams()](xref:HFI.Wss.StimulationController.GetAllStimParams)
- [GetStimIntensity(string)](xref:HFI.Wss.StimulationController.GetStimIntensity(System.String))
- [GetStimParam(string)](xref:HFI.Wss.StimulationController.GetStimParam(System.String))
- [StimulateNormalized(string, float)](xref:HFI.Wss.StimulationController.StimulateNormalized(System.String,System.Single))

For channel-level behavior in the params layer, see:

- [GetChannelAmpMode(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelAmpMode(System.Int32))
- [GetChannelDefault(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelDefault(System.Int32))
- [GetChannelMax(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelMax(System.Int32))
- [GetChannelMin(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelMin(System.Int32))

### Lower-level behavior your application may depend on

- [IStimulationCore.StartStim(WssTarget)](xref:Wss.CoreModule.IStimulationCore.StartStim(Wss.CoreModule.WssTarget))
- [IStimulationCore.LoadConfigFile()](xref:Wss.CoreModule.IStimulationCore.LoadConfigFile)
- [IStimulationCore.IsChannelInRange(int)](xref:Wss.CoreModule.IStimulationCore.IsChannelInRange(System.Int32))
- [StimParamsLayer.ZeroOutStim(WssTarget)](xref:Wss.CalibrationModule.StimParamsLayer.ZeroOutStim(Wss.CoreModule.WssTarget))

If you are not sure how these capabilities are composed underneath the integration library, see [Layering Guide (Modules)](layering-guide.md).

## Next Steps

- If you need to create a new integration library for another language or platform, continue to [Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md).
- If you need to understand how the existing architecture behaves underneath your application, see [Layering Guide (Modules)](layering-guide.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Previous: Start Here: Using an Application](start-here-using-an-application.md)
- [Next: Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md)
