using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// Refers to a single satisfaction rating for a single ticket.
    ///
    /// The Ratings dictionary will include answers to questions as configured
    /// in the Freshdesk instance.
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_ticket_satisfaction_ratings
    /// </summary>
    public class SatisfactionRating
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("survey_id")]
        public long SurveyId { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("agent_id")]
        public long AgentId { get; set; }

        [JsonPropertyName("feedback")]
        public string Feedback { get; set; }

        [JsonPropertyName("group_id")]
        public long? GroupId { get; set; }

        [JsonPropertyName("ticket_id")]
        public long TicketId { get; set; }

        [JsonPropertyName("ratings")]
        public Dictionary<string, int> Ratings { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(SurveyId)}: {SurveyId}, {nameof(UserId)}: {UserId}, {nameof(AgentId)}: {AgentId}, {nameof(Feedback)}: {Feedback}, {nameof(GroupId)}: {GroupId}, {nameof(TicketId)}: {TicketId}, {nameof(Ratings)}: {Ratings}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}";
        }
    }
}
