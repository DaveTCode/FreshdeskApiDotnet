using FreshdeskApi.Client.Solutions.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Solutions.Requests
{
    /// <summary>
    /// Object used to define what properties to set whilst creating a new
    /// solution article.
    /// 
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    public class CreateArticleRequest
    {
        /// <summary>
        /// Mandatory. Title of the solution article
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; }

        /// <summary>
        /// Mandatory. Description of the solution article
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; }

        /// <summary>
        /// Mandatory. The status of the article after creation
        /// </summary>
        [JsonProperty("status")]
        public ArticleStatus Status { get; }

        /// <summary>
        /// Optional. Meta data for search engine optimization. Allows
        /// meta_title, meta_description and meta_keywords
        /// </summary>
        [JsonProperty("seo_data")]
        public SeoData? SeoData { get; }

        /// <summary>
        /// Tags that have been associated with the solution article
        /// </summary>
        [JsonProperty("tags")]
        public string[]? Tags { get; }

        public CreateArticleRequest(string title, string description, ArticleStatus status, SeoData? seoData = null, string[]? tags = null)
        {
            Title = title;
            Description = description;
            Status = status;
            SeoData = seoData;
            Tags = tags;
        }

        public override string ToString()
        {
            return $"{nameof(Title)}: {Title}, {nameof(Status)}: {Status}, {nameof(SeoData)}: {SeoData}, {nameof(Tags)}: {Tags}";
        }
    }
}
