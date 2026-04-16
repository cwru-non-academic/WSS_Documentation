# Start Here

Use this section when your main question is what kind of WSS work you are trying to do.

## Choose The Closest Path

- [Using an Application](conceptual/start-here-using-an-application.md)
  - Choose this if you want to operate WSS through an existing GUI, CLI, Unity workflow, or Python-based tool.
  - This is the best starting point for application users.
- [Developing an Application](conceptual/start-here-developing-an-application.md)
  - Choose this if you are building a user-facing tool on top of an existing WSS integration library.
  - This is the best starting point for most application developers.
- [Building a New Integration Library](conceptual/start-here-building-a-new-integration-library.md)
  - Choose this if no existing integration library fits your language, platform, or transport environment.
  - This is the best path when you need to expose WSS to a new runtime.
- [Minor Core Modifications](conceptual/start-here-minor-core-modifications.md)
  - Choose this if you need a focused internal change such as updating firmware support, limits, message definitions, settings decoding, or setup order.
- [Adding Layers or Core Functionality](conceptual/start-here-adding-layers-or-core-functionality.md)
  - Choose this if you are extending WSS itself with new reusable capabilities rather than making a focused internal fix.

## Fast Decision Guide

- I already have an app and just need to run WSS.
  - Go to [Using an Application](conceptual/start-here-using-an-application.md).
- I want to build a GUI, CLI, ROS node, or automation tool.
  - Go to [Developing an Application](conceptual/start-here-developing-an-application.md).
- I need WSS available in a new language or platform.
  - Go to [Building a New Integration Library](conceptual/start-here-building-a-new-integration-library.md).
- I need to change one core behavior without redesigning the stack.
  - Go to [Minor Core Modifications](conceptual/start-here-minor-core-modifications.md).
- I need to add a reusable feature set or new layer.
  - Go to [Adding Layers or Core Functionality](conceptual/start-here-adding-layers-or-core-functionality.md).

## Common Supporting Guides

- [Config Files Reference](conceptual/config-files-reference.md)
  - Useful when your application or integration relies on the standard `Config/` directory.
- [Core Architecture (Transport, Codec, Core)](conceptual/core-architecture.md)
  - Useful when you need to understand how setup, streaming, and lifecycle behavior actually work.
- [Choosing a Runtime for an Integration Library](conceptual/integration-library-runtime-selection.md)
  - Useful before starting a new integration library.
- [Repository and Kit Links](conceptual/repository-and-kit-links.md)
  - Use this when you want one place to collect application, integration library, and core links.

Navigation:

- [Back to Home](index.md)
