using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

public class RecordPage<T>
{
    [JsonProperty("records")]
    public List<Record<T>>? Records { get; set; }

    [JsonProperty("_links")]
    public RecordPageLinks? Links { get; set; }
}

public class RecordPageLinks
{
    [JsonProperty("next")]
    public Link? Next { get; set; }

    [JsonProperty("prev")]
    public Link? Prev { get; set; }

    [JsonProperty("count")]
    public Link? Count { get; set; }
}

public class Link
{
    [JsonProperty("href")]
    public string? Href { get; set; }
}

public class RecordCount
{
    [JsonProperty("count")]
    public int Count { get; set; }
}
