using System.Collections.Generic;
using System.Net.Http.Headers;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

public sealed class ListPaginationConfiguration : BasePaginationConfiguration
{
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public ListPaginationConfiguration(
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    ) : base(beforeProcessingPageAsync, processedPageAsync)
    {
    }

    public override Dictionary<string, string> BuildInitialPageParameters() => new();

    public override Dictionary<string, string>? BuildNextPageParameters<T>(int currentPage, PagedResponse<T> response)
    {
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
