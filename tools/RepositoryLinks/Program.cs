using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

// One implementation for Windows, Linux, local previews and CI. No NuGet dependencies.
try
{
    var root = FindRoot();
    var command = args.FirstOrDefault() ?? "test";
    string? Option(string name) => Array.IndexOf(args, name) is var i && i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    var catalogPath = Option("--catalog") ?? Path.Combine(root, "apiDocs/repository-links.json");
    var output = Option("--output") ?? Path.Combine(root, "apiDocs/generated/repository-links");
    var tool = Option("--docfx");
    switch (command)
    {
        case "generate": Generate(Load(catalogPath), output); break;
        case "test": Test(root, catalogPath, args.Contains("--render") || tool != null, tool); break;
        case "site":
            Generate(Load(catalogPath), Path.Combine(root, "apiDocs/generated/repository-links"));
            CompleteSite.Build(root, Option("--output") ?? Path.Combine(root, "artifacts/site"), a => Docfx(root, tool, a));
            break;
        case "hub": Hub(root, catalogPath, Option("--output") ?? Path.Combine(root, "artifacts/hub"), tool, args.Contains("--serve")); break;
        default: throw new ArgumentException("Use generate, test [--render], hub [--serve], or site; optional --catalog, --output, --docfx <executable>.");
    }
    return 0;
}
catch (Exception e)
{
    Console.Error.WriteLine(e.Message);
    return 1;
}

static string FindRoot()
{
    for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir != null; dir = dir.Parent)
        if (File.Exists(Path.Combine(dir.FullName, "apiDocs/repository-links.json"))) return dir.FullName;
    throw new DirectoryNotFoundException("Cannot locate documentation repository root.");
}

static JsonObject Load(string path) => JsonNode.Parse(File.ReadAllText(path))?.AsObject()
    ?? throw new InvalidDataException("Catalog must be a JSON object.");

static string Text(JsonNode? value) => value?.GetValue<string>() ?? throw new InvalidDataException("Expected a string in repository catalog.");

static string Slug(string value)
{
    if (!Regex.IsMatch(value, @"\A[A-Za-z0-9_-][A-Za-z0-9_.-]*\z") || value is "." or "..")
        throw new InvalidDataException($"Invalid repository identifier, owner or name: {value}");
    return value;
}

static List<Reference> Resolve(JsonObject catalog)
{
    var owner = Slug(Text(catalog["defaultOwner"]));
    var repos = catalog["repositories"]?.AsObject() ?? throw new InvalidDataException("Missing repositories object.");
    if (repos.Count == 0) throw new InvalidDataException("Catalog must contain repositories.");
    var refs = new List<Reference>();
    foreach (var (id, node) in repos.OrderBy(p => p.Key, StringComparer.Ordinal))
    {
        Slug(id);
        var repo = node?.AsObject() ?? throw new InvalidDataException($"Missing repository: {id}");
        var name = Slug(Text(repo["name"]));
        var repoOwner = repo["owner"] == null ? owner : Slug(Text(repo["owner"]));
        var url = $"https://github.com/{repoOwner}/{name}";
        refs.Add(new($"repo.{id}", name, url));
        if (repo["links"] == null) continue;
        foreach (var (linkId, suffixNode) in repo["links"]!.AsObject().OrderBy(p => p.Key, StringComparer.Ordinal))
        {
            Slug(linkId);
            var suffix = Text(suffixNode);
            if (suffix.Length == 0 || !"/#?".Contains(suffix[0]) || suffix.StartsWith("//", StringComparison.Ordinal)
                || suffix.Any(c => char.IsWhiteSpace(c) || "\\\"<>".Contains(c))
                || Regex.IsMatch(suffix, @"(^|/)\.\.(/|$)"))
                throw new InvalidDataException($"Invalid URL suffix for repo.{id}.{linkId}: {suffix}");
            refs.Add(new($"repo.{id}.{linkId}", $"{name}: {linkId}", url + suffix));
        }
    }
    if (refs.Select(r => r.uid).Distinct(StringComparer.OrdinalIgnoreCase).Count() != refs.Count)
        throw new InvalidDataException("Duplicate repository link UID.");
    return refs;
}

static List<Reference> Generate(JsonObject catalog, string output)
{
    var refs = Resolve(catalog); // Fully validate before creating output.
    Directory.CreateDirectory(output);
    // JSON is YAML-compatible and accepted by DocFX as an xref map.
    File.WriteAllText(Path.Combine(output, "xrefmap.yml"), JsonSerializer.Serialize(new { references = refs }, new JsonSerializerOptions { WriteIndented = true }));
    foreach (var id in catalog["repositories"]!.AsObject().Select(p => p.Key))
        File.WriteAllText(Path.Combine(output, $"{id}-clone.sh"), $"git clone {refs.Single(r => r.uid == $"repo.{id}").href}.git\n");
    Console.WriteLine($"Generated {refs.Count} repository references in {output}");
    return refs;
}

static void Docfx(string root, string? executable, params string[] arguments)
{
    var start = new ProcessStartInfo(executable ?? "dotnet") { WorkingDirectory = root, UseShellExecute = false };
    if (executable == null)
        foreach (var arg in new[] { "tool", "run", "docfx", "--" }) start.ArgumentList.Add(arg);
    foreach (var arg in arguments) start.ArgumentList.Add(arg);
    using var process = Process.Start(start) ?? throw new InvalidOperationException("Could not start DocFX.");
    process.WaitForExit();
    if (process.ExitCode != 0) throw new InvalidOperationException($"DocFX exited with code {process.ExitCode}.");
}

static void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

static void CheckHtml(string path, IEnumerable<Reference> refs, string cloneRoot)
{
    var html = File.ReadAllText(path);
    foreach (var reference in refs)
        Require(html.Contains($"href=\"{WebUtility.HtmlEncode(reference.href)}\"", StringComparison.Ordinal), $"Missing rendered href: {reference.href}");
    Require(html.Contains($"git clone {cloneRoot}.git", StringComparison.Ordinal), "Copyable clone command missing.");
    Require(!Regex.IsMatch(html, @"xref:repo\.|\[!code-"), "Unresolved DocFX directive in fixture.");
}

static void Test(string root, string catalogPath, bool render, string? tool)
{
    NavigationValidation.Fixtures();
    NavigationPresentation.TestFixtures();
    NavigationValidation.Sources(root, catalogPath);
    var temp = Path.Combine(Path.GetTempPath(), "wss-links-" + Guid.NewGuid().ToString("N"));
    Directory.CreateDirectory(temp);
    try
    {
        var catalog = Load(catalogPath);
        var originalOwner = Text(catalog["defaultOwner"]);
        var repos = catalog["repositories"]!.AsObject();
        repos["core"]!["links"] = new JsonObject { ["readme"] = "/blob/main/README.md?plain=1#L10" };
        // Resolve every catalog root in a temporary page; the actual catalog is never modified.
        var page = "# Repository fixture\n\n" + string.Join("\n\n", Resolve(catalog).Select(r => $"[{r.uid}](xref:{r.uid})"))
            + "\n\n[!code-bash[Clone](generated/core-clone.sh)]\n";
        File.WriteAllText(Path.Combine(temp, "index.md"), page);
        var config = new { build = new { content = new[] { new { files = new[] { "index.md" } } }, xref = new[] { "generated/xrefmap.yml" }, template = new[] { "default" }, dest = "_site", disableGitFeatures = true } };
        File.WriteAllText(Path.Combine(temp, "docfx.json"), JsonSerializer.Serialize(config));
        foreach (var phase in new[] { "baseline", "move" })
        {
            if (phase == "move")
            {
                catalog["defaultOwner"] = "fixture-new-org";
                repos["python"]!["owner"] = "fixture-retained-owner";
                repos["core"]!["name"] = "fixture-renamed-core";
            }
            var refs = Generate(catalog, Path.Combine(temp, "generated"));
            Require(refs.Count == repos.Count + repos.Sum(p => p.Value!["links"]?.AsObject().Count ?? 0), "Missing root or deep reference.");
            foreach (var (id, repo) in repos)
            {
                var expected = $"https://github.com/{Text(repo!["owner"] ?? catalog["defaultOwner"])}/{Text(repo["name"])}";
                Require(refs.Single(r => r.uid == $"repo.{id}").href == expected, $"Wrong destination for {id} in {phase}.");
                Require(File.ReadAllText(Path.Combine(temp, "generated", $"{id}-clone.sh")).Trim() == $"git clone {expected}.git", $"Wrong clone command for {id}.");
            }
            var coreRoot = refs.Single(r => r.uid == "repo.core").href;
            Require(refs.Single(r => r.uid == "repo.core.readme").href == coreRoot + "/blob/main/README.md?plain=1#L10", "Deep suffix lost.");
            if (!render) continue;
            Docfx(root, tool, "build", Path.Combine(temp, "docfx.json"), "--warningsAsErrors");
            CheckHtml(Path.Combine(temp, "_site/index.html"), refs, coreRoot);
            if (phase == "move") Require(!File.ReadAllText(Path.Combine(temp, "_site/index.html")).Contains($"https://github.com/{originalOwner}/"), "Stale owner in output.");
        }
        foreach (var bad in new[] { "//external.example/path", "/../other", "/bad path", "https://external.example" })
        {
            repos["core"]!["links"]!["bad"] = bad;
            var rejected = false;
            try { Resolve(catalog); } catch (InvalidDataException) { rejected = true; }
            Require(rejected, $"Invalid suffix accepted: {bad}");
        }
        var live = Resolve(Load(catalogPath)).Select(r => r.uid).ToHashSet(StringComparer.Ordinal);
        foreach (var path in Directory.EnumerateFiles(Path.Combine(root, "apiDocs"), "*.md", SearchOption.AllDirectories)
            .Where(p => !p.Split(Path.DirectorySeparatorChar).Any(part => part is "api" or "external" or "generated" or "shims")))
        {
            var prose = Regex.Replace(File.ReadAllText(path), @"(?ms)^```.*?^```[^\r\n]*", "");
            foreach (Match match in Regex.Matches(prose, @"xref:(repo\.[A-Za-z0-9_.-]+)"))
                Require(live.Contains(match.Groups[1].Value), $"Unknown xref in {path}: {match}");
        }
        Console.WriteLine("PASS: organization propagation, owner override, rename, deep links, copyable snippets, invalid suffixes and authored xrefs.");
        if (!render) Console.WriteLine("Rendered checks skipped; add --render after dotnet tool restore.");
    }
    finally
    {
        var tempRoot = Path.GetFullPath(Path.GetTempPath()).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        Require(Path.GetFullPath(temp).StartsWith(tempRoot, StringComparison.OrdinalIgnoreCase), "Unexpected fixture cleanup path.");
        Directory.Delete(temp, true);
    }
}

static void Hub(string root, string catalogPath, string output, string? tool, bool serve)
{
    NavigationValidation.Sources(root, catalogPath);
    var docs = Path.Combine(root, "apiDocs");
    var generated = Path.Combine(docs, "generated/repository-links");
    var refs = Generate(Load(catalogPath), generated);
    var config = Load(Path.Combine(docs, "docfx.json"));
    var configPath = Path.Combine(generated, "hub.docfx.json");
    config["metadata"] = new JsonArray();
    var build = config["build"]!.AsObject();
    // Preview navigation lists conceptual pages only, without external API sections.
    // JSON avoids YAML scalar quoting pitfalls in titles containing colons.
    var pages = Directory.EnumerateFiles(docs, "*.md")
        .Concat(Directory.EnumerateFiles(Path.Combine(docs, "conceptual"), "*.md", SearchOption.AllDirectories))
        .OrderBy(p => p, StringComparer.Ordinal)
        .Select(p => new { name = File.ReadLines(p).FirstOrDefault(l => l.StartsWith("# ", StringComparison.Ordinal))?[2..]
            ?? Path.GetFileNameWithoutExtension(p), href = Path.GetRelativePath(generated, p).Replace('\\', '/') });
    File.WriteAllText(Path.Combine(generated, "toc.yml"), JsonSerializer.Serialize(pages));
    build["content"] = new JsonArray(
        new JsonObject { ["src"] = Path.GetRelativePath(generated, docs), ["files"] = new JsonArray("*.md", "conceptual/**/*.md") },
        new JsonObject { ["files"] = new JsonArray("toc.yml") });
    build["resource"] = new JsonArray();
    build["xref"] = new JsonArray(Path.Combine(generated, "xrefmap.yml")); // Hub validation is independent of remote BCL xref services.
    build["template"] = new JsonArray("default", Path.Combine(docs, "templates/wss"));
    build["dest"] = Path.GetFullPath(output);
    build["disableGitFeatures"] = true;
    File.WriteAllText(configPath, config.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
    Docfx(root, tool, "build", configPath);
    var html = File.ReadAllText(Path.Combine(output, "conceptual/repository-and-kit-links.html"));
    var used = Regex.Matches(File.ReadAllText(Path.Combine(docs, "conceptual/repository-and-kit-links.md")), @"xref:(repo\.[A-Za-z0-9_.-]+)")
        .Select(m => m.Groups[1].Value).ToHashSet(StringComparer.Ordinal);
    foreach (var reference in refs.Where(r => used.Contains(r.uid)))
        Require(html.Contains($"href=\"{WebUtility.HtmlEncode(reference.href)}\"", StringComparison.Ordinal), $"Catalog root missing from repository index: {reference.uid}");
    Console.WriteLine("PASS: hub repository index resolves catalog roots. This is a hub preview, not a complete release.");
    if (serve) Docfx(root, tool, "serve", Path.GetFullPath(output));
}

internal sealed record Reference(string uid, string name, string href);
