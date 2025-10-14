namespace WSSInterfacing {
using System;
using System.Collections.Generic;

/// <summary>
/// Contract for framing and deframing WSS messages over byte-stream transports (Serial, BLE, TCP).
/// </summary>
public interface IFrameCodec
{
    /// <summary>
    /// Builds a wire-safe frame for transmission using SLIP-style escaping.
    /// Output format (before escaping): <c>[sender][target][payload...][checksum][END]</c>.
    /// </summary>
    /// <param name="sender">Sender address byte.</param>
    /// <param name="target">Target/receiver address byte (e.g., device address or broadcast).</param>
    /// <param name="payload">
    /// The protocol payload (e.g., <c>[cmd][len][...]</c>). The codec does not modify the payload.
    /// </param>
    /// <returns>Escaped frame ready to send (always terminates with END).</returns>
    byte[] Frame(byte sender, byte target, byte[] payload);
    
    /// <summary>
    /// Progressive deframer: feed any-size incoming chunks and get back zero or more
    /// complete, unescaped frames. Each returned frame has the format
    /// <c>[sender][target][payload...][checksum]</c> (END is consumed).
    /// </summary>
    /// <param name="chunk">A raw byte chunk from the transport (may be partial or contain multiple frames).</param>
    /// <returns>0..N complete, unescaped frames (END removed). Checksum is not validated here.</returns>
    IEnumerable<byte[]> Deframe(byte[] chunk);
}


}
