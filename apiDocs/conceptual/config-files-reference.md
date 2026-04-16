# Config Files Reference

[Home](../index.md) | [Back to Concepts](../concepts.md)

This reference describes the configuration files used by WSS applications that rely on a `Config/` directory.

In the Python CLI application, the default config directory contains:

- `stimConfig.json`
- `stimParams.json`
- `modelConfig.json`

Together, these files define the hardware/core setup, per-channel stimulation behavior, and optional model-layer behavior.

## `stimConfig.json`

This is the core configuration file. It controls how many WSS units the application expects, which firmware behavior to target, and whether custom amplitude conversion curves should be used.

Sections:
- `maxWSS`
  - The maximum number of WSS devices the application should manage.
  - This also determines how many total channels exist in the running system.
- `firmware`
  - The firmware version string used by the core when it creates the WSS version handler.
  - See [Firmware Compatibility Matrix](firmware-compatibility-matrix.md) for the currently documented supported versions and version-gated features.
- `useConfigAmpCurves`
  - Controls whether amplitude conversion should come from this file or from the built-in/default mapping.
- `ampCurves`
  - Per-WSS amplitude curve parameters.
  - Each entry defines the piecewise mapping from amplitude in mA to the device-side drive value.
  - The fields are:
    - `LowThreshold`
    - `LowConst`
    - `ExpPower`
    - `LinearOffset`
    - `LinearSlope`

## `stimParams.json`

This is the stimulation-parameters configuration file. It is mainly used by the calibration/params layer to define how each channel should behave.

Sections:
- `stim`
  - Root stimulation-parameter section.
- `stim.ch`
  - Per-channel parameter table.
  - Each numbered child such as `1`, `2`, or `3` defines one stimulation channel.
- Per-channel fields
  - `ampMode`
    - Whether the channel is controlled through pulse width (`PW`) or pulse amplitude (`PA`).
  - `minPW` and `maxPW`
    - Pulse-width limits used when the channel is operating in `PW` mode.
  - `minPA` and `maxPA`
    - Amplitude limits used when the channel is operating in `PA` mode.
  - `defaultPA`
    - Default amplitude used when pulse width is the actively controlled quantity.
  - `defaultPW`
    - Default pulse width used when pulse amplitude is the actively controlled quantity.
  - `IPI`
    - Inter-pulse interval for the channel.

In general, this file defines how normalized or higher-level stimulation requests get translated into channel-specific stimulation values.

## `modelConfig.json`

This is the model-layer configuration file. It defines the controller mode and the constants used by the model layer before requests are passed down into the stimulation-parameters layer.

Sections:
- `calib`
  - Contains the active controller mode.
- `calib.mode`
  - The model/controller mode to use.
  - Current examples are `P` and `PD`.
- `constants`
  - Controller constants used by the selected model mode.
  - Current fields include:
    - `PModeProportional`
    - `PModeOffset`
    - `PDModeProportional`
    - `PDModeDerivative`
    - `PDModeOffset`

In general, this file defines how application input is transformed by the model layer before it becomes stimulation output.

Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
- [Related: Start Here: Using an Application](start-here-using-an-application.md)
