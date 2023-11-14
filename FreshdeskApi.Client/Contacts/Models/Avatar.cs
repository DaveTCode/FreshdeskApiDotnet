using System;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models;

/// <summary>
/// A contact may have an avatar which contains some metadata and a URL
/// which the image is hosted at.
/// </summary>
public class Avatar
{
    [JsonProperty("avatar_url")]
    public string? Url { get; set; }

    [JsonProperty("content_type")]
    public string? ContentType { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    public override string ToString()
    {
            return $"{nameof(Url)}: {Url}, {nameof(ContentType)}: {ContentType}, {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Size)}: {Size}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}";
        }
}
