using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FreshdeskApi.Client.TicketFields.Models;

public class TicketField
{
    /// <summary>
    /// ID of the ticket field
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Name of the ticket field
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Display name for the field (as seen by agents)
    /// </summary>
    [JsonProperty("label")]
    public string? Label { get; set; }

    /// <summary>
    /// Description of the ticket field
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Position in which the ticket field is displayed in the form
    /// </summary>
    [JsonProperty("position")]
    public long Position { get; set; }

    /// <summary>
    /// Set to true if the field is mandatory for closing the ticket
    /// </summary>
    [JsonProperty("required_for_closure")]
    public bool RequiredForClosure { get; set; }

    /// <summary>
    /// Set to true if the field is mandatory for Agents
    /// </summary>
    [JsonProperty("required_for_agents")]
    public bool RequiredForAgents { get; set; }

    /// <summary>
    /// For custom ticket fields, The type of value associated with the
    /// field will be given (Examples custom_date, custom_text...)
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Set to true if the field is not a custom field
    /// </summary>
    [JsonProperty("default")]
    public bool Default { get; set; }

    /// <summary>
    /// Set to true if the field can be updated by customers
    /// </summary>
    [JsonProperty("customers_can_edit")]
    public bool CustomersCanEdit { get; set; }

    /// <summary>
    /// Display name for the field (as seen in the customer portal)
    /// </summary>
    [JsonProperty("label_for_customers")]
    public string? LabelForCustomers { get; set; }

    /// <summary>
    /// Set to true if the field is mandatory in the customer portal
    /// </summary>
    [JsonProperty("required_for_customers")]
    public bool RequiredForCustomers { get; set; }

    /// <summary>
    /// Set to true if the field is displayed in the customer portal
    /// </summary>
    [JsonProperty("displayed_to_customers")]
    public bool DisplayedToCustomers { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Last update timestamp
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// Applicable only for the requester field. Set to true if customer
    /// can add additional requesters to a ticket
    /// </summary>
    [JsonProperty("portal_cc")]
    public bool? PortalCc { get; set; }

    /// <summary>
    /// Applicable only if portal_cc is set to true. Value will be all
    /// when a customer can add any requester to the CC list and company
    /// when a customer can add only company contacts to the CC list
    /// </summary>
    [JsonProperty("portal_cc_to")]
    public string? PortalCcTo { get; set; }

    /// <summary>
    /// The type of this object is dependent on the type of the ticket
    /// field. If it is a "nested_field" then this is a dictionary, if
    /// its custom_dropdown then this is a list of possible choices.
    /// </summary>
    [JsonProperty("choices")]
    public JToken? Choices { get; set; }

    /// <summary>
    /// True if the Ticket field is inside FSM section (Applicable only if
    /// FSM is enabled)
    /// </summary>
    [JsonProperty("is_fsm")]
    public bool? IsFsm { get; set; }

    /// <summary>
    /// True if the choice update is in progress (Applicable for the
    /// fields which has 100+ choices)
    /// </summary>
    [JsonProperty("field_update_in_progress")]
    public bool? FieldUpdateInProgress { get; set; }

    /// <summary>
    /// The set of ticket fields which are nested within this one, note
    /// that not all fields are populated on nested ticket fields.
    /// </summary>
    [JsonProperty("nested_ticket_fields")]
    public TicketField[]? NestedTicketFields { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Label)}: {Label}, {nameof(Description)}: {Description}, {nameof(Position)}: {Position}, {nameof(RequiredForClosure)}: {RequiredForClosure}, {nameof(RequiredForAgents)}: {RequiredForAgents}, {nameof(Type)}: {Type}, {nameof(Default)}: {Default}, {nameof(CustomersCanEdit)}: {CustomersCanEdit}, {nameof(LabelForCustomers)}: {LabelForCustomers}, {nameof(RequiredForCustomers)}: {RequiredForCustomers}, {nameof(DisplayedToCustomers)}: {DisplayedToCustomers}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(PortalCc)}: {PortalCc}, {nameof(PortalCcTo)}: {PortalCcTo}, {nameof(IsFsm)}: {IsFsm}, {nameof(FieldUpdateInProgress)}: {FieldUpdateInProgress}";
    }
}
