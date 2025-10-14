namespace WSSInterfacing {
/// <summary>
/// Protocol message identifiers used by the WSS firmware.
/// Values correspond to on-wire command codes.
/// </summary>
public enum WSSMessageIDs : byte
{
    /// <summary>Error reply or status.</summary>
    Error =0x05,
    /// <summary>Queries module identity and capabilities.</summary>
    ModuleQuery = 0x01,
    /// <summary>Device reset command.</summary>
    Reset = 0x04,
    /// <summary>Ping/pong transport echo.</summary>
    Echo = 0x07,
    /// <summary>Requests analog value(s).</summary>
    RequestAnalog = 0x02,
    /// <summary>Clears device state or buffers.</summary>
    Clear = 0x40,
    /// <summary>Requests configuration block.</summary>
    RequestConfig = 0x41,
    /// <summary>Creates a contact configuration.</summary>
    CreateContactConfig = 0x42,
    /// <summary>Deletes a contact configuration.</summary>
    DeleteContactConfig = 0x43,
    /// <summary>Creates a stimulation event.</summary>
    CreateEvent = 0x44,
    /// <summary>Deletes a stimulation event.</summary>
    DeleteEvent = 0x45,
    /// <summary>Adds an event to a schedule.</summary>
    AddEventToSchedule = 0x46,
    /// <summary>Removes an event from a schedule.</summary>
    RemoveEventFromSchedule = 0x47,
    /// <summary>Moves an event within a schedule.</summary>
    MoveEventToSchedule = 0x48,
    /// <summary>Edits an existing event configuration.</summary>
    EditEventConfig = 0x49,
    /// <summary>Creates a stimulation schedule.</summary>
    CreateSchedule = 0x4A,
    /// <summary>Deletes a stimulation schedule.</summary>
    DeleteSchedule = 0x4B,
    /// <summary>Synchronizes a group of devices.</summary>
    SyncGroup = 0x4C,
    /// <summary>Changes a group state.</summary>
    ChangeGroupState = 0x4D,
    /// <summary>Changes schedule configuration.</summary>
    ChangeScheduleConfig = 0x4E,
    /// <summary>Resets schedule to defaults.</summary>
    ResetSchedule = 0x4F,
    /// <summary>Uploads or selects a custom waveform.</summary>
    CustomWaveform = 0x9D,
    /// <summary>Global stimulation on/off switch.</summary>
    StimulationSwitch = 0x0B,
    /// <summary>Generic board control and diagnostics.</summary>
    BoardCommands = 0x09,
    /// <summary>Streaming update: all fields.</summary>
    StreamChangeAll = 0x30,
    /// <summary>Streaming update: no IPI field.</summary>
    StreamChangeNoIPI = 0x31,
    /// <summary>Streaming update: no pulse amplitude.</summary>
    StreamChangeNoPA = 0x33,
    /// <summary>Streaming update: no pulse width.</summary>
    StreamChangeNoPW = 0x32,
}

}
