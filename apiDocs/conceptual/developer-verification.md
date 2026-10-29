# Verify a Developer Checkout

[Home](../index.md) | [Back to Start Here](../start-here.md)

Start with the setup and software-only example in the selected [repository README](repository-and-kit-links.md). An application README owns its run commands and configuration paths; an integration README owns installation and API examples. This guide describes the shared verification contract.

## Preserve Dependency Identity

Clone applications with their pinned submodules, using `git clone --recurse-submodules` or `git submodule update --init --recursive`. For a repeatable checkout, avoid `git submodule update --remote`, which selects a different dependency revision.

Keep each candidate in a separate checkout. Record application and integration commits, gitlinks, and distribution hashes. Source commits, CI artifacts and tagged releases identify different inputs; select them explicitly. Keep the complete managed/native dependency distribution together instead of overlaying individual DLLs.

## Start Without Hardware

Use the repository's explicit emulator, test or conformance mode. These modes can have different behavior: a basic test transport can print commands, while a deterministic emulator checks initialization and protocol behavior. Follow the README's supported command and expected output.

A useful software-only check should establish that:

- the documented environment can load the complete dependency distribution;
- configuration resolves from the documented location;
- initialization reaches a usable state within a bounded timeout;
- the documented operations produce the expected emulated result;
- shutdown releases software resources.

Simulator and conformance results cover the behaviors they exercise. They do not establish device behavior, Unity Editor compatibility, or an application's coverage merely because a related wrapper passed.

## Own the Lifecycle Once

C# and Python controllers own their background ticking. Unity advances through its component lifecycle. An application composing Core/layers directly supplies the tick loop, waits for a usable state, and performs shutdown. The following helper assumes the caller already selected and configured the Core/layer stack; it is not a standalone application.

Use an emulator for software-only checks: initialization itself queues device setup and `StartStim`.

[!code-csharp[Direct Core lifecycle](../examples/DirectCoreLifecycle.cs)]

`Shutdown()` retains the transport for reinitialization. The caller owns final `Dispose()`, after which the Core instance cannot be reused.

## Test at the Changed Layer

For a protocol change, inspect framing, message codecs, version gates and the matching Core tests. For lifecycle changes, cover initialization, timeout, stopping stimulation, shutdown/reinitialization and final disposal. For an integration change, run that wrapper's tests with the exact dependency distribution. For an application change, run its own startup and configuration path.

See [Core Architecture](core-architecture.md), [Setup Order and Modification](setup-order-and-modification.md), and [First-party Core Promotion](ci-core-promotion.md) for the shared lifecycle and dependency promotion contracts.

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
