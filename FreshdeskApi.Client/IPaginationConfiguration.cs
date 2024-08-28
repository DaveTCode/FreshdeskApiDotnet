using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client;

/// <summary>
/// This object allows configuring of Freshdesk pagination
/// e. g. restart from given page, change page size, ....
/// </summary>
public interface IPaginationConfiguration
{
    public Dictionary<string, string> BuildInitialPageParameters();

    public Dictionary<string, string>? BuildNextPageParameters<T>(int page, PagedResponse<T> response);

    public PagedResponse<T> DeserializeResponse<T>(JsonTextReader reader, HttpResponseHeaders httpResponseHeaders);

    public delegate Task ProcessPageDelegate(int page, string url, CancellationToken? cancellationToken = default);

    /// <summary>
    /// This event is invoked right after deserialization of the page
    /// </summary>
    ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    /// <summary>
    /// This event is invoked after processing of all items on given page
    /// </summary>
    ProcessPageDelegate? ProcessedPageAsync { get; }
}
