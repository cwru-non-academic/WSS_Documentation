# Concepts

Use this section when you already know you are working with WSS and need to understand how the software stack is organized.

## Start With These

- [Layering Guide (Modules)](conceptual/layering-guide.md)
  - Start here if you need the highest-level mental model of how WSS grows from Core to Params to Model.
- [Core Architecture (Transport, Codec, Core)](conceptual/core-architecture.md)
  - Start here if you need to understand transports, framing, lifecycle, setup sequencing, and streaming behavior.
- [Config Files Reference](conceptual/config-files-reference.md)
  - Start here if your work depends on `stimConfig.json`, `stimParams.json`, or `modelConfig.json`.

## Architecture And Behavior

- [Layering Guide (Modules)](conceptual/layering-guide.md)
  - Explains responsibility boundaries between the core and optional layers.
- [Core Architecture (Transport, Codec, Core)](conceptual/core-architecture.md)
  - Explains how bytes move through the stack and how the core manages runtime behavior.
- [Setup Order and Modification](conceptual/setup-order-and-modification.md)
  - Explains the difference between default startup setup and later setup changes made by an application or integration library.

## Compatibility And Configuration

- [Firmware Compatibility Matrix](conceptual/firmware-compatibility-matrix.md)
  - Summarizes the firmware-version behavior currently documented in the core.
- [Config Files Reference](conceptual/config-files-reference.md)
  - Describes the standard config files and what each one controls.

## When To Leave This Section

- If you are still deciding what kind of work you are doing, go to [Start Here](start-here.md).
- If you need byte-level commands or raw serial communication details, go to [Advanced](advanced.md).

Navigation:

- [Back to Home](index.md)
