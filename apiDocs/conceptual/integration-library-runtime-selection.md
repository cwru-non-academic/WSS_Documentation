# Choosing a Runtime for an Integration Library

[Home](../index.md) | [Back to Start Here](../start-here.md) | [Back to Building a New Integration Library](start-here-building-a-new-integration-library.md)

Choose the application host separately from the portable Core and transport assemblies. A library's target framework describes its managed API compatibility; the host runtime and native OS/architecture assets determine where it runs.

| Component | Managed target | Host responsibility |
| --- | --- | --- |
| Core `WSS_Core_Interface.csproj` | `netstandard2.0` | Supply a runtime and select a transport. |
| Serial `WSS.Transport.Serial/WSS.Transport.Serial.csproj` | `netstandard2.0` | Include its managed dependencies and the correct runtime assets for the host. |
| BLE transport abstraction and backends | `netstandard2.0` | Deploy the matching platform backend and native assets. A portable assembly target does not make a native backend portable. |
| C# integration `src/Wss.CSharpImplementation.csproj` | `net9.0` | Use a .NET 9-compatible host and the complete dependency distribution. |
| Unity integration | Unity project | Follow its supported Editor version and plugin import settings. A successful standalone .NET build does not establish Unity compatibility. |
| Python integration | CPython plus pythonnet | Select the .NET runtime before importing the wrapper and retain the complete dependency distribution. |
| MATLAB integration | MATLAB plus its .NET host | Follow the library README for MATLAB prerequisites, path setup and managed/native dependency loading. |

The documentation toolchain has its own pinned SDK and DocFX versions. Those build tools do not define the application's runtime requirement.

## Choose an Existing Integration First

For a .NET application, use the [C# integration README](https://github.com/cwru-non-academic/CSharp_WSS_Integration_Lib#readme). For Python, use the [Python integration README](https://github.com/cwru-non-academic/Python_WSS_Integration_Lib#readme). For Unity, follow the [Unity integration README](https://github.com/cwru-non-academic/Unity_WSS_Integration_Lib#readme). For MATLAB, follow the [MATLAB integration README](https://github.com/cwru-non-academic/Matlab_WSS_Integration_Lib#readme).

These READMEs own concrete installation commands, submodule paths, runtime setup and minimal examples. The [repository directory](repository-and-kit-links.md) links to the supported applications.

For a new integration, start with Core interfaces, define who owns initialization, ticking and shutdown, and make transport selection explicit. When composing Core directly, the host drives `Tick()`; existing wrappers can own that loop. See [Core Architecture](core-architecture.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Back to Building a New Integration Library](start-here-building-a-new-integration-library.md)
