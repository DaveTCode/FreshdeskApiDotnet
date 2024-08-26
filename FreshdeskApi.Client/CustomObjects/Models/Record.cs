using System;
using System.Collections.Generic;
using FreshdeskApi.Client.JsonConverters;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public record Record<T>
{
    [JsonProperty("display_id")]
    public string? DisplayId { get; set; }

    [JsonProperty("created_time")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime CreatedTime { get; set; }

    [JsonProperty("updated_time")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime UpdatedTime { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("data")]
    public T? Data { get; set; }

    [JsonProperty("metadata")]
    public IReadOnlyDictionary<string, string>? Metadata { get; set; }
}
