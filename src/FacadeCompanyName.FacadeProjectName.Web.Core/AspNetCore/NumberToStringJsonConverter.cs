using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.AspNetCore
{
    public class NumberToStringJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    {
                        return reader.TryGetInt64(out var l)
                            ? l.ToString()
                            : reader.GetDecimal().ToString("G29");
                    }
                // case JsonTokenType.True:
                //     return "true";
                // case JsonTokenType.False:
                //     return "false";
                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.String:
                default:
                    return reader.GetString();
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }

}