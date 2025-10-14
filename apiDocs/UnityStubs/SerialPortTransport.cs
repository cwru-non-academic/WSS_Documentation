using System;
using System.Threading;
using System.Threading.Tasks;

// Minimal stub to satisfy references during DocFX build only.
public class SerialPortTransport : ITransport
{
    public event Action<byte[]> BytesReceived;
    public bool IsConnected { get; private set; }

    public SerialPortTransport() { }
    public SerialPortTransport(string portName) { }

    public Task ConnectAsync(CancellationToken ct = default)
    {
        IsConnected = true;
        return Task.CompletedTask;
    }

    public Task DisconnectAsync(CancellationToken ct = default)
    {
        IsConnected = false;
        return Task.CompletedTask;
    }

    public Task SendAsync(byte[] data, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public void Dispose() { }
}

