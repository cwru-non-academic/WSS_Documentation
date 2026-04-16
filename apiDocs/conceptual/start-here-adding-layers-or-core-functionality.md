# Start Here: Adding Layers or Core Functionality

[Home](../index.md) | [Back to Start Here](../start-here.md) | [Back to Concepts](../concepts.md)

## Who This Is For

This guide is for contributors extending WSS itself rather than only consuming existing APIs.

Use this path when you want to add new reusable functionality to the core, introduce a new layer, or reshape how capabilities are composed.

If you only need to make a targeted core change such as adding a firmware version, updating a message, changing a limit, updating `ModuleSettings`, or changing setup order, use [Start Here: Minor Core Modifications](start-here-minor-core-modifications.md) instead.

## Start With the Layering Guide

Start with the [Layering Guide (Modules)](layering-guide.md).

That guide shows how functionality grows from:
- Core only
- Core + Params
- Core + Params + Model

Use that structure as the default pattern for introducing future reusable layers.

The main idea is that functionality should be optional and composable. WSS should not force every consumer to adopt every feature that anyone has ever needed.

Instead of putting all possible behavior directly into the core:

- keep the core focused on transport, framing, lifecycle, setup, and streaming behavior
- add reusable domain functionality in layers
- let integration libraries and applications choose which layers they actually need

This makes it possible to keep the base stack smaller while still allowing advanced systems to build on top of it.

## Why Layers Exist

Layers exist so that new functionality can be added without making all integrations carry that functionality by default.

That matters because different consumers often need very different things:

- one integration may only need the base core
- another may need Params but not Model
- another may need Model plus additional domain logic above it
- another may need a completely different feature stack and should not be forced to include unrelated layers

With layers, each new feature set can stay optional.

That means:

- you do not need to implement everything in the core just because one project needed it
- you do not need to expose every possible feature in every integration library
- you can build higher-level functionality on top of lower-level layers only when it is needed

## Layer Dependencies and Composition

Layers can depend on previous layers.

For example:

- Params depends on the core
- Model depends on Params, which depends on the core
- a future layer could depend on Model, which already depends on Params and Core

This allows a higher-level layer to reuse the behavior below it instead of re-implementing it.

It also means different feature paths can exist side by side.

For example, if one future layer sits three layers above Model, you can include that path and get all of its dependencies automatically. But if another feature path also sits three layers above Model and depends on three different intermediate layers that your project does not need, you do not have to include that path at all.

That is one of the main benefits of the layering approach: feature stacks can grow in different directions without forcing every consumer to adopt every branch.

## When To Add a Layer vs. Change the Core

Add a new layer when the functionality is:

- optional rather than universally required
- domain-specific rather than transport/protocol-specific
- reusable across more than one integration or application
- naturally expressed on top of an existing layer or the base core

Change the core when the functionality is truly part of the shared foundation, such as:

- lifecycle behavior
- setup sequencing
- streaming behavior
- transport/codec integration
- common device control behavior that everything depends on

If a feature can reasonably be absent in some integrations, that is a strong sign it should be a layer rather than a core change.

This is especially important for stimulator-specific firmware features. An optional layer is a good fit when a feature is supported by a stimulator's firmware, but it is not part of the minimum functionality required for the stimulator to work through the shared core.

In other words:

- the core should contain the behavior that every supported stimulator needs in some form
- an optional layer should contain additional stimulator functionality that is useful, but not required for baseline operation

This separation becomes more important as WSS expands to support more stimulators. Different stimulators may all support the shared core concepts, but each one may have different advanced firmware features. Optional layers make it possible for each stimulator family to expose its own advanced capabilities without forcing those features into the shared core for every device.

## Core Architecture

Then review [Core Architecture (Transport, Codec, Core)](core-architecture.md).

That guide explains:
- transport responsibilities
- framing and deframing
- lifecycle state transitions
- setup queues and mid-stream edits
- shutdown and error handling

Changes to layers should respect these core behaviors rather than bypass them.

The core is the shared foundation that layers build on top of. A good layer extends the system by using the core correctly, not by replacing the core's responsibilities.

For example, layers should not take over:

- transport-specific behavior
- frame parsing and protocol mechanics
- lifecycle state management
- streaming loop ownership

Those belong in the architecture described by the core guide. Layers should add behavior above that foundation.

## Development Philosophy

When adding new functionality:

- keep transport concerns in transport code
- keep protocol framing in codec/core code
- keep domain-specific behavior in layers
- compose functionality at the application or integration-library boundary
- prefer reusable capability layers over one-off feature branches inside the core
- expose only the API surface that a consumer actually needs

The goal is not to make one giant stack that contains every feature by default. The goal is to make a system where capabilities can be combined in useful ways.

Good layering design usually has these properties:

- each layer has a clear responsibility
- each layer adds something meaningful on top of the layer below it
- a layer can be left out when a project does not need it
- higher-level layers reuse lower-level layers instead of duplicating their logic
- integration libraries can expose only the layers they actually support

Good additions usually make one of these clearer:
- what the base core is responsible for
- what a reusable layer adds
- what an integration library chooses to expose

## A Good Mental Model

Think of the system as a set of optional building blocks:

- Core gives you the shared device and lifecycle foundation.
- Params adds reusable parameter-management behavior.
- Model adds reusable model-driven behavior on top of Params.
- Optional stimulator-specific layers can add advanced firmware-supported behavior that is not required for all devices.
- Future layers can continue building upward from there.

An integration library or application can then choose the exact stack it needs.

That is why the layering approach scales better than putting every feature directly into the core: it keeps the system reusable, optional, and easier to evolve.

## Next Steps

- If you are exposing WSS to a new language or platform, continue to [Start Here: Building a New Integration Library](start-here-building-a-new-integration-library.md).
- If you are using existing capabilities from an application, continue to [Start Here: Developing an Application](start-here-developing-an-application.md).

Navigation:

- [Back to Home](../index.md)
- [Back to Start Here](../start-here.md)
- [Back to Concepts](../concepts.md)
- [Previous: Start Here: Minor Core Modifications](start-here-minor-core-modifications.md)
- [Related: Layering Guide (Modules)](layering-guide.md)
