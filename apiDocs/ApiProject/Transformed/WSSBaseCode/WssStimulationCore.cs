namespace WSSInterfacing {
using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Unity-agnostic WSS stimulation core that manages connection, setup (via a queued step runner),
/// and a background streaming loop. Public mutator methods enqueue device edits and return immediately.
/// </summary>
public sealed class WssStimulationCore : IStimulationCore, IBasicStimulation
{
    #region ========== Fields & nested types ==========
    // ---- transport & config ----
    private readonly CoreConfigController _coreConfig;
    private readonly bool _testMode;
    private readonly string _comPort = null;
    private readonly string _jsonPath;
    private readonly int _maxSetupTries;
    private readonly int _delayMsBetweenPackets = 10; // radio throttling
    private WssClient _wss;
    private bool _resumeStreamingAfter;

    // ---- runtime state ----
    private CoreState _state = CoreState.Disconnected;
    private int _currentSetupTries;
    private readonly Dictionary<WssTarget, int> _cursor = new(); // per-target step index
    private readonly Dictionary<WssTarget, List<Func<Task<string>>>> _steps = new();
    private int _maxWSS = 1;
    private readonly SemaphoreSlim _setupGate = new(1, 1);

    // ---- background tasks ----
    private CancellationTokenSource _streamCts;
    private Task _streamTask;
    private Task _connectTask;
    private Task _setupRunner;

    // ---- channels & controller state ----
    private float[] _chAmps;  // mA (mapped to 0..255 for device)
    private int[] _chPWs;   // us
    private int[] _chIPIs;   // ms period
    private int[] _lastIpiSentPerCh; //used to check changes since frequency reset schedule only send if there is a change
    private int _maxWSSChannels = 0;

    //defaults vaules used for initial setup
    private readonly int _defaultIPI=10;
    private readonly float _defaultAmp = 1.0f;
    private readonly int _defaultSync = 170;
    private readonly int _defaultRatio = 8;
    private readonly int _defaultIPD = 50;
    

    private enum CoreState { Disconnected, Connecting, SettingUp, Ready, Started, Streaming, Error }
    #endregion

    #region ========== Construction ==========
    /// <summary>Construct with explicit COM port.</summary>
    public WssStimulationCore(string comPort, string JSONpath, bool testMode = false, int maxSetupTries = 5)
    {
        _comPort = comPort;
        _jsonPath = JSONpath;
        _coreConfig = new CoreConfigController(_jsonPath);
        _testMode = testMode;
        _maxSetupTries = maxSetupTries;
    }

    /// <summary>Construct without specifying COM port (let transport decide).</summary>
    public WssStimulationCore(string JSONpath, bool testMode = false, int maxSetupTries = 5)
    {
        _jsonPath = JSONpath;
        _comPort = null;
        _coreConfig = new CoreConfigController(_jsonPath);
        _testMode = testMode;
        _maxSetupTries = maxSetupTries;
    }
    #endregion

    #region ========== Lifecycle (Initialize / Tick / Shutdown) ==========
    /// <inheritdoc/>
    public void Initialize()
    {
        if (_state is CoreState.Ready or CoreState.SettingUp) Shutdown();

        _currentSetupTries = 0;

        // (re)load app config
        _coreConfig.LoadJson();
        _maxWSS = _coreConfig.MaxWss;

        InitStimArrays();

        //if test mode use fake transport, otheriwse use a serial transport with port if given or auto method if not given.
        if (_testMode)
        {
            _wss = new WssClient(new TestModeTransport(), new WssFrameCodec(), new WSSVersionHandler(_coreConfig.Firmware));
        } else {
            if (_comPort != null)
            {
                _wss = new WssClient(new SerialPortTransport(_comPort), new WssFrameCodec(), new WSSVersionHandler(_coreConfig.Firmware));
            } else {
                _wss = new WssClient(new SerialPortTransport(), new WssFrameCodec(), new WSSVersionHandler(_coreConfig.Firmware));
            }
        }
        _state = CoreState.Connecting;
        _connectTask = _wss.ConnectAsync();
    }

    /// <inheritdoc/>
    public void Tick()
    {
        switch (_state)
        {
            case CoreState.Connecting:
                if (_connectTask == null) _connectTask = _wss.ConnectAsync();
                else if (_connectTask.IsFaulted)
                {
                    Log.Error("Connect failed: " + _connectTask.Exception?.GetBaseException().Message);
                    _state = CoreState.Error;
                }
                else if (_connectTask.IsCompletedSuccessfully)
                {
                    NormalSetup();                // seed all targets once
                    _state = CoreState.SettingUp;
                }
                break;

            case CoreState.SettingUp:
                // Start or keep the runner. If a pass finished but queue isn't empty (new steps arrived),
                // restart the runner to drain remaining work.
                if (_setupRunner == null || (_setupRunner.IsCompleted && !SetupQueueEmpty()))
                    EnsureSetupRunner();
                else if (_setupRunner.IsFaulted)
                {
                    Log.Error("Setup failed: " + _setupRunner.Exception?.GetBaseException().Message);
                    _setupRunner = null;
                    if (++_currentSetupTries > _maxSetupTries) _state = CoreState.Error;
                }
                else if (_setupRunner.IsCompleted && SetupQueueEmpty())
                {
                    _state = CoreState.Ready;
                }
                break;

            case CoreState.Ready:
                if (_wss.Started)
                {
                    _state = CoreState.Started;
                }
                break;

            case CoreState.Started:
                StartStreamingInternal();
                _state = CoreState.Streaming;
                break;

            case CoreState.Streaming:
                // background streaming task is running; Tick has nothing to do
                break;

            case CoreState.Error:
                StopStreamingInternal();
                SafeDisconnect();
                break;
        }
    }

    /// <inheritdoc/>
    public void Shutdown()
    {
        StopStreamingInternal();
        if (_wss != null)
        {
            try { _wss.ZeroOutStim(); } catch { }
            SafeDisconnect();
            try { _wss.Dispose(); } catch { }
        }

        _state = CoreState.Disconnected;
        _streamCts?.Cancel();
        _wss = null;
    }

    /// <summary>
    /// Reloads the stimulation JSON configuration from disk into memory.
    /// </summary>
    public void LoadConfigFile() => _coreConfig.LoadJson();

    /// <inheritdoc/>
    public void Dispose() => Shutdown();
    #endregion

    #region ========== Status ==========
    /// <inheritdoc/>
    public bool Started() => _state is CoreState.Started or CoreState.Streaming;

    /// <inheritdoc/>
    public bool Ready() => _state is CoreState.Ready;
    #endregion

    #region ========== Public control API (non-blocking) ==========
    /// <inheritdoc/>
    public void StimulateAnalog(int channel, int PW, float amp, int IPI)
    {
        if (channel <= 0 || channel > _maxWSS * 3) return;

        _chAmps[channel - 1] = amp;
        _chPWs[channel - 1] = PW;
        _chIPIs[channel - 1] = IPI;
    }

    /// <inheritdoc/>
    public void ZeroOutStim(WssTarget wsstarget = WssTarget.Broadcast)
    {
        _ = _wss.ZeroOutStim(wsstarget);
    }

    /// <inheritdoc/>
    public void StartStim(WssTarget targetWSS = WssTarget.Broadcast)
    {
        if (_wss == null) return;
        switch (_state)
        {
            case CoreState.Ready:
                _ = ScheduleSetupChangeAsync(targetWSS,
                    () => StepLogger(_wss.StartStim(targetWSS),      $"StartStim[{targetWSS}]")
                );
                _currentSetupTries = 0;
                break;
            case CoreState.Started:
            case CoreState.Streaming:
                if (Started())
                {
                    Log.Info("WSS already started, to force restart call reset radio, or stop stim and start again.");
                } else {
                    Log.Error("Stim is not started, but it originally passed the started test. Reseting radio now");
                    Shutdown();
                    Initialize();
                }
                break;
            case CoreState.Connecting:
            case CoreState.SettingUp:
                Log.Warn("State must be " + CoreState.Ready.ToString() + " and it is currently " + _state.ToString());
                break;
            case CoreState.Disconnected:
            case CoreState.Error:
                Log.Warn("Wss is disconeted or error out. Trying to stablish connection");
                Shutdown();
                Initialize();
                break;
        }
    }

    /// <inheritdoc/>
    public void StopStim(WssTarget targetWSS = WssTarget.Broadcast)
    {
        if (_wss == null) return;

        switch (_state)
        {
            case CoreState.SettingUp:
            case CoreState.Ready:
            case CoreState.Started:
                _ = ScheduleSetupChangeAsync(targetWSS,
                    () => StepLogger(_wss.StopStim(targetWSS),      $"StopStim[{targetWSS}]")
                );
                break;
            case CoreState.Streaming:
                _ = ScheduleSetupChangeAsync(targetWSS,
                    () => StepLogger(_wss.StopStim(targetWSS),      $"StopStim[{targetWSS}]")
                );
                StopStreamingInternal();
                break;
        }
    }

    /// <inheritdoc/>
    public bool IsChannelInRange(int ch)
    {
        return ch > 0 && ch <= _maxWSSChannels;
    }

    /// <inheritdoc/>
    public void UpdateIPD(int IPD, WssTarget targetWSS = WssTarget.Broadcast)
    {
        int _currentIPD = Math.Clamp(IPD, 1, 1000);

        _ = ScheduleSetupChangeAsync(targetWSS,
            () => StepLogger(_wss.EditEventPw(1, new[] { 0, 0, _currentIPD }, targetWSS), $"UpdateIPD[{targetWSS}], Event[1]"),
            () => StepLogger(_wss.EditEventPw(2, new[] { 0, 0, _currentIPD }, targetWSS), $"UpdateIPD[{targetWSS}], Event[2]"),
            () => StepLogger(_wss.EditEventPw(3, new[] { 0, 0, _currentIPD }, targetWSS), $"UpdateIPD[{targetWSS}], Event[3]")
        );
    }

    /// <inheritdoc/>
    public void UpdateIPD(int IPD, int eventID, WssTarget targetWSS = WssTarget.Broadcast)
    {
        int _currentIPD = Math.Clamp(IPD, 1, 1000);

        _ = ScheduleSetupChangeAsync(targetWSS,
            () => StepLogger(_wss.EditEventPw(eventID, new[] { 0, 0, _currentIPD }, targetWSS), $"UpdateIPD[{targetWSS}], Event[{eventID}]")
        );
    }
    

    /// <inheritdoc/>
    public void UpdateWaveform(int[] waveform, int eventID, WssTarget targetWSS = WssTarget.Broadcast)
    {
        WaveformSetup(new WaveformBuilder(waveform), eventID, targetWSS);
    }

    /// <inheritdoc/>
    public void UpdateWaveform(WaveformBuilder waveform, int eventID, WssTarget targetWSS = WssTarget.Broadcast)
    {
        WaveformSetup(waveform, eventID, targetWSS);
    }

    /// <inheritdoc/>
    public void UpdateEventShape(int cathodicWaveform, int anodicWaveform, int eventID, WssTarget targetWSS = WssTarget.Broadcast)
    {
        _ = ScheduleSetupChangeAsync(targetWSS,
            () => StepLogger(_wss.EditEventShape(eventID, cathodicWaveform, anodicWaveform, targetWSS),      $"UpdateShape[{targetWSS}], Event[{eventID}]")
        );
    }

    /// <inheritdoc/>
    public void LoadWaveform(string fileName, int eventID)
    {
        string candidatePath = Path.Combine(_jsonPath, fileName);
        if (!candidatePath.EndsWith("WF.json", StringComparison.OrdinalIgnoreCase))
        {
            var stem = Path.GetFileNameWithoutExtension(fileName) + "WF";
            candidatePath = Path.Combine(_jsonPath, Path.ChangeExtension(stem, "json"));
        }

        try
        {
            string json = File.ReadAllText(candidatePath);
            var shape = JsonConvert.DeserializeObject<Waveform>(json);
            if (shape == null) throw new InvalidDataException("Waveform JSON deserialized to null");
            UpdateWaveform(new WaveformBuilder(shape), eventID);
        }
        catch (Exception ex)
        {
            Log.Error($"[WssStimulationCore] JSON loading error: {ex.Message} ({candidatePath})");
        }
    }

    /// <inheritdoc/>
    public void Save(WssTarget targetWSS = WssTarget.Broadcast)
    {
        _ = ScheduleSetupChangeAsync(targetWSS, () => StepLogger(_wss.PopulateFramSettings(targetWSS), $"SaveChanges[{targetWSS}]"));
    }

    /// <inheritdoc/>
    public void Load(WssTarget targetWSS = WssTarget.Broadcast)
    {
        _ = ScheduleSetupChangeAsync(targetWSS, () => StepLogger(_wss.PopulateBoardSettings(targetWSS), $"Load[{targetWSS}]"));
    }

    /// <inheritdoc/>
    public void Request_Configs(int command, int id, WssTarget targetWSS = WssTarget.Broadcast)
    {
        _ = ScheduleSetupChangeAsync(targetWSS, () => StepLogger(_wss.RequestConfigs(command, id, targetWSS), $"RequestConfig[{command}, {id}]"));
    }

    /// <inheritdoc/>
    public CoreConfigController GetCoreConfigController()
    {
        return _coreConfig;
    }
    #endregion

    #region ========== Setup seeding (NormalSetup) ==========
    /// <summary>
    /// Seeds the per-target setup step lists for a full initial configuration and starts
    /// the setup runner. Used once after connect.
    /// </summary>
    public void NormalSetup()
    {
        var tgts = Targets(_maxWSS);
        foreach (var t in tgts)
        {
            if (!_steps.ContainsKey(t)) { _steps[t] = new List<Func<Task<string>>>(); _cursor[t] = 0; }
            _steps[t].Clear();
            _cursor[t] = 0;

            _steps[t].AddRange(new Func<Task<string>>[]
            {
                () => StepLogger(_wss.Clear(0, t), $"Clear[{t}]"),

                // Schedule 1 (Ch1)
                () => StepLogger(_wss.CreateSchedule(1, _defaultIPI, _defaultSync, t), $"CreateSchedule#1[{t}]"), //TODO frequewncy from config
                () => StepLogger(_wss.CreateContactConfig(1, new[]{1,2,0,0}, new[]{2,1,0,0}, 2, t), $"CreateContactConfig#1[{t}]"),
                () => StepLogger(_wss.CreateEvent(1, 0, 1, 0, 0,
                        new[]{AmpTo255Convention(_defaultAmp),AmpTo255Convention(_defaultAmp),0,0},
                        new[]{AmpTo255Convention(_defaultAmp),AmpTo255Convention(_defaultAmp),0,0},
                        new[]{0,0,_defaultIPD}, t), $"CreateEvent#1[{t}]"),
                () => StepLogger(_wss.EditEventRatio(1, _defaultRatio, t), $"EditEventRatio#1[{t}]"),
                () => StepLogger(_wss.AddEventToSchedule(1, 1, t), $"AddEventToSchedule#1[{t}]"),

                // Schedule 2 (Ch2)
                () => StepLogger(_wss.CreateSchedule(2, _defaultIPI, _defaultSync, t), $"CreateSchedule#2[{t}]"),
                () => StepLogger(_wss.CreateContactConfig(2, new[]{1,0,2,0}, new[]{2,0,1,0}, 4, t), $"CreateContactConfig#2[{t}]"),
                () => StepLogger(_wss.CreateEvent(2, 2, 2, 0, 0,
                        new[]{AmpTo255Convention(_defaultAmp),AmpTo255Convention(_defaultAmp),0,0},
                        new[]{AmpTo255Convention(_defaultAmp),AmpTo255Convention(_defaultAmp),0,0},
                        new[]{0,0,_defaultIPD}, t), $"CreateEvent#2[{t}]"),
                () => StepLogger(_wss.EditEventRatio(2, _defaultRatio, t), $"EditEventRatio#2[{t}]"),
                () => StepLogger(_wss.AddEventToSchedule(2, 2, t), $"AddEventToSchedule#2[{t}]"),

                // Schedule 3 (Ch3)
                () => StepLogger(_wss.CreateSchedule(3, _defaultIPI, _defaultSync, t), $"CreateSchedule#3[{t}]"),
                () => StepLogger(_wss.CreateContactConfig(3, new[]{1,0,0,2}, new[]{2,0,0,1}, 8, t), $"CreateContactConfig#3[{t}]"),
                () => StepLogger(_wss.CreateEvent(3, 4, 3, 0, 0,
                        new[]{AmpTo255Convention(_defaultAmp),AmpTo255Convention(_defaultAmp),0,0},
                        new[]{AmpTo255Convention(_defaultAmp),AmpTo255Convention(_defaultAmp),0,0},
                        new[]{0,0,_defaultIPD}, t), $"CreateEvent#3[{t}]"),
                () => StepLogger(_wss.EditEventRatio(3, _defaultRatio, t), $"EditEventRatio#3[{t}]"),
                () => StepLogger(_wss.AddEventToSchedule(3, 3, t), $"AddEventToSchedule#3[{t}]"),

                () => StepLogger(_wss.SyncGroup(_defaultSync, t), $"SyncGroup[{t}]"),
                () => StepLogger(_wss.StartStim(t),      $"StartStim[{t}]"),
            });
        }
        _resumeStreamingAfter = true; // after full setup, begin streaming automatically
        _state = CoreState.SettingUp;
        EnsureSetupRunner();  // single runner drains all targets
    }
    #endregion

    #region ========== Waveforms (enqueue uploads) ==========
    /// <inheritdoc/>
    public void WaveformSetup(WaveformBuilder wave, int eventID, WssTarget targetWSS = WssTarget.Broadcast)
    {
        var cat = wave.getCatShapeArray();
        var ano = wave.getAnodicShapeArray();

        // Defensive: ensure arrays are long enough for 4 chunks; if not, clamp slices.
        static int[] SliceSafe(int[] a, int start, int end)
        {
            start = Math.Max(0, Math.Min(start, a.Length));
            end   = Math.Max(start, Math.Min(end, a.Length));
            var r = new int[end - start];
            Array.Copy(a, start, r, 0, r.Length);
            return r;
        }

        int Lc = cat.Length, La = ano.Length;
        int s0 = 0,  e0 = Math.Max(0, Lc - 24);
        int s1 = 8,  e1 = Math.Max(8, Lc - 16);
        int s2 = 16, e2 = Math.Max(16, Lc - 8);
        int s3 = 24, e3 = Lc;

        int t0 = 0,  u0 = Math.Max(0, La - 24);
        int t1 = 8,  u1 = Math.Max(8, La - 16);
        int t2 = 16, u2 = Math.Max(16, La - 8);
        int t3 = 24, u3 = La;

        _ = ScheduleSetupChangeAsync(targetWSS,
            () => _wss.SetCustomWaveform(0, SliceSafe(cat, s0, e0), 0, targetWSS),
            () => _wss.SetCustomWaveform(0, SliceSafe(cat, s1, e1), 1, targetWSS),
            () => _wss.SetCustomWaveform(0, SliceSafe(cat, s2, e2), 2, targetWSS),
            () => _wss.SetCustomWaveform(0, SliceSafe(cat, s3, e3), 3, targetWSS),
            () => _wss.SetCustomWaveform(1, SliceSafe(ano, t0, u0), 0, targetWSS),
            () => _wss.SetCustomWaveform(1, SliceSafe(ano, t1, u1), 1, targetWSS),
            () => _wss.SetCustomWaveform(1, SliceSafe(ano, t2, u2), 2, targetWSS),
            () => _wss.SetCustomWaveform(1, SliceSafe(ano, t3, u3), 3, targetWSS),
            () => _wss.EditEventShape(eventID, 11, 12, targetWSS)
        );
    }
    #endregion

    #region ========== Streaming internals ==========
    /// <summary>Background task that pushes streaming packets to each WSS at ~12ms cadence.</summary>
    private void StartStreamingInternal()
    {
        if (_streamTask is { IsCompleted: false }) return;
        _streamCts = new CancellationTokenSource();
        var tk = _streamCts.Token;

        _streamTask = Task.Run(async () =>
        {
            while (!tk.IsCancellationRequested)
            {
                for (int w = 1; w <= _maxWSS; w++)
                {
                    int wIdx    = w - 1;
                    int baseIdx = wIdx * 3;

                    var amps = new[] {
                        AmpTo255Convention(_chAmps[baseIdx + 0]),
                        AmpTo255Convention(_chAmps[baseIdx + 1]),
                        AmpTo255Convention(_chAmps[baseIdx + 2])
                    };
                    var pws = new[] {
                        _chPWs[baseIdx + 0],
                        _chPWs[baseIdx + 1],
                        _chPWs[baseIdx + 2]
                    };

                    // Desired per-channel IPIs for this WSS (source of truth)
                    var desiredIpis = new[] {
                        _chIPIs[baseIdx + 0],
                        _chIPIs[baseIdx + 1],
                        _chIPIs[baseIdx + 2]
                    };

                    // Per-channel change memory, gated by per-WSS cooldown
                    bool anyChanged =
                        _lastIpiSentPerCh[baseIdx + 0] != desiredIpis[0] ||
                        _lastIpiSentPerCh[baseIdx + 1] != desiredIpis[1] ||
                        _lastIpiSentPerCh[baseIdx + 2] != desiredIpis[2];

                    if (anyChanged)
                    {
                        // Send one WSS-level IPI update (array API). This is the only time we send.
                        _ = _wss.StreamChange(amps, pws, desiredIpis, IntToWssTarget(w));

                        // Update per-channel last-sent memory and start per-WSS cooldown
                        _lastIpiSentPerCh[baseIdx + 0] = desiredIpis[0];
                        _lastIpiSentPerCh[baseIdx + 1] = desiredIpis[1];
                        _lastIpiSentPerCh[baseIdx + 2] = desiredIpis[2];
                    }
                    else
                    {
                        // Do not send any IPI during cooldown or if nothing changed
                        _ = _wss.StreamChange(amps, pws, null, IntToWssTarget(w));
                    }

                    await Task.Delay(_delayMsBetweenPackets, tk);
                }
            }
        }, tk);
    }

    /// <summary>Stop the streaming background task.</summary>
    private void StopStreamingInternal()
    {
        _streamCts?.Cancel();
        try { _streamTask?.Wait(250); } catch { }
        _streamTask = null;
    }

    /// <summary>Disconnect transport safely.</summary>
    private void SafeDisconnect()
    {
        try { _wss?.DisconnectAsync().GetAwaiter().GetResult(); } catch { }
    }
    #endregion

    #region ========== Setup runner internals ==========
    /// <summary>Append steps to a target, pause streaming if needed, and ensure the runner is active.</summary>
    private async Task ScheduleSetupChangeAsync(WssTarget t, params Func<Task<string>>[] newSteps)
    {
        await _setupGate.WaitAsync();
        try
        {
            if (!_steps.ContainsKey(t)) { _steps[t] = new List<Func<Task<string>>>(); _cursor[t] = 0; }
            _steps[t].AddRange(newSteps);

            // pause streaming once; resume when queue drains
            if (_state == CoreState.Streaming) { StopStreamingInternal(); _resumeStreamingAfter = true; }
            _state = CoreState.SettingUp;

            EnsureSetupRunner();
        }
        finally { _setupGate.Release(); }
    }

    /// <summary>Start the background runner if needed.</summary>
    private void EnsureSetupRunner()
    {
        if (_setupRunner is { IsCompleted: false }) return;
        _setupRunner = Task.Run(SetupWorkerAsync);
    }

    /// <summary>Run a pass over all targets, advancing each cursor. Resume streaming when empty.</summary>
    private async Task SetupWorkerAsync()
    {
        try
        {
            // Iterate deterministic target order (snapshot keys to avoid collection-modified issues)
            foreach (var kvp in _steps.ToArray())
            {
                var t = kvp.Key;
                var list = kvp.Value;
                if (list == null || list.Count == 0) continue;

                for (int i = _cursor[t]; i < list.Count; i++)
                {
                    _cursor[t] = i; // resume point
                    var res = await list[i](); // throws on timeout/IO; returns "Error: ..." on device error
                    if (res.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException($"{t} step#{i} failed: {res}");
                }
                _cursor[t] = list.Count; // finished this target
            }

            if (SetupQueueEmpty())
            {
                if (_resumeStreamingAfter) { StartStreamingInternal(); _resumeStreamingAfter = false; }
            }
            // else: leave state as SettingUp; Tick() will Observe runner completed + !empty and spawn another pass
        }
        catch (Exception ex)
        {
            Log.Error($"[SetupWorker] {ex.Message}");
            _state = CoreState.Error;
        }
    }

    /// <summary>True when all target cursors have consumed their lists.</summary>
    private bool SetupQueueEmpty()
    {
        foreach (var kvp in _steps)
        {
            if (kvp.Value != null && _cursor.TryGetValue(kvp.Key, out var c))
                if (c < kvp.Value.Count) return false;
        }
        return true;
    }

    /// <summary>Which targets to include (clamped to transport capability).</summary>
    private static WssTarget[] Targets(int maxWss)
    {
        int n = Math.Max(1, Math.Min(maxWss, 3));
        return n switch
        {
            1 => new[] { WssTarget.Wss1 },
            2 => new[] { WssTarget.Wss1, WssTarget.Wss2 },
            _ => new[] { WssTarget.Wss1, WssTarget.Wss2, WssTarget.Wss3 }
        };
    }

    /// <summary>Log per-step result; throw on device "Error:" or timeout. (Non-blocking.)</summary>
    private static async Task<string> StepLogger(Task<string> op, string name)
    {
        try
        {
            var res = await op.ConfigureAwait(false);
            if (res.StartsWith("Error:", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"{name} failed: {res}");
            Log.Info($"{name}: {res}");
            return res;
        }
        catch (OperationCanceledException)
        {
            throw new TimeoutException($"{name} timed out (no reply).");
        }
    }
    #endregion

    #region ========== Helpers ==========
    /// <summary> Initializes arrays to hold values neccessary for streaming</summary>
    private void InitStimArrays()
    {
        _maxWSSChannels = _maxWSS * 3;
        _chAmps = new float[_maxWSSChannels];
        _chPWs = new int[_maxWSSChannels];
        _chIPIs = new int[_maxWSSChannels];
        _lastIpiSentPerCh = new int[_maxWSSChannels];

        for (int i = 0; i < _maxWSSChannels; i++)
        {
            _chAmps[i] = AmpTo255Convention(_defaultAmp);
            _chPWs[i] = 0;
            _chIPIs[i] = _defaultIPI;
            _lastIpiSentPerCh[i] = _defaultIPI;
        }
    }
    
    /// <summary> hanndles the  non-linear conversion of an amplitude in mA to bytes depending on WSS capabilities </summary>
    private int AmpTo255Convention(float amp)
    {
        if (amp < 4)
        {
            double v = Math.Pow(amp / 0.0522f, 1.0 / 1.5466f) + 1.0;
            return (int)v;
        }
        else
        {
            double v = ((amp + 1.7045f) / 0.3396f) + 1.0;
            return (int)v;
        }
    }

    /// <summary> Helper to use int instead of targets in for loops </summary>
    private WssTarget IntToWssTarget(int i)
    {
        return i switch
        {
            0 => WssTarget.Broadcast,
            1 => WssTarget.Wss1,
            2 => WssTarget.Wss2,
            3 => WssTarget.Wss3,
            _ => WssTarget.Wss1
        };
    }
    #endregion
}

}
