using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.CustomObjects;
using FreshdeskApi.Client.CustomObjects.Models;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client;

public abstract class BasePaginationConfiguration(
    IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
    IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null)
    : IPaginationConfiguration
{
    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; } = beforeProcessingPageAsync;
    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; } = processedPageAsync;

    /// <summary>
    /// Note this is obviously not a full method for parsing RFC5988 link
    /// headers. I don't currently believe one is needed for the Freshdesk
    /// API.
    /// </summary>
    private static readonly Regex LinkHeaderRegex = new(@"\<(?<url>.+)?\>");

    protected string? GetLinkValue(
        HttpResponseHeaders httpResponseHeaders
    )
    {
        if (httpResponseHeaders.TryGetValues("link", out var linkHeaderValues))
        {
            var linkHeaderValue = linkHeaderValues.FirstOrDefault();
            if (linkHeaderValue == null || !LinkHeaderRegex.IsMatch(linkHeaderValue))
            {
                return null;
            }

            var nextLinkMatch = LinkHeaderRegex.Match(linkHeaderValue);
            return nextLinkMatch.Groups["url"].Value;
        }

        return null;
    }

    public abstract Dictionary<string, string> BuildInitialPageParameters();
    public abstract Dictionary<string, string>? BuildNextPageParameters<T>(int page, PagedResponse<T> response);
    public abstract PagedResponse<T> DeserializeResponse<T>(JsonTextReader reader, HttpResponseHeaders httpResponseHeaders);
}

public sealed class PageBasedPaginationConfiguration : BasePaginationConfiguration
{
    /// <param name="startingPage">Page to start from. Default 1</param>
    /// <param name="pageSize">Page size, default unspecified</param>
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public PageBasedPaginationConfiguration(
        int startingPage = 1,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    ) : base(beforeProcessingPageAsync, processedPageAsync)
    {
        StartingPage = startingPage;
        PageSize = pageSize;
    }

    public int? StartingPage { get; }

    public int? PageSize { get; }

    private Dictionary<string, string> BuildParameter(int? page)
    {
        var result = new Dictionary<string, string>();

        if (page is not null)
            result["page"] = page.Value.ToString();

        if (PageSize is not null)
            result["per_page"] = PageSize.Value.ToString();

        return result;
    }

    public override Dictionary<string, string> BuildInitialPageParameters()
        => BuildParameter(StartingPage);

    public override Dictionary<string, string>? BuildNextPageParameters<T>(int currentPage, PagedResponse<T> response)
    {
        // only returns 10 pages of data maximum because for some api calls, e.g. for getting filtered tickets,
        // To scroll through the pages you add page parameter to the url. The page number starts with 1 and should not exceed 10.
        // as can be seen here: https://developers.freshdesk.com/api/#filter_tickets
        if (response.Items.Any() && currentPage < 10)
        {
            return BuildParameter(currentPage + 1);
        }

        return null;
    }

    public override PagedResponse<T> DeserializeResponse<T>(JsonTextReader reader, HttpResponseHeaders httpResponseHeaders)
    {
        var serializer = new JsonSerializer();

        var response = serializer.Deserialize<PagedResult<T>>(reader)?.Results;

        return new PagedResponse<T>(response ?? [], GetLinkValue(httpResponseHeaders));

    }
}

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
            if(nextToken is not null)
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
