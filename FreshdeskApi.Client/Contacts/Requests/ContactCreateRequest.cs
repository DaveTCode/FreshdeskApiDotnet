using System.Collections.Generic;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Contacts.Models;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.Contacts.Requests;

/// <summary>
/// A class containing the fields passed to create a contact.
///
/// Any field set to null will be unset in Freshdesk
/// 
/// c.f. https://developers.freshdesk.com/api/#create_contact
/// </summary>
public class ContactCreateRequest : IRequestWithAttachment
{
    /// <summary>
    /// Name of the contact
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; }

    /// <summary>
    /// Primary email address of the contact. If you want to associate
    /// additional email(s) with this contact, use the other_emails
    /// attribute.
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; }

    /// <summary>
    /// Telephone number of the contact. Must be unique amongst all contacts.
    /// </summary>
    [JsonProperty("phone")]
    public string? Phone { get; }

    /// <summary>
    /// Mobile number of the contact. Must be unique amongst all contacts.
    /// </summary>
    [JsonProperty("mobile")]
    public string? Mobile { get; }

    /// <summary>
    /// Twitter handle of the contact. Must be unique amongst all contacts.
    /// </summary>
    [JsonProperty("twitter_id")]
    public string? TwitterId { get; }

    /// <summary>
    /// External ID of the contact. Must be unique amongst all contacts.
    /// </summary>
    [JsonProperty("unique_external_id")]
    public string? UniqueExternalId { get; }

    /// <summary>
    /// Additional emails associated with the contact
    /// </summary>
    [JsonProperty("other_emails")]
    public string[]? OtherEmails { get; }

    /// <summary>
    /// ID of the primary company to which this contact belongs
    /// </summary>
    [JsonProperty("company_id")]
    public long? CompanyId { get; }

    /// <summary>
    /// Set to true if the contact can see all the tickets that are
    /// associated with the company to which they belong
    /// </summary>
    [JsonProperty("view_all_tickets")]
    public bool? ViewAllTickets { get; }

    /// <summary>
    /// Additional companies associated with the contact. This attribute
    /// can only be set if the Multiple Companies feature is enabled
    /// (Estate plan and above.)
    /// </summary>
    [JsonProperty("other_companies")]
    public ContactCompany[]? OtherCompanies { get; }

    /// <summary>
    /// Address of the contact
    /// </summary>
    [JsonProperty("address")]
    public string? Address { get; }

    /// <summary>
    /// Key value pairs containing the name and value of the custom field.
    /// Only dates in the format YYYY-MM-DD are accepted as input for
    /// custom date fields.
    ///
    /// c.f. https://support.freshdesk.com/support/solutions/articles/216553
    /// </summary>
    [JsonProperty("custom_fields")]
    public Dictionary<string, object>? CustomFields { get; }

    /// <summary>
    /// A small description of the contact
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; }

    /// <summary>
    /// Job title of the contact
    /// </summary>
    [JsonProperty("job_title")]
    public string? JobTitle { get; }

    /// <summary>
    /// Language of the contact. Default language is "en". This attribute
    /// can only be set if the Multiple Language feature is enabled (Garden
    /// plan and above)
    /// </summary>
    [JsonProperty("language")]
    public string? Language { get; }

    /// <summary>
    /// Tags associated with this contact
    /// </summary>
    [JsonProperty("tags")]
    public string[]? Tags { get; }

    /// <summary>
    /// Time zone of the contact. Default value is the time zone of the
    /// domain. This attribute can only be set if the Multiple Time Zone
    /// feature is enabled (Garden plan and above)
    /// </summary>
    [JsonProperty("time_zone")]
    public string? TimeZone { get; }

    [JsonIgnore, Multipart(Name = "avatar")]
    public FileAttachment? Avatar { get; }

    public ContactCreateRequest(string name, string? email = null, string? phone = null, string? mobile = null, string? twitterId = null,
        string? uniqueExternalId = null, string[]? otherEmails = null, long? companyId = null, bool? viewAllTickets = null,
        ContactCompany[]? otherCompanies = null, string? address = null, Dictionary<string, object>? customFields = null,
        string? description = null, string? jobTitle = null, string? language = null, string[]? tags = null, string? timeZone = null,
        FileAttachment? avatar = null)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Mobile = mobile;
        TwitterId = twitterId;
        UniqueExternalId = uniqueExternalId;
        OtherEmails = otherEmails;
        CompanyId = companyId;
        ViewAllTickets = viewAllTickets;
        OtherCompanies = otherCompanies;
        Address = address;
        CustomFields = customFields;
        Description = description;
        JobTitle = jobTitle;
        Language = language;
        Tags = tags;
        TimeZone = timeZone;
        Avatar = avatar;
    }

    public bool IsMultipartFormDataRequired() => Avatar != null;

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(Phone)}: {Phone}, {nameof(Mobile)}: {Mobile}, {nameof(TwitterId)}: {TwitterId}, {nameof(UniqueExternalId)}: {UniqueExternalId}, {nameof(OtherEmails)}: {OtherEmails}, {nameof(CompanyId)}: {CompanyId}, {nameof(ViewAllTickets)}: {ViewAllTickets}, {nameof(OtherCompanies)}: {OtherCompanies}, {nameof(Address)}: {Address}, {nameof(CustomFields)}: {CustomFields}, {nameof(Description)}: {Description}, {nameof(JobTitle)}: {JobTitle}, {nameof(Language)}: {Language}, {nameof(Tags)}: {Tags}, {nameof(TimeZone)}: {TimeZone}";
    }
}
