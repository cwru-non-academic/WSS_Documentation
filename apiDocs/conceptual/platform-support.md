# Platforms and transports

[Home](../index.md) | [Back to Concepts](../concepts.md)

WSS 1.0 supports desktop workflows on Windows, Linux and macOS through the integrations below. Choose both a host runtime and a transport: support in Core does not mean every wrapper exposes the same transport-selection API.

## Choose a host

| Workflow | Entry point | Runtime and packaging details |
| --- | --- | --- |
| C#/.NET | [C# integration](xref:repo.csharp.readme) | Managed host with matching Core and transport dependencies |
| Python | [Python integration](xref:repo.python.readme) | Python, pythonnet and its selected .NET runtime |
| ROS 2 | [ROS driver](xref:repo.python-ros.readme) | ROS environment plus the Python integration |
| Unity | [Unity integration](xref:repo.unity.readme) | Unity Editor/player settings and plugin import rules |
| MATLAB | [MATLAB integration](xref:repo.matlab.readme) | MATLAB-supported .NET runtime and managed assemblies |

Use the linked README for exact runtime versions and available transport options. ROS additionally requires a supported ROS 2 installation for its host; do not infer a native Windows/macOS ROS package from the Python library's portability.

## Transport choices

- **Simulator/test mode:** exercises software setup and lifecycle without serial or BLE devices. Use the application README's explicit simulator option before selecting hardware.
- **Serial:** selects an operating-system serial endpoint. Port names and access permissions belong to the host; the application decides how the endpoint is configured.
- **BLE:** Core provides platform backends for Windows, Linux and macOS. The matching backend and native dependencies must travel with the managed bundle. Use the integration README to determine how BLE is exposed in that host.

macOS BLE uses the system Bluetooth stack. Desktop permissions, adapter availability and runtime packaging are separate from the shared protocol. Transport availability is not a statement of device certification or equivalent behavior across arbitrary adapters.

## Related contracts

Firmware features are described in the [firmware matrix](firmware-compatibility-matrix.md). Runtime targeting is described in [runtime selection](integration-library-runtime-selection.md). Keep application, integration, Core and backend revisions together as explained in [testing and deployment](testing-and-deployment.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
- [Repository setup guides](repository-and-kit-links.md)
