using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using FreshdeskApi.Client.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Pagination;

[SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
public abstract partial class BasePaginationConfiguration(
    IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
    IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
) : IPaginationConfiguration
{
    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; } = beforeProcessingPageAsync;
    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; } = processedPageAsync;

    /// <summary>
    /// Note this is obviously not a full method for parsing RFC5988 link
    /// headers. I don't currently believe one is needed for the Freshdesk
    /// API.
    /// </summary>
#if NET
    [GeneratedRegex(@"\<(?<url>.+)?\>")]
    private static partial Regex LinkHeaderRegex();
#else
    private static readonly Regex LinkHeaderRegexInstance = new(@"\<(?<url>.+)?\>");

    private static Regex LinkHeaderRegex() => LinkHeaderRegexInstance;
#endif

    protected string? GetLinkValue(
        HttpResponseHeaders httpResponseHeaders
    )
    {
        if (httpResponseHeaders.TryGetValues("link", out var linkHeaderValues))
        {
            var linkHeaderRegex = LinkHeaderRegex();
            var linkHeaderValue = linkHeaderValues.FirstOrDefault();
            if (linkHeaderValue == null || !linkHeaderRegex.IsMatch(linkHeaderValue))
            {
                return null;
            }

            var nextLinkMatch = linkHeaderRegex.Match(linkHeaderValue);
            return nextLinkMatch.Groups["url"].Value;
        }

        return null;
    }

    public abstract IEnumerable<KeyValuePair<string, string>> BuildInitialPageParameters();

    public abstract Uri? BuildNextPageUri<T>(int page, PagedResponse<T> response, string initialUrl, string originalQueryString);


    public abstract PagedResponse<T> DeserializeResponse<T>(JsonTextReader reader, HttpResponseHeaders httpResponseHeaders);
}
