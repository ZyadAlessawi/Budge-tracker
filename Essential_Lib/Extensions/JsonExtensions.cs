using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Essential_Lib.Extensions
{
    public static class JsonExtensions
    {
        public static string? Serialize<T>(this T? DeserializedObject, JsonSerializerOptions? serializerOptions = null)
        {
            if (DeserializedObject == null)
                return null;
            try
            {
                return JsonSerializer.Serialize<T>(DeserializedObject, serializerOptions);
            }
            catch (Exception)
            {
                //ex.DisplayExecptionAlert();
                return default;
            }
        }
        public static T? Deserialize<T>(this string? SerializedString, JsonSerializerOptions? serializerOptions = null)
        {
            if (string.IsNullOrWhiteSpace(SerializedString))
                return default;
            try
            {
                return JsonSerializer.Deserialize<T>(SerializedString, serializerOptions);
            }
            catch (Exception ex)
            {
                //ex.DisplayExecptionAlert(ExtraInfo: SerializedString);
                return default;
            }
        }
    }
}
