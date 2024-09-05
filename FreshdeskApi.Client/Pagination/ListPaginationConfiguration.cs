using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

public sealed class ListPaginationConfiguration : BasePaginationConfiguration, IListPaginationConfiguration
{
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public ListPaginationConfiguration(
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    ) : base(beforeProcessingPageAsync, processedPageAsync)
    {
    }

    public override IEnumerable<KeyValuePair<string, string>> BuildInitialPageParameters() => [];
    public override Uri? BuildNextPageUri<T>(int page, PagedResponse<T> response, string initialUrl, string originalQueryString)
    {
        // For a list pagination, the link header value contains the next page as absolute URI
        if (response.LinkHeaderValues is { } nextLinkHeaderValues)
        {
            return new Uri(nextLinkHeaderValues, UriKind.Absolute);
        }

        return null;
    }

    public override PagedResponse<T> DeserializeResponse<T>(
        JsonTextReader reader,
        HttpResponseHeaders httpResponseHeaders
    )
    {
        var serializer = new JsonSerializer();

        var response = serializer.Deserialize<List<T>>(reader);

        return new PagedResponse<T>(response ?? [], GetLinkValue(httpResponseHeaders));
    }
}
