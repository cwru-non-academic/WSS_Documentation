namespace WSSInterfacing {
// StimParamsBasicExtensions.cs
using System;

/// <summary>
/// Convenience extension methods over <see cref="IStimParamsCore"/> that forward
/// to the optional BASIC layer (<see cref="IBasicStimulation"/>). These helpers
/// make common operations (waveform upload, shape selection, save/load, requests)
/// concise while preserving the core API surface.
/// </summary>
public static class StimParamsBasicExtensions
{
    /// <summary>Forward to BASIC.UpdateWaveform(WaveformBuilder,...).</summary>
    public static void UpdateWaveform(this IStimParamsCore s, WaveformBuilder wf, int eventId, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.UpdateWaveform(wf, eventId, t);
    }

    /// <summary>Forward to BASIC.UpdateWaveform(int[],...).</summary>
    public static void UpdateWaveform(this IStimParamsCore s, int[] waveform, int eventId, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.UpdateWaveform(waveform, eventId, t);
    }

    /// <summary>Forward to BASIC.UpdateEventShape(...).</summary>
    public static void UpdateEventShape(this IStimParamsCore s, int cathodicWaveform, int anodicWaveform, int eventId, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.UpdateEventShape(cathodicWaveform, anodicWaveform, eventId, t);
    }

    /// <summary>Forward to BASIC.LoadWaveform(...).</summary>
    public static void LoadWaveform(this IStimParamsCore s, string fileName, int eventId)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.LoadWaveform(fileName, eventId);
    }

    /// <summary>Forward to BASIC.WaveformSetup(...).</summary>
    public static void WaveformSetup(this IStimParamsCore s, WaveformBuilder wf, int eventId, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.WaveformSetup(wf, eventId, t);
    }

    /// <summary>Forward to BASIC.Save(...).</summary>
    public static void SaveBoard(this IStimParamsCore s, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.Save(t);
    }

    /// <summary>Forward to BASIC.Load(...).</summary>
    public static void LoadBoard(this IStimParamsCore s, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.Load(t);
    }

    /// <summary>Forward to BASIC.Request_Configs(...).</summary>
    public static void RequestConfigs(this IStimParamsCore s, int command, int id, WssTarget t)
    {
        if (!s.TryGetBasic(out var b)) throw new NotSupportedException("Basic stimulation not available.");
        b.Request_Configs(command, id, t);
    }

    // Optional â€œTry*â€ variants if you prefer no-throw:

    public static bool TryUpdateWaveform(this IStimParamsCore s, WaveformBuilder wf, int eventId, WssTarget t)
        => s.TryGetBasic(out var b) && TryCall(() => b.UpdateWaveform(wf, eventId, t));

    private static bool TryCall(Action a) { try { a(); return true; } catch { return false; } }
}

}
