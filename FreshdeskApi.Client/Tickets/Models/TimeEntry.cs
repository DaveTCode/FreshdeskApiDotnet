using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// A single time entry linked to a single ticket.
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_ticket_timeentries
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TimeEntry
    {
        [JsonPropertyName("billable")]
        public bool Billable { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("timer_running")]
        public bool TimerRunning { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("agent_id")]
        public long AgentId { get; set; }

        [JsonPropertyName("ticket_id")]
        public long TicketId { get; set; }

        [JsonPropertyName("time_spent")]
        public string TimeSpent { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("executed_at")]
        public DateTimeOffset? ExecutedAt { get; set; }

        [JsonPropertyName("start_time")]
        public DateTimeOffset? StartTime { get; set; }

        public override string ToString()
        {
            return $"{nameof(Billable)}: {Billable}, {nameof(Note)}: {Note}, {nameof(TimerRunning)}: {TimerRunning}, {nameof(Id)}: {Id}, {nameof(AgentId)}: {AgentId}, {nameof(TicketId)}: {TicketId}, {nameof(TimeSpent)}: {TimeSpent}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(ExecutedAt)}: {ExecutedAt}, {nameof(StartTime)}: {StartTime}";
        }
    }
}
