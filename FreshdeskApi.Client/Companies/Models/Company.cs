using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

#pragma warning disable 8618

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
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Name of the company
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the company
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Any specific note about the company
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Domains of the company. Email addresses of contacts that contain
        /// this domain will be associated with that company automatically.
        /// </summary>
        [JsonProperty("domains")]
        public string[] Domains { get; set; }

        /// <summary>
        /// Key value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Company creation timestamp
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Company updated timestamp
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The strength of your relationship with the company
        /// </summary>
        [JsonProperty("health_score")]
        public string HealthScore { get; set; }

        /// <summary>
        /// Classification based on how much value the company brings to your business
        /// </summary>
        [JsonProperty("account_tier")]
        public string AccountTier { get; set; }

        /// <summary>
        /// Date when your contract or relationship with the company is due for renewal.
        ///
        /// Optional field
        /// </summary>
        [JsonProperty("renewal_date")]
        public DateTimeOffset? RenewalDate { get; set; }

        /// <summary>
        /// The industry the company serves in
        /// </summary>
        [JsonProperty("industry")]
        public string Industry { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Note)}: {Note}, {nameof(Domains)}: {Domains}, {nameof(CustomFields)}: {CustomFields}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(HealthScore)}: {HealthScore}, {nameof(AccountTier)}: {AccountTier}, {nameof(RenewalDate)}: {RenewalDate}, {nameof(Industry)}: {Industry}";
        }
    }
}
