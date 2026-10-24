using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

internal static class CompleteSite
{
    private static readonly string[] ContentRoots = ["apiDocs/", "hardwareDocs/", "howtoCompileAPIDocs/", "simpleSerialPortDocs/", "wssCommandsDocs/"];
    private static readonly HashSet<string> StaticExtensions = new(StringComparer.OrdinalIgnoreCase)
    { ".html", ".htm", ".css", ".js", ".json", ".map", ".svg", ".png", ".jpg", ".jpeg", ".gif", ".webp", ".ico", ".pdf", ".woff", ".woff2", ".ttf", ".eot", ".otf", ".txt", ".xml" };

    internal static void Build(string root, string output, Action<string[]> docfx)
    {
        output = Path.GetFullPath(output);
        var artifactRoot = Path.Combine(root, "artifacts") + Path.DirectorySeparatorChar;
        var tempRoot = Path.GetFullPath(Path.GetTempPath()).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        Require(output.StartsWith(artifactRoot, StringComparison.OrdinalIgnoreCase) || output.StartsWith(tempRoot, StringComparison.OrdinalIgnoreCase),
            "Site output must be under this repository's artifacts directory or the OS temporary directory.");
        Require(!Directory.Exists(output) || !Directory.EnumerateFileSystemEntries(output).Any(),
            "Site output is not empty; choose a new --output directory. No existing site is deleted.");
        Directory.CreateDirectory(output);

        var preserved = PublishedFiles(root);
        foreach (var file in preserved)
        {
            var destination = Path.Combine(output, file);
            Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
            File.Copy(Path.Combine(root, file), destination);
        }

        var docs = Path.Combine(root, "apiDocs");
        var generated = Path.Combine(docs, "generated/repository-links");
        var config = JsonNode.Parse(File.ReadAllText(Path.Combine(docs, "docfx.json")))!.AsObject();
        config["metadata"] = new JsonArray(); // Use reviewed, checked-in API metadata; never inspect external checkouts here.
        var build = config["build"]!.AsObject();
        build["content"] = new JsonArray(new JsonObject
        {
            ["src"] = Path.GetRelativePath(generated, docs), ["dest"] = "apiDocs",
            ["files"] = new JsonArray("*.md", "conceptual/**/*.md", "toc.yml", "api/**/*.yml", "api/**/index.md")
        });
        // Map preserved assets/standalone HTML as resources so DocFX can resolve cross-section links.
        // Existing apiDocs HTML is copied above, but not declared as a resource that could collide
        // with newly rendered conceptual/API pages at the same URL.
        var resources = new JsonArray();
        foreach (var file in preserved.Where(p => !(p.StartsWith("apiDocs/", StringComparison.Ordinal) && p.EndsWith(".html", StringComparison.OrdinalIgnoreCase))))
            resources.Add(file);
        build["resource"] = new JsonArray(new JsonObject { ["src"] = Path.GetRelativePath(generated, root), ["files"] = resources });
        build["xref"] = new JsonArray(Path.Combine(generated, "xrefmap.yml"));
        build["template"] = new JsonArray("default", Path.Combine(docs, "templates/wss"));
        build["dest"] = output;
        build["disableGitFeatures"] = true;
        var configPath = Path.Combine(generated, "site.docfx.json");
        File.WriteAllText(configPath, config.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        docfx(["build", configPath]);
        // Actions serves the artifact directly. Do not depend on Jekyll or a stale CNAME file.
        File.WriteAllText(Path.Combine(output, ".nojekyll"), "");
        Verify(root, output, preserved);
    }

    private static List<string> PublishedFiles(string root)
    {
        var start = new ProcessStartInfo("git") { WorkingDirectory = root, RedirectStandardOutput = true, UseShellExecute = false };
        start.ArgumentList.Add("ls-files"); start.ArgumentList.Add("-z");
        using var process = Process.Start(start) ?? throw new InvalidOperationException("Cannot inventory tracked public files.");
        var tracked = process.StandardOutput.ReadToEnd().Split('\0', StringSplitOptions.RemoveEmptyEntries);
        process.WaitForExit();
        Require(process.ExitCode == 0, "git ls-files failed.");
        return tracked.Where(p =>
            (p is "index.html" or "readme.html" or "rmd-overrides.css" || ContentRoots.Any(prefix => p.StartsWith(prefix, StringComparison.Ordinal)))
            && !p.Split('/').Any(part => part is "bin" or "obj" or "ref" or "ApiProject" or "shims" or "generated" or "templates")
            && StaticExtensions.Contains(Path.GetExtension(p))
            && !(p.StartsWith("apiDocs/", StringComparison.Ordinal) && (p.Contains("docfx", StringComparison.OrdinalIgnoreCase) && p.EndsWith(".json", StringComparison.OrdinalIgnoreCase)
                || p.EndsWith("repos.manifest.json", StringComparison.Ordinal))))
            .OrderBy(p => p, StringComparer.Ordinal).ToList();
    }

    private static void Verify(string root, string output, List<string> preserved)
    {
        foreach (var file in preserved) Require(File.Exists(Path.Combine(output, file)), $"Previously public path lost: {file}");
        var unchanged = preserved.Where(p => !p.StartsWith("apiDocs/", StringComparison.Ordinal)).ToList();
        foreach (var file in unchanged)
            Require(Hash(Path.Combine(root, file)) == Hash(Path.Combine(output, file)), $"Preserved root/standalone file changed: {file}");
        Require(!File.Exists(Path.Combine(output, "CNAME")), "No custom domain may be assumed in the staged artifact.");
        Require(File.ReadAllText(Path.Combine(output, "index.html")).Contains("apiDocs/", StringComparison.Ordinal), "Root redirect missing.");

        var docs = Path.Combine(root, "apiDocs");
        var fresh = Directory.EnumerateFiles(docs, "*.md")
            .Concat(Directory.EnumerateFiles(Path.Combine(docs, "conceptual"), "*.md", SearchOption.AllDirectories))
            .Select(p => Path.Combine(output, "apiDocs", Path.ChangeExtension(Path.GetRelativePath(docs, p), ".html"))).ToList();
        fresh.Add(Path.Combine(output, "apiDocs/toc.html"));
        foreach (var page in fresh)
        {
            Require(File.Exists(page), $"Missing freshly rendered page: {page}");
            var html = File.ReadAllText(page);
            if (!page.EndsWith("toc.html", StringComparison.Ordinal))
                Require(html.Contains("property=\"docfx:navrel\"", StringComparison.Ordinal), $"Page was not rendered by DocFX: {page}");
            var prose = Regex.Replace(html, @"<pre\b[^>]*>.*?</pre>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Require(!Regex.IsMatch(prose, @"xref:repo\.|\[!code-"), $"Unresolved repository/snippet directive: {page}");
            CheckLocalLinks(output, page, html);
        }
        var guide = File.ReadAllText(Path.Combine(output, "apiDocs/conceptual/repository-links.html"));
        var catalog = JsonNode.Parse(File.ReadAllText(Path.Combine(docs, "repository-links.json")))!;
        var core = catalog["repositories"]!["core"]!;
        var owner = (core["owner"] ?? catalog["defaultOwner"])!.GetValue<string>();
        var rootUrl = $"https://github.com/{owner}/{core["name"]!.GetValue<string>()}";
        Require(guide.Contains($"git clone {rootUrl}.git", StringComparison.Ordinal), "Expanded clone snippet missing from published guide.");
        var toc = File.ReadAllText(Path.Combine(output, "apiDocs/toc.html"));
        foreach (var section in new[] { "core", "csharp", "unity" })
        {
            Require(File.Exists(Path.Combine(output, $"apiDocs/api/{section}/toc.html")), $"API navigation missing: {section}");
            Require(toc.Contains($"api/{section}/", StringComparison.Ordinal), $"API section lost from main navigation: {section}");
        }
        var report = new
        {
            preservedPaths = preserved, byteIdenticalRootAndStandalone = unchanged,
            freshDocfxPages = fresh.Select(p => Path.GetRelativePath(output, p).Replace('\\', '/')).ToList(),
            checkedInApiMetadata = true, externalApiRegeneration = false, customDomainAssumed = false
        };
        File.WriteAllText(Path.Combine(Path.GetDirectoryName(output)!, "site-validation.json"), JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine($"PASS: complete site; {preserved.Count} public paths retained, {unchanged.Count} root/standalone files byte-identical, {fresh.Count} fresh pages and API navigation validated.");
    }

    private static void CheckLocalLinks(string site, string page, string html)
    {
        foreach (Match match in Regex.Matches(html, "(?:href|src)=\"([^\"]+)\"", RegexOptions.IgnoreCase))
        {
            var url = WebUtility.HtmlDecode(match.Groups[1].Value);
            if (url.Length == 0 || url[0] == '#' || url.StartsWith("//", StringComparison.Ordinal)
                || Regex.IsMatch(url, @"^[A-Za-z][A-Za-z0-9+.-]*:") || url.Contains("PASTE_", StringComparison.Ordinal)) continue;
            var path = Uri.UnescapeDataString(url.Split('?', '#')[0]);
            if (path.Length == 0) continue;
            var destination = path.StartsWith('/') ? Path.Combine(site, path.TrimStart('/')) : Path.Combine(Path.GetDirectoryName(page)!, path);
            destination = Path.GetFullPath(destination);
            Require(destination.StartsWith(Path.GetFullPath(site) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase), $"Link escapes staged site: {url}");
            if (Directory.Exists(destination)) destination = Path.Combine(destination, "index.html");
            Require(File.Exists(destination), $"Broken staged local link in {Path.GetRelativePath(site, page)}: {url}");
        }
    }

    private static string Hash(string path) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path)));
    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
