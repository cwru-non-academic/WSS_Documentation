using System.Net;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

internal static class NavigationValidation
{
    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    internal static void Sources(string root, string catalogPath)
    {
        var docs = Path.Combine(root, "apiDocs");
        var catalog = JsonNode.Parse(File.ReadAllText(catalogPath))!;
        var repositories = catalog["repositories"]!.AsObject();
        var names = repositories.Select(p => p.Value!["name"]!.GetValue<string>()).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach (var file in Directory.EnumerateFiles(docs, "*.md", SearchOption.AllDirectories)
            .Where(p => !Path.GetRelativePath(docs, p).Split(Path.DirectorySeparatorChar).Any(s => s is "api" or "generated" or "external" or "shims" or "_site")))
        {
            var text = WithoutExamples(File.ReadAllText(file));
            var line = 0;
            foreach (var sourceLine in text.Split('\n'))
            {
                line++;
                // Explicit single-line exceptions require a reason. Pinned commit evidence is immutable history.
                if (Regex.IsMatch(sourceLine, @"<!-- repository-link-literal: .{12,}? -->")) continue;
                foreach (Match url in Regex.Matches(sourceLine, @"https://github\.com/([^/\s)]+)/([^/#?\s)]+)([^\s)]*)"))
                {
                    // Match known repository names across owners so an organization move cannot hide stale literals.
                    if (!names.Contains(url.Groups[2].Value)) continue;
                    if (Regex.IsMatch(url.Groups[3].Value, @"^/(?:(blob|tree)/[a-fA-F0-9]{40}(/|$)|commit/[a-fA-F0-9]{40}($|[#?])|actions/runs/[0-9]+($|[/#?])|(pull|issues)/[0-9]+($|[/#?]))")) continue;
                    throw new InvalidOperationException($"Use a repository catalog xref in {Path.GetRelativePath(root, file)}:{line}: {url.Value}. For a necessary literal, add <!-- repository-link-literal: reason --> on that line.");
                }
                foreach (Match link in Regex.Matches(sourceLine, @"\]\(([^\s)]+)\)"))
                {
                    if (sourceLine.Contains("[!code-", StringComparison.Ordinal)) continue; // Generated snippets are checked after rendering.
                    var url = link.Groups[1].Value.Trim('<', '>');
                    if (External(url) || url.StartsWith('#')) continue;
                    var path = Uri.UnescapeDataString(url.Split('?', '#')[0]);
                    if (path.Length == 0) continue;
                    var destination = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file)!, path));
                    // Source Markdown may intentionally link generated HTML; verify that route after rendering.
                    Require(File.Exists(destination) || Directory.Exists(destination)
                        || (path.EndsWith(".html", StringComparison.OrdinalIgnoreCase) && File.Exists(Path.ChangeExtension(destination, ".md"))),
                        $"Broken authored link in {Path.GetRelativePath(root, file)}:{line}: {url}");
                }
            }
        }
        var toc = Path.Combine(docs, "toc.yml");
        var tocLine = 0;
        foreach (var line in File.ReadLines(toc))
        {
            tocLine++;
            var href = Regex.Match(line, @"^\s*(?:-\s*)?(?:href|topicHref):\s*['\x22]?([^'\x22\s]+)");
            if (!href.Success) continue;
            var url = href.Groups[1].Value;
            Require(!Regex.IsMatch(url, @"(^|/)toc\.(yml|html)([#?].*)?$", RegexOptions.IgnoreCase),
                $"Primary navigation must target a landing page, not a bare TOC: apiDocs/toc.yml:{tocLine}: {url}");
            if (!External(url)) Require(File.Exists(Path.Combine(docs, url.Split('#', '?')[0])), $"Missing authored TOC destination: {url}");
        }
        Console.WriteLine("PASS: authored local paths, primary navigation and repository catalog literals.");
    }

    private static string WithoutExamples(string text) => Regex.Replace(text, @"(?ms)^\s*(```|~~~).*?^\s*\1[^\r\n]*", m => new string('\n', m.Value.Count(c => c == '\n')));
    private static bool External(string url) => url.StartsWith("//", StringComparison.Ordinal) || Regex.IsMatch(url, @"^[A-Za-z][A-Za-z0-9+.-]*:");

    internal static void RenderedLinks(string site, string page, string html)
    {
        foreach (Match match in Regex.Matches(html, "(?<![\\w:-])(?:href|src)\\s*=\\s*[\"']([^\"']+)[\"']", RegexOptions.IgnoreCase))
        {
            var url = WebUtility.HtmlDecode(match.Groups[1].Value);
            if (url.Length == 0 || External(url)) continue;
            var hash = url.IndexOf('#');
            var path = Uri.UnescapeDataString(url.Split('?', '#')[0]);
            var destination = path.Length == 0 ? page : path.StartsWith('/') ? Path.Combine(site, path.TrimStart('/')) : Path.Combine(Path.GetDirectoryName(page)!, path);
            destination = Path.GetFullPath(destination);
            Require(destination.StartsWith(Path.GetFullPath(site).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase), $"Link escapes staged site: {url}");
            if (Directory.Exists(destination)) destination = Path.Combine(destination, "index.html");
            Require(File.Exists(destination), $"Broken staged local link in {Path.GetRelativePath(site, page)}: {url}");
            if (hash < 0 || hash == url.Length - 1 || !Path.GetExtension(destination).Equals(".html", StringComparison.OrdinalIgnoreCase)) continue;
            var fragment = Uri.UnescapeDataString(url[(hash + 1)..]);
            // HTML defines the ASCII-insensitive "top" fragment as the document's top even without an ID.
            if (fragment.Equals("top", StringComparison.OrdinalIgnoreCase)) continue;
            if (fragment.StartsWith(":~:text=", StringComparison.Ordinal)) continue;
            var target = File.ReadAllText(destination);
            var anchors = Regex.Matches(target, "(?<![\\w:-])(?:id|name)\\s*=\\s*[\"']([^\"']*)[\"']", RegexOptions.IgnoreCase)
                .Select(m => WebUtility.HtmlDecode(m.Groups[1].Value)).ToHashSet(StringComparer.Ordinal);
            Require(anchors.Contains(fragment), $"Broken staged fragment in {Path.GetRelativePath(site, page)}: {url}");
        }
    }

    internal static void Fixtures()
    {
        var dir = Path.Combine(Path.GetTempPath(), "wss-navigation-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dir);
        try
        {
            var page = Path.Combine(dir, "index.html");
            File.WriteAllText(page, "<h1 id='present'>Title</h1><a name='legacy'></a><span data-uid='not-an-id'></span>");
            File.WriteAllText(Path.Combine(dir, "other.html"), "<h2 id=\"encoded value\">Other</h2>");
            RenderedLinks(dir, page, "<a href='#present'>ok</a><a href='#legacy'>old</a><a href='#top'>top</a><a href='other.html#ToP'>other top</a><a href='other.html?x=1#encoded%20value'>encoded</a><a href='https://example.org/#remote'>remote</a>");
            foreach (var url in new[] { "missing.html", "#absent", "#topical", "#not-an-id", "other.html#absent", "../escape.html" })
            {
                var rejected = false;
                try { RenderedLinks(dir, page, $"<a href='{url}'>bad</a>"); }
                catch (InvalidOperationException) { rejected = true; }
                Require(rejected, $"Navigation fixture accepted: {url}");
            }
            var docs = Path.Combine(dir, "apiDocs");
            Directory.CreateDirectory(docs);
            var catalog = Path.Combine(docs, "repository-links.json");
            File.WriteAllText(catalog, "{\"defaultOwner\":\"fixture-owner\",\"repositories\":{\"core\":{\"name\":\"fixture-core\"}}}");
            File.WriteAllText(Path.Combine(docs, "landing.md"), "# Landing\n");
            var toc = Path.Combine(docs, "toc.yml");
            File.WriteAllText(toc, "- name: API\n  topicHref: landing.md\n  tocHref: api/core/toc.yml\n");
            var source = Path.Combine(docs, "index.md");
            File.WriteAllText(source, "# Home\n[Landing](landing.md)\n```text\nhttps://github.com/fixture-owner/fixture-core\n```\n[History](https://github.com/fixture-owner/fixture-core/blob/0123456789012345678901234567890123456789/README.md)\n");
            Sources(dir, catalog); // topicHref + structural tocHref, fenced example and immutable evidence are valid.
            File.WriteAllText(source, "[Run](https://github.com/fixture-owner/fixture-core/actions/runs/123)\n[Review](https://github.com/fixture-owner/fixture-core/pull/12)\n[Literal](https://github.com/fixture-owner/fixture-core) <!-- repository-link-literal: documented syntax example -->\n");
            Sources(dir, catalog);
            foreach (var bad in new[] { "[Missing](absent.md)", "[Literal](https://github.com/fixture-owner/fixture-core#readme)", "[Stale owner](https://github.com/previous-owner/fixture-core#readme)" })
            {
                File.WriteAllText(source, bad);
                var rejected = false;
                try { Sources(dir, catalog); } catch (InvalidOperationException) { rejected = true; }
                Require(rejected, $"Authored fixture accepted: {bad}");
            }
            File.WriteAllText(source, "# Home\n");
            foreach (var key in new[] { "href", "topicHref" })
            {
                File.WriteAllText(toc, $"- name: API\n  {key}: api/core/toc.yml\n");
                var rejected = false;
                try { Sources(dir, catalog); } catch (InvalidOperationException) { rejected = true; }
                Require(rejected, $"Bare primary TOC fixture accepted: {key}");
            }
        }
        finally { Directory.Delete(dir, true); }
        Console.WriteLine("PASS: same-page/cross-page/encoded/legacy fragments; broken paths, fragments and escapes rejected.");
    }
}
