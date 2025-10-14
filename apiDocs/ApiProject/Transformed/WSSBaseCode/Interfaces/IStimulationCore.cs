namespace WSSInterfacing {
using System;

/// <summary>
/// Unity-agnostic stimulation core lifecycle and control surface.
/// Implementations manage connection, setup, and a background streaming loop.
/// Public mutators should enqueue edits and return immediately.
/// </summary>
public interface IStimulationCore : IDisposable
{
    // lifecycle

    /// <summary>
    /// Initializes the core: loads JSON config, prepares buffers, and begins connecting
    /// to the transport. Non-blocking; call <see cref="Tick"/> to advance state.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Advances the internal state machine (e.g., Connecting â†’ SettingUp â†’ Ready â†’ Streaming).
    /// Call regularly from the main loop.
    /// </summary>
    void Tick();

    /// <summary>
    /// Stops streaming, attempts to zero outputs, disconnects transport, and releases resources.
    /// Safe to call multiple times.
    /// </summary>
    void Shutdown();

    // status

    /// <summary>
    /// True when device transport is started or streaming.
    /// </summary>
    bool Started();

    /// <summary>
    /// True when the core is ready to accept start stimulation.
    /// </summary>
    bool Ready();

    // streaming / control

    /// <summary>
    /// Caches per-channel amplitude, pulse width, and IPI. No I/O here;
    /// the streaming loop pushes cached values to the device.
    /// </summary>
    /// <param name="channel">1-based logical channel.</param>
    /// <param name="PW">Pulse width (Âµs).</param>
    /// <param name="amp">Amplitude (mA domain; mapped to device scale during streaming).</param>
    /// <param name="IPI">Inter-pulse interval (ms).</param>
    void StimulateAnalog(int channel, int PW, float amp, int IPI);

    /// <summary>
    /// Sends a zero-out command. Does not alter cached values.
    /// </summary>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void ZeroOutStim(WssTarget wssTarget);

    /// <summary>
    /// Starts stimulation on the target and launches streaming when ready.
    /// </summary>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void StartStim(WssTarget wssTarget);

    /// <summary>
    /// Stops stimulation on the target and, if streaming, stops the background loop.
    /// </summary>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void StopStim(WssTarget wssTarget);

    /// <summary>
    /// Reloads the stimulation JSON configuration from disk into memory.
    /// </summary>
    void LoadConfigFile();

    /// <summary>
    /// Gets the JSON-backed stimulation configuration controller currently used by this core.
    /// </summary>
    /// <returns>
    /// The active <see cref="CoreConfigController"/> instance that provides read/write access
    /// to stimulation parameters and constants loaded from the configuration file.
    /// </returns>
    /// <remarks>
    /// Returned reference is live, not a copy and thread safe.
    /// </remarks>
    CoreConfigController GetCoreConfigController();

    /// <summary>
    /// Returns true if <paramref name="ch"/> is within the valid channel range.
    /// </summary>
    /// <param name="ch">1-based channel to check.</param>
    bool IsChannelInRange(int ch);
}

}
