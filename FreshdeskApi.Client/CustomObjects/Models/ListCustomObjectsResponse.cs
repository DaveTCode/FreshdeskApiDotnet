using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public class ListCustomObjectsResponse
{
    /// <summary>
    /// </summary>
    [JsonProperty("schemas")]
    public CustomObject[]? Schemas { get; set; }

}
