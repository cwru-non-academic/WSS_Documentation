#!/usr/bin/env bash
#
# build-docs.sh — Clean build script for WSS documentation
#
# This script generates a clean DocFX site for the WSS Interfacing Module,
# while keeping your code in its original repository. It:
#   - Accepts the code location via env var CODE_ROOT (or WSS_CODE_ROOT) or as an argument
#   - Cleans previous outputs (api/, _site/, and Transformed/)
#   - Copies/wraps source files into ApiProject/Transformed
#   - Builds the ApiProject to produce XML docs
#   - Runs DocFX metadata and build
#
# Usage examples:
#   CODE_ROOT="/path/to/SimpleWSSStimConsole/Assets/SubModules/WSSInterfacingModule" ./build-docs.sh
#   ./build-docs.sh "/path/to/SimpleWSSStimConsole/Assets/SubModules/WSSInterfacingModule"
#
set -euo pipefail

# 1) Resolve the code root
CODE_ROOT="${CODE_ROOT:-${WSS_CODE_ROOT:-}}"
if [[ $# -ge 1 && -n "${1:-}" ]]; then CODE_ROOT="$1"; fi

# Location of this script and docs directory
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DOCS_DIR="$SCRIPT_DIR"
APIPROJ="$DOCS_DIR/ApiProject/ApiProject.csproj"
TRANSFORMED_DIR="$DOCS_DIR/ApiProject/Transformed"

# Fallback: guess sibling repo layout if CODE_ROOT not provided
if [[ -z "${CODE_ROOT}" ]]; then
  PARENT="$(cd "$DOCS_DIR/.." && pwd)"
  GUESS="$PARENT/../SimpleWSSStimConsole/Assets/SubModules/WSSInterfacingModule"
  if [[ -d "$GUESS" ]]; then CODE_ROOT="$GUESS"; fi
fi

if [[ -z "${CODE_ROOT}" || ! -d "$CODE_ROOT" ]]; then
  echo "ERROR: CODE_ROOT not set or not a directory." >&2
  echo "Set CODE_ROOT or WSS_CODE_ROOT to your WSSInterfacingModule path, or pass it as an argument." >&2
  echo "Example: CODE_ROOT=\"/abs/path/to/.../WSSInterfacingModule\" ./build-docs.sh" >&2
  exit 1
fi

echo "Using CODE_ROOT: $CODE_ROOT"
echo "Docs directory: $DOCS_DIR"

# 2) Clean previous outputs for a truly clean build
# Note: Removing api/, _site/, and the transformed sources ensures DocFX regenerates everything.
echo "Cleaning previous outputs..."
rm -rf "$DOCS_DIR/_site" "$DOCS_DIR/api" "$TRANSFORMED_DIR"
mkdir -p "$TRANSFORMED_DIR"

# 3) Transform/copy source into ApiProject/Transformed
# Prefer the provided PowerShell transformer (wraps files into a namespace if missing)
WRAP_PS="$DOCS_DIR/tools/WrapForDocs.ps1"
POWERSHELL=""
if command -v pwsh >/dev/null 2>&1; then
  POWERSHELL="pwsh"
elif command -v powershell >/dev/null 2>&1; then
  POWERSHELL="powershell"
fi

if [[ -f "$WRAP_PS" ]]; then
  if [[ -n "$POWERSHELL" ]]; then
    echo "Wrapping source with $POWERSHELL..."
    "$POWERSHELL" -NoProfile -ExecutionPolicy Bypass -File "$WRAP_PS" -SourceDir "$CODE_ROOT" -OutDir "$TRANSFORMED_DIR"
  else
    echo "WARNING: PowerShell not found; copying sources without wrapping namespaces."
    # Copy only .cs files preserving structure; fall back to simple copy if rsync is missing
    if command -v rsync >/dev/null 2>&1; then
      rsync -a --include '*/' --include '*.cs' --exclude '*' "$CODE_ROOT/" "$TRANSFORMED_DIR/"
    else
      find "$CODE_ROOT" -type f -name '*.cs' -print0 | while IFS= read -r -d '' f; do
        rel="${f#"$CODE_ROOT/"}"
        mkdir -p "$TRANSFORMED_DIR/$(dirname "$rel")"
        cp "$f" "$TRANSFORMED_DIR/$rel"
      done
    fi
  fi
else
  echo "WARNING: $WRAP_PS not found; copying sources without wrapping."
  if command -v rsync >/dev/null 2>&1; then
    rsync -a --include '*/' --include '*.cs' --exclude '*' "$CODE_ROOT/" "$TRANSFORMED_DIR/"
  else
    find "$CODE_ROOT" -type f -name '*.cs' -print0 | while IFS= read -r -d '' f; do
      rel="${f#"$CODE_ROOT/"}"
      mkdir -p "$TRANSFORMED_DIR/$(dirname "$rel")"
      cp "$f" "$TRANSFORMED_DIR/$rel"
    done
  fi
fi

# 4) Build the API project to produce XML docs consumed by DocFX
# If you only use docfx metadata, this build may be optional, but it helps catch reference issues.
echo "Building ApiProject..."
dotnet build "$APIPROJ" -c Debug

# 5) Run DocFX to generate metadata and the site
if command -v docfx >/dev/null 2>&1; then
  echo "Running docfx metadata..."
  (cd "$DOCS_DIR" && docfx metadata)
  echo "Building site..."
  (cd "$DOCS_DIR" && docfx build)
  echo "Done. Site is available at: $DOCS_DIR/_site"
else
  echo "WARNING: 'docfx' not found in PATH. Install DocFX, then run:" >&2
  echo "  cd \"$DOCS_DIR\" && docfx metadata && docfx build" >&2
fi
