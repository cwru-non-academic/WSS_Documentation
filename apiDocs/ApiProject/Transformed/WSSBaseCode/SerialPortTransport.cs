namespace WSSInterfacing {
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Management;

/// <summary>
/// Serial-port implementation of <see cref="ITransport"/> for WSS communications.
/// Wraps <see cref="SerialPort"/> and exposes an event-driven receive API that works well in Unity/.NET Standard 2.0.
/// </summary>
/// <remarks>
/// <para><b>Threading:</b> <see cref="BytesReceived"/> is raised from a background task. If your app (e.g., Unity)
/// requires main-thread processing, marshal the callback to the main thread before touching UI/GameObjects.</para>
/// <para><b>Chunking:</b> Serial I/O does not preserve message boundaries. Each chunk may contain partial frames or
/// multiple frames. Always pass chunks to a frame codec/deframer.</para>
/// <para><b>Timeouts:</b> The read loop polls <see cref="SerialPort.BytesToRead"/> and catches <see cref="TimeoutException"/>s,
/// which are normal with short read timeouts.</para>
/// </remarks>
public sealed class SerialPortTransport : ITransport
{
    private readonly SerialPort _port;
    private CancellationTokenSource _cts;
    private Task _readLoop;

    /// <summary>
    /// Creates a serial transport bound to the given port name and parameters.
    /// </summary>
    /// <param name="portName">Port name (e.g., <c>"COM5"</c> on Windows, <c>"/dev/ttyUSB0"</c> on Linux/macOS).</param>
    /// <param name="baud">Baud rate. Default is 115200.</param>
    /// <param name="parity">Parity setting. Default is <see cref="Parity.None"/>.</param>
    /// <param name="dataBits">Data bits. Default is 8.</param>
    /// <param name="stopBits">Stop bits. Default is <see cref="StopBits.One"/>.</param>
    /// <param name="readTimeoutMs">
    /// Synchronous read timeout in milliseconds (used by <see cref="SerialPort.Read(byte[], int, int)"/>).
    /// Typical value is small (e.g., 10ms). Timeouts are caught and ignored in the read loop.
    /// </param>
    public SerialPortTransport(string portName, int baud = 115200, Parity parity = Parity.None,
                               int dataBits = 8, StopBits stopBits = StopBits.One, int readTimeoutMs = 10)
    {
        _port = new SerialPort(portName, baud, parity, dataBits, stopBits) { ReadTimeout = readTimeoutMs };
    }

    /// <summary>
    /// Creates a serial transport bound to the given parameters. Th port name is automatically selected as the firts port in the list.
    /// </summary>
    /// <param name="baud">Baud rate. Default is 115200.</param>
    /// <param name="parity">Parity setting. Default is <see cref="Parity.None"/>.</param>
    /// <param name="dataBits">Data bits. Default is 8.</param>
    /// <param name="stopBits">Stop bits. Default is <see cref="StopBits.One"/>.</param>
    /// <param name="readTimeoutMs">
    /// Synchronous read timeout in milliseconds (used by <see cref="SerialPort.Read(byte[], int, int)"/>).
    /// Typical value is small (e.g., 10ms). Timeouts are caught and ignored in the read loop.
    /// </param>
    public SerialPortTransport(int baud = 115200, Parity parity = Parity.None,
                               int dataBits = 8, StopBits stopBits = StopBits.One, int readTimeoutMs = 10)
    {
        _port = new SerialPort(GetComPort(), baud, parity, dataBits, stopBits) { ReadTimeout = readTimeoutMs };
    }

    /// <inheritdoc/>
    public bool IsConnected => _port.IsOpen;

    /// <inheritdoc/>
    /// <remarks>
    /// This event is typically raised on a background thread by the internal read loop.
    /// Consumers should copy the buffer if it needs to be retained beyond the callback.
    /// </remarks>
    public event Action<byte[]> BytesReceived;

    /// <inheritdoc/>
    /// <remarks>
    /// Opens the serial port and starts a background read loop that forwards incoming data to <see cref="BytesReceived"/>.
    /// </remarks>
    public Task ConnectAsync(CancellationToken ct = default)
    {
        _port.Open();
        _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        _readLoop = Task.Run(ReadLoopAsync, _cts.Token);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Stops the background read loop and closes the serial port.
    /// </remarks>
    public async Task DisconnectAsync(CancellationToken ct = default)
    {
        try
        {
            _cts?.Cancel();
            if (_readLoop != null)
                await _readLoop.ConfigureAwait(false); // ignore cancellations below
        }
        catch
        {
            // ignored
        }

        if (_port.IsOpen) _port.Close();
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Writes the buffer synchronously to the underlying <see cref="SerialPort"/> and flushes the base stream.
    /// </remarks>
    public Task SendAsync(byte[] data, CancellationToken ct = default)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        if (!_port.IsOpen) throw new InvalidOperationException("Transport is not connected.");

        //Log.Info("Out: "+BitConverter.ToString(data).Replace("-", " ").ToLowerInvariant());
        _port.Write(data, 0, data.Length);
        _port.BaseStream.Flush();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Background task that polls the serial port and raises <see cref="BytesReceived"/> when data arrives.
    /// </summary>
    /// <remarks>
    /// The loop polls <see cref="SerialPort.BytesToRead"/> and performs short delays (2ms) when idle
    /// to reduce CPU usage. It exits on cancellation or unexpected exceptions.
    /// </remarks>
    private async Task ReadLoopAsync()
    {
        var token = _cts.Token;
        var buf = new byte[256];

        while (!token.IsCancellationRequested)
        {
            try
            {
                if (_port.BytesToRead > 0)
                {
                    var read = _port.Read(buf, 0, buf.Length);
                    if (read > 0)
                    {
                        var chunk = new byte[read];
                        Buffer.BlockCopy(buf, 0, chunk, 0, read);
                        BytesReceived?.Invoke(chunk);
                    }
                }
                else
                {
                    await Task.Delay(2, token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) { /* normal during shutdown */ }
            catch (TimeoutException) { /* normal with short read timeouts */ }
            catch (Exception)
            {
                // TODO Consider surfacing an OnError event or logging this exception.
                break;
            }
        }
    }

    /// <summary>
    /// Disposes the serial port and stops the read loop.
    /// </summary>
    public void Dispose()
    {
        try { _cts?.Cancel(); } catch { /* ignored */ }
        _port?.Dispose();
    }


    /// <summary>
    /// Selects a serial COM port. If <paramref name="preferredPort"/> is provided,
    /// that port is used. Otherwise, prefers ports whose PNPDeviceID contains
    /// <c>VID_0403&amp;PID_6001</c> (FTDI FT232R). If none match, returns the lowest
    /// COM port by natural sort (COM2, COM10, â€¦).
    /// </summary>
    /// <param name="preferredPort">Exact port name to use, e.g., <c>COM11</c>. Optional.</param>
    /// <returns>The selected COM port name, e.g., <c>COM3</c>.</returns>
    /// <exception cref="InvalidOperationException">No serial ports found.</exception>
    /// <remarks>
    /// Uses WMI (<c>Win32_SerialPort</c>) to read <c>PNPDeviceID</c>. On .NET Core/5+,
    /// add the <c>System.Management</c> package. If the VID/PID probe fails, the method
    /// falls back to the lowest COM by natural sort.
    /// </remarks>
    private static string GetComPort(string preferredPort = null)
    {
        var ports = SerialPort.GetPortNames();
        if (ports.Length == 0)
            throw new InvalidOperationException("No serial ports found.");

        // Natural sort: COM2 < COM10
        Array.Sort(ports, (a, b) =>
        {
            int na = TryParseComNum(a, out var ia) ? ia : int.MaxValue;
            int nb = TryParseComNum(b, out var ib) ? ib : int.MaxValue;
            int cmp = na.CompareTo(nb);
            return cmp != 0 ? cmp : string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
        });

        if (!string.IsNullOrWhiteSpace(preferredPort))
        {
            var match = ports.FirstOrDefault(p => string.Equals(p, preferredPort, StringComparison.OrdinalIgnoreCase));
            if (match != null)
            {
                Log.Info($"Using preferred port: {match}");
                return match;
            }
            Log.Warn($"Preferred port '{preferredPort}' not found.");
        }

        // Try to find FTDI FT232R (VID_0403&amp;PID_6001)
        string[] ftdiMatches = Array.Empty<string>();
        /* try
        {
            using var searcher = new ManagementObjectSearcher("SELECT DeviceID,PNPDeviceID FROM Win32_SerialPort");
            var map = searcher.Get()
                .OfType<ManagementObject>()
                .Select(mo => (Dev: (string)mo["DeviceID"], Pnp: (string)mo["PNPDeviceID"]))
                .Where(t => !string.IsNullOrEmpty(t.Dev) && !string.IsNullOrEmpty(t.Pnp))
                .ToDictionary(t => t.Dev, t => t.Pnp, StringComparer.OrdinalIgnoreCase);

            ftdiMatches = ports
                .Where(p => map.TryGetValue(p, out var pnp) &&
                            pnp.IndexOf("VID_0403&amp;PID_6001", StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();
        }
        catch (Exception ex)
        {
            Log.Warn($"WMI probe failed: {ex.Message}. Skipping VID/PID filter.");
        } */

        if (ftdiMatches.Length == 1)
        {
            Log.Info($"Detected FTDI FT232R on {ftdiMatches[0]}");
            return ftdiMatches[0];
        }
        if (ftdiMatches.Length > 1)
        {
            Log.Warn($"Multiple FTDI FT232R ports: {string.Join(", ", ftdiMatches)}. Using {ftdiMatches[0]}.");
            return ftdiMatches[0];
        }

        if (ports.Length > 1)
            Log.Warn($"Multiple ports detected: {string.Join(", ", ports)}. Using {ports[0]}.");

        return ports[0];

        static bool TryParseComNum(string s, out int n)
        {
            n = 0;
            return s.StartsWith("COM", StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(s.Substring(3), out n);
        }
    }

}


}
