using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Solutions.Models
{
    /// <summary>
    /// Undocumented attachment information for solution articles.
    ///
    /// c.f. https://developers.freshdesk.com/api/#solution_article_attributes
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Attachment
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("attachment_url")]
        public Uri AttachmentUrl { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(ContentType)}: {ContentType}, {nameof(Size)}: {Size}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(AttachmentUrl)}: {AttachmentUrl}";
        }
    }
}
