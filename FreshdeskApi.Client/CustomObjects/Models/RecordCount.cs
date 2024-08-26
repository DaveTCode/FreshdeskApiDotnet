using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public record RecordCount
{
    [JsonProperty("count")]
    public int Count { get; set; }
}
