using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

/// <summary>
/// This object allows configuring of Freshdesk pagination
/// e. g. restart from given page, change page size, ....
/// </summary>
public interface IPaginationConfiguration
{
    IEnumerable<KeyValuePair<string, string>> BuildInitialPageParameters();

    Uri? BuildNextPageUri<T>(int page, PagedResponse<T> response, string initialUrl, string originalQueryString);

    PagedResponse<T> DeserializeResponse<T>(JsonTextReader reader, HttpResponseHeaders httpResponseHeaders);

    delegate Task ProcessPageDelegate(int page, Uri uri, CancellationToken? cancellationToken = default);

    /// <summary>
    /// This event is invoked right after deserialization of the page
    /// </summary>
    ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    /// <summary>
    /// This event is invoked after processing of all items on given page
    /// </summary>
    ProcessPageDelegate? ProcessedPageAsync { get; }
}
