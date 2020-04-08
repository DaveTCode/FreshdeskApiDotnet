using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Contacts.Models
{
    /// <summary>
    /// A contact is a customer or a potential customer who has raised a
    /// support ticket through any channel.
    ///
    /// c.f. https://developers.freshdesk.com/api/#contacts
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Contact
    {
        /// <summary>
        /// Set to true if the contact has been verified
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Address of the contact
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// ID of the primary company to which this contact belongs
        /// </summary>
        [JsonPropertyName("company_id")]
        public long? CompanyId { get; set; }

        /// <summary>
        /// Set to true if the contact can see all tickets that are associated
        /// with the company to which they belong.
        /// </summary>
        [JsonPropertyName("view_all_tickets")]
        public bool ViewAllTickets { get; set; }

        /// <summary>
        /// A short description of the contact
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Primary email address of the contact. If you want to associate
        /// additional email(s) with this contact, use the other_emails
        /// attribute
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// ID of the contact
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// Job title of the contact
        /// </summary>
        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// Language of the contact
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// Mobile number of the contact
        /// </summary>
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// Name of the contact
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Telephone number of the contact
        /// </summary>
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Time zone in which the contact resides
        /// </summary>
        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Twitter handle of the contact
        /// </summary>
        [JsonPropertyName("twitter_id")]
        public string TwitterId { get; set; }

        /// <summary>
        /// Key value pair containing the name and value of the custom fields.
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Tags associated with this contact
        /// </summary>
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Additional emails associated with the contact
        /// </summary>
        [JsonPropertyName("other_emails")]
        public string[] OtherEmails { get; set; }

        [JsonPropertyName("facebook_id")]
        public string FacebookId { get; set; }

        /// <summary>
        /// Contact creation timestamp
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Contact updated timestamp
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Additional companies associated with the contact
        /// </summary>
        [JsonPropertyName("other_companies")]
        public ContactCompany[] OtherCompanies { get; set; }

        /// <summary>
        /// External ID of the contact
        /// </summary>
        [JsonPropertyName("unique_external_id")]
        public long? UniqueExternalId { get; set; }

        [JsonPropertyName("twitter_profile_status")]
        public bool TwitterProfileStatus { get; set; }

        [JsonPropertyName("twitter_followers_count")]
        public long? TwitterFollowersCount { get; set; }

        /// <summary>
        /// Optional avatar of the contact
        /// </summary>
        [JsonPropertyName("avatar")]
        public Avatar Avatar { get; set; }

        public override string ToString()
        {
            return $"{nameof(Active)}: {Active}, {nameof(Address)}: {Address}, {nameof(CompanyId)}: {CompanyId}, {nameof(ViewAllTickets)}: {ViewAllTickets}, {nameof(Description)}: {Description}, {nameof(Email)}: {Email}, {nameof(Id)}: {Id}, {nameof(JobTitle)}: {JobTitle}, {nameof(Language)}: {Language}, {nameof(Mobile)}: {Mobile}, {nameof(Name)}: {Name}, {nameof(Phone)}: {Phone}, {nameof(TimeZone)}: {TimeZone}, {nameof(TwitterId)}: {TwitterId}, {nameof(CustomFields)}: {CustomFields}, {nameof(Tags)}: {Tags}, {nameof(OtherEmails)}: {OtherEmails}, {nameof(FacebookId)}: {FacebookId}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(OtherCompanies)}: {OtherCompanies}, {nameof(UniqueExternalId)}: {UniqueExternalId}, {nameof(TwitterProfileStatus)}: {TwitterProfileStatus}, {nameof(TwitterFollowersCount)}: {TwitterFollowersCount}, {nameof(Avatar)}: {Avatar}";
        }
    }
}
