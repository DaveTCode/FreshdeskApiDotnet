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
        /// <summary>
        /// Unique ID of the group
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Name of the group
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the group
        /// </summary>
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

        /// <summary>
        /// Array of agent user IDs separated by commas.
        /// </summary>
        [JsonPropertyName("agent_ids")]
        public long[] AgentIds { get; set; }

        [JsonPropertyName("group_type")]
        public string GroupType { get; set; }

        /// <summary>
        /// Group creation timestamp
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Group updated timestamp
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Set to true if automatic ticket assignment has been enabled.
        /// Automatic ticket assignment is only available on certain plans.
        ///
        /// TODO - Turns out that it can an integer sometimes as well?? WTF
        /// </summary>
        [JsonPropertyName("auto_ticket_assign")]
        public object AutoTicketAssign { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(EscalateTo)}: {EscalateTo}, {nameof(UnassignedFor)}: {UnassignedFor}, {nameof(BusinessHourId)}: {BusinessHourId}, {nameof(AgentIds)}: {AgentIds}, {nameof(GroupType)}: {GroupType}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(AutoTicketAssign)}: {AutoTicketAssign}";
        }
    }
}
