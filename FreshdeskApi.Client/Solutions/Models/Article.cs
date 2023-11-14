using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Solutions.Models;

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
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Unknown - not documented here: https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("type")]
    public long Type { get; set; }

    /// <summary>
    /// Status of the solution article
    /// </summary>
    [JsonProperty("status")]
    public ArticleStatus Status { get; set; }

    /// <summary>
    /// ID of the agent who created the solution article
    /// </summary>
    [JsonProperty("agent_id")]
    public long AgentId { get; set; }

    /// <summary>
    /// Solution Article creation timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// ID of the category to which the solution article belongs
    /// </summary>
    [JsonProperty("category_id")]
    public long CategoryId { get; set; }

    /// <summary>
    /// ID of the folder to which the solution article belongs
    /// </summary>
    [JsonProperty("folder_id")]
    public long FolderId { get; set; }

    /// <summary>
    /// Undocumented entry on the API which is not always filled in
    ///
    /// Represents the visibility of the containing folder
    /// </summary>
    [JsonProperty("folder_visibility")]
    public FolderVisibility? FolderVisibility { get; set; }

    /// <summary>
    /// Title of the solution article
    /// </summary>
    [JsonProperty("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Solution Article updated timestamp
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Description of the solution article
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Description of the solution article in plain text
    /// </summary>
    [JsonProperty("description_text")]
    public string? DescriptionText { get; set; }

    /// <summary>
    /// Meta data for search engine optimization. Allows meta_title,
    /// meta_description and meta_keywords
    /// </summary>
    [JsonProperty("seo_data")]
    public SeoData? SeoData { get; set; }

    /// <summary>
    /// Tags that have been associated with the solution article
    /// </summary>
    [JsonProperty("tags")]
    public string[]? Tags { get; set; }

    /// <summary>
    /// Platforms that have been associated with the solution folder.
    /// 
    /// NOTE: This is property is read only and not documented.
    /// </summary>
    [JsonProperty("platforms")]
    public Platform[]? Platforms { get; set; }

    /// <summary>
    /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("attachments")]
    public Attachment[]? Attachments { get; set; }

    /// <summary>
    /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("cloud_files")]
    public object[]? CloudFiles { get; set; }

    /// <summary>
    /// Number of upvotes for the solution article
    /// </summary>
    [JsonProperty("thumbs_up")]
    public long ThumbsUp { get; set; }

    /// <summary>
    /// Number of down votes for the solution article
    /// </summary>
    [JsonProperty("thumbs_down")]
    public long ThumbsDown { get; set; }

    /// <summary>
    /// Number of views for the solution article
    /// </summary>
    [JsonProperty("hits")]
    public long Hits { get; set; }

    /// <summary>
    /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("suggested")]
    public long Suggested { get; set; }

    /// <summary>
    /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("feedback_count")]
    public long FeedbackCount { get; set; }

    /// <summary>
    /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("folder_name")]
    public string? FolderName { get; set; }

    /// <summary>
    /// Undocumented field on https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [JsonProperty("category_name")]
    public string? CategoryName { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Type)}: {Type}, {nameof(Status)}: {Status}, {nameof(AgentId)}: {AgentId}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(CategoryId)}: {CategoryId}, {nameof(FolderId)}: {FolderId}, {nameof(Title)}: {Title}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(SeoData)}: {SeoData}, {nameof(Tags)}: {Tags}, {nameof(Attachments)}: {Attachments}, {nameof(CloudFiles)}: {CloudFiles}, {nameof(ThumbsUp)}: {ThumbsUp}, {nameof(ThumbsDown)}: {ThumbsDown}, {nameof(Hits)}: {Hits}, {nameof(Suggested)}: {Suggested}, {nameof(FeedbackCount)}: {FeedbackCount}, {nameof(FolderName)}: {FolderName}, {nameof(CategoryName)}: {CategoryName}";
    }
}
