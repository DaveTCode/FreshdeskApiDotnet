using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Companies.Requests
{
    /// <summary>
    /// Defines the set of properties to update on a comapny
    ///
    /// c.f. https://developers.freshdesk.com/api/#update_company
    /// </summary>
    public class UpdateCompanyRequest
    {
        /// <summary>
        /// Name of the company - this has to be unique
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; }

        /// <summary>
        /// Domains of the company. Email addresses of contacts that contain this 
        /// domain will be associated with that company automatically.
        /// </summary>
        [JsonProperty("domains")]
        public string[]? Domains { get; }

        /// <summary>
        /// Description of the company
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; }

        /// <summary>
        /// Any specific note about the company
        /// </summary>
        [JsonProperty("note")]
        public string? Note { get; }

        /// <summary>
        /// The strength of your relationship with the company
        /// </summary>
        [JsonProperty("health_score")]
        public string? HealthScore { get; }

        /// <summary>
        /// Classification based on how much value the company brings to your business
        /// </summary>
        [JsonProperty("account_tier")]
        public string? AccountTier { get; }

        /// <summary>
        /// Date when your contract or relationship with the company is due for renewal
        /// </summary>
        [JsonProperty("renewal_date")]
        public DateTime? RenewalDate { get; }

        /// <summary>
        /// The industry the company serves in
        /// </summary>
        [JsonProperty("industry")]
        public string? Industry { get; }
        /// <summary>
        /// Key value pairs containing the name and value of the custom field.
        /// Only dates in the format YYYY-MM-DD are accepted as input for
        /// custom date fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; }

        public UpdateCompanyRequest(string? name = null, string[]? domains = null, string? description = null, string? note = null, string? healthScore = null,
            string? accountTier = null, DateTime? renewalDate = null, string? industry = null, Dictionary<string, object>? customFields = null)
        {
            Name = name;
            Domains = domains;
            Description = description;
            Note = note;
            HealthScore = healthScore;
            AccountTier = accountTier;
            RenewalDate = renewalDate;
            Industry = industry;
            CustomFields = customFields;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Domains)}: {Domains}, {nameof(Description)}: {Description}, {nameof(Note)}: {Note}, {nameof(HealthScore)}: {HealthScore}, {nameof(AccountTier)}: {AccountTier}, {nameof(RenewalDate)}: {RenewalDate}, {nameof(Industry)}: {Industry}, {nameof(CustomFields)}: {CustomFields}";
        }
    }
}
