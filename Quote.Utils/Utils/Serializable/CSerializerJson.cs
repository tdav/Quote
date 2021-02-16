using Newtonsoft.Json;

namespace QuoteServer.Utils.Serializable
{
    public class CSerializerJson
    {
        public static string ToStr(object value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                PreserveReferencesHandling = PreserveReferencesHandling.Arrays
                            });
        }

        public static T ToObj<T>(string value) where T : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
    }
}
