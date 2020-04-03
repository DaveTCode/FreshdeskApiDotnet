using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using FreshdeskApi.Client.Contacts.Models;

namespace FreshdeskApi.Client.Agents.Models
{
    /// <summary>
    /// Refers to an agent as seen on Freshdesk API
    ///
    /// c.f. https://developers.freshdesk.com/api/#agents
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Agent
    {
        [JsonPropertyName("available")]
        public bool Available { get; set; }

        [JsonPropertyName("occasional")]
        public bool Occasional { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("ticket_scope")]
        public long TicketScope { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("group_ids")]
        public long[] GroupIds { get; set; }

        [JsonPropertyName("role_ids")]
        public long[] RoleIds { get; set; }

        [JsonPropertyName("skill_ids")]
        public long[] SkillIds { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("last_active_at")]
        public DateTimeOffset? LastActiveAt { get; set; }

        [JsonPropertyName("available_since")]
        public DateTimeOffset? AvailableSince { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("contact")]
        public Contact Contact { get; set; }
    }
}
