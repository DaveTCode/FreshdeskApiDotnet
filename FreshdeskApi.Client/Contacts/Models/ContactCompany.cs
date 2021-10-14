using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Models
{
    /// <summary>
    /// Used when a contact is a member of multiple companies, this object
    /// references that other company but without the full company information.
    /// </summary>
    public class ContactCompany
    {
        [JsonProperty("company_id")]
        public long Id { get; set; }

        [JsonProperty("view_all_tickets")]
        public bool ViewAllTickets { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(ViewAllTickets)}: {ViewAllTickets}";
        }
    }
}
