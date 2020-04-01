using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// An optional include on the ticket retrieval API to provide statistics
    /// about a specific ticket.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TicketStats
    {
        [JsonPropertyName("agent_responded_at")]
        public DateTimeOffset? AgentRespondedAt { get; set; }

        [JsonPropertyName("requester_responded_at")]
        public DateTimeOffset? RequesterRespondedAt { get; set; }

        [JsonPropertyName("first_responded_at")]
        public DateTimeOffset? FirstRespondedAt { get; set; }

        [JsonPropertyName("status_updated_at")]
        public DateTimeOffset? StatusUpdatedAt { get; set; }

        [JsonPropertyName("reopened_at")]
        public DateTimeOffset? ReopenedAt { get; set; }

        [JsonPropertyName("resolved_at")]
        public DateTimeOffset? ResolvedAt { get; set; }

        [JsonPropertyName("closed_at")]
        public DateTimeOffset? ClosedAt { get; set; }

        [JsonPropertyName("pending_since")]
        public DateTimeOffset? PendingSince { get; set; }

        public override string ToString()
        {
            return $"{nameof(AgentRespondedAt)}: {AgentRespondedAt}, {nameof(RequesterRespondedAt)}: {RequesterRespondedAt}, {nameof(FirstRespondedAt)}: {FirstRespondedAt}, {nameof(StatusUpdatedAt)}: {StatusUpdatedAt}, {nameof(ReopenedAt)}: {ReopenedAt}, {nameof(ResolvedAt)}: {ResolvedAt}, {nameof(ClosedAt)}: {ClosedAt}, {nameof(PendingSince)}: {PendingSince}";
        }
    }
}
