using System;
using FreshdeskApi.Client.Conversations.Requests;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Channel.Requests;

public class ChannelCreateReplyRequest : CreateReplyRequest
{
    [JsonProperty("import_id")]
    public long ImportId { get; }

    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; }

    [JsonProperty("updated_at")]
    public DateTimeOffset? UpdatedAt { get; }

    public ChannelCreateReplyRequest(long importId, string bodyHtml, string? fromEmail = null, long? userId = null, string[]? ccEmails = null, string[]? bccEmails = null, DateTimeOffset? createdAt = null, DateTimeOffset? updatedAt = null)
        : base(bodyHtml, fromEmail, userId, ccEmails, bccEmails)
    {
            ImportId = importId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
}