#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
DOCFX_BASE="$SCRIPT_DIR/docfx.json"
DOCFX_GENERATED="$SCRIPT_DIR/docfx.generated.json"
MANIFEST_PATH="${DOCS_MANIFEST_PATH:-$SCRIPT_DIR/repos.manifest.json}"
SERVE=0
SKIP_PYTHON=0
MAIN_ONLY=0

while [[ $# -gt 0 ]]; do
  case "$1" in
    --manifest)
      MANIFEST_PATH="$2"
      shift 2
      ;;
    --serve)
      SERVE=1
      shift
      ;;
    --skip-python)
      SKIP_PYTHON=1
      shift
      ;;
    --main)
      MAIN_ONLY=1
      shift
      ;;
    *)
      echo "Unknown argument: $1" >&2
      echo "Usage: ./apiDocs/build-docs.sh [--manifest <path>] [--serve] [--skip-python] [--main]" >&2
      exit 1
      ;;
  esac
done

if [[ ! -f "$DOCFX_BASE" ]]; then
  echo "ERROR: Missing DocFX base config: $DOCFX_BASE" >&2
  exit 1
fi

if [[ ! -f "$MANIFEST_PATH" ]]; then
  echo "ERROR: Manifest not found: $MANIFEST_PATH" >&2
  exit 1
fi

if ! command -v jq >/dev/null 2>&1; then
  echo "ERROR: jq is required to parse the manifest." >&2
  exit 1
fi

if ! command -v docfx >/dev/null 2>&1; then
  echo "ERROR: docfx is required and must be on PATH." >&2
  exit 1
fi

expand_path_tokens() {
  local raw="$1"
  if [[ -z "$raw" ]]; then
    REPLY=""
    return 0
  fi

  local out="$raw"
  while [[ "$out" =~ \$\{([A-Za-z_][A-Za-z0-9_]*)\} ]]; do
    local var_name="${BASH_REMATCH[1]}"
    local var_value="${!var_name-}"
    if [[ -z "$var_value" ]]; then
      echo "ERROR: Environment variable '$var_name' is not set (referenced by: $raw)." >&2
      return 1
    fi
    out="${out//\$\{$var_name\}/$var_value}"
  done

  REPLY="$out"
  return 0
}

resolve_path() {
  local base="$1"
  local raw="$2"
  expand_path_tokens "$raw" || return 1
  local expanded="$REPLY"
  if [[ "$expanded" = /* ]]; then
    REPLY="$expanded"
  else
    REPLY="$base/$expanded"
  fi
  return 0
}

python_has_sphinx() {
  local py_exec="$1"
  "$py_exec" -c 'import sphinx' >/dev/null 2>&1
}

metadata_entries='[]'
declare -a api_nav=()
declare -a python_nav=()

resolve_csharp_source() {
  local repo_json="$1"

  local source_raw
  source_raw="$(jq -r '.solution // .csproj // empty' <<<"$repo_json")"
  if [[ -z "$source_raw" ]]; then
    echo "ERROR: C# repo is missing 'solution' or 'csproj'." >&2
    return 1
  fi

  local source_base
  source_base="$(jq -r '.solutionBase // .csprojBase // "repo"' <<<"$repo_json")"

  REPLY="$source_raw|$source_base"
  return 0
}

repo_filter='.repositories[] | select(.enabled != false)'
if [[ $MAIN_ONLY -eq 1 ]]; then
  repo_filter+=" | select(.id == \"core\")"
fi

mapfile -t repos < <(jq -c "$repo_filter" "$MANIFEST_PATH")

rm -rf "$SCRIPT_DIR/api" "$SCRIPT_DIR/external"

for repo in "${repos[@]}"; do
  id="$(jq -r '.id // empty' <<<"$repo")"
  kind="$(jq -r '.kind // empty' <<<"$repo")"
  root_raw="$(jq -r '.root // empty' <<<"$repo")"
  title="$(jq -r '.title // empty' <<<"$repo")"

  if [[ -z "$id" || -z "$kind" || -z "$root_raw" ]]; then
    echo "ERROR: Each enabled repository needs id, kind, and root." >&2
    exit 1
  fi

  if [[ "$kind" == "python" && $SKIP_PYTHON -eq 1 ]]; then
    continue
  fi

  if ! resolve_path "$REPO_ROOT" "$root_raw"; then
    exit 1
  fi
  root="$REPLY"
  if [[ -z "$root" ]]; then
    echo "ERROR: Could not resolve repository root for '$id'." >&2
    exit 1
  fi
  if [[ ! -d "$root" ]]; then
    echo "ERROR: Repository root not found for '$id': $root" >&2
    exit 1
  fi

  if [[ "$kind" == "csharp" ]]; then
    resolve_csharp_source "$repo" || exit 1
    csharp_source_raw="${REPLY%%|*}"
    csharp_source_base="${REPLY#*|}"
    global_ns_id="$(jq -r '.globalNamespaceId // empty' <<<"$repo")"

    csharp_source_resolve_base="$root"
    if [[ "$csharp_source_base" == "hub" ]]; then
      csharp_source_resolve_base="$REPO_ROOT"
    fi

    if ! resolve_path "$csharp_source_resolve_base" "$csharp_source_raw"; then
      exit 1
    fi
    csharp_source_path="$REPLY"
    if [[ -z "$csharp_source_path" ]]; then
      echo "ERROR: Could not resolve C# project or solution path for '$id'." >&2
      exit 1
    fi
    if [[ ! -f "$csharp_source_path" ]]; then
      echo "ERROR: C# project or solution not found for '$id': $csharp_source_path" >&2
      exit 1
    fi
    if [[ "$csharp_source_path" != *.csproj && "$csharp_source_path" != *.sln ]]; then
      echo "ERROR: C# repo '$id' must point to a .csproj or .sln file: $csharp_source_path" >&2
      exit 1
    fi
    csharp_source_dir="$(dirname "$csharp_source_path")"
    csharp_source_file="$(basename "$csharp_source_path")"

    dest="$(jq -r '.docfxDest // empty' <<<"$repo")"
    if [[ -z "$dest" ]]; then
      dest="api/$id"
    elif [[ "$dest" != api/* ]]; then
      dest="api/$dest"
    fi

    props='null'
    if jq -e '.msbuildProperties != null' >/dev/null <<<"$repo"; then
      props="$(jq -c '.msbuildProperties' <<<"$repo")"

      # Expand ${ENV_VAR} tokens inside MSBuild property values.
      # (DocFX passes these to MSBuild; leaving tokens unexpanded can break paths.)
      while IFS= read -r key; do
        val="$(jq -r --arg k "$key" '.[$k] // empty' <<<"$props")"
        if [[ -n "$val" && "$val" == *'${'* ]]; then
          expand_path_tokens "$val" || exit 1
          props="$(jq -c --arg k "$key" --arg v "$REPLY" '.[$k] = $v' <<<"$props")"
        fi
      done < <(jq -r 'keys[]' <<<"$props")
    fi

    metadata_entries="$(jq -c \
      --arg dest "$dest" \
      --arg sourceDir "$csharp_source_dir" \
      --arg sourceFile "$csharp_source_file" \
      --arg globalNsId "$global_ns_id" \
      --argjson props "$props" \
      '. + [
        (
          {
            dest: $dest,
            filter: "filterConfig.yml",
            disableGitFeatures: true,
            src: [
              { src: $sourceDir, files: [$sourceFile] }
            ]
          }
          + (if $props == null then {} else { properties: $props } end)
          + (if $globalNsId == "" then {} else { globalNamespaceId: $globalNsId } end)
        )
      ]' <<<"$metadata_entries")"

    if [[ -z "$title" ]]; then
      title="API: $id"
    fi
    page_link="$dest/toc.html"
    api_nav+=("$title|$dest/toc.yml|$page_link")
  elif [[ "$kind" == "python" ]]; then
    sphinx_source_raw="$(jq -r '.sphinxSource // "docs"' <<<"$repo")"
    publish_raw="$(jq -r --arg id "$id" '.publishDir // ("external/" + $id)' <<<"$repo")"
    if ! resolve_path "$root" "$sphinx_source_raw"; then
      exit 1
    fi
    sphinx_source="$REPLY"
    if [[ -z "$sphinx_source" ]]; then
      echo "ERROR: Could not resolve Sphinx source path for '$id'." >&2
      exit 1
    fi
    if ! resolve_path "$SCRIPT_DIR" "$publish_raw"; then
      exit 1
    fi
    publish_dir="$REPLY"
    if [[ -z "$publish_dir" ]]; then
      echo "ERROR: Could not resolve publish path for '$id'." >&2
      exit 1
    fi

    if [[ ! -d "$sphinx_source" ]]; then
      echo "ERROR: Sphinx source not found for '$id': $sphinx_source" >&2
      exit 1
    fi

    rm -rf "$publish_dir"
    mkdir -p "$publish_dir"

    echo "Building Sphinx docs for '$id'..."
    if command -v sphinx-build >/dev/null 2>&1; then
      sphinx-build -b html "$sphinx_source" "$publish_dir"
    elif [[ -x "$root/.venv/bin/sphinx-build" ]]; then
      "$root/.venv/bin/sphinx-build" -b html "$sphinx_source" "$publish_dir"
    elif [[ -x "$root/.venv/bin/python" ]] && python_has_sphinx "$root/.venv/bin/python"; then
      "$root/.venv/bin/python" -m sphinx -b html "$sphinx_source" "$publish_dir"
    elif command -v python3 >/dev/null 2>&1 && python_has_sphinx "python3"; then
      python3 -m sphinx -b html "$sphinx_source" "$publish_dir"
    elif command -v python >/dev/null 2>&1 && python_has_sphinx "python"; then
      python -m sphinx -b html "$sphinx_source" "$publish_dir"
    elif command -v py >/dev/null 2>&1; then
      py -m sphinx -b html "$sphinx_source" "$publish_dir"
    else
      echo "ERROR: Could not find a usable Sphinx runner for '$id'." >&2
      echo "Tried: sphinx-build, <repo>/.venv/bin/sphinx-build, and Python interpreters with Sphinx installed." >&2
      echo "Install docs dependencies in the Python repo, e.g.: python3 -m pip install -e '.[docs]'" >&2
      exit 1
    fi

    if [[ -z "$title" ]]; then
      title="Python: $id"
    fi
    python_nav+=("$title|$publish_raw/index.html")
  else
    echo "ERROR: Unsupported repo kind '$kind' for '$id'." >&2
    exit 1
  fi
done

{
  printf '%s\n' '- name: Home'
  printf '%s\n' '  href: index.md'
  printf '%s\n' '- name: Start Here'
  printf '%s\n' '  href: start-here.md'
  printf '%s\n' '  items:'
  printf '%s\n' '  - name: Start Here: Using an Application'
  printf '%s\n' '    href: conceptual/start-here-using-an-application.md'
  printf '%s\n' '  - name: Start Here: Developing an Application'
  printf '%s\n' '    href: conceptual/start-here-developing-an-application.md'
  printf '%s\n' '  - name: Start Here: Building a New Integration Library'
  printf '%s\n' '    href: conceptual/start-here-building-a-new-integration-library.md'
  printf '%s\n' '  - name: Start Here: Minor Core Modifications'
  printf '%s\n' '    href: conceptual/start-here-minor-core-modifications.md'
  printf '%s\n' '  - name: Start Here: Adding Layers or Core Functionality'
  printf '%s\n' '    href: conceptual/start-here-adding-layers-or-core-functionality.md'
  printf '%s\n' '  - name: Choosing a Runtime for an Integration Library'
  printf '%s\n' '    href: conceptual/integration-library-runtime-selection.md'
  printf '%s\n' '  - name: Repository and Kit Links'
  printf '%s\n' '    href: conceptual/repository-and-kit-links.md'
  printf '%s\n' '- name: Concepts'
  printf '%s\n' '  href: concepts.md'
  printf '%s\n' '  items:'
  printf '%s\n' '  - name: Layering Guide (Modules)'
  printf '%s\n' '    href: conceptual/layering-guide.md'
  printf '%s\n' '  - name: Core Architecture (Transport, Codec, Core)'
  printf '%s\n' '    href: conceptual/core-architecture.md'
  printf '%s\n' '  - name: Setup Order and Modification'
  printf '%s\n' '    href: conceptual/setup-order-and-modification.md'
  printf '%s\n' '  - name: Firmware Compatibility Matrix'
  printf '%s\n' '    href: conceptual/firmware-compatibility-matrix.md'
  printf '%s\n' '  - name: Config Files Reference'
  printf '%s\n' '    href: conceptual/config-files-reference.md'
  printf '%s\n' '- name: Advanced'
  printf '%s\n' '  href: advanced.md'
  printf '%s\n' '  items:'
  printf '%s\n' '  - name: WSS Commands Reference'
  printf '%s\n' '    href: conceptual/wss-commands-reference.md'
  printf '%s\n' '  - name: Simple Serial Communication with WSS'
  printf '%s\n' '    href: conceptual/simple-serial-communication.md'

  if [[ ${#api_nav[@]} -gt 0 ]]; then
    printf '%s\n' '- name: C# API'
    printf '%s\n' '  items:'
    for item in "${api_nav[@]}"; do
      nav_title="${item%%|*}"
      rest="${item#*|}"
      nav_href="${rest%%|*}"
      printf '  - name: %s\n' "$nav_title"
      printf '    href: %s\n' "$nav_href"
    done
  fi

  printf '%s\n' '- name: Maintainers'
  printf '%s\n' '  href: maintainers.md'
  printf '%s\n' '  items:'
  printf '%s\n' '  - name: Building Software API Docs'
  printf '%s\n' '    href: conceptual/building-software-api-docs.md'
} > "$SCRIPT_DIR/toc.yml"

{
  printf '%s\n' '# WSS Documentation Hub'
  printf '%s\n' ''
  printf '%s\n' 'This site is organized first for people using WSS applications and for developers building applications or integrations on top of WSS.'
  printf '%s\n' ''
  printf '%s\n' '## Choose Your Path'
  printf '%s\n' ''
  printf '%s\n' '- [Start Here](start-here.md)'
  printf '%s\n' '  - The best entry point if you are deciding whether you are using an existing application, building a new application, creating a new integration library, or making focused core changes.'
  printf '%s\n' '- [Using an Application](conceptual/start-here-using-an-application.md)'
  printf '%s\n' '  - Start here if you want to run WSS through an existing GUI, CLI, Unity, or Python workflow.'
  printf '%s\n' '- [Developing an Application](conceptual/start-here-developing-an-application.md)'
  printf '%s\n' '  - Start here if you are building a user-facing tool on top of an existing WSS integration library.'
  printf '%s\n' '- [Building a New Integration Library](conceptual/start-here-building-a-new-integration-library.md)'
  printf '%s\n' '  - Start here if you need to expose WSS to a new language, platform, or transport environment.'
  printf '%s\n' ''
  printf '%s\n' '## Repository And Kit Links'
  printf '%s\n' ''
  printf '%s\n' '- [Repository and Kit Links](conceptual/repository-and-kit-links.md)'
  printf '%s\n' '  - One page for grouped application, integration library, and core repository and kit links.'
  printf '%s\n' ''
  printf '%s\n' '## Core Concepts'
  printf '%s\n' ''
  printf '%s\n' '- [Concepts](concepts.md)'
  printf '%s\n' '  - Overview of the main architecture, layering, setup, firmware compatibility, and config references.'
  printf '%s\n' '- [Layering Guide (Modules)](conceptual/layering-guide.md)'
  printf '%s\n' '  - Explains how WSS grows from Core to Params to Model and where new reusable functionality should live.'
  printf '%s\n' '- [Core Architecture (Transport, Codec, Core)](conceptual/core-architecture.md)'
  printf '%s\n' '  - Explains transports, framing, lifecycle, setup sequencing, and streaming behavior.'
  printf '%s\n' '- [Config Files Reference](conceptual/config-files-reference.md)'
  printf '%s\n' '  - Describes the standard config files used by applications and integration libraries.'
  printf '%s\n' ''
  if [[ ${#api_nav[@]} -gt 0 ]]; then
    printf '%s\n' '## API Reference'
    printf '%s\n' ''
    for item in "${api_nav[@]}"; do
      nav_title="${item%%|*}"
      rest="${item#*|}"
      nav_href_html="${rest#*|}"
      printf -- '- [%s](%s)\n' "$nav_title" "$nav_href_html"
    done
    if [[ ${#python_nav[@]} -gt 0 ]]; then
      printf '%s\n' '- Python Integration (Python)'
      printf '%s\n' '  - Published under `external/<repo>/` when Python docs are enabled in the docs build manifest.'
    fi
    printf '%s\n' ''
  fi

  printf '%s\n' '## Hardware Documentation'
  printf '%s\n' ''
  printf '%s\n' '- [Hardware Overview](../hardwareDocs/wsshardware.html)'
  printf '%s\n' '  - Direct access to the hardware documentation.'
  printf '%s\n' ''

  printf '%s\n' '## Advanced Reference'
  printf '%s\n' ''
  printf '%s\n' '- [Advanced](advanced.md)'
  printf '%s\n' '  - Lower-level protocol and raw communication material for debugging and direct device work.'
  printf '%s\n' '- [WSS Commands Reference](conceptual/wss-commands-reference.md)'
  printf '%s\n' '  - Byte-level command and protocol reference.'
  printf '%s\n' '- [Simple Serial Communication with WSS](conceptual/simple-serial-communication.md)'
  printf '%s\n' '  - Raw serial communication examples for macOS, Windows, and MATLAB.'
  printf '%s\n' ''
  printf '%s\n' '## Docs Maintenance'
  printf '%s\n' ''
  printf '%s\n' '- [Maintainers](maintainers.md)'
  printf '%s\n' '  - Build and maintain the documentation hub itself.'
  printf '%s\n' '- [Building Software API Docs](conceptual/building-software-api-docs.md)'
  printf '%s\n' '  - Build workflow for the multi-repository DocFX site and generated API docs.'
} > "$SCRIPT_DIR/index.md"

jq --argjson metadata "$metadata_entries" '.metadata = $metadata' "$DOCFX_BASE" > "$DOCFX_GENERATED"

pushd "$SCRIPT_DIR" >/dev/null
if [[ "$(jq 'length' <<<"$metadata_entries")" -gt 0 ]]; then
  docfx metadata "$DOCFX_GENERATED"
else
  echo "No enabled C# repositories; skipping docfx metadata."
fi

if [[ $SERVE -eq 1 ]]; then
  docfx build "$DOCFX_GENERATED" --serve
else
  docfx build "$DOCFX_GENERATED"
fi
popd >/dev/null

echo "Done. Output at: $SCRIPT_DIR"
