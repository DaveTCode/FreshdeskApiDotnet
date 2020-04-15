using FreshdeskApi.Client.Agents.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Requests
{
    public class MakeAgentRequest
    {
        /// <summary>
        /// Set to true if this is an occasional agent (true => occasional,
        /// false => full-time)
        /// </summary>
        [JsonProperty("occasional")]
        public bool? Occasional { get; }

        /// <summary>
        /// Signature of the agent in HTML format
        /// </summary>
        [JsonProperty("signature")]
        public string SignatureHtml { get; }

        /// <summary>
        /// Ticket permission of the agent
        /// (1 -> Global Access, 2 -> Group Access, 3 -> Restricted Access)
        ///
        /// Current logged in agent can't update his/her ticket_scope.
        /// </summary>
        [JsonProperty("ticket_scope")]
        public TicketScope? TicketScope { get; }

        /// <summary>
        /// Skill ids associated with the agent
        /// </summary>
        [JsonProperty("skill_ids")]
        public long[] SkillIds { get; }

        /// <summary>
        /// Group ids associated with the agent
        /// </summary>
        [JsonProperty("group_ids")]
        public long[] GroupIds { get; }

        /// <summary>
        /// Role IDs associated with the agent. At least one role should be
        /// associated with the agent. Current logged in agent can't update
        /// his/her role_ids
        ///
        /// TODO - Does this imply not null with at least one entry?
        /// </summary>
        [JsonProperty("role_ids")]
        public long[] RoleIds { get; }

        public MakeAgentRequest(bool? occasional = null, string signatureHtml = null, TicketScope? ticketScope = null, long[] skillIds = null, long[] groupIds = null, long[] roleIds = null)
        {
            Occasional = occasional;
            SignatureHtml = signatureHtml;
            TicketScope = ticketScope;
            SkillIds = skillIds;
            GroupIds = groupIds;
            RoleIds = roleIds;
        }

        public override string ToString()
        {
            return $"{nameof(Occasional)}: {Occasional}, {nameof(SignatureHtml)}: {SignatureHtml}, {nameof(TicketScope)}: {TicketScope}, {nameof(SkillIds)}: {SkillIds}, {nameof(GroupIds)}: {GroupIds}, {nameof(RoleIds)}: {RoleIds}";
        }
    }
}
