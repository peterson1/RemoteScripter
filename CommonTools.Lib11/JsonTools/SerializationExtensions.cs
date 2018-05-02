using CommonTools.Lib11.StringTools;
using Newtonsoft.Json;
using System;

namespace CommonTools.Lib11.JsonTools
{
    public static class SerializationExtensions
    {
        public static T ReadJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }


        public static string ToJson(this object obj, bool indented = false)
            => JsonConvert.SerializeObject(obj, 
                indented ? Formatting.Indented : Formatting.None);


        public static string SHA1OfJson(this object obj)
            => obj.ToJson().SHA1ForUTF8();
    }
}
