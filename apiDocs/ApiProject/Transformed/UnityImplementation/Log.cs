namespace WSSInterfacing {
#if UNITY_2019_1_OR_NEWER
using Debug = UnityEngine.Debug;
#endif
using System;

/// <summary>
/// Minimal logging facade that writes to Unity's console when running inside
/// Unity, and falls back to System.Console on nonâ€‘Unity environments.
/// </summary>
public static class Log
{
    /// <summary>
    /// Logs an informational message.
    /// </summary>
    public static void Info(string msg)
    {
#if UNITY_2019_1_OR_NEWER
        Debug.Log(msg);
#else
        Console.WriteLine(msg);
#endif
    }

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    public static void Warn(string msg)
    {
#if UNITY_2019_1_OR_NEWER
        Debug.LogWarning(msg);
#else
        Console.WriteLine("[WARN] " + msg);
#endif
    }

    /// <summary>
    /// Logs an error message, optionally including an exception's details.
    /// </summary>
    /// <param name="msg">Message to log.</param>
    /// <param name="ex">Optional exception to append.</param>
    public static void Error(string msg, Exception ex = null)
    {
#if UNITY_2019_1_OR_NEWER
        Debug.LogError(ex == null ? msg : $"{msg}\n{ex}");
#else
        Console.Error.WriteLine(ex == null ? msg : $"{msg}\n{ex}");
#endif
    }
}

}
