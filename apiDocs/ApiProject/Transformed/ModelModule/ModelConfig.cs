namespace WSSInterfacing {
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

/// <summary>
/// JSON-backed model configuration that stores controller mode and constants
/// under a dotted-key hierarchy (e.g., "calib.mode", "constants.PModeProportional").
/// Provides typed access helpers via <see cref="DictConfigBase"/>.
/// </summary>
public sealed class ModelConfig : DictConfigBase
{
    public ModelConfig(string path)
    : base(path, defaults: new JObject
    {
        ["calib"] = new JObject
        {
            ["mode"] = "P"
        },
        ["constants"] = new JObject
        {
            ["PModeProportional"]  = 1.0,
            ["PModeOffset"]        = 0.0,
            ["PDModeProportional"] = 0.5,
            ["PDModeDerivative"]   = 0.2,
            ["PDModeOffset"]       = 0.0
        }
    })
    { }


    /// <summary>
    /// Gets the required calibration mode from key "calib.mode". Throws if missing.
    /// </summary>
    public string GetModeRequired() => GetRequired<string>("calib.mode");

    // Generic helpers
    /// <summary>
    /// Attempts to read a value at <paramref name="key"/> and convert it to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Target type (non-nullable).</typeparam>
    /// <param name="key">Dotted key path.</param>
    /// <param name="value">Result when conversion succeeds.</param>
    /// <returns><c>true</c> if the value exists and converts; otherwise <c>false</c>.</returns>
    public bool TryGet<T>(string key, out T value) where T : notnull
    {
        lock (_sync)
        {
            value = default!;
            var tok = _root.SelectToken(key);
            if (tok == null) return false;
            try { value = tok.ToObject<T>(); return true; }
            catch { return false; }
        }
    }

    /// <summary>
    /// Returns the value at <paramref name="key"/> or a default when missing.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    /// <param name="key">Dotted key path.</param>
    /// <param name="dflt">Fallback value when the key is absent.</param>
    public T GetOrDefault<T>(string key, T dflt = default!)
    {
        return TryGet<T>(key, out var v) ? v : dflt!;
    }

    /// <summary>
    /// Gets a required value at <paramref name="key"/>. Throws if missing or invalid.
    /// </summary>
    /// <typeparam name="T">Target type.</typeparam>
    /// <param name="key">Dotted key path.</param>
    /// <exception cref="KeyNotFoundException">When the key is not present.</exception>
    public T GetRequired<T>(string key)
    {
        if (TryGet<T>(key, out var v)) return v;
        throw new KeyNotFoundException($"Missing required parameter '{key}' in {Path}");
    }
}


}
