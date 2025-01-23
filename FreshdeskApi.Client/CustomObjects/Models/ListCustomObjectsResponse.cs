using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public record ListCustomObjectsResponse
{
    /// <summary>
    /// </summary>
    [JsonProperty("schemas")]
    public IReadOnlyCollection<CustomObject>? Schemas { get; set; }
}
