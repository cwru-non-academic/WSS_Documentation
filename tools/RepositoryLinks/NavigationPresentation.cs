using System.Net;
using System.Text.RegularExpressions;

// Staging-only presentation adapter. Published RMarkdown source/output stays untouched.
internal static class NavigationPresentation
{
    private const RegexOptions HtmlOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
    private static readonly Regex AddedNavigation = new(
        """<nav\b(?=[^>]*\bdata-wss-navigation=["'](?:top|bottom)["'])[^>]*>.*?</nav>""", HtmlOptions);
    private static readonly Regex Anchor = new(
        """<a\b[^>]*\bhref\s*=\s*["'](?<href>[^"']*)["'][^>]*>(?<label>.*?)</a>""", HtmlOptions);

    internal static string EnhanceStandalone(string root, string relativeFile, string html, string hubPath, string title)
    {
        if (!IsStandalone(root, relativeFile)) return html;
        var siteRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var page = Path.GetFullPath(Path.Combine(siteRoot, relativeFile));
        var hub = Path.GetFullPath(Path.Combine(siteRoot, hubPath));
        Require(hub.StartsWith(siteRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase), "Hub path escapes site root.");
        var href = Path.GetRelativePath(Path.GetDirectoryName(page)!, hub).Replace('\\', '/');
        var clean = NormalizeForPreservation(html);
        var heading = Regex.Match(clean, @"<h1\b[^>]*>.*?</h1>", HtmlOptions);
        var bodyEnd = Regex.Match(clean, @"</body\s*>", HtmlOptions);
        Require(heading.Success && bodyEnd.Success && heading.Index < bodyEnd.Index, $"Standalone HTML needs an h1 and closing body: {relativeFile}");
        var top = Navigation("top", href, title);
        var bottom = Navigation("bottom", href, title);
        // Insert from the end so original character offsets remain valid.
        return clean.Insert(bodyEnd.Index, bottom).Insert(heading.Index + heading.Length, top);
    }

    internal static bool IsStandalone(string root, string relativeFile)
    {
        var portable = relativeFile.Replace('\\', '/');
        if (portable.StartsWith("apiDocs/", StringComparison.OrdinalIgnoreCase)
            || !portable.EndsWith(".html", StringComparison.OrdinalIgnoreCase)) return false;
        var siteRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var file = Path.GetFullPath(Path.Combine(siteRoot, relativeFile));
        if (!file.StartsWith(siteRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) return false;
        var directory = Path.GetDirectoryName(file)!;
        return Directory.Exists(directory) && Directory.EnumerateFiles(directory)
            .Any(p => Path.GetFileNameWithoutExtension(p).Equals(Path.GetFileNameWithoutExtension(file), StringComparison.OrdinalIgnoreCase)
                && Path.GetExtension(p).Equals(".rmd", StringComparison.OrdinalIgnoreCase));
    }

    // Apply the same narrow normalization to before/after HTML for preservation verification.
    // It does not normalize prose, scripts, whitespace, section links or general HTML.
    internal static string NormalizeForPreservation(string html)
    {
        var clean = AddedNavigation.Replace(html, "");
        // Legacy top nav is a paragraph containing only links, separators and whitespace.
        clean = Regex.Replace(clean, @"<p\b[^>]*>(?<inside>.*?)</p>", paragraph =>
        {
            var inside = paragraph.Groups["inside"].Value;
            if (Anchor.Matches(inside).Count == 0 || !Regex.IsMatch(Anchor.Replace(inside, ""), @"^[\s|]*$"))
                return paragraph.Value;
            var remaining = Anchor.Replace(inside, a => IsGenericHubAnchor(a) ? "" : a.Value);
            if (remaining == inside) return paragraph.Value;
            remaining = Regex.Replace(remaining, @"^\s*(?:\|\s*)+|(?:\s*\|)+\s*$", "");
            remaining = Regex.Replace(remaining, @"\|\s*\|", "|");
            return string.IsNullOrWhiteSpace(remaining) ? "" : paragraph.Value.Replace(inside, remaining, StringComparison.Ordinal);
        }, HtmlOptions);
        // Only a list explicitly headed Navigation is a legacy bottom nav list.
        clean = Regex.Replace(clean, @"(?<heading><p\b[^>]*>\s*Navigation:\s*</p>)(?<gap>\s*)(?<list><ul\b[^>]*>)(?<items>.*?)</ul>", block =>
        {
            var items = Regex.Replace(block.Groups["items"].Value, @"<li\b[^>]*>\s*(?<item>.*?)\s*</li>", item =>
            {
                var value = item.Groups["item"].Value;
                var matches = Anchor.Matches(value);
                return matches.Count == 1 && string.IsNullOrWhiteSpace(Anchor.Replace(value, "")) && IsGenericHubAnchor(matches[0])
                    ? "" : item.Value;
            }, HtmlOptions);
            return string.IsNullOrWhiteSpace(items) ? "" : block.Groups["heading"].Value + block.Groups["gap"].Value
                + block.Groups["list"].Value + items + "</ul>";
        }, HtmlOptions);
        return clean;
    }

    private static bool IsGenericHubAnchor(Match anchor)
    {
        var href = WebUtility.HtmlDecode(anchor.Groups["href"].Value);
        var label = WebUtility.HtmlDecode(anchor.Groups["label"].Value).Trim();
        return (href == "../index.html" && (label == "Home" || label == "Back to Home"))
            || (href == "../apiDocs/index.html" && (label == "Docs Hub" || label == "Back to Docs Hub"));
    }

    private static string Navigation(string position, string href, string title) =>
        $"<nav class=\"container-fluid main-container\" data-wss-navigation=\"{position}\" aria-label=\"Documentation navigation\"><p><a href=\"{WebUtility.HtmlEncode(href)}\">{WebUtility.HtmlEncode(title)}</a></p></nav>";

    internal static void TestFixtures()
    {
        var root = Path.Combine(Path.GetTempPath(), "wss-navigation-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(root, "guides/nested"));
        Directory.CreateDirectory(Path.Combine(root, "apiDocs/api/unity"));
        try
        {
            File.WriteAllText(Path.Combine(root, "guides/nested/example.Rmd"), "# Fixture");
            File.WriteAllText(Path.Combine(root, "guides/legacy.rmd"), "# Fixture");
            File.WriteAllText(Path.Combine(root, "apiDocs/api/unity/type.Rmd"), "# Fixture");
            const string content = "<html><body><h1>Fixture</h1><p>Keep <a href='../index.html'>Home</a> in prose.</p><pre>Navigation: &lt;a&gt;</pre><script>const s = 'Home';</script></body></html>";
            var nested = EnhanceStandalone(root, "guides/nested/example.html", content, "apiDocs/index.html", "Documentation & hub");
            Require(nested.Contains("href=\"../../apiDocs/index.html\"", StringComparison.Ordinal), "Nested standalone hub path.");
            Require(nested.Contains("Documentation &amp; hub", StringComparison.Ordinal), "Navigation label escaping.");
            Require(NormalizeForPreservation(nested) == content, "Standalone non-navigation content changed.");
            Require(EnhanceStandalone(root, "guides/nested/example.html", nested, "apiDocs/index.html", "Documentation & hub") == nested, "Standalone transformation is not idempotent.");
            Require(EnhanceStandalone(root, "apiDocs/api/unity/type.html", content, "apiDocs/index.html", "Hub") == content, "API page treated as standalone.");
            Require(EnhanceStandalone(root, "guides/missing.html", content, "apiDocs/index.html", "Hub") == content, "Page without RMarkdown source transformed.");
            const string legacy = "<html><body><h1>Fixture</h1><p><a href='../index.html'>Home</a> | <a href='../apiDocs/index.html'>Docs Hub</a> | <a href='../apiDocs/advanced.html'>Back to Advanced</a></p><p>Useful body</p><p>Navigation:</p><ul><li><a href='../index.html'>Back to Home</a></li><li><a href='../apiDocs/index.html'>Back to Docs Hub</a></li><li><a href='../apiDocs/advanced.html'>Back to Advanced</a></li></ul></body></html>";
            var enhanced = EnhanceStandalone(root, "guides/legacy.html", legacy, "apiDocs/index.html", "Documentation hub");
            Require(NormalizeForPreservation(legacy) == NormalizeForPreservation(enhanced), "Legacy navigation preservation mismatch.");
            Require(Regex.Matches(enhanced, "Back to Advanced").Count == 2 && enhanced.Contains("<p>Useful body</p>", StringComparison.Ordinal), "Section navigation or body removed.");
            Require(Regex.Matches(enhanced, "data-wss-navigation=").Count == 2, "Expected exactly two standalone navigation regions.");
            Require(!enhanced.Contains(">Docs Hub</a>", StringComparison.Ordinal) && !enhanced.Contains(">Back to Home</a>", StringComparison.Ordinal), "Generic legacy links remain.");
            const string onlyGeneric = "<html><body><h1>Fixture</h1><p><a href='../index.html'>Home</a> | <a href='../apiDocs/index.html'>Docs Hub</a></p><p>Navigation:</p><ul><li><a href='../index.html'>Back to Home</a></li></ul></body></html>";
            Require(NormalizeForPreservation(onlyGeneric) == "<html><body><h1>Fixture</h1></body></html>", "Empty legacy navigation containers remain.");
            Console.WriteLine("PASS: standalone navigation fixtures (nested paths, escaping, exclusion, idempotence, legacy/related links and non-navigation preservation).");
        }
        finally { Directory.Delete(root, true); }
    }

    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
