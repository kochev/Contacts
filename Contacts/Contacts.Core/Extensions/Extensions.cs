using System;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace Contacts.Core.Extensions
{
    public static class Extensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var genericEnumType = value.GetType();
            var memberInfo = genericEnumType.GetMember(value.ToString());
            if (memberInfo.Any())
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes?.Any() ?? false) return ((DescriptionAttribute) attributes.ElementAt(0)).Description;
            }

            return value.ToString();
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
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