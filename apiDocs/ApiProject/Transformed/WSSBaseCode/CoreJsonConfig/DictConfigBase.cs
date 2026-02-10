namespace WSSInterfacing {
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

/// <summary>
/// Base class for JSON-backed configuration objects that expose dotted-key access.
/// Wraps a <see cref="JObject"/> and provides thread-safe getters/setters and save.
/// </summary>
public abstract class DictConfigBase
{
    protected readonly object _sync = new();
    /// <summary>Root JSON object for this configuration.</summary>
    protected JObject _root;
    /// <summary>Resolved file path used for persistence.</summary>
    public string Path { get; }

    /// <summary>
    /// Initializes the configuration by loading a JSON file (or writing defaults when missing).
    /// </summary>
    /// <param name="path">Destination file path. Parent directory is created when needed.</param>
    /// <param name="defaults">Default JSON content to save and use when the file does not exist.</param>
    protected DictConfigBase(string path, JObject defaults)
    {
        Path = path;
        _root = JsonReader.LoadJObject(path, defaults);
    }

    /// <summary>Persists the current JSON content to <see cref="Path"/>.</summary>
    public void Save()
    { lock (_sync) JsonReader.SaveJObject(Path, _root); }

    /// <summary>Try to read a float from a dotted key.</summary>
    public bool TryGetFloat(string key, out float v)
    { lock (_sync) { v = 0f; var t = _root.SelectToken(key); if (t==null) return false; v = t.Value<float>(); return true; } }

    /// <summary>Try to read an int from a dotted key.</summary>
    public bool TryGetInt(string key, out int v)
    { lock (_sync) { v = 0; var t = _root.SelectToken(key); if (t==null) return false; v = t.Value<int>(); return true; } }

    /// <summary>Try to read a string from a dotted key.</summary>
    public bool TryGetString(string key, out string s)
    { lock (_sync) { s = null; var t = _root.SelectToken(key); if (t==null) return false; s = t.Type==JTokenType.String ? (string)t : t.ToString(); return true; } }

    /// <summary>Try to read a float array from a dotted key.</summary>
    public bool TryGetFloatArray(string key, out float[] arr)
    {
        lock (_sync)
        {
            arr = null;
            var t = _root.SelectToken(key);
            if (t is not JArray a) return false;
            var list = new List<float>();
            foreach (var x in a) list.Add(x.Value<float>());
            arr = list.ToArray(); return true;
        }
    }

    /// <summary>Gets a float or returns a default when missing.</summary>
    public float GetFloat(string key, float dflt = 0f) => TryGetFloat(key, out var v) ? v : dflt;
    /// <summary>Gets an int or returns a default when missing.</summary>
    public int   GetInt  (string key, int dflt = 0)   => TryGetInt  (key, out var v) ? v : dflt;
    /// <summary>Gets a string or returns a default when missing.</summary>
    public string GetString(string key, string dflt="") => TryGetString(key, out var s) ? s : dflt;

    /// <summary>
    /// Writes <paramref name="value"/> at <paramref name="dottedKey"/>, creating intermediate objects as needed.
    /// </summary>
    /// <param name="dottedKey">Dotted key, e.g., "constants.PModeProportional".</param>
    /// <param name="value">JSON token to assign.</param>
    public void Set(string dottedKey, JToken value)
    {
        lock (_sync)
        {
            var parts = dottedKey.Split('.');
            JObject cur = _root;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var p = parts[i];
                if (cur[p] is not JObject next) { next = new JObject(); cur[p] = next; }
                cur = next;
            }
            cur[parts[^1]] = value;
        }
    }
}

}
