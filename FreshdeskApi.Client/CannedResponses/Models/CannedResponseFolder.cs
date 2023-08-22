using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CannedResponses.Models
{
    /// <summary>
    ///
    ///
    /// c.f. https://developers.freshdesk.com/api/#canned_response_folder_attributes
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class CannedResponseFolder
    {
        /// <summary>
        /// Unique ID of the canned response folder
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Name of the canned response folder
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Set true if the folder can be accessed only by you.
        /// </summary>
        [JsonProperty("personal")]
        public bool? Personal { get; set; }

        /// <summary>
        /// canned response Folder creation timestamp
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// canned response Folder updated timestamp
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Number of canned responses in the folder
        /// </summary>
        [JsonProperty("responses_count")]
        public long? ResponsesCount { get; set; }

        /// <summary>
        /// Canned responses in the folder
        /// </summary>
        [JsonProperty("canned_responses")]
        public CannedResponse[]? CannedResponses { get; set; }


    }

}

