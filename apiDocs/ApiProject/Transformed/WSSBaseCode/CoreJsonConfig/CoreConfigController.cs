namespace WSSInterfacing {
using System;
using System.IO;
using System.Threading;

/// <summary>
/// Controls loading, validating, reading, and writing a stimulation configuration JSON file.
/// Thread-safe. Ensures a valid default config exists on disk.
/// </summary>
public sealed class CoreConfigController : ICoreConfig
{
    private readonly object _sync = new object();
    private CoreConfig _config;
    private bool _jsonLoaded;

    /// <summary>Resolved file path to the configuration JSON.</summary>
    public string _configPath { get; private set; }

    /// <summary>
    /// Initializes a controller that reads/writes "stimConfig.json" in the current directory.
    /// </summary>
    public CoreConfigController() : this(Path.Combine(Environment.CurrentDirectory, "stimConfig.json")) { }

    /// <summary>
    /// Initializes a controller pointing to a custom path.
    /// If a directory path is provided, "stimConfig.json" is created inside it.
    /// </summary>
    /// <param name="path">Absolute or relative file path, or a directory path.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="path"/> is null, empty, or whitespace.</exception>
    public CoreConfigController(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Invalid config path.", nameof(path));

        // If a directory was provided, append the default filename.
        if (Directory.Exists(path)
            || path.EndsWith(Path.DirectorySeparatorChar.ToString())
            || path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
        {
            _configPath = Path.Combine(path, "stimConfig.json");
        }
        else
        {
            _configPath = path;
        }
        _config = JsonReader.LoadObject(_configPath, new CoreConfig());
        Volatile.Write(ref _jsonLoaded, true);
    }

    /// <summary>
    /// Indicates whether the JSON configuration has been loaded into memory.
    /// </summary>
    public bool IsLoaded => Volatile.Read(ref _jsonLoaded);

    /// <summary>
    /// Loads the configuration from disk. Creates and saves a default config if missing or invalid.
    /// Safe to call multiple times.
    /// </summary>
    public void LoadJson() {
        lock (_sync)
        {
            _config = JsonReader.LoadObject(_configPath, new CoreConfig());
            Volatile.Write(ref _jsonLoaded, true);
        }
    } 

    /// <summary>
    /// Loads the core configuration from disk at the specified <paramref name="path"/>.
    /// If <paramref name="path"/> is a directory, the file name <c>core.json</c> is appended.
    /// If the file does not exist or is invalid, a default configuration is created and saved.
    /// Safe to call multiple times; the operation is thread-safe.
    /// </summary>
    /// <param name="path">
    /// Absolute or relative file path, or a directory path ending with a path separator.
    /// </param>
    public void LoadJson(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Invalid config path.", nameof(path));

        lock (_sync)
        {
            // Treat trailing separator or existing directory as a directory input
            bool isDir = Directory.Exists(path)
                        || path.EndsWith(Path.DirectorySeparatorChar.ToString())
                        || path.EndsWith(Path.AltDirectorySeparatorChar.ToString());

            _configPath = isDir ? Path.Combine(path, "stimConfig.json") : path;

            _config = JsonReader.LoadObject(_configPath, new CoreConfig());
            Volatile.Write(ref _jsonLoaded, true);
        }
    }

    /// <summary>
    /// Threads safe, persists the current configuration to disk.
    /// No-op if not loaded yet.
    /// </summary>
    public void SaveJson()
    {
        lock (_sync)
        {
            if (!IsLoaded) return;
            JsonReader.SaveObject(_configPath, _config);
        }
    }

    /// <summary>
    /// Gets max number of WSS supported by this app.
    /// Thread safe get.
    /// </summary>
    public int MaxWss
    {
        get { lock (_sync) return _config.maxWSS; }
    }

    /// <summary>
    /// Gets the firmware version saved on config.
    /// Thread safe get.
    /// </summary>
    public string Firmware
    {
        get { lock (_sync) return _config.firmware; }
    }

    /// <summary>
    /// Updates the maximum number of supported WSS devices in the loaded configuration
    /// and immediately persists the change to disk. Thread safe.
    /// </summary>
    /// <param name="v">
    /// New maximum WSS count. Must be greater than zero.
    /// </param>
    public void SetMaxWss(int v)
    {
        if (v <= 0) throw new ArgumentOutOfRangeException(nameof(v));
        lock (_sync) { _config.maxWSS = v; JsonReader.SaveObject(_configPath, _config); }
    }

    /// <summary>
    /// Updates the firmware version recorded in the configuration and immediately saves it to disk.
    /// Thread safe.
    /// </summary>
    /// <param name="v">
    /// Firmware version string to record (for example, "H03").
    /// </param>
    /// <param name="verHandler">
    /// Version handler used to validate whether the supplied firmware string is supported.
    /// </param>
    public void SetFirmware(string v, WSSVersionHandler verHandler)
    {
        if (verHandler == null) throw new ArgumentNullException(nameof(verHandler));
        if (!verHandler.isVersionSupported(v))
            throw new ArgumentException($"Firmware version '{v}' is not supported.", nameof(v));
        lock (_sync) { _config.firmware = v; JsonReader.SaveObject(_configPath, _config); }
    }
}
}
