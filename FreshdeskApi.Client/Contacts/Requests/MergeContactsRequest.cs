using System.Collections.Generic;
using FreshdeskApi.Client.Contacts.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Requests;

public class MergeContactsRequest
{
    /// <summary>
    /// The Id of the contact that you want to be the main contact when merging
    /// </summary>
    [JsonProperty("primary_contact_id")]
    public long PrimaryContactId { get; }

    /// <summary>
    /// A list of Ids for the contacts that you want to be the merged into the main contact
    /// </summary>
    [JsonProperty("secondary_contact_ids")]
    public List<long> SecondaryContactIds { get; }

    /// <summary>
    /// Contact infromation that can be updated in a merge. Phone, Mobile, Twitter Id and 
    /// Unique external values are mandatory if they are present in both primary and secondary contacts.
    /// </summary>
    [JsonProperty("contact")]
    public MergeContactFields? Contact { get; }

    public MergeContactsRequest(long primaryContactId, List<long> secondaryContactIds, MergeContactFields? contact = null)
    {
            PrimaryContactId = primaryContactId;
            SecondaryContactIds = secondaryContactIds;
            Contact = contact;
        }
}
