using System.Collections.Generic;
using System.Net.Http.Headers;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

public sealed class PageBasedPaginationConfiguration : BasePaginationConfiguration, IPageBasedPaginationConfiguration
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

    public int StartingPage { get; }

    public int? PageSize { get; }

    private IEnumerable<KeyValuePair<string, string>> BuildParameter(int? page)
    {
        if (page is not null)
        {
            yield return new KeyValuePair<string, string>("page", page.Value.ToString());
        }

        if (PageSize is not null)
        {
            yield return new KeyValuePair<string, string>("per_page", PageSize.Value.ToString());
        }
    }

    public override IEnumerable<KeyValuePair<string, string>> BuildInitialPageParameters()
        => BuildParameter(StartingPage);

    public override IEnumerable<KeyValuePair<string, string>>? BuildNextPageParameters<T>(int page, PagedResponse<T> response)
    {
        // only returns 10 pages of data maximum because for some api calls, e.g. for getting filtered tickets,
        // To scroll through the pages you add page parameter to the url. The page number starts with 1 and should not exceed 10.
        // as can be seen here: https://developers.freshdesk.com/api/#filter_tickets
        if (response.Items.Count != 0 && page < 10)
        {
            return BuildParameter(page + 1);
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
