namespace WSSInterfacing {
using System.Collections.Generic;

/// <summary>
/// Handles WSS firmware version parsing and feature availability based on supported firmware versions.
/// </summary>
public class WSSVersionHandler
{
    /// <summary>
    /// Enum representing known supported WSS firmware versions.
    /// Integer values correspond to internal numeric codes.
    /// </summary>
    public enum SupportedVersions : int
    {
        H03 = 83,
        J03 = 93
    }

    /// <summary>
    /// Currently active firmware version for this handler.
    /// </summary>
    private readonly SupportedVersions version;

    /// <summary>
    /// Defines the minimum firmware version required for each feature.
    /// </summary>
    private static readonly Dictionary<string, SupportedVersions> FeatureMinimumVersions = new()
    {
        { "AmplitudeCheck", SupportedVersions.J03 },
        { "LEDSettings", SupportedVersions.J03 }
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="WSSVersionHandler"/> class using a string version.
    /// Defaults to <see cref="SupportedVersions.H03"/> if an unknown version string is provided.
    /// </summary>
    /// <param name="strVersion">Firmware version string (e.g., "H03", "J03").</param>
    public WSSVersionHandler(string strVersion)
    {
        version = strVersion switch
        {
            "J03" => SupportedVersions.J03,
            "H03" => SupportedVersions.H03,
            _     => SupportedVersions.H03
        };
    }

    /// <summary>
    /// Determines whether the specified firmware version string is supported by this application.
    /// </summary>
    /// <param name="strVersion">
    /// Firmware version identifier to check (for example, <c>"H03"</c> or <c>"J03"</c>).
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the supplied version is recognized and supported; 
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool isVersionSupported(string strVersion)
    {
        bool supported = strVersion switch
        {
            "J03" => true,
            "H03" => true,
            _ => false
        };
        return supported;
    }

    /// <summary>
    /// Gets the current firmware version as a named enum.
    /// </summary>
    /// <returns>The current <see cref="SupportedVersions"/> enum value.</returns>
    public SupportedVersions GetVersion() => version;

    /// <summary>
    /// Checks if the specified feature is supported by the current firmware version.
    /// </summary>
    /// <param name="feature">The name of the feature to check (e.g., "AmplitudeCheck").</param>
    /// <returns><c>true</c> if the feature is available in this firmware version; otherwise, <c>false</c>.</returns>
    public bool IsFeatureAvailable(string feature)
    {
        return FeatureMinimumVersions.TryGetValue(feature, out var requiredVersion)
            && version >= requiredVersion;
    }

    /// <summary>
    /// Indicates whether amplitude check support is available in the current firmware version.
    /// </summary>
    /// <returns><c>true</c> if amplitude check is supported; otherwise, <c>false</c>.</returns>
    public bool IsAmplitudeCheckAvailable() => IsFeatureAvailable("AmplitudeCheck");

    /// <summary>
    /// Indicates whether LED settings are available in the current firmware version.
    /// </summary>
    /// <returns><c>true</c> if LED settings are supported; otherwise, <c>false</c>.</returns>
    public bool IsLEDSettingsAvailable() => IsFeatureAvailable("LEDSettings");

    /// <summary>
    /// Returns the string representation of the current firmware version (e.g., "H03").
    /// </summary>
    /// <returns>The string name of the current firmware version.</returns>
    public override string ToString() => version.ToString();
}

}
