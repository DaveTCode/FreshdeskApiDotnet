using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Solutions.Models
{
    /// <summary>
    /// Refers to a single article within the KB
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Article
    {
        /// <summary>
        /// Unique ID of the solution article
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Unknown - not documented here: https://developers.freshdesk.com/api/#solution_article_attributes
        /// </summary>
        [JsonPropertyName("type")]
        public long Type { get; set; }

        /// <summary>
        /// Status of the solution article
        /// </summary>
        [JsonPropertyName("status")]
        public ArticleStatus Status { get; set; }

        /// <summary>
        /// ID of the agent who created the solution article
        /// </summary>
        [JsonPropertyName("agent_id")]
        public long AgentId { get; set; }

        /// <summary>
        /// Solution Article creation timestamp
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// ID of the category to which the solution article belongs
        /// </summary>
        [JsonPropertyName("category_id")]
        public long CategoryId { get; set; }

        /// <summary>
        /// ID of the folder to which the solution article belongs
        /// </summary>
        [JsonPropertyName("folder_id")]
        public long FolderId { get; set; }

        /// <summary>
        /// Title of the solution article
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Solution Article updated timestamp
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Description of the solution article
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Description of the solution article in plain text
        /// </summary>
        [JsonPropertyName("description_text")]
        public string DescriptionText { get; set; }

        /// <summary>
        /// Meta data for search engine optimization. Allows meta_title,
        /// meta_description and meta_keywords
        /// </summary>
        [JsonPropertyName("seo_data")]
        public SeoData SeoData { get; set; }

        /// <summary>
        /// Tags that have been associated with the solution article
        /// </summary>
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
        /// </summary>
        [JsonPropertyName("attachments")]
        public Attachment[] Attachments { get; set; }

        /// <summary>
        /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
        /// </summary>
        [JsonPropertyName("cloud_files")]
        public object[] CloudFiles { get; set; }

        /// <summary>
        /// Number of upvotes for the solution article
        /// </summary>
        [JsonPropertyName("thumbs_up")]
        public long ThumbsUp { get; set; }

        /// <summary>
        /// Number of down votes for the solution article
        /// </summary>
        [JsonPropertyName("thumbs_down")]
        public long ThumbsDown { get; set; }

        /// <summary>
        /// Number of views for the solution article
        /// </summary>
        [JsonPropertyName("hits")]
        public long Hits { get; set; }

        /// <summary>
        /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
        /// </summary>
        [JsonPropertyName("suggested")]
        public long Suggested { get; set; }

        /// <summary>
        /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
        /// </summary>
        [JsonPropertyName("feedback_count")]
        public long FeedbackCount { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Type)}: {Type}, {nameof(Status)}: {Status}, {nameof(AgentId)}: {AgentId}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(CategoryId)}: {CategoryId}, {nameof(FolderId)}: {FolderId}, {nameof(Title)}: {Title}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(SeoData)}: {SeoData}, {nameof(Tags)}: {Tags}, {nameof(Attachments)}: {Attachments}, {nameof(CloudFiles)}: {CloudFiles}, {nameof(ThumbsUp)}: {ThumbsUp}, {nameof(ThumbsDown)}: {ThumbsDown}, {nameof(Hits)}: {Hits}, {nameof(Suggested)}: {Suggested}, {nameof(FeedbackCount)}: {FeedbackCount}";
        }
    }
}
