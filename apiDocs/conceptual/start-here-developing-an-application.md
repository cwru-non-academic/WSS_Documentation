# Developing a WSS application

[Home](../index.md) | [Back to Start Here](../start-here.md)

Build on the integration library for your host. Its README owns installation commands, submodule paths, runtime prerequisites and a minimal example:

- [C# integration](xref:repo.csharp.readme), used by the [C# CLI](xref:repo.csharp-cli.readme).
- [Python integration](xref:repo.python.readme), used by the [Python CLI](xref:repo.python-cli.readme) and [ROS driver](xref:repo.python-ros.readme).
- [Unity integration](xref:repo.unity.readme), used by the [Unity GUI](xref:repo.unity-app.readme).
- [MATLAB integration](xref:repo.matlab.readme), used by the [MATLAB CLI](xref:repo.matlab-cli.readme).

## Responsibilities

The integration adapts Core and optional parameter/model layers to its host. Your application owns user interaction, configuration selection, update scheduling, readiness display and shutdown. See [layering](layering-guide.md) for the division of reusable behavior.

Keep one tested set of pinned integration files and runtime dependencies. A Git submodule records a revision; initialize it recursively after cloning, and review dependency updates alongside their tests. Do not replace one DLL in a runtime bundle without checking the matching dependencies.

## Build the lifecycle first

1. Construct the integration with an explicit configuration directory and simulator/test mode.
2. Initialize the stack using the selected library's API.
3. Advance its update loop as required by that host. Unity, MATLAB and CLI applications schedule updates differently; do not add a second update loop around a controller that already owns one.
4. Observe the usable state documented by the integration with a bounded wait and handle failure or timeout. Core Ready can be transient before Started/Streaming.
5. Expose actions only when the corresponding state permits them. Readiness and output enablement are separate.
6. Shut down on normal exit, errors and cancellation using the library's documented cleanup mechanism.

Start with status inspection and clean shutdown. Add stimulation controls after the lifecycle works. Method spellings, units, return values and disposal rules differ by integration; use that library's README and generated API instead of translating a C# signature literally into another language.

## Configuration and setup

Choose the configuration directory before initialization. Keep Core settings, per-channel limits and model settings together as an experiment configuration. See [configuration fields](config-files-reference.md) and [setup dependencies](setup-order-and-modification.md). Missing-file generation is an integration-specific convenience; do not rely on it to supply an experiment's intended settings.

Setup changes must respect the Core sequence and active lifecycle state. Validate values and surface errors before allowing output. The [Core architecture](core-architecture.md) explains connection, setup and streaming boundaries.

## Test and deliver

Use the [testing and deployment guide](testing-and-deployment.md) to separate simulator, host-runtime and hardware checks. See [application dependency updates](ci-application-updates.md) for reviewing pinned upstream changes. If no existing integration fits your runtime, continue to [building an integration library](start-here-building-a-new-integration-library.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Previous: Use an application](start-here-using-an-application.md)
- [Next: Build an integration library](start-here-building-a-new-integration-library.md)
