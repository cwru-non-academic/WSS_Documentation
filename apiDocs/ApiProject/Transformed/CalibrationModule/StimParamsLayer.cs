namespace WSSInterfacing {
using System;
using System.Collections.Generic;

/// <summary>
/// Layer that wraps an <see cref="IStimulationCore"/> and a <see cref="StimParamsConfigController"/>.
/// Computes pulse widths from normalized inputs and forwards stimulation to the core.
/// Also exposes dotted-key parameter access and an optional BASIC capability.
/// </summary>
public sealed class StimParamsLayer : IStimParamsCore
{
    private readonly IStimulationCore _core;          // required core
    private readonly StimParamsConfigController _ctrl;      // params context
    private readonly IBasicStimulation _basic;       // optional BASIC capability
    private int _totalChannels;
    private int[] _lastPw;

    /// <summary>
    /// Constructs the layer over an existing core and a params context path.
    /// </summary>
    /// <param name="core">Initialized stimulation core to wrap.</param>
    /// <param name="pathOrDir">Params file path or directory.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="core"/> is null.</exception>
    public StimParamsLayer(IStimulationCore core, string pathOrDir)
    {
        _core = core ?? throw new ArgumentNullException(nameof(core));

        int maxWss = _core.GetCoreConfigController().MaxWss;
        _ctrl = new StimParamsConfigController(pathOrDir, maxWss);
        _basic = _core as IBasicStimulation;

        _totalChannels = _ctrl.PerWss * Math.Max(1, maxWss);
        _lastPw = new int[_totalChannels];
        InitArrays();
    }

    // ---- IStimParamsCore ----

    /// <inheritdoc/>
    public void StimulateNormalized(int channel, float value01)
    {
        if (channel < 1 || channel > _totalChannels)
            throw new ArgumentOutOfRangeException(nameof(channel), $"Channel must be 1..{_totalChannels}.");

        value01 = Math.Clamp(value01, 0f, 1f);

        string baseKey = $"stim.ch.{channel}";
        float amp  = _ctrl.TryGetStimParam($"{baseKey}.amp",   out var a) ? a : 1f;
        float pwLo = _ctrl.TryGetStimParam($"{baseKey}.minPW", out var mn) ? mn : 50f;
        float pwHi = _ctrl.TryGetStimParam($"{baseKey}.maxPW", out var mx) ? mx : 500f;
        float ipi  = _ctrl.TryGetStimParam($"{baseKey}.IPI",   out var ii) ? ii : 20f;

        if (pwHi < pwLo) { var tmp = pwLo; pwLo = pwHi; pwHi = tmp; }

        _lastPw[channel - 1] = (int)MathF.Round(pwLo + (pwHi - pwLo) * value01);
        _core.StimulateAnalog(channel, _lastPw[channel - 1], amp, (int)ipi);
    }

    /// <inheritdoc/>
    public float GetStimIntensity(int channel)
    {
        if (channel < 1 || channel > _totalChannels)
            throw new ArgumentOutOfRangeException(nameof(channel), $"Channel must be 1..{_totalChannels}.");
        return _lastPw[channel - 1];
    }

    /// <inheritdoc/>
    public StimParamsConfigController GetStimParamsConfigController()
    {
        return _ctrl;
    }

    /// <inheritdoc cref="StimParamsConfigController.SaveParamsJson"/>
    public void SaveParamsJson() => _ctrl.SaveParamsJson();

    /// <inheritdoc cref="StimParamsConfigController.LoadParamsJson()"/>
    public void LoadParamsJson() => _ctrl.LoadParamsJson();

    /// <inheritdoc cref="StimParamsConfigController.LoadParamsJson(string)"/>
    public void LoadParamsJson(string path) => _ctrl.LoadParamsJson(path);

    /// <inheritdoc cref="StimParamsConfigController.AddOrUpdateStimParam(string,float)"/>
    public void AddOrUpdateStimParam(string key, float value) => _ctrl.AddOrUpdateStimParam(key, value);

    /// <inheritdoc cref="StimParamsConfigController.GetStimParam(string)"/>
    public float GetStimParam(string key) => _ctrl.GetStimParam(key);

    /// <inheritdoc cref="WSSInterfacing.StimParamsConfigController.TryGetStimParam(string, out float)"/>
    public bool TryGetStimParam(string key, out float value) => _ctrl.TryGetStimParam(key, out value);

    /// <inheritdoc cref="StimParamsConfigController.GetAllStimParams"/>
    public Dictionary<string, float> GetAllStimParams() => _ctrl.GetAllStimParams();

    /// <inheritdoc cref="StimParamsConfigController.SetChannelAmp(int,float)"/>
    public void SetChannelAmp(int ch, float mA) => _ctrl.SetChannelAmp(ch, mA);

    /// <inheritdoc cref="StimParamsConfigController.SetChannelPWMin(int,int)"/>
    public void SetChannelPWMin(int ch, int us) => _ctrl.SetChannelPWMin(ch, us);

    /// <inheritdoc cref="StimParamsConfigController.SetChannelPWMax(int,int)"/>
    public void SetChannelPWMax(int ch, int us) => _ctrl.SetChannelPWMax(ch, us);

    /// <inheritdoc cref="StimParamsConfigController.SetChannelIPI(int,int)"/>
    public void SetChannelIPI(int ch, int ms) => _ctrl.SetChannelIPI(ch, ms);

    /// <inheritdoc cref="StimParamsConfigController.GetChannelAmp(int)"/>
    public float GetChannelAmp(int ch) => _ctrl.GetChannelAmp(ch);

    /// <inheritdoc cref="StimParamsConfigController.GetChannelPWMin(int)"/>
    public int GetChannelPWMin(int ch) => _ctrl.GetChannelPWMin(ch);

    /// <inheritdoc cref="StimParamsConfigController.GetChannelPWMax(int)"/>
    public int GetChannelPWMax(int ch) => _ctrl.GetChannelPWMax(ch);

    /// <inheritdoc cref="StimParamsConfigController.GetChannelIPI(int)"/>
    public int GetChannelIPI(int ch) => _ctrl.GetChannelIPI(ch);

    /// <inheritdoc cref="StimParamsConfigController.IsChannelInRange(int)"/>
    public bool IsChannelInRange(int ch) => _ctrl.IsChannelInRange(ch);

    /// <inheritdoc/>
    public bool TryGetBasic(out IBasicStimulation basic)
    {
        if (_basic != null)
        {
            basic = _basic;
            return true;
        }
        basic = null!;
        return false;
    }

    // ---- IStimulationCore passthrough ----

    /// <inheritdoc cref="IStimulationCore.Initialize"/>
    public void Initialize()
    {
        _core.Initialize();
        _ctrl.LoadParamsJson(); // load params after core init
        int maxWss = _core.GetCoreConfigController().MaxWss;
        _totalChannels = _ctrl.PerWss * Math.Max(1, maxWss);
        _lastPw = new int[_totalChannels];
        InitArrays();
    }

    /// <inheritdoc cref="IStimulationCore.Tick"/>
    public void Tick() => _core.Tick();

    /// <inheritdoc cref="IStimulationCore.Shutdown"/>
    public void Shutdown() => _core.Shutdown();

    /// <inheritdoc cref="IStimulationCore.Started"/>
    public bool Started() => _core.Started();

    /// <inheritdoc cref="IStimulationCore.Ready"/>
    public bool Ready() => _core.Ready();

    /// <inheritdoc cref="IStimulationCore.StimulateAnalog(int,int,float,int)"/>
    public void StimulateAnalog(int ch, int pw, float amp, int ipi) => _core.StimulateAnalog(ch, pw, amp, ipi);

    /// <inheritdoc cref="IStimulationCore.ZeroOutStim(WssTarget)"/>
    public void ZeroOutStim(WssTarget t) => _core.ZeroOutStim(t);

    /// <inheritdoc cref="IStimulationCore.StartStim(WssTarget)"/>
    public void StartStim(WssTarget t) => _core.StartStim(t);

    /// <inheritdoc cref="IStimulationCore.StopStim(WssTarget)"/>
    public void StopStim(WssTarget t) => _core.StopStim(t);

    /// <inheritdoc cref="IStimulationCore.LoadConfigFile"/>
    public void LoadConfigFile() => _core.LoadConfigFile();

    /// <inheritdoc cref="IStimulationCore.GetCoreConfigController"/>
    public CoreConfigController GetCoreConfigController() => _core.GetCoreConfigController();

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose() => _core.Dispose();

    // ---- StimParamsLayer-specific ----

    /// <summary>
    /// Initializes internal arrays that cache per-channel pulse width results.
    /// </summary>
    private void InitArrays()
    {
        for (int i = 0; i < _lastPw.Length; i++)
            _lastPw[i] = 0;
    }
}

}
