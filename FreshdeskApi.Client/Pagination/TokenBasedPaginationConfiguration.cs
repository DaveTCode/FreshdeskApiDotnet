using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web;
using FreshdeskApi.Client.CustomObjects;
using FreshdeskApi.Client.CustomObjects.Models;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

public sealed class TokenBasedPaginationConfiguration : BasePaginationConfiguration
{
    /// <param name="startingToken">Token to start from</param>
    /// <param name="pageSize">Page size, default unspecified</param>
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public TokenBasedPaginationConfiguration(
        string? startingToken = null,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    ) : base(beforeProcessingPageAsync, processedPageAsync)
    {
        StartingToken = startingToken;
        PageSize = pageSize;
    }

    public string? StartingToken { get; }

    public int? PageSize { get; }

    private Dictionary<string, string> BuildParameter(string? token)
    {
        var result = new Dictionary<string, string>();

        if (token is not null)
            result["next_token"] = token;

        if (PageSize is not null)
            result["page_size"] = PageSize.Value.ToString();

        return result;
    }

    public override Dictionary<string, string> BuildInitialPageParameters()
        => BuildParameter(StartingToken);

    public override Dictionary<string, string>? BuildNextPageParameters<T>(int page, PagedResponse<T> response)
    {
        string? currentToken = null;
        if (response.LinkHeaderValues is not null)
        {
            var queryString = HttpUtility.ParseQueryString(response.LinkHeaderValues);
            var nextToken = queryString["next_token"];
            if (nextToken is not null)
                return BuildParameter(nextToken);
        }

        return null;
    }

    public override PagedResponse<T> DeserializeResponse<T>(JsonTextReader reader, HttpResponseHeaders httpResponseHeaders)
    {
        var serializer = new JsonSerializer();

        var recordPage = serializer.Deserialize<RecordPage<T>>(reader);

        string? link;
        if (recordPage?.Links?.Next?.Href is { } nextHref)
        {
            link = $"{IFreshdeskCustomObjectClient.UrlPrefix}{nextHref}";
        }
        else
        {
            link = GetLinkValue(httpResponseHeaders);
        }

        return new PagedResponse<T>(recordPage?.Records ?? [], link);
    }
}
