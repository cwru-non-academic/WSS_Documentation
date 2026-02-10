# WSS Documentation

This repository contains end‑to‑end documentation for the Wearable Stimulation System (WSS): hardware, serial/command protocol, and a generated software API site (DocFX).

## Quick Links
  - Start Here: [GitHub Pages](https://cwru-non-academic.github.io/WSS_Documentation/) use to access all of the content bellow in html format.
  
- Repos
  - Core C# Library: [GitHub](https://github.com/cwru-non-academic/WSSCoreInterface)
  - Unity Implementation: [GitHub](https://github.com/cwru-non-academic/HFI_WSS_Unity_Interface)

- [Hardware Overview](./hardwareDocs/wsshardware.html)
  - What: Photos, panels, cables/electrodes, and the gold control unit.

- [WSS Commands Reference](./wssCommandsDocs/wsscommands.html)
  - What: Complete message construction guide (IDs, inputs/units, examples, checksum) with a linked index.

- [Simple Serial Guide](./simpleSerialPortDocs/SimpleSerial.html) (Windows, macOS, MATLAB)
  - What: How to list ports, configure, and send/receive Echo frames on each platform.

- [Software API Website](./apiDocs/api/WSSInterfacing.html)
  - What: Generated API documentation for core code (interfaces, classes, methods) via DocFX.

## Building the documentation site (multi-repo)

Scripts live under `apiDocs/`:
- PowerShell (Windows): `apiDocs/build-docs.ps1`
- Bash (macOS/Linux/Git Bash): `apiDocs/build-docs.sh`

The docs hub supports multiple repositories and tools via `apiDocs/repos.manifest.json`:
- C# repos: DocFX API metadata (separate API sections per repo)
- Python repos: Sphinx HTML builds hosted inside the DocFX site

Run with the manifest:

```bash
./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json
```

```powershell
./apiDocs/build-docs.ps1 -ManifestPath ./apiDocs/repos.manifest.json
```

See [How to Compile Docs using DocFX](./howtoCompileAPIDocs/BuildSoftwareDocs.html) for step‑by‑step instructions, prerequisites, and troubleshooting.
