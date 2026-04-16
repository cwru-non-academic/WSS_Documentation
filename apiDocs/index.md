# WSS Documentation Hub

This site is organized first for people using WSS applications and for developers building applications or integrations on top of WSS.

## Choose Your Path

- [Start Here](start-here.md)
  - The best entry point if you are deciding whether you are using an existing application, building a new application, creating a new integration library, or making focused core changes.
- [Using an Application](conceptual/start-here-using-an-application.md)
  - Start here if you want to run WSS through an existing GUI, CLI, Unity, or Python workflow.
- [Developing an Application](conceptual/start-here-developing-an-application.md)
  - Start here if you are building a user-facing tool on top of an existing WSS integration library.
- [Building a New Integration Library](conceptual/start-here-building-a-new-integration-library.md)
  - Start here if you need to expose WSS to a new language, platform, or transport environment.

## Repository And Kit Links

- [Repository and Kit Links](conceptual/repository-and-kit-links.md)
  - One page for grouped application, integration library, and core repository and kit links.

## Core Concepts

- [Concepts](concepts.md)
  - Overview of the main architecture, layering, setup, firmware compatibility, and config references.
- [Layering Guide (Modules)](conceptual/layering-guide.md)
  - Explains how WSS grows from Core to Params to Model and where new reusable functionality should live.
- [Core Architecture (Transport, Codec, Core)](conceptual/core-architecture.md)
  - Explains transports, framing, lifecycle, setup sequencing, and streaming behavior.
- [Config Files Reference](conceptual/config-files-reference.md)
  - Describes the standard config files used by applications and integration libraries.

## API Reference

- [API: Core (C#)](xref:Wss.CoreModule)
- [API: C# Integration (C#)](xref:HFI.Wss)
- [API: Unity Integration (C#)](xref:WSS.Unity)

## Advanced Reference

- [Advanced](advanced.md)
  - Lower-level protocol and raw communication material for debugging and direct device work.
- [WSS Commands Reference](conceptual/wss-commands-reference.md)
  - Byte-level command and protocol reference.
- [Simple Serial Communication with WSS](conceptual/simple-serial-communication.md)
  - Raw serial communication examples for macOS, Windows, and MATLAB.

## Docs Maintenance

- [Maintainers](maintainers.md)
  - Build and maintain the documentation hub itself.
- [Building Software API Docs](conceptual/building-software-api-docs.md)
  - Build workflow for the multi-repository DocFX site and generated API docs.
