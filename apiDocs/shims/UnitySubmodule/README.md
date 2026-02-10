# Unity Submodule DocFX Shim

Use this shim project when the Unity repository you want to document is a full Unity app/UI project,
but the code you actually want API docs for is a submodule folder (or a subset of the repo).

DocFX requires a C# project (`.csproj`) to generate metadata. Unity projects often pull in
UnityEditor/UI/build-time dependencies that are not needed (or not available) for API docs.
This shim:

- Compiles only the files you select (defaults to all `*.cs` under `DocsSourceRoot`)
- References Unity assemblies from `apiDocs/ref/*.dll`
- Produces XML docs so DocFX can generate API pages

## How To Use

1) Decide what folder contains the integration code you want documented.

Example (submodule checkout):

- `.../MyUnityApp/Assets/SubModules/WSSUnityIntegration`

2) Add a repo entry in `apiDocs/repos.manifest.json`.

Example:

```json
{
  "id": "unity-integration",
  "title": "API: Unity Integration (C#)",
  "kind": "csharp",
  "enabled": true,
  "root": "${WSS_UNITY_REPO_ROOT}",

  // Use the shim .csproj stored in this docs hub
  "csprojBase": "hub",
  "csproj": "apiDocs/shims/UnitySubmodule/UnitySubmodule.Docs.csproj",

  // Separate API section
  "docfxDest": "api/unity",

  // Tell the shim which source tree to include
  "msbuildProperties": {
    "DocsSourceRoot": "${WSS_UNITY_INTEGRATION_SRC}",

    // Optional: override includes/excludes
    // "DocsInclude": "${WSS_UNITY_INTEGRATION_SRC}/UnityImplementation/**/*.cs",
    // "DocsExclude": "${WSS_UNITY_INTEGRATION_SRC}/**/Tests/**",

    // Optional: Unity compilation symbols (semicolon-separated)
    "DefineConstants": "UNITY_2021_3_OR_NEWER;UNITY_EDITOR"
  }
}
```

3) Set environment variables used in the manifest.

Example (bash):

```bash
export WSS_UNITY_REPO_ROOT="/abs/path/to/MyUnityApp"
export WSS_UNITY_INTEGRATION_SRC="$WSS_UNITY_REPO_ROOT/Assets/SubModules/WSSUnityIntegration"
```

Note: the shim also supports providing the source root *without* MSBuild properties by setting
`WSS_UNITY_INTEGRATION_SRC` (or `DOCS_SOURCE_ROOT`) in the environment.

4) Build:

```bash
./apiDocs/build-docs.sh --manifest ./apiDocs/repos.manifest.json
```

## Notes

- `DocsSourceRoot` is required. If not set, the shim fails with a clear error.
- `DocsSourceRoot` must not be `/` (or a drive root like `C:\\`), otherwise MSBuild globbing becomes extremely slow and fails.
- By default, `Assets/**/Editor/**` is excluded to avoid `UnityEditor`-only code.
- If you need Editor APIs documented, remove that exclude and add `UnityEditor` references.
- If Unity types are missing, ensure `apiDocs/ref/*.dll` contains the correct Unity assemblies.
- If the submodule includes dependency assemblies under `Plugins/`, the shim automatically references `Plugins/**/*.dll` (excluding common Unity/Newtonsoft/system DLLs that are already referenced).
