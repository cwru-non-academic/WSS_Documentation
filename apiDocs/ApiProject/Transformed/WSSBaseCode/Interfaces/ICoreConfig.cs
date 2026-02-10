namespace WSSInterfacing {
/// <summary>
/// Read-only view of core configuration values loaded from the JSON config file.
/// Implementations expose firmware information and global limits.
/// </summary>
public interface ICoreConfig
{
    /// <summary>
    /// Maximum number of WSS devices supported by this configuration.
    /// </summary>
    int MaxWss { get; }
    /// <summary>
    /// Firmware version string (e.g., "H03", "J03").
    /// </summary>
    string Firmware { get; }
}

}
