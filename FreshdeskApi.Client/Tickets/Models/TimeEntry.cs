using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

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
        [JsonProperty("billable")]
        public bool Billable { get; set; }

        [JsonProperty("note")]
        public string? Note { get; set; }

        [JsonProperty("timer_running")]
        public bool TimerRunning { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("agent_id")]
        public long AgentId { get; set; }

        [JsonProperty("ticket_id")]
        public long TicketId { get; set; }

        [JsonProperty("time_spent")]
        public string? TimeSpent { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("executed_at")]
        public DateTimeOffset? ExecutedAt { get; set; }

        [JsonProperty("start_time")]
        public DateTimeOffset? StartTime { get; set; }

        public override string ToString()
        {
            return $"{nameof(Billable)}: {Billable}, {nameof(Note)}: {Note}, {nameof(TimerRunning)}: {TimerRunning}, {nameof(Id)}: {Id}, {nameof(AgentId)}: {AgentId}, {nameof(TicketId)}: {TicketId}, {nameof(TimeSpent)}: {TimeSpent}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(ExecutedAt)}: {ExecutedAt}, {nameof(StartTime)}: {StartTime}";
        }
    }
}
