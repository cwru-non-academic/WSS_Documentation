using System;
using System.Diagnostics;
using System.Threading;
using Wss.CoreModule;

public static class DirectCoreLifecycle
{
    // The caller provides a configured Core/layer stack and owns final Dispose().
    // Use only for direct composition: C#/Python controllers already own ticking.
    public static void InitializeAndObserve(IStimulationCore core, TimeSpan timeout)
    {
        if (core == null) throw new ArgumentNullException(nameof(core));
        if (timeout <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(timeout));
        try
        {
            core.Initialize();
            var watch = Stopwatch.StartNew();
            // NormalSetup can advance through Ready into Started/Streaming.
            while (!core.Ready() && !core.Started())
            {
                if (watch.Elapsed >= timeout) throw new TimeoutException("WSS setup did not complete.");
                core.Tick();
                Thread.Sleep(5);
            }
        }
        finally
        {
            core.Shutdown(); // Retains the transport; terminal disposal belongs to the caller.
        }
    }
}
