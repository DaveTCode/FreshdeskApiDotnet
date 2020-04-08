using System;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Contacts.Models
{
    /// <summary>
    /// A contact may have an avatar which contains some metadata and a URL
    /// which the image is hosted at.
    /// </summary>
    public class Avatar
    {
        [JsonPropertyName("avatar_url")]
        public string Url { get; set; }

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{nameof(Url)}: {Url}, {nameof(ContentType)}: {ContentType}, {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Size)}: {Size}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}";
        }
    }
}
