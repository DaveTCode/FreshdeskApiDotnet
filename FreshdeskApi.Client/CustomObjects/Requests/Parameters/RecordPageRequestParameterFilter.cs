using FreshdeskApi.Client.CommonModels;

namespace FreshdeskApi.Client.CustomObjects.Requests.Parameters;

/// <summary>
/// See: https://developers.freshdesk.com/api/#filter_records_of_custom_objects
/// </summary>
public record RecordPageRequestParameterFilter(
    string FieldName,
    EFilterOperator FilterOperator,
    string Value
)
{
    /// <summary>
    /// The filtered property
    /// Case sensitive. It seems like you need to use an all lower-case string.
    /// You can also apply filter on created_time and updated_time
    ///
    /// NOTE: The field must be marked as filterable in the freshdesk custom object editor
    /// </summary>
    public string FieldName { get; } = FieldName;

    /// <summary>
    /// To filter records using a Lookup field, the ID of the Object should be specified here.
    ///     - For Tickets, display_id should be used as the field value
    ///     - For Contacts, org_contact_id should be used as the field value.
    ///     - For Companies, org_company_id should be used as the field value
    ///     - For Custom Objects, id of the record should be used as the Lookup field value
    /// </summary>
    public string Value { get; } = Value;

    public string QueryStringParameterName => $"{FieldName}{FilterOperator.ToQueryParameter()}";
}
