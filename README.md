# WSS Documentation

This repository contains end‑to‑end documentation for the Wearable Stimulation System (WSS): hardware, serial/command protocol, and a generated software API site (DocFX).

## Quick Links

- Repos
  - Core C# Library: [GitHub](https://github.com/cwru-non-academic/WSSCoreInterface)
  - Unity Implementation: [GitHub](https://github.com/cwru-non-academic/HFI_WSS_Unity_Interface)

- Hardware Overview
  - HTML: [hardwareDocs/wsshardware.html](./hardwareDocs/wsshardware.html)
  - What: Photos, panels, cables/electrodes, and the gold control unit.

- WSS Commands Reference
  - HTML: [wssCommandsDocs/wsscommands.html](./wssCommandsDocs/wsscommands.html)
  - What: Complete message construction guide (IDs, inputs/units, examples, checksum) with a linked index.

- Simple Serial Guide (Windows, macOS, MATLAB)
  - HTML: [simpleSerialPortDocs/SimpleSerial.html](./simpleSerialPortDocs/SimpleSerial.html)
  - What: How to list ports, configure, and send/receive Echo frames on each platform.

- Build Software Docs (DocFX)
  - HTML: [howtoCompileAPIDocs/BuildSoftwareDocs.html](./howtoCompileAPIDocs/BuildSoftwareDocs.html)
  - What: How to generate the API website from the external codebase using the scripts in `apiDocs/`.

- Software API Website
  - HTML: [apiDocs/api/WSSInterfacing.html](./apiDocs/api/WSSInterfacing.html)
  - What: Generated API documentation for core code (interfaces, classes, methods) via DocFX.

## Building the API site

Scripts live under `apiDocs/`:
- PowerShell (Windows): `apiDocs/build-docs.ps1`
- Bash (macOS/Linux/Git Bash): `apiDocs/build-docs.sh`

See [howtoCompileAPIDocs/BuildSoftwareDocs.html](./howtoCompileAPIDocs/BuildSoftwareDocs.html) for step‑by‑step instructions, prerequisites, and troubleshooting.
