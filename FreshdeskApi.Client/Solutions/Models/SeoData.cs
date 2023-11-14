using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Solutions.Models;

/// <summary>
/// Provides SEO metadata about an article in the KB
///
/// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class SeoData
{
    [JsonProperty("meta_title")]
    public string? MetaTitle { get; set; }

    [JsonProperty("meta_description")]
    public string? MetaDescription { get; set; }

    [JsonProperty("meta_keywords")]
    public string? MetaKeywords { get; set; }

    public override string ToString()
    {
        return $"{nameof(MetaTitle)}: {MetaTitle}, {nameof(MetaDescription)}: {MetaDescription}, {nameof(MetaKeywords)}: {MetaKeywords}";
    }
}
