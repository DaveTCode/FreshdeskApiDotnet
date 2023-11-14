using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models;

public class Contact : ContactBase
{
    /// <summary>
    /// Additional companies associated with the contact
    ///
    /// Note that this field is only returned when getting a single
    /// contact, not when listing/filtering.
    /// </summary>
    [JsonProperty("other_companies")]
    public ContactCompany[]? OtherCompanies { get; set; }
}
