# WSS Documentation

This repository contains end‑to‑end documentation for the Wearable Stimulation System (WSS): hardware, serial/command protocol, and a generated software API site (DocFX).

## Quick Links

- Hardware Overview
  - Rmd: [hardwareDocs/wsshardware.rmd](./hardwareDocs/wsshardware.rmd)
  - HTML: [hardwareDocs/wsshardware.html](./hardwareDocs/wsshardware.html)
  - What: Photos, panels, cables/electrodes, and the gold control unit.

- WSS Commands Reference
  - Rmd: [wssCommandsDocs/wsscommands.Rmd](./wssCommandsDocs/wsscommands.Rmd)
  - HTML: [wssCommandsDocs/wsscommands.html](./wssCommandsDocs/wsscommands.html)
  - What: Complete message construction guide (IDs, inputs/units, examples, checksum) with a linked index.

- Simple Serial Guide (Windows, macOS, MATLAB)
  - Rmd: [simpleSerialPortDocs/SimpleSerial.Rmd](./simpleSerialPortDocs/SimpleSerial.Rmd)
  - HTML: [simpleSerialPortDocs/SimpleSerial.html](./simpleSerialPortDocs/SimpleSerial.html)
  - What: How to list ports, configure, and send/receive Echo frames on each platform.

- Build Software Docs (DocFX)
  - Rmd: [BuildSoftwareDocs.Rmd](./BuildSoftwareDocs.Rmd)
  - What: How to generate the API website from the external codebase using the scripts in `apiDocs/`.

- Software API Website
  - Site: [apiDocs/_site/index.html](./apiDocs/_site/index.html)
  - What: Generated API documentation for core code (interfaces, classes, methods) via DocFX.

## Building the API site

Scripts live under `apiDocs/`:
- PowerShell (Windows): `apiDocs/build-docs.ps1`
- Bash (macOS/Linux/Git Bash): `apiDocs/build-docs.sh`

See [BuildSoftwareDocs.Rmd](./BuildSoftwareDocs.Rmd) for step‑by‑step instructions, prerequisites, and troubleshooting.
