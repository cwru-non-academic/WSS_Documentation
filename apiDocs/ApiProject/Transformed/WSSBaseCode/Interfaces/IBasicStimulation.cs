namespace WSSInterfacing {
using System;

/// <summary>
/// Basic stimulation operations that upload or select waveforms and manage
/// board configuration. Calls are non-blocking; implementations may enqueue
/// setup steps and return immediately.
/// </summary>
public interface IBasicStimulation : IDisposable
{
    /// <summary>
    /// Sets event shape IDs directly for the given <paramref name="eventID"/>.
    /// Implementations should send setup edits with replies.
    /// </summary>
    /// <param name="cathodicWaveform">Shape ID for the standard phase.</param>
    /// <param name="anodicWaveform">Shape ID for the recharge phase.</param>
    /// <param name="eventID">Event slot to modify.</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void UpdateEventShape(int cathodicWaveform, int anodicWaveform, int eventID, WssTarget wssTarget);

    /// <summary>
    /// Uploads a prepared waveform and assigns shapes for <paramref name="eventID"/>.
    /// </summary>
    /// <param name="waveform">Prepared <see cref="WaveformBuilder"/>.</param>
    /// <param name="eventID">Event slot to target.</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void UpdateWaveform(WaveformBuilder waveform, int eventID, WssTarget wssTarget);

    /// <summary>
    /// Builds a custom waveform from raw points and schedules the upload
    /// for the specified <paramref name="eventID"/>.
    /// </summary>
    /// <param name="waveform">Concatenated waveform definition.</param>
    /// <param name="eventID">Event slot to target.</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void UpdateWaveform(int[] waveform, int eventID, WssTarget wssTarget);

    /// <summary>
    /// Loads a waveform JSON file (â€¦WF.json), builds it, and enqueues an upload.
    /// </summary>
    /// <param name="fileName">File name or path; â€œWF.jsonâ€ suffix is enforced.</param>
    /// <param name="eventID">Event slot to target.</param>
    void LoadWaveform(string fileName, int eventID);

    /// <summary>
    /// Schedules the upload of custom waveform chunks and points
    /// <paramref name="eventID"/> at the uploaded shapes.
    /// </summary>
    /// <param name="wave">Waveform builder.</param>
    /// <param name="eventID">Event slot to target.</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void WaveformSetup(WaveformBuilder wave, int eventID, WssTarget wssTarget);

    // setup & edits 

    /// <summary>
    /// Saves board settings to non-volatile memory. Implementations should
    /// pause streaming if needed, perform the save, then resume.
    /// </summary>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void Save(WssTarget wssTarget);

    /// <summary>
    /// Loads board settings from non-volatile memory. Implementations should
    /// pause streaming if needed, perform the load, then resume.
    /// </summary>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void Load(WssTarget wssTarget);

    /// <summary>
    /// Requests configuration blocks from the device.
    /// </summary>
    /// <param name="command">Command group identifier.</param>
    /// <param name="id">Sub-id / selector.</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void Request_Configs(int command, int id, WssTarget wssTarget);

    /// <summary>
    /// Updates inter-phase delay (IPD) for events 1–3 via setup commands (with replies).
    /// If currently Streaming, the core pauses streaming, sends edits, and resumes when done.
    /// </summary>
    /// <param name="ipd">Inter-phase delay in microseconds (clamped internally).</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void UpdateIPD(int ipd, WssTarget wssTarget = WssTarget.Broadcast);

    /// <summary>
    /// Updates inter-phase delay (IPD) for a specific <paramref name="eventID"/> via setup commands (with replies).
    /// If currently Streaming, the core pauses streaming, sends the edit, and resumes when done.
    /// </summary>
    /// <param name="ipd">Inter-phase delay in microseconds (clamped internally).</param>
    /// <param name="eventID">Event slot to target.</param>
    /// <param name="wssTarget">Target device or broadcast.</param>
    void UpdateIPD(int ipd, int eventID, WssTarget wssTarget = WssTarget.Broadcast);
}



}
