using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models
{
    public class ListContact : ContactBase
    {
        /// <summary>
        /// Additional companies associated with the contact
        /// </summary>
        [JsonProperty("other_companies")]
        public long[] OtherCompanies { get; set; }
    }
}
