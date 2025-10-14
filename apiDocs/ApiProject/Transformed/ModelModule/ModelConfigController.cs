namespace WSSInterfacing {
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
/// High-level controller for <see cref="ModelConfig"/> persistence and keyed access.
/// Paths may be file or directory; if a directory is given, the file name <c>modelConfig.json</c> is used.
/// Dotted keys address JSON nodes, e.g. <c>"calib.mode"</c>, <c>"calib.targets.index.threshold"</c>.
/// </summary>
public sealed class ModelConfigController
{
    private ModelConfig _cfg;

    /// <summary>
    /// Create or open a model config at <paramref name="path"/>.
    /// If <paramref name="path"/> is a directory, appends <c>modelConfig.json</c>.
    /// Ensures the file exists by saving defaults when first created.
    /// </summary>
    /// <param name="path">
    /// File path like <c>"C:\data\modelConfig.json"</c> or directory like <c>"C:\data\"</c>.
    /// </param>
    /// <exception cref="ArgumentException">When <paramref name="path"/> is null or whitespace.</exception>
    /// <example>
    /// <code>
    /// var mc = new ModelConfigController(@"Configs\");      // uses Configs\modelConfig.json
    /// var mc2 = new ModelConfigController(@"Configs\m.json"); // uses given file directly
    /// </code>
    /// </example>
    public ModelConfigController(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Invalid config path.", nameof(path));

        bool isDir = Directory.Exists(path)
                  || path.EndsWith(Path.DirectorySeparatorChar.ToString())
                  || path.EndsWith(Path.AltDirectorySeparatorChar.ToString());

        string resolved = isDir ? Path.Combine(path, "modelConfig.json") : path;

        _cfg = new ModelConfig(resolved); // loads via DictConfigBase
        _cfg.Save();                      // persist defaults if first run
    }

    /// <summary>
    /// Save the current in-memory model configuration to disk.
    /// Uses <see cref="DictConfigBase.Save"/>.
    /// Call after any Set/Update to persist changes.
    /// </summary>
    /// <example>
    /// <code>
    /// mc.SetModelParam("calib.mode", "midpoint");
    /// mc.SaveModelJson();
    /// </code>
    /// </example>
    public void SaveModelJson() => _cfg.Save();

    /// <summary>
    /// Reload the current model configuration from its existing path.
    /// Reinstantiates the underlying <see cref="ModelConfig"/> so disk edits are reflected in memory.
    /// </summary>
    /// <example>
    /// <code>
    /// // external tool edited the JSON on disk:
    /// mc.LoadModelJson(); // refresh memory view
    /// </code>
    /// </example>
    public void LoadModelJson()
    {
        _cfg = new ModelConfig(_cfg.Path);
    }

    /// <summary>
    /// Load a model configuration from a new path. If <paramref name="path"/> is a directory,
    /// appends <c>modelConfig.json</c>.
    /// Replaces the active config instance.
    /// </summary>
    /// <param name="path">File path like <c>"..\profiles\a.json"</c> or directory like <c>"..\profiles\"</c>.</param>
    /// <exception cref="ArgumentException">When <paramref name="path"/> is null or whitespace.</exception>
    /// <example>
    /// <code>
    /// mc.LoadModelJson(@"Profiles\UserA\");       // Profiles\UserA\modelConfig.json
    /// mc.LoadModelJson(@"Profiles\AltModel.json"); // specific file
    /// </code>
    /// </example>
    public void LoadModelJson(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Invalid config path.", nameof(path));

        bool isDir = Directory.Exists(path)
                  || path.EndsWith(Path.DirectorySeparatorChar.ToString())
                  || path.EndsWith(Path.AltDirectorySeparatorChar.ToString());

        string resolved = isDir ? Path.Combine(path, "modelConfig.json") : path;

        _cfg = new ModelConfig(resolved);
    }

    // ---------- Keyed access ----------

    /// <summary>
    /// Set a value at the dotted key and immediately save.
    /// Use for scalars or small objects that serialize to a JSON value.
    /// </summary>
    /// <typeparam name="T">Serializable type (e.g., <c>string</c>, <c>int</c>, <c>float</c>, small POCO).</typeparam>
    /// <param name="key">Dotted key, e.g., <c>"calib.mode"</c>, <c>"calib.targets.index.threshold"</c>.</param>
    /// <param name="value">Value to write.</param>
    /// <exception cref="ArgumentException">If <paramref name="key"/> is null or whitespace.</exception>
    /// <example>
    /// <code>
    /// mc.SetModelParam("calib.mode", "midpoint");
    /// mc.SetModelParam("calib.targets.index.threshold", 0.42f);
    /// </code>
    /// </example>
    public void SetModelParam<T>(string key, T value)
    {
        ValidateKey(key);
        _cfg.Set(key, JToken.FromObject(value!));
        _cfg.Save();
    }

    /// <summary>
    /// Get a required typed value from a dotted key.
    /// Throws if the key is missing or the value cannot be converted to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Expected return type.</typeparam>
    /// <param name="key">Dotted key, e.g., <c>"calib.mode"</c>.</param>
    /// <returns>Value at the key as <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">If <paramref name="key"/> is null or whitespace.</exception>
    /// <exception cref="KeyNotFoundException">If the key does not exist.</exception>
    /// <example>
    /// <code>
    /// string mode = mc.GetModelParam&lt;string&gt;("calib.mode");
    /// float thr   = mc.GetModelParam&lt;float&gt;("calib.targets.index.threshold");
    /// </code>
    /// </example>
    public T GetModelParam<T>(string key)
    {
        ValidateKey(key);
        return _cfg.GetRequired<T>(key);
    }

    /// <summary>
    /// Try to get a typed value without throwing.
    /// </summary>
    /// <typeparam name="T">Expected return type.</typeparam>
    /// <param name="key">Dotted key, e.g., <c>"calib.targets.middle.gain"</c>.</param>
    /// <param name="value">Output value when found and convertible.</param>
    /// <returns><c>true</c> if the key exists and was converted; otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentException">If <paramref name="key"/> is null or whitespace.</exception>
    /// <example>
    /// <code>
    /// if (mc.TryGetModelParam("calib.targets.thumb.offset", out float off)) { /* use off */ }
    /// </code>
    /// </example>
    public bool TryGetModelParam<T>(string key, out T value) where T : notnull
    {
        ValidateKey(key);
        return _cfg.TryGet<T>(key, out value);
    }

    /// <summary>
    /// Get a typed value or a default when missing.
    /// Does not throw on absent keys.
    /// </summary>
    /// <typeparam name="T">Expected return type.</typeparam>
    /// <param name="key">Dotted key, e.g., <c>"calib.sampling.rateHz"</c>.</param>
    /// <param name="dflt">Fallback value to return when the key is missing.</param>
    /// <returns>Value at the key, or <paramref name="dflt"/> if absent.</returns>
    /// <example>
    /// <code>
    /// int rate = mc.GetOrDefault("calib.sampling.rateHz", 1000);
    /// </code>
    /// </example>
    public T GetOrDefault<T>(string key, T dflt = default!) => _cfg.GetOrDefault(key, dflt);

    // ---------- Calib helpers ----------

    /// <summary>
    /// Get the modelâ€™s calibration mode from <c>"calib.mode"</c>. Throws if missing.
    /// </summary>
    /// <returns>Calibration mode string, e.g., <c>"midpoint"</c>, <c>"sweep"</c>.</returns>
    /// <example>
    /// <code>
    /// string mode = mc.GetCalibMode();
    /// </code>
    /// </example>
    public string GetCalibMode() => _cfg.GetModeRequired();

    /// <summary>
    /// Set <c>"calib.mode"</c> and save.
    /// </summary>
    /// <param name="mode">Non-empty mode string, e.g., <c>"midpoint"</c>.</param>
    /// <exception cref="ArgumentException">If <paramref name="mode"/> is null or whitespace.</exception>
    /// <example>
    /// <code>
    /// mc.SetCalibMode("midpoint");
    /// </code>
    /// </example>
    public void SetCalibMode(string mode)
    {
        if (string.IsNullOrWhiteSpace(mode)) throw new ArgumentException("Mode must be non-empty.", nameof(mode));
        _cfg.Set("calib.mode", JToken.FromObject(mode));
        _cfg.Save();
    }

    /// <summary>
    /// Return a flat snapshot of scalar values under <c>"calib"</c>.
    /// Keys are dotted, e.g., <c>"calib.mode"</c>, <c>"calib.targets.index.threshold"</c>.
    /// </summary>
    /// <returns>A copy of key/value pairs as strings.</returns>
    /// <example>
    /// <code>
    /// var all = mc.GetAllCalibParams();
    /// foreach (var kv in all) Debug.Log($"{kv.Key} = {kv.Value}");
    /// </code>
    /// </example>
    public Dictionary<string, string> GetAllCalibParams()
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var root = Root();
        if (root["calib"] is JObject calib) CollectScalars("calib", calib, result);
        return result;
    }

    // ---------- Internals ----------

    /// <summary>Validate non-empty keys.</summary>
    private static void ValidateKey(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Key must be non-empty.", nameof(name));
    }

    /// <summary>Access underlying JSON object from the config.</summary>
    private JObject Root()
    {
        var f = typeof(DictConfigBase).GetField("_root",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (JObject)f.GetValue(_cfg);
    }

    /// <summary>Collect plain (non-array, non-object) JSON values into a flat dotted map.</summary>
    private static void CollectScalars(string prefix, JObject obj, Dictionary<string, string> acc)
    {
        foreach (var kv in obj)
        {
            string key = string.IsNullOrEmpty(prefix) ? kv.Key : $"{prefix}.{kv.Key}";
            if (kv.Value is JObject child) { CollectScalars(key, child, acc); }
            else if (kv.Value is JValue v && v.Type != JTokenType.Object && v.Type != JTokenType.Array)
            {
                acc[key] = v.ToString();
            }
        }
    }
}


}
