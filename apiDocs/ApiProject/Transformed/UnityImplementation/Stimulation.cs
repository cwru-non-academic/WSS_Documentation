namespace WSSInterfacing {
using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// High-level Unity wrapper for the full stimulation model layer.
/// Extends <c>StimParamsLayer</c> with proportional and derivative control modes,
/// calibration constants, and model-based drive computation.
/// This is the top-level interface that user code should normally interact with.
/// </summary>
public class Stimulation : MonoBehaviour
{
    #region ==== Serialized Fields ====

    /// <summary>
    /// Forces connection to a specific COM port instead of auto-detecting.
    /// </summary>
    [SerializeField] public bool forcePort = false;

    /// <summary>
    /// Enables simulated test mode without real hardware communication.
    /// </summary>
    [SerializeField] private bool testMode = true;

    /// <summary>
    /// Maximum number of setup retries before failing initialization.
    /// </summary>
    [SerializeField] private int maxSetupTries = 5;

    /// <summary>
    /// Target COM port to use when <see cref="forcePort"/> is enabled.
    /// </summary>
    [SerializeField] public string comPort = "COM7";

    #endregion

    private IModelParamsCore WSS;
    private IBasicStimulation basicWSS;
    /// <summary>True after <see cref="StartStimulation"/> succeeds.</summary>
    public bool started = false;
    /// <summary>True if the underlying core exposes basic-stimulation APIs.</summary>
    private bool basicSupported = false;

    #region ==== Unity Lifecycle ====

    /// <summary>
    /// Creates the full stimulation stack: core → params layer → model layer.
    /// Detects hardware if <see cref="forcePort"/> is false.
    /// </summary>
    public void Awake()
    {
        IStimulationCore WSScore =
            forcePort
            ? new WssStimulationCore(comPort, Application.streamingAssetsPath, testMode, maxSetupTries)
            : new WssStimulationCore(Application.streamingAssetsPath, testMode, maxSetupTries);

        IStimParamsCore paramsWSS = new StimParamsLayer(WSScore, Application.streamingAssetsPath);
        WSS = new ModelParamsLayer(paramsWSS, Application.streamingAssetsPath);
        WSS.TryGetBasic(out basicWSS);
        basicSupported = (basicWSS != null);
    }

    /// <summary>Initializes the WSS device connection when the component becomes active.</summary>
    void OnEnable() => WSS.Initialize();

    /// <summary>
    /// Advances communication tick and listens for debug input.
    /// Press <c>A</c> to reload configuration file manually.
    /// </summary>
    void Update()
    {
        WSS.Tick();
        if (Input.GetKeyDown(KeyCode.A))
            WSS.LoadConfigFile();
    }

    /// <summary>Ensures clean shutdown when the component is disabled.</summary>
    void OnDisable() => WSS.Shutdown();

    #endregion

    #region ==== Connection Management ====

    /// <summary>Explicitly closes the radio connection.</summary>
    public void releaseRadio() => WSS.Shutdown();

    /// <summary>Resets the radio by shutting down and reinitializing the device.</summary>
    public void resetRadio()
    {
        WSS.Shutdown();
        WSS.Initialize();
    }

    #endregion

    #region ==== Stimulation methods: basic and core ====

    /// <summary>
    /// Direct analog stimulation using raw parameters.
    /// Recommended only for discrete-sensor triggers.
    /// </summary>
    /// <param name="finger">Finger label or alias (e.g., "index" or "ch2").</param>
    /// <param name="PW">Pulse width in microseconds.</param>
    /// <param name="amp">Amplitude in milliamps (default = 3).</param>
    /// <param name="IPI">Inter-pulse interval in milliseconds (default = 10).</param>
    public void StimulateAnalog(string finger, int PW, int amp = 3, int IPI = 10)
    {
        int channel = FingerToChannel(finger);
        WSS.StimulateAnalog(channel, PW, amp, IPI);
    }

    /// <inheritdoc cref="IStimulationCore.StartStim(WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void StartStimulation() => WSS.StartStim(WssTarget.Broadcast);

    /// <inheritdoc cref="IStimulationCore.StopStim(WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void StopStimulation() => WSS.StopStim(WssTarget.Broadcast);

    /// <inheritdoc cref="IBasicStimulation.Save(WssTarget)"/>
    public void Save(int targetWSS)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.Save(IntToWssTarget(targetWSS));
    }

    /// <inheritdoc cref="IBasicStimulation.Save(WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void Save()
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.Save(WssTarget.Broadcast);
    }

    /// <inheritdoc cref="IBasicStimulation.Load(WssTarget)"/>
    public void load(int targetWSS)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.Load(IntToWssTarget(targetWSS));
    }

    /// <inheritdoc cref="IBasicStimulation.Load(WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void load()
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.Load(WssTarget.Broadcast);
    }

    /// <inheritdoc cref="IBasicStimulation.Request_Configs(int,int,WssTarget)"/>
    public void request_Configs(int targetWSS, int command, int id)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.Request_Configs(command, id, IntToWssTarget(targetWSS));
    }

    /// <inheritdoc cref="IBasicStimulation.UpdateWaveform(int[],int,WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void updateWaveform(int[] waveform, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateWaveform(waveform, eventID, WssTarget.Broadcast);
    }

    /// <inheritdoc cref="IBasicStimulation.UpdateWaveform(int[],int,WssTarget)"/>
    public void updateWaveform(int targetWSS, int[] waveform, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateWaveform(waveform, eventID, IntToWssTarget(targetWSS));
    }

    /// <inheritdoc cref="IBasicStimulation.UpdateEventShape(int,int,int,WssTarget)"/>
    public void updateWaveform(int cathodicWaveform, int anodicWaveform, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateEventShape(cathodicWaveform, anodicWaveform, eventID, WssTarget.Broadcast);
    }

    /// <summary>
    /// Selects predefined or custom waveform shapes for a specific WSS target.
    /// </summary>
    public void updateWaveform(int targetWSS, int cathodicWaveform, int anodicWaveform, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateEventShape(cathodicWaveform, anodicWaveform, eventID, IntToWssTarget(targetWSS));
    }

    /// <inheritdoc cref="IBasicStimulation.UpdateWaveform(WaveformBuilder,int,WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void updateWaveform(WaveformBuilder waveform, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateWaveform(waveform, eventID, WssTarget.Broadcast);
    }

    /// <summary>Loads a waveform definition from JSON for a target unit.</summary>
    public void updateWaveform(int targetWSS, WaveformBuilder waveform, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateWaveform(waveform, eventID, IntToWssTarget(targetWSS));
    }

    /// <summary>Loads a waveform file into memory.</summary>
    public void loadWaveform(string fileName, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.LoadWaveform(fileName, eventID);
    }

    /// <summary>Defines a custom waveform for an event slot on all units.</summary>
    public void WaveformSetup(WaveformBuilder wave, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.WaveformSetup(wave, eventID, WssTarget.Broadcast);
    }

    /// <summary>Defines a custom waveform for an event slot on a specific unit.</summary>
    public void WaveformSetup(int targetWSS, WaveformBuilder wave, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.WaveformSetup(wave, eventID, IntToWssTarget(targetWSS));
    }

    /// <inheritdoc cref="IBasicStimulation.UpdateIPD(int,int,WssTarget)"/>
    /// <remarks>Broadcasts to all connected WSS units.</remarks>
    public void UpdateIPD(int ipd, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateIPD(ipd, eventID, WssTarget.Broadcast);
    }

    /// <inheritdoc cref="IBasicStimulation.UpdateIPD(int,int,WssTarget)"/>
    public void UpdateIPD(int targetWSS, int ipd, int eventID)
    {
        if (!basicSupported) { Log.Error("Basic stimulation not supported."); return; }
        basicWSS.UpdateIPD(ipd, eventID, IntToWssTarget(targetWSS));
    }

    #endregion

    #region ==== Stimulation methods: params and model layers ====

    /// <summary>
    /// Normalized drive [0..1]. Computes PW via per-finger calibration and model constants, then stimulates.
    /// </summary>
    public void StimulateNormalized(string finger, float magnitude)
    {
        int ch = FingerToChannel(finger);
        WSS.StimulateNormalized(ch, magnitude);
    }

    /// <summary>Returns last computed pulse width for the specified finger.</summary>
    public int GetStimIntensity(string finger)
    {
        int ch = FingerToChannel(finger);
        return (int)WSS.GetStimIntensity(ch);
    }

    /// <summary>Saves model-layer parameters JSON to disk.</summary>
    public void SaveParamsJson() => WSS.SaveParamsJson();

    /// <summary>Loads model-layer parameters JSON from default location.</summary>
    public void LoadParamsJson() => WSS.LoadParamsJson();

    /// <summary>Loads model-layer parameters JSON from a specified path.</summary>
    public void LoadParamsJson(string pathOrDir) => WSS.LoadParamsJson(pathOrDir);

    /// <summary>Sets a parameter by dotted key (e.g., "stim.ch.1.amp").</summary>
    public void AddOrUpdateStimParam(string key, float value) => WSS.AddOrUpdateStimParam(key, value);

    /// <summary>Gets a parameter by dotted key.</summary>
    public float GetStimParam(string key) => WSS.GetStimParam(key);

    /// <summary>Attempts to read a parameter by key, returns <c>true</c> on success.</summary>
    public bool TryGetStimParam(string key, out float v) => WSS.TryGetStimParam(key, out v);

    /// <summary>Returns a dictionary copy of all current stimulation parameters.</summary>
    public Dictionary<string, float> GetAllStimParams() => WSS.GetAllStimParams();

    /// <summary>Sets per-finger amplitude in milliamps.</summary>
    public void SetChannelAmp(string finger, float mA)
    {
        int ch = FingerToChannel(finger);
        WSS.SetChannelAmp(ch, mA);
    }

    /// <summary>Sets per-finger minimum PW in microseconds.</summary>
    public void SetChannelPWMin(string finger, int us)
    {
        int ch = FingerToChannel(finger);
        WSS.SetChannelPWMin(ch, us);
    }

    /// <summary>Sets per-finger maximum PW in microseconds.</summary>
    public void SetChannelPWMax(string finger, int us)
    {
        int ch = FingerToChannel(finger);
        WSS.SetChannelPWMax(ch, us);
    }

    /// <summary>Sets per-finger IPI in milliseconds.</summary>
    public void SetChannelIPI(string finger, int ms)
    {
        int ch = FingerToChannel(finger);
        WSS.SetChannelIPI(ch, ms);
    }

    /// <summary>Gets per-finger amplitude in milliamps.</summary>
    public float GetChannelAmp(string finger)
    {
        int ch = FingerToChannel(finger);
        return WSS.GetChannelAmp(ch);
    }

    /// <summary>Gets per-finger minimum PW in microseconds.</summary>
    public int GetChannelPWMin(string finger)
    {
        int ch = FingerToChannel(finger);
        return WSS.GetChannelPWMin(ch);
    }

    /// <summary>Gets per-finger maximum PW in microseconds.</summary>
    public int GetChannelPWMax(string finger)
    {
        int ch = FingerToChannel(finger);
        return WSS.GetChannelPWMax(ch);
    }

    /// <summary>Gets per-finger IPI in milliseconds.</summary>
    public int GetChannelIPI(string finger)
    {
        int ch = FingerToChannel(finger);
        return WSS.GetChannelIPI(ch);
    }

    /// <summary>Checks whether the specified finger maps to a valid channel index.</summary>
    public bool IsFingerValid(string finger)
    {
        int ch = FingerToChannel(finger);
        return WSS.IsChannelInRange(ch);
    }

    /// <summary>
    /// Applies model-driven stimulation using proportional or PD mode.
    /// </summary>
    /// <param name="finger">Finger label or alias.</param>
    /// <param name="magnitude">Normalized or physical magnitude input to the model.</param>
    public void StimWithMode(string finger, float magnitude)
    {
        int ch = FingerToChannel(finger);
        WSS.StimWithMode(ch, magnitude);
    }

    /// <summary>
    /// Updates calibration parameters for a specific finger.
    /// </summary>
    public void UpdateChannelParams(string finger, int max, int min, int amp)
    {
        int ch = FingerToChannel(finger);
        if (!WSS.IsChannelInRange(ch))
            throw new ArgumentOutOfRangeException(nameof(finger), $"Channel {ch} is not valid for current config.");

        string baseKey = $"stim.ch.{ch}";
        WSS.AddOrUpdateStimParam($"{baseKey}.maxPW", max);
        WSS.AddOrUpdateStimParam($"{baseKey}.minPW", min);
        WSS.AddOrUpdateStimParam($"{baseKey}.amp", amp);
    }

    #endregion

    #region ==== Config and state ====

    /// <summary>Returns <c>true</c> if the model mode currently loaded is valid.</summary>
    public bool isModeValid() => WSS.IsModeValid();

    /// <summary>Returns <c>true</c> if the device is initialized and ready.</summary>
    public bool Ready() => WSS.Ready();

    /// <summary>Returns <c>true</c> if stimulation is currently active.</summary>
    public bool Started() => WSS.Started();

    /// <summary>Provides access to the model configuration controller.</summary>
    public ModelConfigController GetModelConfigCTRL() => WSS.GetModelConfigController();

    /// <summary>Provides access to the core configuration controller.</summary>
    public CoreConfigController GetCoreConfigCTRL() => WSS.GetCoreConfigController();

    #endregion

    #region ==== Utility ====

    private WssTarget IntToWssTarget(int i) =>
        i switch
        {
            0 => WssTarget.Broadcast,
            1 => WssTarget.Wss1,
            2 => WssTarget.Wss2,
            3 => WssTarget.Wss3,
            _ => WssTarget.Wss1
        };

    /// <summary>
    /// Maps a finger label or alias to its numeric channel.
    /// Supports "thumb", "index", "middle", "ring", "pinky" or "little",
    /// and generic "chN" notation. Returns 0 if invalid.
    /// </summary>
    private int FingerToChannel(string fingerOrAlias)
    {
        if (string.IsNullOrWhiteSpace(fingerOrAlias)) return 0;

        if (fingerOrAlias.StartsWith("ch", StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(fingerOrAlias.AsSpan(2), out var n))
            return n;

        return fingerOrAlias.ToLowerInvariant() switch
        {
            "thumb" => 1,
            "index" => 2,
            "middle" => 3,
            "ring" => 4,
            "pinky" or "little" => 5,
            _ => 0
        };
    }

    #endregion
}

}
