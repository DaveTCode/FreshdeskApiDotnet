using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public class Link
{
    [JsonProperty("href")]
    public string? Href { get; set; }
}