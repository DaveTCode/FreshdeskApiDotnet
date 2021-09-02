using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models
{
    public class ListContact : ContactBase
    {
        /// <summary>
        /// Additional companies associated with the contact<br/>
        /// 
        /// NOTE: the list api has the limitation to only return the company ID and not the view_all_tickets property from the <see cref="ContactCompany" />.
        /// This is an intentional limitation, see: https://community.freshworks.com/freshdesk-api-11012/api-v2-contacts-returning-only-the-id-s-in-other-companies-22089?postid=55712#post55712
        /// </summary>
        [JsonProperty("other_companies")]
        public long[]? OtherCompanies { get; set; }
    }
}
