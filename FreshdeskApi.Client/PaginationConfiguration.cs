using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreshdeskApi.Client.Models;

namespace FreshdeskApi.Client;

public sealed class PageBasedPaginationConfiguration : IPaginationConfiguration
{
    /// <param name="startingPage">Page to start from. Default 1</param>
    /// <param name="pageSize">Page size, default unspecified</param>
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public PageBasedPaginationConfiguration(
        int startingPage = 1,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null,
        EPagingMode pagingMode = EPagingMode.ListStyle
    )
    {
        StartingPage = startingPage;
        PageSize = pageSize;
        BeforeProcessingPageAsync = beforeProcessingPageAsync;
        ProcessedPageAsync = processedPageAsync;
    }

    public int? StartingPage { get; }

    public int? PageSize { get; }
    
    public EPagingMode PagingMode { get; }

    private Dictionary<string, string> BuildParameter(int? page)
    {
        var result = new Dictionary<string, string>();

        if (page is not null)
            result["page"] = page.Value.ToString();

        if (PageSize is not null)
            result["per_page"] = PageSize.Value.ToString();
        
        return result;
    }

    public Dictionary<string, string> BuildInitialPageParameters()
        => BuildParameter(StartingPage);

    public Dictionary<string, string>? BuildNextPageParameters<T>(int currentPage, IReadOnlyCollection<T> newData, string link)
    {
        if (PagingMode is EPagingMode.PageContract)
        {
            // only returns 10 pages of data maximum because for some api calls, e.g. for getting filtered tickets,
            // To scroll through the pages you add page parameter to the url. The page number starts with 1 and should not exceed 10.
            // as can be seen here: https://developers.freshdesk.com/api/#filter_tickets
            if (newData.Any() && currentPage < 10)
            {
                return BuildParameter(currentPage + 1);
            }
        }

        return null;
    }

    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; }
}

public sealed class TokenBasedPaginationConfiguration : IPaginationConfiguration
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
    )
    {
        StartingToken = startingToken;
        PageSize = pageSize;
        BeforeProcessingPageAsync = beforeProcessingPageAsync;
        ProcessedPageAsync = processedPageAsync;
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

    public Dictionary<string, string> BuildInitialPageParameters()
        => BuildParameter(StartingToken);

    public Dictionary<string, string>? BuildNextPageParameters<T>(int page, IReadOnlyCollection<T> newData, string link)
    {
        string? currentToken = null;
        if (link is not null)
        {
            var queryString = HttpUtility.ParseQueryString(link);
            var nextToken = queryString["next_token"];
            if(nextToken is not null)
                return BuildParameter(nextToken);
        }

        return null;
    }

    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; }
}
