using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FreshdeskApi.Client.CommonModels;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CannedResponses.Models
{
    /// <summary>
    ///
    ///
    /// c.f. https://developers.freshdesk.com/api/#canned-responses
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class CannedResponse
    {
        /// <summary>
        /// Unique ID of the canned response
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Title of the canned response
        /// </summary>
        [JsonProperty("title")]
        public string? Title { get; set; }

        /// <summary>
        /// HTML version of the canned response content
        /// </summary>
        [JsonProperty("content_html")]
        public string? ContentHtml { get; set; }

        /// <summary>
        /// Plaintext version of the canned response content
        /// </summary>
        [JsonProperty("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Folder where the canned response is located 
        /// </summary>
        [JsonProperty("folder_id")]
        public long? FolderId { get; set; }

        /// <summary>
        /// Denotes the visibility of the canned response.
        ///
        /// <seealso cref="CannedResponseVisibility"/>
        /// </summary>
        [JsonProperty("visibility")]
        public CannedResponseVisibility? Visibility { get; set; }

        /// <summary>
        /// Groups for which the canned response is visible. Use only if visibility is set to 2. 
        /// </summary>
        [JsonProperty("group_ids")]
        public long[]? GroupIds { get; set; }

        /// <summary>
        /// Attachments associated with the canned response.
        /// </summary>
        [JsonProperty("attachments")]
        public AttachmentResponse[]? Attachments { get; set; }

        /// <summary>
        /// canned response creation timestamp
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// canned response updated timestamp
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }


    }
    public enum CannedResponseVisibility
    {
        AllAgents = 0,     
        Personal = 1,     
        SelectedGroups = 2,
    }
}
