using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Companies.Models
{
    /// <summary>
    /// Refers to an company as seen on Freshdesk API
    ///
    /// c.f. https://developers.freshdesk.com/api/#companies
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Company
    {
        /// <summary>
        /// Unique ID of the company
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the company
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Any specific note about the company
        /// </summary>
        [JsonPropertyName("note")]
        public string Note { get; set; }

        /// <summary>
        /// Domains of the company. Email addresses of contacts that contain
        /// this domain will be associated with that company automatically.
        /// </summary>
        [JsonPropertyName("domains")]
        public string[] Domains { get; set; }

        /// <summary>
        /// Key value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Company creation timestamp
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Company updated timestamp
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The strength of your relationship with the company
        /// </summary>
        [JsonPropertyName("health_score")]
        public string HealthScore { get; set; }

        /// <summary>
        /// Classification based on how much value the company brings to your business
        /// </summary>
        [JsonPropertyName("account_tier")]
        public string AccountTier { get; set; }

        /// <summary>
        /// Date when your contract or relationship with the company is due for renewal.
        ///
        /// Optional field
        /// </summary>
        [JsonPropertyName("renewal_date")]
        public DateTimeOffset? RenewalDate { get; set; }

        /// <summary>
        /// The industry the company serves in
        /// </summary>
        [JsonPropertyName("industry")]
        public string Industry { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Note)}: {Note}, {nameof(Domains)}: {Domains}, {nameof(CustomFields)}: {CustomFields}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(HealthScore)}: {HealthScore}, {nameof(AccountTier)}: {AccountTier}, {nameof(RenewalDate)}: {RenewalDate}, {nameof(Industry)}: {Industry}";
        }
    }
}
