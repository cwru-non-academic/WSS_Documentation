namespace WSSInterfacing {
/// <summary>
/// Addresses for targeting individual WSS units or broadcasting to all.
/// Values are on-wire destination IDs used by commands.
/// </summary>
public enum WssTarget : byte
{
    /// <summary>Broadcast to all connected WSS devices.</summary>
    Broadcast = 0x8F,
    /// <summary>First WSS device.</summary>
    Wss1 = 0x81,
    /// <summary>Second WSS device.</summary>
    Wss2 = 0x82,
    /// <summary>Third WSS device.</summary>
    Wss3 = 0x83,
}

}
