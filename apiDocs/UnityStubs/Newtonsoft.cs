// Minimal Newtonsoft.Json stubs for DocFX compilation only
namespace Newtonsoft.Json
{
    public static class JsonConvert
    {
        public static T? DeserializeObject<T>(string value) => default;
        public static string SerializeObject(object? value) => string.Empty;
        public static string SerializeObject(object? value, Formatting formatting) => string.Empty;
    }

    public enum Formatting { None, Indented }
}

namespace Newtonsoft.Json.Linq
{
    using System.Collections;
    using System.Collections.Generic;

    public enum JTokenType { None, Object, Array, Integer, Float, String, Boolean, Null }

    public class JToken
    {
        public JTokenType Type { get; set; } = JTokenType.None;
        public static JToken FromObject(object? value) => new JValue { RawValue = value };
        public static implicit operator JToken(int v) => new JValue { RawValue = v };
        public static implicit operator JToken(double v) => new JValue { RawValue = v };
        public static implicit operator JToken(float v) => new JValue { RawValue = v };
        public static implicit operator JToken(string? v) => new JValue { RawValue = v };
        public static implicit operator JToken(bool v) => new JValue { RawValue = v };
    }

    public class JObject : JToken, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable
    {
        public static JObject Parse(string json) => new JObject();
        public T? Value<T>(string property) => default;
        public bool TryGetValue(string propertyName, out JToken value)
        {
            value = new JToken();
            return false;
        }
        public JToken? SelectToken(string path) => null;
        public JToken this[string key]
        {
            get => new JToken();
            set { }
        }
        public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
        {
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class JValue : JToken
    {
        public object? RawValue { get; set; }
        public T? Value<T>() => default;
    }

    public static class JObjectExtensions
    {
        public static T? ToObject<T>(this JToken token) => default;
        public static T? Value<T>(this JToken token) => default;
    }

    public class JArray : JToken, IEnumerable<JToken>, System.Collections.IEnumerable
    {
        public IEnumerator<JToken> GetEnumerator()
        {
            yield break;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
