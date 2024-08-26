using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public class RecordCount
{
    [JsonProperty("count")]
    public int Count { get; set; }
}