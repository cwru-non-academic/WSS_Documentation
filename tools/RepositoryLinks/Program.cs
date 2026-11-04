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
    foreach (var (id, node) in catalog["externalLinks"]?.AsObject() ?? new JsonObject())
    {
        Slug(id);
        var href = Text(node?["href"]);
        if (!Uri.TryCreate(href, UriKind.Absolute, out var uri) || uri.Scheme != "https"
            || string.IsNullOrEmpty(uri.Host) || !string.IsNullOrEmpty(uri.UserInfo)
            || href.Any(c => char.IsWhiteSpace(c) || "\\\"<>".Contains(c)))
            throw new InvalidDataException($"External link {id} must be an absolute HTTPS URL without credentials.");
        refs.Add(new($"link.{id}", Text(node?["name"]), href));
    }
    if (refs.Select(r => r.uid).Distinct(StringComparer.OrdinalIgnoreCase).Count() != refs.Count)
        throw new InvalidDataException("Duplicate repository link UID.");
    return refs;
}

static List<Reference> Generate(JsonObject catalog, string output)
{
    var refs = Resolve(catalog); // Fully validate before creating output.
    var pins = VerificationPins(catalog, refs);
    Directory.CreateDirectory(output);
    // JSON is YAML-compatible and accepted by DocFX as an xref map.
    File.WriteAllText(Path.Combine(output, "xrefmap.yml"), JsonSerializer.Serialize(new { references = refs }, new JsonSerializerOptions { WriteIndented = true }));
    foreach (var id in catalog["repositories"]!.AsObject().Select(p => p.Key))
        File.WriteAllText(Path.Combine(output, $"{id}-clone.sh"), $"git clone {refs.Single(r => r.uid == $"repo.{id}").href}.git\n");
    File.WriteAllText(Path.Combine(output, "verification-pins.md"), pins);
    Console.WriteLine($"Generated {refs.Count} repository references in {output}");
    return refs;
}

static string VerificationPins(JsonObject catalog, List<Reference> refs)
{
    if (catalog["verificationPins"] == null) return "";
    string Cell(JsonNode? node)
    {
        var value = Text(node);
        if (value.Length == 0 || value.Any(c => "\r\n|<>[]".Contains(c)))
            throw new InvalidDataException("Verification pin text must be a nonempty table cell.");
        return value;
    }
    var snapshot = Text(catalog["verificationSnapshotDate"]);
    if (!DateOnly.TryParseExact(snapshot, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out _))
        throw new InvalidDataException("Verification snapshot date must use yyyy-MM-dd.");
    var lines = new List<string> { $"**Snapshot inspected: {snapshot}. These values are not live deployment status.**", "",
        "| Verification identity | Recorded value | Purpose | Inspected immutable source |", "| --- | --- | --- | --- |" };
    foreach (var (id, pin) in catalog["verificationPins"]?.AsObject() ?? new JsonObject())
    {
        Slug(id);
        var value = Text(pin?["value"]);
        var kind = Text(pin?["kind"]);
        var length = kind switch { "commit" => 40, "sha256" => 64, _ => throw new InvalidDataException($"Unknown pin kind: {kind}") };
        if (!Regex.IsMatch(value, $"\\A[0-9a-f]{{{length}}}\\z"))
            throw new InvalidDataException($"Pin {id} requires a full lowercase {kind} value.");
        var source = Text(pin?["source"]);
        var reference = refs.SingleOrDefault(r => r.uid == source);
        if (reference == null || !source.StartsWith("repo.", StringComparison.Ordinal)
            || !Regex.IsMatch(reference.href, @"/blob/[0-9a-f]{40}/[^?#]+"))
            throw new InvalidDataException($"Verification pin {id} requires a commit-pinned configuration source: {source}");
        lines.Add($"| {Cell(pin?["name"])} | `{value}` ({kind}) | {Cell(pin?["purpose"])} | [Inspected source](xref:{source}) |");
    }
    return string.Join("\n", lines) + "\n";
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
        VerificationPins(catalog, Resolve(catalog));
        var originalOwner = Text(catalog["defaultOwner"]);
        var repos = catalog["repositories"]!.AsObject();
        repos["core"]!["links"] = new JsonObject { ["readme"] = "/blob/main/README.md?plain=1#L10",
            ["reviewed-source"] = "/blob/" + new string('c', 40) + "/.github/fixture.json" };
        // Resolve every catalog root in a temporary page; the actual catalog is never modified.
        catalog["externalLinks"] = new JsonObject { ["fixture"] = new JsonObject { ["name"] = "External fixture", ["href"] = "https://example.org/before?mode=one#anchor" } };
        catalog["verificationPins"] = new JsonObject { ["fixture"] = new JsonObject {
            ["name"] = "Fixture commit", ["kind"] = "commit", ["value"] = new string('a', 40),
            ["purpose"] = "Reviewed source identity", ["source"] = "repo.core.reviewed-source" } };
        var page = "# Repository fixture\n\n" + string.Join("\n\n", Resolve(catalog).Select(r => $"[{r.uid}](xref:{r.uid})"))
            + "\n\n[!code-bash[Clone](generated/core-clone.sh)]\n\n[!include[](generated/verification-pins.md)]\n";
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
                catalog["externalLinks"]!["fixture"]!["href"] = "https://example.net/after?mode=two#changed";
                catalog["verificationPins"]!["fixture"]!["value"] = new string('b', 40);
            }
            var refs = Generate(catalog, Path.Combine(temp, "generated"));
            Require(refs.Count == repos.Count + repos.Sum(p => p.Value!["links"]?.AsObject().Count ?? 0) + 1, "Missing root, deep or external reference.");
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
            var rendered = File.ReadAllText(Path.Combine(temp, "_site/index.html"));
            Require(rendered.Contains(new string(phase == "move" ? 'b' : 'a', 40)), "Generated pin table did not render its current value.");
            if (phase == "move") Require(!rendered.Contains(new string('a', 40)) && !rendered.Contains("https://example.org/before"), "Stale pin or external URL remained after catalog edit.");
            if (phase == "move") Require(!File.ReadAllText(Path.Combine(temp, "_site/index.html")).Contains($"https://github.com/{originalOwner}/"), "Stale owner in output.");
        }
        foreach (var bad in new[] { "http://example.org", "javascript:alert(1)", "https://user:password@example.org", "//example.org" })
        {
            catalog["externalLinks"]!["fixture"]!["href"] = bad;
            var rejected = false;
            try { Resolve(catalog); } catch (InvalidDataException) { rejected = true; }
            Require(rejected, $"Invalid external destination accepted: {bad}");
        }
        catalog["externalLinks"]!["fixture"]!["href"] = "https://example.net/after";
        catalog["verificationPins"]!["fixture"]!["value"] = "main";
        var floatingRejected = false;
        try { Generate(catalog, Path.Combine(temp, "invalid")); } catch (InvalidDataException) { floatingRejected = true; }
        Require(floatingRejected && !Directory.Exists(Path.Combine(temp, "invalid")), "Floating pin accepted or invalid catalog wrote output.");
        catalog["verificationPins"]!["fixture"]!["value"] = new string('b', 40);
        catalog["verificationPins"]!["fixture"]!["source"] = "repo.core.readme";
        var floatingSourceRejected = false;
        try { Generate(catalog, Path.Combine(temp, "invalid-source")); } catch (InvalidDataException) { floatingSourceRejected = true; }
        Require(floatingSourceRejected && !Directory.Exists(Path.Combine(temp, "invalid-source")), "Moving-branch snapshot source accepted.");
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
            foreach (Match match in Regex.Matches(prose, @"xref:((?:repo|link)\.[A-Za-z0-9_.-]+)"))
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
