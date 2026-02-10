// Minimal UnityEngine stubs for DocFX compilation only
namespace UnityEngine
{
    public class MonoBehaviour { }

    public sealed class SerializeFieldAttribute : System.Attribute { }

    public enum KeyCode { A = 65 }

    public static class Input
    {
        public static bool GetKeyDown(KeyCode key) => false;
    }

    public static class Application
    {
        public static string streamingAssetsPath => string.Empty;
    }
}

