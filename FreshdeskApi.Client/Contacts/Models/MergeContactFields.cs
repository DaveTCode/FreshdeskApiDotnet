using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models;

/// <summary>
/// The fields for a contact that can be updated during a merge.
/// Phone / Mobile / Twitter id / Unique External values are mandatory
/// if they are present in both primary and secondary contacts.
///
/// c.f. https://developers.freshdesk.com/api/#merge_contact
/// </summary>
public class MergeContactFields
{
    /// <summary>
    /// Primary email address of the contact. If you want to associate
    /// additional email(s) with this contact, use the other_emails
    /// attribute
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Telephone number of the contact
    /// </summary>
    [JsonProperty("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Mobile number of the contact
    /// </summary>
    [JsonProperty("mobile")]
    public string? Mobile { get; set; }

    /// <summary>
    /// Twitter handle of the contact
    /// </summary>
    [JsonProperty("twitter_id")]
    public string? TwitterId { get; set; }

    /// <summary>
    /// External ID of the contact
    /// </summary>
    [JsonProperty("unique_external_id")]
    public string? UniqueExternalId { get; set; }

    /// <summary>
    /// Additional emails associated with the contact
    /// </summary>
    [JsonProperty("other_emails")]
    public string[]? OtherEmails { get; set; }

    /// <summary>
    /// IDs of the companies associated with the contact
    /// </summary>
    [JsonProperty("company_ids")]
    public int[]? CompanyIds { get; set; }

    public override string ToString()
    {
        return $"{nameof(Email)}: {Email}, {nameof(Phone)}: {Phone}, {nameof(Mobile)}: {Mobile}, {nameof(TwitterId)}: {TwitterId}, {nameof(UniqueExternalId)}: {UniqueExternalId}, {nameof(OtherEmails)}: {OtherEmails}, {nameof(CompanyIds)}: {CompanyIds}";
    }
}
