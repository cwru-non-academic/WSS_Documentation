namespace WSSInterfacing {
using System;
using System.Collections.Generic;

/// <summary>
/// WSS frame codec implementing a SLIP-like protocol:
/// <list type="bullet">
/// <item><description><b>END</b> (<c>0xC0</c>) terminates a frame.</description></item>
/// <item><description><b>ESC</b> (<c>0xDB</c>) escapes special bytes inside the frame.</description></item>
/// <item><description><b>END_SUB</b> (<c>0xDC</c>) represents END when escaped.</description></item>
/// <item><description><b>ESC_SUB</b> (<c>0xDD</c>) represents ESC when escaped.</description></item>
/// </list>
/// Frames on the wire are <c>[sender][target][payload...][checksum][END]</c>.
/// The checksum is computed over all bytes except the last two (checksum + END).
/// </summary>
public sealed class WssFrameCodec : IFrameCodec
{
    // Protocol bytes
    private const byte END     = 0xC0;
    private const byte ESC     = 0xDB;
    private const byte END_SUB = 0xDC;
    private const byte ESC_SUB = 0xDD;

    // Accumulator for partial incoming data between END markers.
    private readonly List<byte> _accum = new List<byte>(256);

    /// <inheritdoc />
    public byte[] Frame(byte sender, byte target, byte[] payload)
    {
        if (payload == null) throw new ArgumentNullException(nameof(payload));

        // raw = [sender][target][payload...][checksum][END]
        var raw = new byte[2 + payload.Length + 2];
        raw[0] = sender;
        raw[1] = target;

        // Copy payload
        for (int i = 0; i < payload.Length; i++)
            raw[2 + i] = payload[i];

        // Compute checksum over all bytes except the last two (checksum + END).
        raw[raw.Length - 2] = ComputeChecksum(raw, trailerBytesToSkip: 2);
        raw[raw.Length - 1] = END;

        // Escape all bytes except the final END terminator.
        var escaped = new List<byte>(raw.Length * 2);
        for (int i = 0; i < raw.Length - 1; i++)
        {
            byte b = raw[i];
            if (b == END)      { escaped.Add(ESC); escaped.Add(END_SUB); }
            else if (b == ESC) { escaped.Add(ESC); escaped.Add(ESC_SUB); }
            else               { escaped.Add(b); }
        }
        escaped.Add(END);

        return escaped.ToArray(); // tight array (size == Count)
    }

    /// <inheritdoc />
    public IEnumerable<byte[]> Deframe(byte[] chunk)
    {
        if (chunk == null || chunk.Length == 0)
            yield break;

        for (int i = 0; i < chunk.Length; i++)
        {
            byte b = chunk[i];
            if (b == END)
            {
                // We reached the end of one frame. Unescape + validate accumulated bytes.
                var pre = _accum.ToArray();
                _accum.Clear();

                if (TryUnescapeAndValidate(pre, out var frame))
                {
                    yield return frame; // Valid frame: [sender][target][payload...][checksum]
                }
                else
                {
                    // TODO(Luis, 2025-08-10): Dropped invalid frame (checksum error). Add logging/metrics here.
                }
            }
            else
            {
                _accum.Add(b);
            }
        }
    }

    /// <summary>
    /// Attempts to unescape and validate a single frame (without END), returning false if
    /// the checksum does not match. Useful if you want checksum enforcement at the codec layer.
    /// </summary>
    /// <param name="preEscaped">Escaped frame bytes (without the END terminator).</param>
    /// <param name="output">
    /// On success, the unescaped frame <c>[sender][target][payload...][checksum]</c>.
    /// On failure, <see cref="Array.Empty{T}"/> is returned.
    /// </param>
    /// <returns><c>true</c> if the checksum is valid; otherwise <c>false</c>.</returns>
    public static bool TryUnescapeAndValidate(ReadOnlySpan<byte> preEscaped, out byte[] output)
    {
        var tmp = new List<byte>(preEscaped.Length);
        for (int i = 0; i < preEscaped.Length; i++)
        {
            byte b = preEscaped[i];
            if (b == ESC && i + 1 < preEscaped.Length)
            {
                byte n = preEscaped[++i];
                tmp.Add(n == END_SUB ? END : n == ESC_SUB ? ESC : n);
            }
            else
            {
                tmp.Add(b);
            }
        }

        if (tmp.Count < 3)
        {
            output = Array.Empty<byte>();
            return false;
        }

        // Compute checksum over everything except the trailing checksum byte.
        var data = tmp.ToArray();
        byte computed = ComputeChecksum(data, trailerBytesToSkip: 1);
        byte expected = data[^1];
        output = data;
        return computed == expected;
    }

    /// <summary>
    /// Unescapes an accumulated frame (without END) into raw bytes.
    /// Does not validate checksum; see <see cref="TryUnescapeAndValidate"/>.
    /// </summary>
    private static List<byte> Unescape(List<byte> pre)
    {
        var outBuf = new List<byte>(pre.Count);
        for (int i = 0; i < pre.Count; i++)
        {
            byte b = pre[i];
            if (b == ESC && i + 1 < pre.Count)
            {
                byte n = pre[++i];
                outBuf.Add(n == END_SUB ? END : n == ESC_SUB ? ESC : n);
            }
            else
            {
                outBuf.Add(b);
            }
        }
        return outBuf;
    }

    /// <summary>
    /// Computes the WSS checksum by summing all included bytes, folding to 8 bits,
    /// and XOR'ing with 0xFF. The last <paramref name="trailerBytesToSkip"/> bytes are excluded.
    /// </summary>
    /// <param name="buffer">Buffer that contains the frame data.</param>
    /// <param name="trailerBytesToSkip">
    /// Number of trailing bytes to exclude from the sum.
    /// Use 2 for outbound frames (<c>[checksum][END]</c>), 1 for inbound frames (<c>[checksum]</c>).
    /// </param>
    private static byte ComputeChecksum(byte[] buffer, int trailerBytesToSkip)
    {
        int end = Math.Max(0, buffer.Length - trailerBytesToSkip);
        int sum = 0;
        for (int i = 0; i < end; i++) sum += buffer[i];
        sum = ((sum & 0x00FF) + (sum >> 8)) ^ 0xFF;
        return (byte)sum;
    }
}
}
