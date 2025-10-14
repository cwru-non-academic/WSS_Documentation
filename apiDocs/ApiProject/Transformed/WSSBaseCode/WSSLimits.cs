namespace WSSInterfacing {
/// <summary>
/// Device- and protocol-level numeric limits and sizes used by the WSS stack.
/// Values reflect firmware constraints such as maximum pulse width and buffer sizes.
/// </summary>
public enum WSSLimits : int
    {
        /// <summary>Max value that fits in one byte.</summary>
        oneByte = 255,
        /// <summary>Number of custom waveform slots.</summary>
        shape = 13,
        /// <summary>Index of the module ID field in replies.</summary>
        moduleIndex=1,
        /// <summary>Index used by Clear command payload.</summary>
        clearIndex = 3,
        /// <summary>Maximum long-form pulse width (Âµs) supported by firmware.</summary>
        pulseWidthLong = 5400,
        //pulseWidthLongLegacy = 1000 TODO
        /// <summary>State field size (bytes).</summary>
        state = 2,
        /// <summary>Number of chunks used to upload a custom waveform.</summary>
        customWaveformChunks = 3,
        /// <summary>Timer base frequency used by device (Hz).</summary>
        Frequency = 10000,//TODO
        /// <summary>Maximum allowed custom waveform amplitude (device units).</summary>
        customWaveformMaxAmp = 2000,
        /// <summary>Maximum inter-pulse delay (ms).</summary>
        IPD = 1000,
        /// <summary>Number of addressable LEDs.</summary>
        LEDs = 8,
        /// <summary>Value used to represent an enabled state.</summary>
        enable = 1,
    }

}
