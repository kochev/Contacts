using Newtonsoft.Json;

namespace Contacts.Core.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }

        public static string ToJson(this object @object, JsonSerializerSettings settings = null)
        {
            return @object == null ? string.Empty : JsonConvert.SerializeObject(@object, settings);
        }

        public static T FromJson<T>(this string json, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}