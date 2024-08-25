using System.Collections.Generic;

namespace FreshdeskApi.Client.CustomObjects.RequestParameters;

public class RecordPageRequestParameter
{
    /// <summary>
    /// The number of record to retrive in the request
    /// API default if null = 20
    /// Maximum 100
    /// </summary>
    public int? PageSize { get; set; }

    /// <summary>
    /// A list of filter to apply to the request.
    /// Will be combined with an 'AND' operator
    /// </summary>
    public List<RecordPageRequestParameterFilter>? Filters { get; set; } = new List<RecordPageRequestParameterFilter>();

    /// <summary>
    /// The sorting property and direction.
    /// </summary>
    public RecordPageRequestParameterSort? Sort { get; set; }
}
