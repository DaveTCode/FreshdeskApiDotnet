using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client
{
    /// <summary>
    /// Some of the paged results from Freshdesk API are contained in a parent
    /// object with Total & Results. This is used to deserialize those objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        [JsonPropertyName("total")]
        public long? Total { get; set; }

        [JsonPropertyName("results")]
        public List<T> Results { get; set; }
    }
}
