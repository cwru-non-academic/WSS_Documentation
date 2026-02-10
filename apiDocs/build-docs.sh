#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
DOCFX_BASE="$SCRIPT_DIR/docfx.json"
DOCFX_GENERATED="$SCRIPT_DIR/docfx.generated.json"
MANIFEST_PATH="${DOCS_MANIFEST_PATH:-$SCRIPT_DIR/repos.manifest.json}"
SERVE=0
SKIP_PYTHON=0

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
    *)
      echo "Unknown argument: $1" >&2
      echo "Usage: ./apiDocs/build-docs.sh [--manifest <path>] [--serve] [--skip-python]" >&2
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

mapfile -t repos < <(jq -c '.repositories[] | select(.enabled != false)' "$MANIFEST_PATH")

for repo in "${repos[@]}"; do
  id="$(jq -r '.id // empty' <<<"$repo")"
  kind="$(jq -r '.kind // empty' <<<"$repo")"
  root_raw="$(jq -r '.root // empty' <<<"$repo")"
  title="$(jq -r '.title // empty' <<<"$repo")"

  if [[ -z "$id" || -z "$kind" || -z "$root_raw" ]]; then
    echo "ERROR: Each enabled repository needs id, kind, and root." >&2
    exit 1
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
    csproj_raw="$(jq -r '.csproj // empty' <<<"$repo")"
    csproj_base="$(jq -r '.csprojBase // "repo"' <<<"$repo")"
    global_ns_id="$(jq -r '.globalNamespaceId // empty' <<<"$repo")"
    if [[ -z "$csproj_raw" ]]; then
      echo "ERROR: C# repo '$id' is missing 'csproj'." >&2
      exit 1
    fi

    csproj_resolve_base="$root"
    if [[ "$csproj_base" == "hub" ]]; then
      csproj_resolve_base="$REPO_ROOT"
    fi

    if ! resolve_path "$csproj_resolve_base" "$csproj_raw"; then
      exit 1
    fi
    csproj="$REPLY"
    if [[ -z "$csproj" ]]; then
      echo "ERROR: Could not resolve csproj path for '$id'." >&2
      exit 1
    fi
    if [[ ! -f "$csproj" ]]; then
      echo "ERROR: C# project not found for '$id': $csproj" >&2
      exit 1
    fi
    csproj_dir="$(dirname "$csproj")"
    csproj_file="$(basename "$csproj")"

    dest="$(jq -r '.docfxDest // empty' <<<"$repo")"
    entry_uid="$(jq -r '.entryUid // empty' <<<"$repo")"
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
      --arg csprojDir "$csproj_dir" \
      --arg csprojFile "$csproj_file" \
      --arg globalNsId "$global_ns_id" \
      --argjson props "$props" \
      '. + [
        (
          {
            dest: $dest,
            filter: "filterConfig.yml",
            disableGitFeatures: true,
            src: [
              { src: $csprojDir, files: [$csprojFile] }
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
    if [[ -n "$entry_uid" ]]; then
      page_link="xref:$entry_uid"
    fi
    api_nav+=("$title|$dest/toc.yml|$page_link")
  elif [[ "$kind" == "python" ]]; then
    if [[ $SKIP_PYTHON -eq 1 ]]; then
      continue
    fi

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

  conceptual_files=("$SCRIPT_DIR"/conceptual/*.md)
  if [[ -e "${conceptual_files[0]}" ]]; then
    printf '%s\n' '- name: Guides'
    printf '%s\n' '  items:'
    for file in "${conceptual_files[@]}"; do
      name="$(basename "$file")"
      title="$(awk '/^# /{sub(/^# /, ""); print; exit}' "$file")"
      if [[ -z "$title" ]]; then
        title="${name%.md}"
      fi
      printf '  - name: %s\n' "$title"
      printf '    href: conceptual/%s\n' "$name"
    done
  fi

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

  if [[ ${#python_nav[@]} -gt 0 ]]; then
    printf '%s\n' '- name: Python API'
    printf '%s\n' '  items:'
    for item in "${python_nav[@]}"; do
      nav_title="${item%%|*}"
      nav_href="${item#*|}"
      printf '  - name: %s\n' "$nav_title"
      printf '    href: %s\n' "$nav_href"
    done
  fi
} > "$SCRIPT_DIR/toc.yml"

{
  printf '%s\n' '# WSS Documentation Hub'
  printf '%s\n' ''
  printf '%s\n' 'This site combines conceptual documentation with API references from multiple repositories.'
  printf '%s\n' ''

  conceptual_files=("$SCRIPT_DIR"/conceptual/*.md)
  if [[ -e "${conceptual_files[0]}" ]]; then
    printf '%s\n' '## Guides'
    for file in "${conceptual_files[@]}"; do
      name="$(basename "$file")"
      title="$(awk '/^# /{sub(/^# /, ""); print; exit}' "$file")"
      if [[ -z "$title" ]]; then
        title="${name%.md}"
      fi
      printf -- '- [%s](conceptual/%s)\n' "$title" "$name"
    done
    printf '%s\n' ''
  fi

  if [[ ${#api_nav[@]} -gt 0 ]]; then
    printf '%s\n' '## C# API'
    for item in "${api_nav[@]}"; do
      nav_title="${item%%|*}"
      rest="${item#*|}"
      nav_href_html="${rest#*|}"
      printf -- '- [%s](%s)\n' "$nav_title" "$nav_href_html"
    done
    printf '%s\n' ''
  fi

  if [[ ${#python_nav[@]} -gt 0 ]]; then
    printf '%s\n' '## Python API'
    for item in "${python_nav[@]}"; do
      nav_title="${item%%|*}"
      nav_href="${item#*|}"
      printf -- '- [%s](%s)\n' "$nav_title" "$nav_href"
    done
  fi
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

echo "Done. Output at: $SCRIPT_DIR/_site"
