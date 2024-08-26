using Newtonsoft.Json;

namespace FreshdeskApi.Client.CommonModels;

public record PageLinks
{
    [JsonProperty("next")]
    public Link? Next { get; set; }

    [JsonProperty("prev")]
    public Link? Prev { get; set; }

    [JsonProperty("count")]
    public Link? Count { get; set; }
}
