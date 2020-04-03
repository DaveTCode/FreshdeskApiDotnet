using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Contacts.Models
{
    /// <summary>
    /// Used when a contact is a member of multiple companies, this object
    /// references that other company but without the full company information.
    /// </summary>
    public class ContactCompany
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("view_all_tickets")]
        public bool ViewAllTickets { get; set; }
    }
}
