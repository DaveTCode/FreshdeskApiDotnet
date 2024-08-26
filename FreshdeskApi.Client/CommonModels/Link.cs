using Newtonsoft.Json;

namespace FreshdeskApi.Client.CommonModels;

public record Link
{
    [JsonProperty("href")]
    public string? Href { get; set; }
}
