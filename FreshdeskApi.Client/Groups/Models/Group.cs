using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Groups.Models
{
    /// <summary>
    /// Refers to a group of agents (as specified in the
    /// <see cref="AgentIds"/> parameter).
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_group
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Group
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The ID of the user to whom an escalation email is sent if a ticket is unassigned.
        ///
        /// null == None
        /// </summary>
        [JsonPropertyName("escalate_to")]
        public long? EscalateTo { get; set; }

        /// <summary>
        /// The time after which an escalation email is sent if a ticket
        /// remains unassigned. The accepted values are "30m" for 30 minutes,
        /// "1h" for 1 hour, "2h" for 2 hours, "4h" for 4 hours, "8h" for 8
        /// hours, "12h" for 12 hours, "1d" for 1 day, "2d" for 2 days, and
        /// "3d" for 3 days
        /// </summary>
        [JsonPropertyName("unassigned_for")]
        public string UnassignedFor { get; set; }

        /// <summary>
        /// Unique ID of the business hour associated with the group
        /// </summary>
        [JsonPropertyName("business_hour_id")]
        public long? BusinessHourId { get; set; }

        [JsonPropertyName("agent_ids")]
        public long[] AgentIds { get; set; }

        [JsonPropertyName("group_type")]
        public string GroupType { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Set to true if automatic ticket assignment has been enabled.
        /// Automatic ticket assignment is only available on certain plans.
        /// </summary>
        [JsonPropertyName("auto_ticket_assign")]
        public bool AutoTicketAssign { get; set; }
    }
}
