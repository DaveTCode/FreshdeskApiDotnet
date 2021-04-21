using System;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CommonModels
{
    /// <summary>
    /// Defines the Attachment field set of properties returned
    /// e.g. from ticket response with Attachment
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_ticket
    /// </summary>
    public class AttachmentResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; } = null!;

        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("attachment_url")]
        public string AttachmentUrl { get; set; } = null!;

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
        
        public override string ToString()
        {
            return $"{nameof(AttachmentUrl)}: {AttachmentUrl}, {nameof(ContentType)}: {ContentType}, {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(FileSize)}: {FileSize}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}";
        }
    }
}
