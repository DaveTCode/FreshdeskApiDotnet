using FreshdeskApi.Client.Solutions.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Solutions.Requests
{
    /// <summary>
    /// Object used to define what properties to set whilst updating a
    /// solution article.
    /// 
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    public class UpdateArticleRequest
    {
        /// <summary>
        /// ID of the agent who created the solution article
        /// </summary>
        [JsonProperty("agent_id")]
        public long? AgentId { get; }

        /// <summary>
        /// Mandatory. Title of the solution article
        /// </summary>
        [JsonProperty("title")]
        public string? Title { get; }

        /// <summary>
        /// Mandatory. Description of the solution article
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; }

        /// <summary>
        /// Mandatory. The status of the article after creation
        /// </summary>
        [JsonProperty("status")]
        public ArticleStatus? Status { get; }

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

        public UpdateArticleRequest(long? agentId = null, string? title = null, string? description = null, ArticleStatus? status = null, SeoData? seoData = null, string[]? tags = null)
        {
            AgentId = agentId;
            Title = title;
            Description = description;
            Status = status;
            SeoData = seoData;
            Tags = tags;
        }

        public override string ToString()
        {
            return $"{nameof(AgentId)}: {AgentId}, {nameof(Title)}: {Title}, {nameof(Status)}: {Status}, {nameof(SeoData)}: {SeoData}, {nameof(Tags)}: {Tags}";
        }
    }
}
