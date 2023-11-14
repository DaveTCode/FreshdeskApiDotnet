using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CommonModels;

/// <summary>
/// Some of the paged results from Freshdesk API are contained in a parent
/// object with Total & Results. This is used to deserialize those objects
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResult<T>
{
    [JsonProperty("total")]
    public long? Total { get; set; }

    [JsonProperty("results")]
    public List<T>? Results { get; set; }
}
