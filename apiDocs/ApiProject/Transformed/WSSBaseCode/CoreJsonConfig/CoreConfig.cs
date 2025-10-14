namespace WSSInterfacing {
/// <summary>
/// Strongly-typed core configuration model persisted in JSON.
/// Exposes the maximum number of WSS devices and the firmware version string.
/// </summary>
public sealed class CoreConfig
{
    /// <summary>Maximum number of WSS devices supported by this app.</summary>
    public int maxWSS { get; set; } = 1;
    /// <summary>Firmware version string (e.g., "H03", "J03").</summary>
    public string firmware { get; set; } = "H03";
}

}
