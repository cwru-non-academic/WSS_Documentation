namespace WSSInterfacing {
using System.Collections.Generic;

/// <summary>
/// Core + parameters surface layered over <see cref="IStimulationCore"/>.
/// Provides normalized-drive stimulation, per-channel parameter access,
/// and JSON persistence for stimulation parameters. BASIC is optional and
/// can be exposed via <see cref="TryGetBasic(out IBasicStimulation)"/>.
/// </summary>
public interface IStimParamsCore : IStimulationCore
{
    // ---- Normalized stimulation ----

    /// <summary>
    /// Computes a pulse width from a normalized value in [0,1] using per-channel
    /// parameters (minPW, maxPW, amp, IPI) and forwards the result to the core.
    /// Implementations clamp the input to [0,1] and cache the last PW sent.
    /// </summary>
    /// <param name="channel">1-based logical channel.</param>
    /// <param name="normalizedValue">Normalized drive in [0,1].</param>
    void StimulateNormalized(int channel, float normalizedValue);

    /// <summary>
    /// Returns the most recently computed stimulation intensity for the channel.
    /// For PW-driven systems, this is the last pulse width (Âµs) sent by
    /// <see cref="StimulateNormalized(int,float)"/>.
    /// </summary>
    /// <param name="channel">1-based logical channel.</param>
    float GetStimIntensity(int channel);

    // ---- Params persistence ----

    /// <summary>
    /// Saves the current stimulation-parameters JSON to disk.
    /// </summary>
    void SaveParamsJson();

    /// <summary>
    /// Loads the stimulation-parameters JSON from the default location.
    /// </summary>
    void LoadParamsJson();

    /// <summary>
    /// Loads the stimulation-parameters JSON from a specific file path or directory.
    /// </summary>
    /// <param name="path">File path or directory.</param>
    void LoadParamsJson(string path);

    // ---- Dotted-key API ("stim.ch.{N}.{leaf}") ----

    /// <summary>
    /// Adds or updates a parameter by dotted key. Examples:
    /// <c>stim.ch.1.amp</c>, <c>stim.ch.2.minPW</c>, <c>stim.ch.3.maxPW</c>, <c>stim.ch.4.IPI</c>.
    /// </summary>
    /// <param name="key">Dotted parameter key.</param>
    /// <param name="value">Value to set.</param>
    void AddOrUpdateStimParam(string key, float value);

    /// <summary>
    /// Reads a parameter by dotted key. Throws if the key is missing.
    /// </summary>
    /// <param name="key">Dotted parameter key.</param>
    float GetStimParam(string key);

    /// <summary>
    /// Tries to read a parameter by dotted key.
    /// </summary>
    /// <param name="key">Dotted parameter key.</param>
    /// <param name="value">Out value if the key exists.</param>
    /// <returns><c>true</c> if found, otherwise <c>false</c>.</returns>
    bool TryGetStimParam(string key, out float value);

    /// <summary>
    /// Returns a copy of all current stimulation parameters as a dotted-key map.
    /// </summary>
    Dictionary<string, float> GetAllStimParams();

    /// <summary>
    /// Returns the parameters/configuration controller for stimulation params.
    /// </summary>
    StimParamsConfigController GetStimParamsConfigController();

    // ---- Channel helpers ----

    /// <summary>Sets per-channel amplitude in mA.</summary>
    /// <param name="ch">1-based logical channel.</param>
    /// <param name="mA">Amplitude in milliamps.</param>
    void SetChannelAmp(int ch, float mA);

    /// <summary>Sets per-channel minimum pulse width in Âµs.</summary>
    void SetChannelPWMin(int ch, int us);

    /// <summary>Sets per-channel maximum pulse width in Âµs.</summary>
    void SetChannelPWMax(int ch, int us);

    /// <summary>Sets per-channel IPI in ms.</summary>
    void SetChannelIPI(int ch, int ms);

    /// <summary>Gets per-channel amplitude in mA.</summary>
    float GetChannelAmp(int ch);

    /// <summary>Gets per-channel minimum pulse width in Âµs.</summary>
    int GetChannelPWMin(int ch);

    /// <summary>Gets per-channel maximum pulse width in Âµs.</summary>
    int GetChannelPWMax(int ch);

    /// <summary>Gets per-channel IPI in ms.</summary>
    int GetChannelIPI(int ch);

    // ---- Optional capability ----

    /// <summary>
    /// Exposes the optional BASIC capability if available from the wrapped core.
    /// Returns <c>true</c> and sets <paramref name="basic"/> if supported.
    /// </summary>
    /// <param name="basic">Out parameter for the BASIC interface.</param>
    bool TryGetBasic(out IBasicStimulation basic);
}
}
