using System;
using FreshdeskApi.Client.Conversations.Requests;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Channel.Requests;

public class ChannelCreateNoteRequest : CreateNoteRequest
{
    [JsonProperty("import_id")]
    public long ImportId { get; }

    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; }

    [JsonProperty("updated_at")]
    public DateTimeOffset? UpdatedAt { get; }

    public ChannelCreateNoteRequest(long importId, string bodyHtml, bool? incoming = null, string[]? notifyEmails = null,
        bool? isPrivate = null, long? userId = null, DateTimeOffset? createdAt = null, DateTimeOffset? updatedAt = null)
        : base(bodyHtml, incoming, notifyEmails, isPrivate, userId)
    {
        ImportId = importId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
