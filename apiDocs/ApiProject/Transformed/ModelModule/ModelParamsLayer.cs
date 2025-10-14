namespace WSSInterfacing {
// ModelParamsLayer.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Model-driven layer (P/PD) that sits on top of an existing params layer.
/// Computes PW from normalized magnitudes and stimulates via the wrapped core.
/// </summary>
public sealed class ModelParamsLayer : IModelParamsCore
{
    private readonly IStimParamsCore _inner;          // params + core underneath
    private readonly ModelConfigController _modelCfg;  // model settings (mode + constants)

    // model math state
    private bool _validMode;
    private float[] _prevMagnitude;
    private float[] _currentMag;
    private float[] _d_dt;
    private float[] _tPrev;
    private Stopwatch _timer;

    public ModelParamsLayer(IStimParamsCore inner, string modelPathOrDir)
    {
        _inner    = inner ?? throw new ArgumentNullException(nameof(inner));
        _modelCfg = new ModelConfigController(modelPathOrDir); 
    }

    // ===== IModelParamsCore (extras) =====

    /// <inheritdoc/>
    public bool IsModeValid()
    {
        VerifyStimMode();
        return _validMode;
    }

    /// <inheritdoc/>
    public void StimWithMode(int ch, float magnitudeIn)
    {
        _inner.StimulateNormalized(ch, CalculateMagnitude(ch, magnitudeIn));
    }

    /// <inheritdoc/>
    public ModelConfigController GetModelConfigController() => _modelCfg;

    // ===== IStimParamsCore (params surface â†’ delegate) =====
    /// <inheritdoc/>
    public void StimulateNormalized(int channel, float normalizedValue) => _inner.StimulateNormalized(channel, normalizedValue);
    /// <inheritdoc/>
    public float GetStimIntensity(int channel) => _inner.GetStimIntensity(channel);
    /// <inheritdoc/>
    public void SaveParamsJson() => _inner.SaveParamsJson();
    /// <inheritdoc/>
    public void LoadParamsJson() => _inner.LoadParamsJson();
    /// <inheritdoc/>
    public void LoadParamsJson(string path) => _inner.LoadParamsJson(path);
    /// <inheritdoc/>
    public StimParamsConfigController GetStimParamsConfigController() => _inner.GetStimParamsConfigController();

    /// <inheritdoc/>
    public void AddOrUpdateStimParam(string key, float value) => _inner.AddOrUpdateStimParam(key, value);
    /// <inheritdoc/>
    public float GetStimParam(string key) => _inner.GetStimParam(key);
    /// <inheritdoc/>
    public bool TryGetStimParam(string key, out float value) => _inner.TryGetStimParam(key, out value);
    /// <inheritdoc/>
    public Dictionary<string, float> GetAllStimParams() => _inner.GetAllStimParams();

    /// <inheritdoc/>
    public void SetChannelAmp(int ch, float mA) => _inner.SetChannelAmp(ch, mA);
    /// <inheritdoc/>
    public void SetChannelPWMin(int ch, int us) => _inner.SetChannelPWMin(ch, us);
    /// <inheritdoc/>
    public void SetChannelPWMax(int ch, int us) => _inner.SetChannelPWMax(ch, us);
    /// <inheritdoc/>
    public void SetChannelIPI(int ch, int ms) => _inner.SetChannelIPI(ch, ms);

    /// <inheritdoc/>
    public float GetChannelAmp(int ch) => _inner.GetChannelAmp(ch);
    /// <inheritdoc/>
    public int GetChannelPWMin(int ch) => _inner.GetChannelPWMin(ch);
    /// <inheritdoc/>
    public int GetChannelPWMax(int ch) => _inner.GetChannelPWMax(ch);
    /// <inheritdoc/>
    public int GetChannelIPI(int ch) => _inner.GetChannelIPI(ch);
    /// <inheritdoc/>
    public bool IsChannelInRange(int ch) => _inner.IsChannelInRange(ch);

    /// <inheritdoc/>
    public bool TryGetBasic(out IBasicStimulation basic) => _inner.TryGetBasic(out basic);

    // ===== IStimulationCore (lifecycle/core â†’ delegate) =====
    /// <inheritdoc/>
    public void Initialize()
    {
        _inner.Initialize();
        EnsureModelArrays(); // lazy arrays stand up after core init
    }

    /// <inheritdoc/>
    public void Tick()
    {
        _inner.Tick(); // advance underlying core first
    }

    /// <inheritdoc/>
    public void Shutdown() => _inner.Shutdown();
    /// <inheritdoc/>
    public bool Started() => _inner.Started();
    /// <inheritdoc/>
    public bool Ready() => _inner.Ready();
    /// <inheritdoc/>
    public void StimulateAnalog(int ch, int pw, float amp, int ipi) => _inner.StimulateAnalog(ch, pw, amp, ipi);
    /// <inheritdoc/>
    public void ZeroOutStim(WssTarget t) => _inner.ZeroOutStim(t);
    /// <inheritdoc/>
    public void StartStim(WssTarget t) => _inner.StartStim(t);
    /// <inheritdoc/>
    public void StopStim(WssTarget t) => _inner.StopStim(t);
    /// <inheritdoc/>
    public void LoadConfigFile() => _inner.LoadConfigFile();
    /// <inheritdoc/>
    public CoreConfigController GetCoreConfigController() => _inner.GetCoreConfigController();
    /// <inheritdoc/>
    public void Dispose() => _inner.Dispose();

    // ===== helpers =====

    /// <summary>
    /// Ensures model-layer working arrays exist and match the current channel count.
    /// Starts the internal stopwatch on first use and initializes per-channel state.
    /// </summary>
    /// <remarks>
    /// Channel count is derived from <see cref="CoreConfigController.MaxWss"/> on the inner core.
    /// Arrays hold last magnitude, derivative estimate, and previous timestamps per channel.
    /// </remarks>
    private void EnsureModelArrays()
    {
        if (_timer == null) { _timer = new Stopwatch(); _timer.Start(); }
        int n = _inner.GetCoreConfigController().MaxWss;
        if (_currentMag != null && _currentMag.Length == n) return;

        _prevMagnitude = new float[n];
        _currentMag = new float[n];
        _d_dt = new float[n];
        _tPrev = new float[n];

        float now = _timer.ElapsedMilliseconds / 1000.0f;
        for (int i = 0; i < n; i++) { _prevMagnitude[i] = 0f; _tPrev[i] = now; }
    }

    /// <summary>
    /// Verifies that the active controller mode in the model config is supported.
    /// </summary>
    /// <remarks>
    /// Sets the private flag <c>_validMode</c> to true for modes <c>"P"</c> or <c>"PD"</c>, false otherwise.
    /// Uses <c>_modelCfg.GetCalibMode()</c>.
    /// </remarks>
    private void VerifyStimMode()
    {
        string mode = _modelCfg.GetCalibMode(); // e.g., "P" or "PD"
        _validMode = mode == "P" || mode == "PD";
    }

    /// <summary>
    /// Computes a normalized output magnitude for a 1-based channel using the current controller mode.
    /// </summary>
    /// <param name="channel1">1-based channel index.</param>
    /// <param name="magnitude01">Input magnitude, clamped to [0,1].</param>
    /// <returns>
    /// Normalized output in [0,1] after applying P or PD mapping with model constants and a finite-difference derivative.
    /// Returns 0 if the channel index is out of range.
    /// </returns>
    /// <remarks>
    /// Updates per-channel caches: current magnitude, previous magnitude, derivative estimate, and last timestamp (seconds).
    /// PD mode uses keys: <c>PDModeDerivative</c>, <c>PDModeProportional</c>, <c>PDModeOffset</c>.  
    /// P mode uses keys: <c>PModeProportional</c>, <c>PModeOffset</c>.  
    /// Missing constants fall back to defaults used in code.
    /// </remarks>
    private float CalculateMagnitude(int channel1, float magnitude01)
    {
        int idx = channel1 - 1;
        EnsureModelArrays();
        if ((uint)idx >= (uint)_currentMag.Length) return 0f;

        // time + derivative bookkeeping
        _currentMag[idx] = Clamp01(magnitude01);
        float tNow = _timer.ElapsedMilliseconds / 1000.0f;
        float dt = MathF.Max(1e-5f, tNow - _tPrev[idx]);
        _d_dt[idx] = (_currentMag[idx] - _prevMagnitude[idx]) / dt;
        _tPrev[idx] = tNow;
        _prevMagnitude[idx] = _currentMag[idx];

        // controller
        string mode = _modelCfg.GetCalibMode();
        float GetConst(string key, float fb) => _modelCfg.TryGetModelParam(key, out float v) ? v : fb;

        float y =
            mode == "PD"
            ? _d_dt[idx] * GetConst("PDModeDerivative", 0.2f)
            + _currentMag[idx] * GetConst("PDModeProportional", 0.5f)
            + GetConst("PDModeOffset", 0.0f)
            : _currentMag[idx] * GetConst("PModeProportional", 1.0f)
            + GetConst("PModeOffset", 0.0f);

        return Clamp01(y);
    }

    /// <summary>
    /// Clamps a float to the inclusive range [0,1].
    /// </summary>
    /// <param name="x">Value to clamp.</param>
    /// <returns><c>0</c> if <paramref name="x"/> &lt; 0, <c>1</c> if &gt; 1, otherwise <paramref name="x"/>.</returns>
    private static float Clamp01(float x) => x < 0f ? 0f : (x > 1f ? 1f : x);
}

}
