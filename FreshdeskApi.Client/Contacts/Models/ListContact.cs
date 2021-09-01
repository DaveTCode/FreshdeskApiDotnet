using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models
{
    public class ListContact : ContactBase
    {
        /// <summary>
        /// Additional companies associated with the contact
        /// </summary>
        [JsonProperty("other_companies")]
        public long[]? OtherCompanies { get; set; } //NOTE: ContactCompany would be logical but that is not supported in the API
    }
}
