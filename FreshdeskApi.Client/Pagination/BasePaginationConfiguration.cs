using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

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
