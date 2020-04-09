using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// An optional include on the ticket retrieval API to provide statistics
    /// about a specific ticket.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TicketStats
    {
        [JsonProperty("agent_responded_at")]
        public DateTimeOffset? AgentRespondedAt { get; set; }

        [JsonProperty("requester_responded_at")]
        public DateTimeOffset? RequesterRespondedAt { get; set; }

        [JsonProperty("first_responded_at")]
        public DateTimeOffset? FirstRespondedAt { get; set; }

        [JsonProperty("status_updated_at")]
        public DateTimeOffset? StatusUpdatedAt { get; set; }

        [JsonProperty("reopened_at")]
        public DateTimeOffset? ReopenedAt { get; set; }

        [JsonProperty("resolved_at")]
        public DateTimeOffset? ResolvedAt { get; set; }

        [JsonProperty("closed_at")]
        public DateTimeOffset? ClosedAt { get; set; }

        [JsonProperty("pending_since")]
        public DateTimeOffset? PendingSince { get; set; }

        public override string ToString()
        {
            return $"{nameof(AgentRespondedAt)}: {AgentRespondedAt}, {nameof(RequesterRespondedAt)}: {RequesterRespondedAt}, {nameof(FirstRespondedAt)}: {FirstRespondedAt}, {nameof(StatusUpdatedAt)}: {StatusUpdatedAt}, {nameof(ReopenedAt)}: {ReopenedAt}, {nameof(ResolvedAt)}: {ResolvedAt}, {nameof(ClosedAt)}: {ClosedAt}, {nameof(PendingSince)}: {PendingSince}";
        }
    }
}
