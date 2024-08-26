using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public class RecordPage<T>
{
    [JsonProperty("records")]
    public IReadOnlyCollection<T>? Records { get; set; }

    [JsonProperty("_links")]
    public RecordPageLinks? Links { get; set; }
}
