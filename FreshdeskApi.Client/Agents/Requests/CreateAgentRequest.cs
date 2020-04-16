using FreshdeskApi.Client.Agents.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Agents.Requests
{
    /// <summary>
    /// Create an agent and the underlying contact
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_agent
    /// </summary>
    public class CreateAgentRequest
    {
        /// <summary>
        /// Email address of the Agent.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; }

        /// <summary>
        /// Ticket permission of the agent
        /// (1 -> Global Access, 2 -> Group Access, 3 -> Restricted Access).
        ///
        /// Current logged in agent can't update their ticket_scope
        /// </summary>
        [JsonProperty("ticket_scope")]
        public TicketScope TicketScope { get; }

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
        /// associated with the agent.
        ///
        /// Current logged in agent can't update their role_ids
        /// </summary>
        [JsonProperty("role_ids")]
        public long[] RoleIds { get; }

        /// <summary>
        /// Name of the Agent
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Telephone number of the Agent.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; }

        /// <summary>
        /// Mobile number of the Agent
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; }

        /// <summary>
        /// Job title of the Agent
        /// </summary>
        [JsonProperty("job_title")]
        public string JobTitle { get; }

        /// <summary>
        /// Language of the Agent. Default language is "en"
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; }

        /// <summary>
        /// Time zone of the Agent. Default value is time zone of the domain
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; }

        public CreateAgentRequest(string email, TicketScope ticketScope, bool? occasional = null, string signatureHtml = null, long[] skillIds = null, long[] groupIds = null, long[] roleIds = null, string name= null, string phone = null, string mobile = null, string jobTitle = null, string language = null, string timeZone = null)
        {
            Email = email;
            TicketScope = ticketScope;
            Occasional = occasional;
            SignatureHtml = signatureHtml;
            SkillIds = skillIds;
            GroupIds = groupIds;
            RoleIds = roleIds;
            Name = name;
            Phone = phone;
            Mobile = mobile;
            JobTitle = jobTitle;
            Language = language;
            TimeZone = timeZone;
        }

        public override string ToString()
        {
            return $"{nameof(Email)}: {Email}, {nameof(TicketScope)}: {TicketScope}, {nameof(Occasional)}: {Occasional}, {nameof(SkillIds)}: {SkillIds}, {nameof(GroupIds)}: {GroupIds}, {nameof(RoleIds)}: {RoleIds}, {nameof(Name)}: {Name}, {nameof(Phone)}: {Phone}, {nameof(Mobile)}: {Mobile}, {nameof(JobTitle)}: {JobTitle}, {nameof(Language)}: {Language}, {nameof(TimeZone)}: {TimeZone}";
        }
    }
}
