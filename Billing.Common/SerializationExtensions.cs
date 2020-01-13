using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Billing.Common
{
    public static class SerializationExtensions
    {
        public static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Include,
            DateParseHandling = DateParseHandling.None,
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string Serialize<T>(this T obj, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, settings ?? DefaultSettings);
        }

        public static T Deserialize<T>(this string obj, JsonSerializerSettings settings = null) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj, settings ?? DefaultSettings);
        }
    }
}
