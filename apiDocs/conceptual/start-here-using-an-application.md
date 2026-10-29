# Using a WSS application

[Home](../index.md) | [Back to Start Here](../start-here.md)

Choose an application, then follow its README for the exact prerequisites, configuration directory and startup commands:

| Goal | Start here |
| --- | --- |
| Use a .NET terminal | [C# CLI README](xref:repo.csharp-cli.readme) |
| Use Python interactively | [Python CLI README](xref:repo.python-cli.readme) |
| Connect a robotics workflow | [ROS 2 README](xref:repo.python-ros.readme) |
| Use a Unity interface | [Unity GUI README](xref:repo.unity-app.readme) |
| Run a MATLAB experiment | [MATLAB CLI README](xref:repo.matlab-cli.readme) |

## First session

1. Clone the application with its pinned submodules as its README describes.
2. Check its runtime prerequisites and start with its simulator/test mode.
3. Confirm initialization, status reporting and shutdown before changing the configuration for a device.
4. Select the transport and configuration for your setup using the application's instructions.
5. Wait for the usable state documented by the application before issuing actions, and use the application's normal shutdown path when finished.

A simulator exercises the software lifecycle without connecting a device. It does not establish hardware behavior. Runtime test mode and CI conformance mode serve different purposes; use the startup mode named by the README.

## Configuration and lifecycle

Applications own configuration locations and user commands. The shared stack interprets Core configuration, stimulation parameters and optional model settings. See the [configuration reference](config-files-reference.md) for field meanings and [setup order](setup-order-and-modification.md) for the device setup sequence.

Initialization performs connection and setup; it does not mean the device is ready immediately. Readiness, output enablement and a requested stimulation value are distinct states. Core can advance through Ready into Started/Streaming during automatic setup; use the operational predicate documented by the host API. Follow the application's status and control flow rather than assuming that initialization alone enables output.

For selecting a host/transport, see [platform support](platform-support.md). For architecture, see the [layering guide](layering-guide.md). Exact method names belong to the library README and its API reference.

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Next: Develop an application](start-here-developing-an-application.md)
