using System.Collections.Generic;
using System.Web;
using FreshdeskApi.Client.Attributes;
using FreshdeskApi.Client.CustomObjects.Requests.Parameters;

namespace FreshdeskApi.Client.CustomObjects.Requests;

[IgnoreJsonValidation]
public record ListAllRecordsRequest
{
    /// <summary>
    /// A list of filter to apply to the request.
    /// Will be combined with an 'AND' operator
    /// </summary>
    public IReadOnlyCollection<RecordPageRequestParameterFilter>? Filters { get; init; }

    /// <summary>
    /// The sorting property and direction.
    /// </summary>
    public RecordPageRequestParameterSort? Sort { get; init; }

    public string? GetQuery()
    {
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        foreach (var filter in Filters ?? [])
        {
            queryString.Add(filter.QueryStringParameterName, filter.Value);
        }

        if (Sort is not null)
        {
            queryString.Add(Sort.QueryStringParameterName, Sort.QueryStringParameterValue);
        }

        if (queryString.Count is 0)
        {
            return null;
        }

        return $"?{queryString}";
    }
}
