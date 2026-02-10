namespace WSSInterfacing {
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Lightweight helpers for loading and saving JSON configuration files.
/// Provides typed object and <see cref="Newtonsoft.Json.Linq.JObject"/> utilities and
/// ensures parent directories exist before writes.
/// </summary>
public static class JsonReader
{
    /// <summary>
    /// Loads a JSON file into a typed object. If the file is missing or invalid,
    /// writes <paramref name="defaults"/> to disk and returns it.
    /// </summary>
    /// <typeparam name="T">Reference type to deserialize to.</typeparam>
    /// <param name="path">File path to read from.</param>
    /// <param name="defaults">Default instance to persist and return when missing/invalid.</param>
    /// <returns>The deserialized object, or <paramref name="defaults"/> when not present/invalid.</returns>
    public static T LoadObject<T>(string path, T defaults) where T : class
    {
        if (File.Exists(path))
        {
            var txt = File.ReadAllText(path);
            var obj = JsonConvert.DeserializeObject<T>(txt);
            if (obj != null) return obj;
        }
        SaveObject(path, defaults);
        return defaults;
    }

    /// <summary>
    /// Serializes <paramref name="obj"/> to indented JSON at <paramref name="path"/>.
    /// Creates the parent directory when needed.
    /// </summary>
    /// <typeparam name="T">Type of the object to serialize.</typeparam>
    /// <param name="path">Destination file path.</param>
    /// <param name="obj">Object to serialize.</param>
    public static void SaveObject<T>(string path, T obj)
    {
        EnsureDir(path);
        File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
    }

    /// <summary>
    /// Loads a JSON file into a <see cref="JObject"/>. If the file is missing,
    /// writes <paramref name="defaults"/> to disk and returns it.
    /// </summary>
    /// <param name="path">File path to read from.</param>
    /// <param name="defaults">Default JSON object to persist and return when missing.</param>
    /// <returns>Parsed <see cref="JObject"/>.</returns>
    public static JObject LoadJObject(string path, JObject defaults)
    {
        if (File.Exists(path))
        {
            var txt = File.ReadAllText(path);
            return JObject.Parse(txt);
        }
        SaveJObject(path, defaults);
        return defaults;
    }

    /// <summary>
    /// Writes a <see cref="JObject"/> to disk with indented formatting.
    /// Ensures the parent directory exists.
    /// </summary>
    /// <param name="path">Destination file path.</param>
    /// <param name="obj">JSON object to write.</param>
    public static void SaveJObject(string path, JObject obj)
    {
        EnsureDir(path);
        File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
    }

    /// <summary>
    /// Ensures that the directory containing <paramref name="path"/> exists.
    /// No-op when the directory is null, empty, or already present.
    /// </summary>
    /// <param name="path">Target file path.</param>
    private static void EnsureDir(string path)
    {
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
    }
}

}
