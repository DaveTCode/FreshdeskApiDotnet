using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Contacts.Models
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Contact
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("company_id")]
        public long? CompanyId { get; set; }

        [JsonPropertyName("view_all_tickets")]
        public bool ViewAllTickets { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("twitter_id")]
        public string TwitterId { get; set; }

        [JsonPropertyName("custom_fields")]
        public Dictionary<string, string> CustomFields { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        [JsonPropertyName("other_emails")]
        public string[] OtherEmails { get; set; }

        [JsonPropertyName("facebook_id")]
        public string FacebookId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("other_companies")]
        public ContactCompany[] OtherCompanies { get; set; }

        [JsonPropertyName("unique_external_id")]
        public long? UniqueExternalId { get; set; }

        [JsonPropertyName("twitter_profile_status")]
        public bool TwitterProfileStatus { get; set; }

        [JsonPropertyName("twitter_followers_count")]
        public long? TwitterFollowersCount { get; set; }

        [JsonPropertyName("avatar")]
        public object Avatar { get; set; }
    }
}
