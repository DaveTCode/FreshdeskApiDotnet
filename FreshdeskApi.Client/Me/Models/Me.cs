using System;
using System.Diagnostics.CodeAnalysis;
using FreshdeskApi.Client.Agents.Models;
using FreshdeskApi.Client.Contacts.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Me.Models
{
    /// <summary>
    /// Refers to an agent as seen on Freshdesk API
    ///
    /// c.f. https://developers.freshdesk.com/api/#agents
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Me
    {
        /// <summary>
        /// If the agent is in a group that has enabled "Automatic Ticket
        /// Assignment", this attribute will be set to true if the agent
        /// is accepting new tickets
        /// </summary>
        [JsonProperty("available")]
        public bool Available { get; set; }

        /// <summary>
        /// Set to true if this is an occasional agent (true => occasional,
        /// false => full-time)
        /// </summary>
        [JsonProperty("occasional")]
        public bool Occasional { get; set; }

        /// <summary>
        /// User ID of the agent
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Ticket permission of the agent
        /// </summary>
        [JsonProperty("ticket_scope")]
        public TicketScope TicketScope { get; set; }

        /// <summary>
        /// Signature of the agent in HTML format
        /// </summary>
        [JsonProperty("signature")]
        public string? Signature { get; set; }

        /// <summary>
        /// Group IDs associated with the agent
        /// </summary>
        [JsonProperty("group_ids")]
        public long[]? GroupIds { get; set; }

        /// <summary>
        /// Role IDs associated with the agent
        /// </summary>
        [JsonProperty("role_ids")]
        public long[]? RoleIds { get; set; }

        /// <summary>
        /// Skill ids associated with the agent
        /// </summary>
        [JsonProperty("skill_ids")]
        public long[]? SkillIds { get; set; }

        /// <summary>
        /// Agent creation timestamp
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Agent updated timestamp
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Timestamp that denotes when the agent became available/unavailable
        /// (depending on the value of the 'available' attribute)
        /// </summary>
        [JsonProperty("available_since")]
        public DateTimeOffset? AvailableSince { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        /// All agents are also contacts, this is the full set of contact
        /// information about the agent.
        /// </summary>
        [JsonProperty("contact")]
        public Contact? Contact { get; set; }

        public override string ToString()
        {
            return $"{nameof(Available)}: {Available}, {nameof(Occasional)}: {Occasional}, {nameof(Id)}: {Id}, {nameof(TicketScope)}: {TicketScope}, {nameof(Signature)}: {Signature}, {nameof(GroupIds)}: {GroupIds}, {nameof(RoleIds)}: {RoleIds}, {nameof(SkillIds)}: {SkillIds}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(AvailableSince)}: {AvailableSince}, {nameof(Type)}: {Type}, {nameof(Contact)}: {Contact}";
        }
    }
}
