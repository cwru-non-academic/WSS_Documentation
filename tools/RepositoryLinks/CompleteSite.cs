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
        NavigationValidation.Sources(root, Path.Combine(root, "apiDocs/repository-links.json"));
        output = Path.GetFullPath(output);
        var artifactRoot = Path.Combine(root, "artifacts") + Path.DirectorySeparatorChar;
        var tempRoot = Path.GetFullPath(Path.GetTempPath()).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        Require(output.StartsWith(artifactRoot, StringComparison.OrdinalIgnoreCase) || output.StartsWith(tempRoot, StringComparison.OrdinalIgnoreCase),
            "Site output must be under this repository's artifacts directory or the OS temporary directory.");
        Require(!Directory.Exists(output) || !Directory.EnumerateFileSystemEntries(output).Any(),
            "Site output is not empty; choose a new --output directory. No existing site is deleted.");
        Directory.CreateDirectory(output);
        var authoredInputs = new[] { "apiDocs/index.md", "apiDocs/toc.yml" }
            .ToDictionary(path => path, path => Hash(Path.Combine(root, path)), StringComparer.Ordinal);

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
        build["globalMetadata"] ??= new JsonObject();
        var hubPath = "apiDocs/index.html";
        build["globalMetadata"]!["_wssHubPath"] = hubPath;
        build["content"] = new JsonArray(new JsonObject
        {
            ["src"] = "apiDocs", ["dest"] = "apiDocs",
            ["files"] = new JsonArray("*.md", "conceptual/**/*.md", "toc.yml", "api/**/*.yml", "api/**/index.md")
        });
        // Map preserved assets/standalone HTML as resources so DocFX can resolve cross-section links.
        // Existing apiDocs HTML is copied above, but not declared as a resource that could collide
        // with newly rendered conceptual/API pages at the same URL.
        var resources = new JsonArray();
        foreach (var file in preserved.Where(p =>
            !(p.StartsWith("apiDocs/", StringComparison.Ordinal) && p.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
            && !(p.StartsWith("apiDocs/api/", StringComparison.Ordinal) && p.EndsWith("/toc.json", StringComparison.OrdinalIgnoreCase))))
            resources.Add(file);
        build["resource"] = new JsonArray(new JsonObject { ["files"] = resources });
        build["xref"] = new JsonArray(Path.Combine(generated, "xrefmap.yml"));
        build["template"] = new JsonArray("default", Path.Combine(docs, "templates/wss"));
        build["dest"] = output;
        build["disableGitFeatures"] = true;
        // Root the config above every source/resource; DocFX cannot export a manifest
        // for resources reached through multiple parent-directory segments.
        var configPath = Path.Combine(root, "docfx.site.generated.json");
        File.WriteAllText(configPath, config.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        docfx(["build", configPath]);
        var hubTitle = build["globalMetadata"]!["_wssHubTitle"]!.GetValue<string>();
        foreach (var file in preserved.Where(file => NavigationPresentation.IsStandalone(root, file)))
        {
            var destination = Path.Combine(output, file);
            File.WriteAllText(destination, NavigationPresentation.EnhanceStandalone(root, file,
                File.ReadAllText(destination), hubPath, hubTitle));
        }
        // Actions serves the artifact directly. Do not depend on Jekyll or a stale CNAME file.
        File.WriteAllText(Path.Combine(output, ".nojekyll"), "");
        Verify(root, output, preserved);
        foreach (var (path, hash) in authoredInputs)
            Require(Hash(Path.Combine(root, path)) == hash, $"Build rewrote an authored navigation input: {path}");
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
        var standalone = preserved.Where(file => NavigationPresentation.IsStandalone(root, file)).ToList();
        var unchanged = preserved.Where(p => !p.StartsWith("apiDocs/", StringComparison.Ordinal) && !standalone.Contains(p)).ToList();
        foreach (var file in unchanged)
            Require(Hash(Path.Combine(root, file)) == Hash(Path.Combine(output, file)), $"Preserved root/standalone file changed: {file}");
        foreach (var file in standalone)
        {
            var html = File.ReadAllText(Path.Combine(output, file));
            Require(NavigationPresentation.NormalizeForPreservation(File.ReadAllText(Path.Combine(root, file)))
                == NavigationPresentation.NormalizeForPreservation(html), $"Standalone content changed outside navigation: {file}");
            NavigationValidation.RenderedLinks(output, Path.Combine(output, file), html);
            VerifyHubNavigation(output, Path.Combine(output, file), html);
        }
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
            NavigationValidation.RenderedLinks(output, page, html);
            if (!page.EndsWith("toc.html", StringComparison.Ordinal)) VerifyHubNavigation(output, page, html);
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
            var apiToc = Path.Combine(output, $"apiDocs/api/{section}/toc.html");
            NavigationValidation.RenderedLinks(output, apiToc, File.ReadAllText(apiToc));
        }
        var apiHubPages = Directory.EnumerateFiles(Path.Combine(docs, "api"), "*.yml", SearchOption.AllDirectories)
            .Where(file => !Path.GetFileName(file).Equals("toc.yml", StringComparison.OrdinalIgnoreCase))
            .Select(file => Path.Combine(output, "apiDocs", Path.ChangeExtension(Path.GetRelativePath(docs, file), ".html"))).ToList();
        foreach (var page in apiHubPages)
        {
            Require(File.Exists(page), $"Missing generated API page: {page}");
            VerifyHubNavigation(output, page, File.ReadAllText(page));
        }
        var report = new
        {
            preservedPaths = preserved, byteIdenticalRootAndAssets = unchanged, standaloneContentPreserved = standalone,
            freshDocfxPages = fresh.Select(p => Path.GetRelativePath(output, p).Replace('\\', '/')).ToList(),
            checkedInApiMetadata = true, externalApiRegeneration = false, customDomainAssumed = false,
            authoredNavigationValidated = true,
            fragmentCheckedPages = fresh.Concat(standalone.Select(path => Path.Combine(output, path)))
                .Concat(new[] { "core", "csharp", "unity" }.Select(section => Path.Combine(output, $"apiDocs/api/{section}/toc.html")))
                .Select(page => Path.GetRelativePath(output, page).Replace('\\', '/')).ToList(),
            apiHubBacklinkCheckedPages = apiHubPages.Select(page => Path.GetRelativePath(output, page).Replace('\\', '/')).ToList()
        };
        File.WriteAllText(Path.Combine(Path.GetDirectoryName(output)!, "site-validation.json"), JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine($"PASS: complete site; {preserved.Count} public paths retained, {unchanged.Count} root/assets byte-identical, {standalone.Count} standalone guides changed only in navigation, {fresh.Count} fresh pages and API navigation validated.");
    }

    private static void VerifyHubNavigation(string site, string page, string html)
    {
        foreach (var position in new[] { "top", "bottom" })
        {
            var regions = Regex.Matches(html, @"<(?<tag>nav|span)\b[^>]*data-wss-navigation=""" + position
                + @"""[^>]*>(?<content>.*?)</\k<tag>>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Require(regions.Count == 1, $"Expected one {position} hub navigation region: {page}");
            var link = Regex.Match(regions[0].Groups["content"].Value, "href=\"([^\"]+)\"");
            Require(link.Success, $"Missing {position} hub link: {page}");
            var href = WebUtility.HtmlDecode(link.Groups[1].Value);
            var destination = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(page)!, Uri.UnescapeDataString(href)));
            Require(destination.Equals(Path.GetFullPath(Path.Combine(site, "apiDocs/index.html")), StringComparison.OrdinalIgnoreCase),
                $"Wrong {position} hub destination in {page}: {href}");
        }
    }

    private static string Hash(string path) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path)));
    private static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}
