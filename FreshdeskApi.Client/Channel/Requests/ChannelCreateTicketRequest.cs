using System;
using System.Collections.Generic;
using FreshdeskApi.Client.Tickets.Models;
using FreshdeskApi.Client.Tickets.Requests;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Channel.Requests
{
    public class ChannelCreateTicketRequest : CreateTicketRequest
    {
        [JsonProperty("import_id")]
        public long ImportId { get; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; }

        public ChannelCreateTicketRequest(long importId, TicketStatus status, TicketPriority priority, TicketSource source, string description,
            string? requesterName = null, long? requesterId = null, string? email = null, string? facebookId = null, string? phoneNumber = null,
            string? twitterId = null, string? uniqueExternalId = null, long? responderId = null, string[]? ccEmails = null,
            Dictionary<string, object>? customFields = null, DateTimeOffset? dueBy = null, long? emailConfigId = null,
            DateTimeOffset? firstResponseDueBy = null, long? groupId = null, long? productId = null, string[]? tags = null,
            long? companyId = null, string? subject = null, string? ticketType = null, long? parentTicketId = null, DateTimeOffset? createdAt = null,
            DateTimeOffset? updatedAt = null) : base(status, priority, source, description, requesterName, requesterId, email, facebookId, phoneNumber, twitterId, uniqueExternalId, responderId, ccEmails, customFields, dueBy, emailConfigId, firstResponseDueBy, groupId, productId, tags, companyId, subject, ticketType, parentTicketId)
        {
            ImportId = importId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
