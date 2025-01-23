using System.Collections.Generic;
using FreshdeskApi.Client.CommonModels;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public record RecordPage<T>
{
    [JsonProperty("records")]
    public IReadOnlyCollection<T>? Records { get; set; }

    [JsonProperty("_links")]
    public PageLinks? Links { get; set; }
}
