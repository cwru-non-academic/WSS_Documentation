namespace WSSInterfacing {
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
/// Provides high-level access to stimulation parameter configuration.
/// Automatically seeds channel entries based on max WSS count and ensures safe access.
/// Supports only dotted keys of form "stim.ch.{N}.{leaf}".
/// </summary>
public sealed class StimParamsConfigController
{
    /// <summary>Fixed number of channels per WSS hardware unit.</summary>
    private const int ChannelsPerWss = 3;

    private StimParamsConfig _cfg;
    private readonly int _maxWss;
    private readonly int _totalChannels;

    /// <summary>Number of WSS units allowed by configuration.</summary>
    public int MaxWss => _maxWss;

    /// <summary>Total number of channels (ChannelsPerWss Ã— MaxWss).</summary>
    public int TotalChannels => _totalChannels;

    /// <summary>Hardware constant: channels per WSS.</summary>
    public int PerWss => ChannelsPerWss;

    /// <summary>
    /// Constructs a new stimulation parameter controller using the provided JSON path and max WSS value.
    /// Automatically creates default channel entries if missing.
    /// </summary>
    /// <param name="path">Path to the JSON configuration file.</param>
    /// <param name="maxWss">Maximum number of WSS devices supported by this instance.</param>
    public StimParamsConfigController(string path, int maxWss)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Invalid config path.", nameof(path));

        bool isDir = Directory.Exists(path)
                  || path.EndsWith(Path.DirectorySeparatorChar.ToString())
                  || path.EndsWith(Path.AltDirectorySeparatorChar.ToString());

        string resolved = isDir ? Path.Combine(path, "stimParams.json") : path;

        _maxWss = maxWss < 1 ? 1 : maxWss;
        _totalChannels = ChannelsPerWss * _maxWss;

        // Loads from disk and merges defaults internally (via DictConfigBase).
        _cfg = new StimParamsConfig(resolved);  // loads here 

        EnsureTemplate(_totalChannels);
        _cfg.Save(); // persist any backfill 
    }

    /// <summary>
    /// Persists the current stimulation params to disk using the underlying config's Save().
    /// Thread-safe via the config layer.
    /// </summary>
    public void SaveParamsJson() => _cfg.Save(); // DictConfigBase.Save() writes _root to Path. :contentReference[oaicite:0]{index=0}

    /// <summary>
    /// Reloads the current context file (same path used at construction or the last Load(path)).
    /// Reinstantiates the config to re-read JSON from disk, then ensures channel template exists.
    /// </summary>
    public void LoadParamsJson()
    {
        // StimParamsConfig(path) loads via DictConfigBase ctor -> JsonReader.LoadJObject(...)
        _cfg = new StimParamsConfig(_cfg.Path);  // uses stored Path property. :contentReference[oaicite:1]{index=1} :contentReference[oaicite:2]{index=2}
        EnsureTemplate(_totalChannels);
    }

    /// <summary>
    /// Loads a new context file. If <paramref name="path"/> is a directory,
    /// uses "stimParams.json" within it. Replaces the active config and backfills the template.
    /// </summary>
    /// <param name="path">Absolute/relative file path or directory.</param>
    /// <exception cref="ArgumentException">If the path is null or whitespace.</exception>
    public void LoadParamsJson(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Invalid config path.", nameof(path));

        bool isDir = Directory.Exists(path)
                || path.EndsWith(Path.DirectorySeparatorChar.ToString())
                || path.EndsWith(Path.AltDirectorySeparatorChar.ToString());

        string resolved = isDir ? Path.Combine(path, "stimParams.json") : path;

        _cfg = new StimParamsConfig(resolved);   // constructor performs the load. :contentReference[oaicite:3]{index=3}
        EnsureTemplate(_totalChannels);
    }


    // ---------- Generic Key/Value Access ----------

    /// <summary>
    /// Adds or updates a stimulation parameter at the specified dotted key.
    /// </summary>
    /// <param name="key">Parameter key (e.g., "stim.ch.1.amp").</param>
    /// <param name="value">Value to assign.</param>
    public void AddOrUpdateStimParam(string key, float value)
    {
        ValidateKey(key);
        EnforceChannelBoundsIfKey(key);
        _cfg.Set(key, JToken.FromObject(value));
        _cfg.Save();
    }

    /// <summary>
    /// Retrieves a stimulation parameter value.
    /// </summary>
    /// <param name="key">Parameter key (e.g., "stim.ch.1.maxPW").</param>
    /// <returns>Parameter value as float.</returns>
    public float GetStimParam(string key)
    {
        ValidateKey(key);
        EnforceChannelBoundsIfKey(key);
        return _cfg.GetFloat(key);
    }

    /// <summary>
    /// Attempts to retrieve a stimulation parameter value without throwing.
    /// </summary>
    /// <param name="key">Parameter key.</param>
    /// <param name="value">Output value if found.</param>
    /// <returns>True if the parameter exists.</returns>
    public bool TryGetStimParam(string key, out float value)
    {
        ValidateKey(key);
        EnforceChannelBoundsIfKey(key);
        if (_cfg.TryGetFloat(key, out var v)) { value = v; return true; }
        value = default;
        return false;
    }

    /// <summary>
    /// Returns all numeric stimulation parameters as a flat dictionary of dotted keys and float values.
    /// </summary>
    public Dictionary<string, float> GetAllStimParams()
    {
        var result = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
        if (Root()["stim"] is JObject stimObj) CollectFloats("stim", stimObj, result);
        return result;
    }

    // ---------- Per-Channel Access ----------

    private static string ChKey(int ch, string leaf) => $"stim.ch.{ch}.{leaf}";

    /// <summary>Sets channel amplitude in milliamps.</summary>
    public void SetChannelAmp(int ch, float mA) { ValidateChannel(ch); AddOrUpdateStimParam(ChKey(ch, "amp"), mA); }

    /// <summary>Sets minimum pulse width for the specified channel in micro seconds.</summary>
    public void SetChannelPWMin(int ch, int us) { ValidateChannel(ch); AddOrUpdateStimParam(ChKey(ch, "minPW"), us); }

    /// <summary>Sets maximum pulse width for the specified channel in micro seconds.</summary>
    public void SetChannelPWMax(int ch, int us) { ValidateChannel(ch); AddOrUpdateStimParam(ChKey(ch, "maxPW"), us); }

    /// <summary>Sets inter-pulse interval (IPI) for the specified channel in mili seconds.</summary>
    public void SetChannelIPI(int ch, int ms)   { ValidateChannel(ch); AddOrUpdateStimParam(ChKey(ch, "IPI"), ms);  }

    /// <summary>Gets channel amplitude in mili amps.</summary>
    public float GetChannelAmp(int ch) { ValidateChannel(ch); return GetStimParam(ChKey(ch, "amp")); }

    /// <summary>Gets minimum pulse width for a channel in micro seconds.</summary>
    public int GetChannelPWMin(int ch) { ValidateChannel(ch); return (int)GetStimParam(ChKey(ch, "minPW")); }

    /// <summary>Gets maximum pulse width for a channel in micro seconds.</summary>
    public int GetChannelPWMax(int ch) { ValidateChannel(ch); return (int)GetStimParam(ChKey(ch, "maxPW")); }

    /// <summary>Gets inter-pulse interval (IPI) for a channel in mili seconds.</summary>
    public int GetChannelIPI(int ch)   { ValidateChannel(ch); return (int)GetStimParam(ChKey(ch, "IPI")); }

    /// <summary>Checks whether a channel index is within the valid range.</summary>
    public bool IsChannelInRange(int ch) => ch >= 1 && ch <= _totalChannels;

    // ---------- Private Helpers ----------

    /// <summary>Validates that the channel index is within the allowed range.</summary>
    private void ValidateChannel(int ch)
    {
        if (ch < 1 || ch > _totalChannels)
            throw new ArgumentOutOfRangeException(nameof(ch), $"Channel must be 1..{_totalChannels}.");
    }

    /// <summary>Ensures a dotted key refers to a valid channel if it contains "stim.ch.{N}".</summary>
    private void EnforceChannelBoundsIfKey(string key)
    {
        if (!key.StartsWith("stim.ch.", StringComparison.OrdinalIgnoreCase)) return;

        var seg = key.Split('.');
        if (seg.Length < 4) return;
        if (int.TryParse(seg[2], out int ch)) ValidateChannel(ch);
    }

    /// <summary>Creates missing channel entries in the JSON structure up to the total channel count.</summary>
    private void EnsureTemplate(int totalChannels)
    {
        var root = Root();
        if (root["stim"] is not JObject stim) { stim = new JObject(); root["stim"] = stim; }
        if (stim["ch"]  is not JObject chObj) { chObj = new JObject(); stim["ch"] = chObj; }

        for (int ch = 1; ch <= totalChannels; ch++)
        {
            if (chObj[$"{ch}"] is not JObject n) { n = new JObject(); chObj[$"{ch}"] = n; }
            if (n["amp"]   == null) n["amp"]   = 0.0;
            if (n["minPW"] == null) n["minPW"] = 0;
            if (n["maxPW"] == null) n["maxPW"] = 0;
            if (n["IPI"]   == null) n["IPI"]   = 0;
        }
    }

    /// <summary>Accesses the root JObject of the loaded configuration via reflection.</summary>
    private JObject Root()
    {
        var f = typeof(DictConfigBase).GetField("_root",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (JObject)f.GetValue(_cfg);
    }

    /// <summary>Recursively collects all numeric values under a JSON subtree into a flat key/value map.</summary>
    private static void CollectFloats(string prefix, JObject obj, Dictionary<string, float> acc)
    {
        foreach (var kv in obj)
        {
            string key = string.IsNullOrEmpty(prefix) ? kv.Key : $"{prefix}.{kv.Key}";
            if (kv.Value is JObject child) CollectFloats(key, child, acc);
            else if (kv.Value is JValue v && (v.Type == JTokenType.Float || v.Type == JTokenType.Integer))
                acc[key] = v.Type == JTokenType.Integer ? v.Value<int>() : v.Value<float>();
        }
    }

    /// <summary>Ensures that a configuration key string is valid and not empty.</summary>
    private static void ValidateKey(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Key must be non-empty.", nameof(name));
    }
}

}
