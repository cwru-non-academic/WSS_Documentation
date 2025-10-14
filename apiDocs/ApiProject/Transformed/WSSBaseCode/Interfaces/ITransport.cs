namespace WSSInterfacing {
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Abstraction over a byte-stream transport (e.g., Serial, Bluetooth, TCP) used by the WSS client.
/// Implementations should surface incoming bytes via <see cref="BytesReceived"/> and provide
/// async connect/disconnect/send operations.
/// </summary>
/// <remarks>
/// <para>
/// This interface is intentionally minimal and transport-agnostic so the higher-level WSS client
/// can be reused with different physical links (UART/COM, RFCOMM, BLE GATT, sockets).
/// </para>
/// <para>
/// <b>Threading:</b> Implementations typically raise <see cref="BytesReceived"/> from a background thread.
/// If your application (e.g., Unity) requires main-thread access for processing, marshal the callback
/// to the main thread before touching UI/GameObjects.
/// </para>
/// <para>
/// <b>Chunking:</b> Incoming chunks may contain partial frames, multiple frames, or arbitrary boundaries.
/// Always pass chunks to a frame codec/deframer instead of assuming message boundaries align with reads.
/// </para>
/// </remarks>
public interface ITransport : IDisposable
{
    /// <summary>
    /// Gets whether the transport is currently connected/open.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Raised whenever raw bytes arrive from the underlying transport.
    /// Each invocation may represent any number of bytes (including zero, depending on implementation),
    /// and may contain partial or multiple protocol frames.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implementations should invoke this event promptly as data becomes available.
    /// Callers should treat the provided buffer as read-only and copy it if they need to retain it.
    /// </para>
    /// <para>
    /// This event is typically raised on a background thread. Consumers should marshal to the main thread
    /// if required by their environment.
    /// </para>
    /// </remarks>
    event Action<byte[]> BytesReceived;

    /// <summary>
    /// Opens/initializes the transport connection (e.g., open a serial port, connect a socket, start BLE session).
    /// </summary>
    /// <param name="ct">Optional cancellation token to abort the connect attempt.</param>
    /// <returns>A task that completes when the transport is connected.</returns>
    /// <exception cref="ObjectDisposedException">If the instance has been disposed.</exception>
    /// <exception cref="OperationCanceledException">If <paramref name="ct"/> is canceled.</exception>
    /// <exception cref="Exception">
    /// Implementations may throw transport-specific exceptions (e.g., unauthorized port access, device not found).
    /// </exception>
    Task ConnectAsync(CancellationToken ct = default);

    /// <summary>
    /// Closes the transport connection and stops any background receive loop.
    /// </summary>
    /// <param name="ct">Optional cancellation token to abort a long-running disconnect.</param>
    /// <returns>A task that completes when the transport is fully disconnected.</returns>
    /// <exception cref="ObjectDisposedException">If the instance has been disposed.</exception>
    /// <exception cref="OperationCanceledException">If <paramref name="ct"/> is canceled.</exception>
    Task DisconnectAsync(CancellationToken ct = default);

    /// <summary>
    /// Sends a block of bytes over the transport.
    /// </summary>
    /// <param name="data">The bytes to write. Callers should provide a buffer that will not be mutated during send.</param>
    /// <param name="ct">
    /// Optional cancellation token to cancel the send before it is written.
    /// For some transports (e.g., synchronous serial writes), cancellation may only be observed before the write starts.
    /// </param>
    /// <returns>A task that completes when the bytes have been handed off to the transport.</returns>
    /// <exception cref="ObjectDisposedException">If the instance has been disposed.</exception>
    /// <exception cref="InvalidOperationException">If the transport is not connected.</exception>
    /// <exception cref="OperationCanceledException">If <paramref name="ct"/> is canceled.</exception>
    Task SendAsync(byte[] data, CancellationToken ct = default);
}

}
