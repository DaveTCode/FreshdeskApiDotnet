using Newtonsoft.Json;

namespace FreshdeskApi.Client.TicketFields.Requests;

public class CreateTicketFieldRequest
{
    [JsonProperty("customers_can_edit")]
    public bool? CustomersCanEdit { get; }

    [JsonProperty("label_for_customers")]
    public string? LabelForCustomers { get; }

    [JsonProperty("displayed_to_customers")]
    public bool? DisplayedToCustomers { get; }

    [JsonProperty("label")]
    public string? Label { get; }

    [JsonProperty("type")]
    public string? FieldType { get; }

    [JsonProperty("position")]
    public long? Position { get; }

    [JsonProperty("required_for_closure")]
    public bool? RequiredForClosure { get; }

    [JsonProperty("required_for_agents")]
    public bool? RequiredForAgents { get; }

    [JsonProperty("required_for_customers")]
    public bool? RequiredForCustomers { get; }

    [JsonProperty("choices")]
    public object? Choices { get; }

    public CreateTicketFieldRequest(bool? customersCanEdit = null, string? labelForCustomers = null, bool? displayedToCustomers = null, string? label = null, string? fieldType = null,
        long? position = null, bool? requiredForClosure = null, bool? requiredForAgents = null, bool? requiredForCustomers = null, object? choices = null)
    {
            CustomersCanEdit = customersCanEdit;
            LabelForCustomers = labelForCustomers;
            DisplayedToCustomers = displayedToCustomers;
            Label = label;
            FieldType = fieldType;
            Position = position;
            RequiredForClosure = requiredForClosure;
            RequiredForAgents = requiredForAgents;
            RequiredForCustomers = requiredForCustomers;
            Choices = choices;
        }

    public override string ToString()
    {
            return $"{nameof(CustomersCanEdit)}: {CustomersCanEdit}, {nameof(LabelForCustomers)}: {LabelForCustomers}, {nameof(DisplayedToCustomers)}: {DisplayedToCustomers}, {nameof(Label)}: {Label}, {nameof(FieldType)}: {FieldType}, {nameof(Position)}: {Position}, {nameof(RequiredForClosure)}: {RequiredForClosure}, {nameof(RequiredForAgents)}: {RequiredForAgents}, {nameof(RequiredForCustomers)}: {RequiredForCustomers}, {nameof(Choices)}: {Choices}";
        }
}
