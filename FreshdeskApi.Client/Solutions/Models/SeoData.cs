using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Solutions.Models
{
    /// <summary>
    /// Provides SEO metadata about an article in the KB
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class SeoData
    {
        [JsonPropertyName("meta_title")]
        public string MetaTitle { get; set; }

        [JsonPropertyName("meta_description")]
        public string MetaDescription { get; set; }

        [JsonPropertyName("meta_keywords")]
        public string MetaKeywords { get; set; }

        public override string ToString()
        {
            return $"{nameof(MetaTitle)}: {MetaTitle}, {nameof(MetaDescription)}: {MetaDescription}, {nameof(MetaKeywords)}: {MetaKeywords}";
        }
    }
}
