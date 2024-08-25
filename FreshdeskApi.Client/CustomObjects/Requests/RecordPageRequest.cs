using System;
using System.Collections.Generic;

namespace FreshdeskApi.Client.CustomObjects.Requests;

public class RecordPageRequest
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
    public List<RecordPageRequestFilter>? Filters { get; set; } = new List<RecordPageRequestFilter>();
    
    /// <summary>
    /// The sorting property and direction.
    /// </summary>
    public RecordPageRequestSort? Sort { get; set; }
}
