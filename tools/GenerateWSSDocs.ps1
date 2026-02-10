param(
  [string]$Root = "Assets/SubModules/WSSInterfacingModule",
  [string]$OutDir = "Docs/WSSInterfacingModule"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Ensure-Directory {
  param([string]$Path)
  if (-not (Test-Path -LiteralPath $Path)) {
    New-Item -ItemType Directory -Path $Path | Out-Null
  }
}

function Convert-XmlDocToMarkdown {
  param([string[]]$XmlDocLines)
  if (-not $XmlDocLines -or $XmlDocLines.Count -eq 0) { return "" }
  # Trim leading '///' and whitespace
  $clean = $XmlDocLines | ForEach-Object { ($_ -replace '^[\s\/]*///\s?', '') }
  $xml = ($clean -join "`n")
  # Remove XML-invalid control chars
  $xml = [System.Text.RegularExpressions.Regex]::Replace($xml, "[\x00-\x08\x0B-\x0C\x0E-\x1F]", "")

  # Wrap in a root node if not present to parse as XML safely
  $wrapped = "<root>" + $xml + "</root>"
  try {
    [xml]$doc = $wrapped
  } catch {
    # Fallback: try to strip tags into readable Markdown
    $text = ($clean -join "`n")
    # Convert <param name="x">y</param>
    $text = [System.Text.RegularExpressions.Regex]::Replace($text, '<param\s+name="([^"]+)">([\s\S]*?)</param>', '- param $1: $2')
    # Convert returns
    $text = [System.Text.RegularExpressions.Regex]::Replace($text, '<returns>([\s\S]*?)</returns>', '- returns: $1')
    # Convert remarks and summary
    $text = [System.Text.RegularExpressions.Regex]::Replace($text, '<remarks>([\s\S]*?)</remarks>', 'Remarks: $1')
    $text = [System.Text.RegularExpressions.Regex]::Replace($text, '<summary>([\s\S]*?)</summary>', '$1')
    # Replace <see cref="X"/> with X
    $text = [System.Text.RegularExpressions.Regex]::Replace($text, '<see\s+cref\s*=\s*"([^"]+)"\s*/?>', '$1')
    # Drop any residual tags
    $text = [System.Text.RegularExpressions.Regex]::Replace($text, '<[^>]+>', '')
    return $text.Trim()
  }

  $sb = New-Object System.Text.StringBuilder

  function XmlFragment-ToMarkdown {
    param([System.Xml.XmlNode]$Node)
    if (-not $Node) { return '' }
    $inner = New-Object System.Text.StringBuilder
    function Walk([System.Xml.XmlNode]$n) {
      $nt = $n.NodeType.ToString()
      if ($nt -eq 'Text') { [void]$inner.Append($n.Value); return }
      if ($nt -eq 'Element') {
        $nm = $n.Name.ToLowerInvariant()
        if ($nm -eq 'see') {
          $attr = $n.Attributes.GetNamedItem('cref')
          if ($attr) {
            $cref = $attr.Value
            $link = Get-TypeLink -TypeName $cref
            if ($link) { [void]$inner.Append("[" + $cref + "](" + $link + ")") }
            else { [void]$inner.Append($cref) }
          }
        } elseif ($nm -eq 'paramref') {
          $nattr = $n.Attributes.GetNamedItem('name')
          if ($nattr) {
            $pname = $nattr.Value
            if ($pname) { [void]$inner.Append('`' + $pname + '`') }
          }
        } else {
          if ($n.HasChildNodes) { foreach ($c in $n.ChildNodes) { Walk $c } }
          else { if ($n.InnerText) { [void]$inner.Append($n.InnerText) } }
        }
        return
      }
      if ($n.InnerText) { [void]$inner.Append($n.InnerText) }
    }
    foreach ($c in $Node.ChildNodes) { Walk $c }
    return $inner.ToString().Trim()
  }

  # Helper to append with blank line separation
  function AppendLine([string]$t) {
    if ([string]::IsNullOrWhiteSpace($t)) { return }
    [void]$sb.AppendLine($t)
  }

  # Summary
  $sumNodes = @($doc.SelectNodes('/root/summary'))
  $summary = if ($sumNodes.Count -gt 0) { ($sumNodes | ForEach-Object { (XmlFragment-ToMarkdown $_).Trim() }) -join " `n" } else { "" }
  if ($summary) { AppendLine($summary) }

  # Remarks
  $remNodes = @($doc.SelectNodes('/root/remarks'))
  $remarks = if ($remNodes.Count -gt 0) { ($remNodes | ForEach-Object { (XmlFragment-ToMarkdown $_).Trim() }) -join " `n" } else { "" }
  if ($remarks) { AppendLine("") ; AppendLine("Remarks: $remarks") }

  # Params
  $params = @()
  foreach ($p in $doc.SelectNodes('/root/param')) {
    if ($p -and $p.name) {
      $pname = $p.name
      $pdesc = $p.InnerText.Trim()
      $params += ("- param " + $pname + ": " + $pdesc)
    }
  }
  if ($params.Count -gt 0) {
    AppendLine("")
    foreach ($l in $params) { AppendLine($l) }
  }

  # Returns
  $retNodes = @($doc.SelectNodes('/root/returns'))
  $returns = if ($retNodes.Count -gt 0) { ($retNodes | ForEach-Object { (XmlFragment-ToMarkdown $_).Trim() }) -join " `n" } else { "" }
  if ($returns) { AppendLine("") ; AppendLine("- returns: $returns") }

  # Exceptions
  foreach ($ex in $doc.SelectNodes('/root/exception')) {
    $cref = ($ex.cref | ForEach-Object { $_ })
    $desc = $ex.InnerText.Trim()
    $label = if ($cref) { $cref } else { 'exception' }
    AppendLine(("- throws " + $label + ": " + $desc))
  }

  # Value (for properties)
  $valNodes = @($doc.SelectNodes('/root/value'))
  $value = if ($valNodes.Count -gt 0) { ($valNodes | ForEach-Object { (XmlFragment-ToMarkdown $_).Trim() }) -join " `n" } else { "" }
  if ($value) { AppendLine("") ; AppendLine("- value: $value") }

  return $sb.ToString().TrimEnd()
}

function Get-RelativePath {
  param([string]$Base,[string]$Full)
  $uriBase = New-Object System.Uri((Resolve-Path $Base).Path)
  $uriFull = New-Object System.Uri((Resolve-Path $Full).Path)
  return [Uri]::UnescapeDataString($uriBase.MakeRelativeUri($uriFull).ToString()).Replace('/', '\\')
}

Ensure-Directory -Path $OutDir

# Collect .cs files
$files = Get-ChildItem -Path $Root -Recurse -File -Include *.cs | Sort-Object FullName
if ($files.Count -eq 0) {
  Write-Host "No .cs files found under $Root"
  exit 0
}

# Build a type-to-doc index for cross-linking
$TypeIndex = @{}
foreach ($f in $files) {
  $relTmp = Get-RelativePath -Base $Root -Full $f.FullName
  $docLeaf = [System.IO.Path]::ChangeExtension(($relTmp -replace '\\','__'), '.md')
  $baseName = [System.IO.Path]::GetFileNameWithoutExtension($f.Name)
  $TypeIndex[$baseName] = $docLeaf
  $contentHead = (Get-Content -LiteralPath $f.FullName -TotalCount 200)
  $typeLine = ($contentHead | Where-Object { $_ -match '\b(class|interface|struct|enum|record)\s+[A-Za-z_][A-Za-z0-9_]*' } | Select-Object -First 1)
  if ($typeLine) {
    if ($typeLine -match '\b(class|interface|struct|enum|record)\s+([A-Za-z_][A-Za-z0-9_]*)') {
      $tname = $Matches[2]
      $TypeIndex[$tname] = $docLeaf
    }
  }
}
$script:TypeIndex = $TypeIndex

function Get-TypeLink {
  param([string]$TypeName)
  if (-not $TypeName) { return $null }
  $clean = $TypeName.Trim()
  if ($clean -match '^([A-Za-z_][A-Za-z0-9_\.]+)') { $clean = $Matches[1] }
  if ($script:TypeIndex.ContainsKey($clean)) { return $script:TypeIndex[$clean] }
  if ($clean -match '([A-Za-z_][A-Za-z0-9_]*)$') {
    $last = $Matches[1]
    if ($script:TypeIndex.ContainsKey($last)) { return $script:TypeIndex[$last] }
  }
  return $null
}

foreach ($file in $files) {
  $rel = Get-RelativePath -Base $Root -Full $file.FullName
  $outPath = Join-Path $OutDir ($rel -replace '\\', '__')
  $outPath = [System.IO.Path]::ChangeExtension($outPath, '.md')
  Ensure-Directory -Path ([System.IO.Path]::GetDirectoryName($outPath))

  $lines = Get-Content -LiteralPath $file.FullName

  $md = New-Object System.Text.StringBuilder
  [void]$md.AppendLine("# $rel")
  [void]$md.AppendLine("")
  [void]$md.AppendLine("Source: " + $file.FullName)
  [void]$md.AppendLine("")

  # Namespace (first matching line)
  $ns = ($lines | Where-Object { $_ -match '^\s*namespace\s+[A-Za-z0-9_.]+' } | Select-Object -First 1)
  if ($ns) { [void]$md.AppendLine("- namespace: " + $ns) ; [void]$md.AppendLine("") }

  # Walk lines capturing XML doc blocks and their following signature lines
  $i = 0
  while ($i -lt $lines.Count) {
    # Capture consecutive XML doc lines starting with '///'
    $docBlock = @()
    while ($i -lt $lines.Count -and $lines[$i].TrimStart().StartsWith('///')) {
      $docBlock += $lines[$i]
      $i++
    }

    # Next non-empty line is treated as signature
    $sig = $null
    while ($i -lt $lines.Count -and [string]::IsNullOrWhiteSpace($lines[$i])) { $i++ }
    if ($i -lt $lines.Count) { $sig = $lines[$i].Trim() }

    if ($docBlock.Count -gt 0 -or ($sig -and ($sig -match '^(public|internal|protected|private)?\s*(class|interface|struct|enum|record|static|sealed|abstract|partial|async|virtual|override|event|readonly)'))) {
      # Determine a section title: if signature defines a type, use it; else label as Member
      $title = 'Member'
      if ($sig -match '\b(class|interface|struct|enum|record)\b') { $title = 'Type' }

      [void]$md.AppendLine("## $title")
      if ($sig) {
        [void]$md.AppendLine("")
        [void]$md.AppendLine("Signature:")
        [void]$md.AppendLine("")
        [void]$md.AppendLine("~~~csharp")
        [void]$md.AppendLine($sig)
        [void]$md.AppendLine("~~~")

        # If this is a type declaration with base types/interfaces, add clickable links
        if ($title -eq 'Type') {
          $inheritList = @()
          if ($sig -match '\b(class|interface|record|struct)\s+[A-Za-z_][A-Za-z0-9_]*\s*:\s*(.+)$') {
            $bases = $Matches[2].Split(',') | ForEach-Object { $_.Trim() }
            foreach ($b in $bases) {
              $t = $b -replace '\s+where.*$',''
              $link = Get-TypeLink -TypeName $t
              if ($link) { $inheritList += ("[" + $t + "](" + $link + ")") }
              else { $inheritList += $t }
            }
          }
          if ($inheritList.Count -gt 0) {
            [void]$md.AppendLine("")
            [void]$md.AppendLine("Implements/Extends: " + ($inheritList -join ', '))
          }
        }
      }

      if ($docBlock.Count -gt 0) {
        [void]$md.AppendLine("")
        [void]$md.AppendLine("Documentation:")
        [void]$md.AppendLine("")
        $mdDoc = Convert-XmlDocToMarkdown -XmlDocLines $docBlock
        if ($mdDoc) { [void]$md.AppendLine($mdDoc) }

        # If documentation inherits from another member/type, surface a link
        $docRaw = ($docBlock -join ' ')
        if ($docRaw -match '<inheritdoc[^>]*cref\s*=\s*"([^"]+)"') {
          $cref = $Matches[1]
          $l2 = Get-TypeLink -TypeName $cref
          if ($l2) {
            [void]$md.AppendLine("")
            [void]$md.AppendLine("Inherits documentation from: [" + $cref + "](" + $l2 + ")")
          }
        } elseif ($docRaw -match '<inheritdoc') {
          # No cref; try link to first implemented type if any
          if ($sig -and $sig -match '\b(class|interface|record|struct)\s+[A-Za-z_][A-Za-z0-9_]*\s*:\s*([^\{]+)') {
            $first = ($Matches[2].Split(',')[0]).Trim()
            $l3 = Get-TypeLink -TypeName $first
            if ($l3) {
              [void]$md.AppendLine("")
              [void]$md.AppendLine("Inherits documentation from: [" + $first + "](" + $l3 + ")")
            }
          }
        }
      }

      [void]$md.AppendLine("")
    }

    # Advance to next line to continue
    $i++
  }

  # Also include a short public API section: extract lines likely to be public signatures
  $publics = @($lines | Where-Object { $_ -match '^\s*public\s' })
  if ($publics.Count -gt 0) {
    [void]$md.AppendLine("## Public API (signatures)")
    [void]$md.AppendLine("")
    foreach ($pl in $publics) {
      $sig = $pl.Trim()
      if ($sig.Length -gt 0) { [void]$md.AppendLine("- " + $sig) }
    }
    [void]$md.AppendLine("")
  }

  Ensure-Directory -Path ([System.IO.Path]::GetDirectoryName($outPath))
  $null = Set-Content -LiteralPath $outPath -Value $md.ToString() -Encoding UTF8
  Write-Host "Generated: $outPath"
}

Write-Host "Done. Output in $OutDir"
