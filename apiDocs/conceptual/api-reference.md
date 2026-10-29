# API Reference

[Home](../index.md) | [Back to Concepts](../concepts.md)

Use the shared API reference to understand the Core interfaces and layers. Use each integration's README for the APIs and setup used by its application examples.

## Core and Layers

- [Core namespace](xref:Wss.CoreModule): lifecycle, configuration, protocol and client operations.
- [Parameter layer](xref:Wss.CalibrationModule): per-channel parameters and stimulation limits.
- [Model layer](xref:Wss.ModelModule): model-driven stimulation.
- [Core repository](xref:repo.core.readme): building and testing Core.

## Integration Entry Points

- [C# integration README](xref:repo.csharp.readme)
- [Python integration README](xref:repo.python.readme)
- [Unity integration README](xref:repo.unity.readme)
- [MATLAB integration README](xref:repo.matlab.readme)

Use the public namespaces and signatures declared by the selected integration. A documentation grouping label does not itself declare a production namespace. Keep the source revision and bundled dependency distribution together when comparing APIs across repositories.

See [Choosing a Runtime](integration-library-runtime-selection.md) for host requirements and [Verify a Developer Checkout](developer-verification.md) for reproducible software-only validation.

Navigation:

- [Back to Home](../index.md)
- [Back to Concepts](../concepts.md)
