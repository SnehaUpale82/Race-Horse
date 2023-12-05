using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Helper;

public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    private readonly string timeZoneId = TimeZoneInfo.Local.ToString();


    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Unexpected token type {reader.TokenType}.");
        }

        string dateString = reader.GetString();
        if (DateTimeOffset.TryParse(dateString, out DateTimeOffset result))
        {
            return result;
        }
        else
        {
            throw new JsonException($"Failed to parse DateTimeOffset from '{dateString}'.");
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        DateTimeOffset convertedDateTimeOffset = TimeZoneInfo.ConvertTime(value, timeZone);

        writer.WriteStringValue(convertedDateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fffffff zzz"));
    }
}