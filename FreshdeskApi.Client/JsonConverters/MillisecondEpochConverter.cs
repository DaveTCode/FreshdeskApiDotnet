using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FreshdeskApi.Client.JsonConverters;

public class MillisecondEpochConverter : DateTimeConverterBase
{
    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);



    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is null) writer.WriteNull();
        else writer.WriteRawValue(((DateTime)value - Epoch).TotalMilliseconds.ToString("F0"));
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null) { return null; }
        return Epoch.AddMilliseconds((long)reader.Value);
    }
}
