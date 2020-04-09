using Newtonsoft.Json;

namespace FreshdeskApi.Client.Groups.Requests
{
    /// <summary>
    /// Defines the properties when creating a new group
    /// </summary>
    public class CreateGroupRequest
    {
        /// <summary>
        /// Name of the group - must be unique
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Description of the group
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; }

        /// <summary>
        /// The time after which an escalation email will be sent if a ticket
        /// remains unassigned. The accepted values are "30m" for 30 minutes,
        /// "1h" for 1 hour, "2h" for 2 hour, "4h" for 4 hour, "8h" for 8 hour,
        /// "12h" for 12 hour, "1d" for 1 day, "2d" for 2days, "3d" for 3 days.
        ///
        /// The default value is "30m"
        /// </summary>
        [JsonProperty("unassigned_for")]
        public string UnassignedFor { get; }

        /// <summary>
        /// The user to whom the escalation email is sent of a ticket is
        /// unassigned. To create/update escalate_to with 'none' provide the
        /// value 'null' in the request
        /// </summary>
        [JsonProperty("escalate_to")]
        public long? EscalateTo { get; }

        /// <summary>
        /// Describes the automatic ticket assignment type. Will not be
        /// supported if the "Round Robin" feature is disabled for the account.
        /// The default value is false.
        /// </summary>
        [JsonProperty("auto_ticket_assign")]
        public bool? AutoTicketAssign { get; }

        /// <summary>
        /// Array of agent user ids
        /// </summary>
        [JsonProperty("agent_ids")]
        public long[] AgentIds { get; }

        public CreateGroupRequest(string name, string description = null, string unassignedFor = null, long? escalateTo = null, bool? autoTicketAssign = null, long[] agentIds = null)
        {
            Name = name;
            Description = description;
            UnassignedFor = unassignedFor;
            EscalateTo = escalateTo;
            AutoTicketAssign = autoTicketAssign;
            AgentIds = agentIds;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(UnassignedFor)}: {UnassignedFor}, {nameof(EscalateTo)}: {EscalateTo}, {nameof(AutoTicketAssign)}: {AutoTicketAssign}, {nameof(AgentIds)}: {AgentIds}";
        }
    }
}
