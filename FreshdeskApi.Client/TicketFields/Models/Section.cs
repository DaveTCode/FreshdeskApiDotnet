using Newtonsoft.Json;

namespace FreshdeskApi.Client.TicketFields.Models
{
    /// <summary>
    /// Refers to a section within the ticket fields
    ///
    /// c.f. https://developers.freshdesk.com/api/#section_attributes
    /// </summary>
    public class Section
    {
        /// <summary>
        /// ID of the section
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Display name of the section
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Ticket Field ID to which the section is mapped
        /// </summary>
        [JsonProperty("parent_ticket_field_id")]
        public long ParentTicketFieldId { get; set; }

        /// <summary>
        /// Ticket Field IDs associated with the section (Ticket fields that
        /// are displayed as part of the section)
        /// </summary>
        [JsonProperty("ticket_field_ids")]
        public long[] TicketFieldIds { get; set; }

        /// <summary>
        /// Set to true if the ticket field is a FSM field (Field Service
        /// Management)
        /// </summary>
        [JsonProperty("is_fsm")]
        public bool? IsFsm { get; set; }

        /// <summary>
        /// Set of Choice IDs mapped to the section. The section will be
        /// displayed on choosing any one of the choices
        /// </summary>
        [JsonProperty("choice_ids")]
        public long[] ChoiceIds { get; set; }
    }
}
