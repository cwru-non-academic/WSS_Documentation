# Start Here: Using an Application

[Home](../index.md) | [Back to Start Here](../start-here.md)

## Who This Is For

This guide is for people who want to use existing WSS functionality through an existing WSS application.

If you want to operate WSS through an existing GUI, CLI, Unity application, ROS node, or other user-facing tool, start here.

## Which Applications Are Available

- Unity application
  - Built on top of the Unity integration library.
  - This is the Unity-facing application path when WSS is being used from within a Unity project or Unity-based workflow.
- C# application
  - Built on top of the C# integration library.
  - This is the .NET application path for desktop tools, experiment software, or other C#-native workflows.
- Python CLI application
  - Built on top of the Python integration library.
  - This is a command-line entry point for initializing WSS, checking status, reloading config, and issuing stimulation commands.
- Python ROS application
  - Built on top of the Python integration library.
  - This is the robotics-oriented application path when WSS needs to be controlled from a ROS workflow.

Applications sit on top of integration libraries. The integration library decides which WSS core and layer functionality is exposed to the application, and the application decides how a user interacts with it.

## How To Use the Config Files

Many WSS applications rely on a `Config/` directory to define how the core, parameter layer, and model layer behave. In the Python CLI application, the default config directory contains three files:

- `stimConfig.json`
- `stimParams.json`
- `modelConfig.json`

Together, these files define the hardware/core setup, per-channel stimulation behavior, and optional model-layer behavior.

The application usually decides where the config files live and passes that location down to the integration library.

Integration libraries usually default to using a root-level config location when the application does not provide one explicitly.

If the application points to a config location and the files do not exist yet, the library stack will usually auto-generate the config files there with default values. Those files can then be edited and reloaded later.

- `stimConfig.json`
  - Core-level runtime configuration.
  - Defines the number of WSS units, firmware target, and amplitude-curve behavior.
- `stimParams.json`
  - Per-channel stimulation parameter configuration.
  - Defines how channel-level stimulation values and limits are interpreted.
- `modelConfig.json`
  - Model-layer configuration.
  - Defines the active controller mode and the constants used by that mode.

For a full reference of each config file and every section inside it, see [Config Files Reference](config-files-reference.md).

When working with an application:
- start from the example or default config shipped with that application
- change only the values you need for your device and workflow
- keep one config set per common setup so it is easy to reuse and compare
- treat `stimConfig.json`, `stimParams.json`, and `modelConfig.json` as related pieces of one runtime configuration rather than unrelated files

## First Connection Flow

The typical startup flow is:

1. Choose the application that matches your workflow.
2. Review and update the config files for your hardware and stimulation setup.
3. Launch the application and call `Initialize()`.
4. During initialization, the application creates the configured WSS stack and starts the connection/setup process.
5. Setup commands are loaded into each WSS so the expected startup state and programming are applied.
6. If setup completes successfully, the system moves into `Ready` and then into active stimulation/streaming behavior.
7. `StartStim(...)` is part of the normal setup flow, so startup stimulation is typically scheduled automatically during initialization.
8. After initialization, the application continues advancing the system through `Tick()`.
9. The application decides the tick interval, and that interval controls how often the periodic update loop runs.
10. When the application shuts down, it calls `Shutdown()`.
11. `Shutdown()` stops stimulation, releases transport and connection resources, and cleans up the running stack.

If you are not sure what happens under the hood, see [Layering Guide (Modules)](layering-guide.md).

## Changing Setup After Startup

If you need to modify setup from within an application, see [Start Here: Developing an Application](start-here-developing-an-application.md).

That guide covers the application-level path for changing setup after startup and points to [Setup Order and Modification](setup-order-and-modification.md) for the setup dependency rules.

## Important API Functions

In many applications these functions are wrapped by buttons, commands, menus, or scripts. The most useful thing here is not to repeat their full documentation, but to point you to the original API pages where parameters, return values, and behavior are already documented.

### Application-facing entry points

- Initialization and lifecycle
  - [Initialize()](xref:HFI.Wss.StimulationController.Initialize)
  - [Ready()](xref:HFI.Wss.StimulationController.Ready)
  - [Shutdown()](xref:HFI.Wss.StimulationController.Shutdown)
- Starting stimulation
  - [StartStimulation()](xref:HFI.Wss.StimulationController.StartStimulation)
  - Under the hood, startup also relies on core-level `StartStim(...)` during the setup sequence.
- Direct stimulation and validation
  - [StimulateAnalog(string, int, int, int)](xref:HFI.Wss.StimulationController.StimulateAnalog(System.String,System.Int32,System.Int32,System.Int32))
  - [IsFingerValid(string)](xref:HFI.Wss.StimulationController.IsFingerValid(System.String))
- Reloading configuration
  - [LoadCoreConfigFile()](xref:HFI.Wss.StimulationController.LoadCoreConfigFile)
  - [LoadParamsJson()](xref:HFI.Wss.StimulationController.LoadParamsJson)
  - [LoadParamsJson(string)](xref:HFI.Wss.StimulationController.LoadParamsJson(System.String))

### Useful stimulation-parameter APIs

These are useful when the selected application or integration library exposes parameter editing or inspection directly:

- [AddOrUpdateStimParam(string, float)](xref:HFI.Wss.StimulationController.AddOrUpdateStimParam(System.String,System.Single))
- [GetAllStimParams()](xref:HFI.Wss.StimulationController.GetAllStimParams)
- [GetStimIntensity(string)](xref:HFI.Wss.StimulationController.GetStimIntensity(System.String))
- [GetStimParam(string)](xref:HFI.Wss.StimulationController.GetStimParam(System.String))
- [StimulateNormalized(string, float)](xref:HFI.Wss.StimulationController.StimulateNormalized(System.String,System.Single))

For channel-level parameter behavior in the calibration layer, see:

- [GetChannelAmpMode(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelAmpMode(System.Int32))
- [GetChannelDefault(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelDefault(System.Int32))
- [GetChannelMax(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelMax(System.Int32))
- [GetChannelMin(int)](xref:Wss.CalibrationModule.IStimParamsCore.GetChannelMin(System.Int32))

For model-layer channel behavior, see:

- [ModelParamsLayer.GetChannelAmpMode(int)](xref:Wss.ModelModule.ModelParamsLayer.GetChannelAmpMode(System.Int32))

For lower-level core behavior that applications often rely on indirectly, see:

- [IStimulationCore.IsChannelInRange(int)](xref:Wss.CoreModule.IStimulationCore.IsChannelInRange(System.Int32))
- [IStimulationCore.LoadConfigFile()](xref:Wss.CoreModule.IStimulationCore.LoadConfigFile)
- [IStimulationCore.StimulateAnalog(int, int, float, int)](xref:Wss.CoreModule.IStimulationCore.StimulateAnalog(System.Int32,System.Int32,System.Single,System.Int32))
- [StimParamsLayer.ZeroOutStim(WssTarget)](xref:Wss.CalibrationModule.StimParamsLayer.ZeroOutStim(Wss.CoreModule.WssTarget))
- [IStimulationCore.StartStim(WssTarget)](xref:Wss.CoreModule.IStimulationCore.StartStim(Wss.CoreModule.WssTarget))

## Next Steps

- If you want to build your own application on top of an existing integration library, continue to [Start Here: Developing an Application](start-here-developing-an-application.md).
- If you need a new language or platform integration, continue to [Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Next: Start Here: Developing an Application](start-here-developing-an-application.md)
