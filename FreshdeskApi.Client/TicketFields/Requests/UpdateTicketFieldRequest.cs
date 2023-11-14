using Newtonsoft.Json;

namespace FreshdeskApi.Client.TicketFields.Requests;

/// <summary>
/// Provides the ability to update ticket fields.
///
/// c.f. https://developers.freshdesk.com/api/#update_ticket_field
/// </summary>
public class UpdateTicketFieldRequest
{
    /// <summary>
    /// To specify whether the customer can edit the ticket field
    /// </summary>
    [JsonProperty("customers_can_edit")]
    public bool? CustomersCanEdit { get; }

    /// <summary>
    /// Ticket Field name to be displayed to customers
    /// </summary>
    [JsonProperty("label_for_customer")]
    public string? LabelForCustomers { get; }

    /// <summary>
    /// Display Ticket Field to customers
    /// </summary>
    [JsonProperty("displayed_to_customers")]
    public bool? DisplayedToCustomers { get; }

    /// <summary>
    /// Display the name of the Ticket Field
    /// </summary>
    [JsonProperty("label")]
    public string? Label { get; }

    /// <summary>
    /// Ticket Field type. Can be custom_dropdown, custom_checkbox, custom_text, etc...
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; }

    /// <summary>
    /// Position in which the ticket field is displayed in the form. If not given, it will be displayed on top
    /// </summary>
    [JsonProperty("position")]
    public long? Position { get; }

    /// <summary>
    /// Set to true if the field is mandatory for closing the ticket
    /// </summary>
    [JsonProperty("required_for_closure")]
    public bool? RequiredForClosure { get; }

    /// <summary>
    /// Set to true if the field is mandatory for Agents
    /// </summary>
    [JsonProperty("required_for_agents")]
    public bool? RequiredForAgents { get; }

    /// <summary>
    /// Set to true if the field is mandatory in the customer portal
    /// </summary>
    [JsonProperty("required_for_customers")]
    public bool? RequiredForCustomers { get; }

    /// <summary>
    /// Api docs claim:
    /// "Array of key, value pairs containing the value and position of dropdown choices"
    /// but I'm dubious since the choice field can mean different things for different tickets
    /// </summary>
    [JsonProperty("choices")]
    public object? Choices { get; }


    public UpdateTicketFieldRequest(bool? customersCanEdit = null, string? labelForCustomers = null, bool? displayedToCustomers = null,
        string? label = null, string? type = null, long? position = null, bool? requiredForClosure = null, bool? requiredForAgents = null,
        bool? requiredForCustomers = null, object? choices = null)
    {
            CustomersCanEdit = customersCanEdit;
            LabelForCustomers = labelForCustomers;
            DisplayedToCustomers = displayedToCustomers;
            Label = label;
            Type = type;
            Position = position;
            RequiredForClosure = requiredForClosure;
            RequiredForAgents = requiredForAgents;
            RequiredForCustomers = requiredForCustomers;
            Choices = choices;
        }
}
