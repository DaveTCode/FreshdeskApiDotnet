using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

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
