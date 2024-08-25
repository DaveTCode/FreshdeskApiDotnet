using System;
using System.Web;

namespace FreshdeskApi.Client.CustomObjects.Requests;

/// <summary>
/// See: https://developers.freshdesk.com/api/#filter_records_of_custom_objects
/// </summary>
public class RecordPageRequestFilter(string fieldName, RecordPageRequestFilterOperator @operator, string value)
{
    /// <summary>
    /// The filtered property
    /// Case sensitive. It seems like you need to use an all lower-case string.
    /// You can also apply filter on created_time and updated_time
    ///
    /// NOTE: The field must be marked as filterable in the freshdesk custom object editor
    /// </summary>
    public string FieldName { get; set; } = fieldName;

    public RecordPageRequestFilterOperator Operator { get; set; } = @operator;

    /// <summary>
    /// To filter records using a Lookup field, the ID of the Object should be specified here.
    ///     - For Tickets, display_id should be used as the field value
    ///     - For Contacts, org_contact_id should be used as the field value.
    ///     - For Companies, org_company_id should be used as the field value
    ///     - For Custom Objects, id of the record should be used as the Lookup field value
    /// </summary>
    public string Value { get; set; } = value;

    public string QueryStringParameterName => $"{FieldName}{Operator.ToOperatorString()}";
}

public enum RecordPageRequestFilterOperator
{
    Equals,
    GreaterThan,
    LessThan,
    GreaterThanOrEqual,
    LessThanOrEqual
}

public static class RecordPageRequestFilterOperatorExtentions
{
    public static string ToOperatorString(this RecordPageRequestFilterOperator @operator) =>
        @operator switch
        {
            RecordPageRequestFilterOperator.Equals => "",
            RecordPageRequestFilterOperator.GreaterThan => "%5Bgt%5D",
            RecordPageRequestFilterOperator.LessThan => "%5Blt%5D",
            RecordPageRequestFilterOperator.GreaterThanOrEqual => "%5Bgte%5D",
            RecordPageRequestFilterOperator.LessThanOrEqual => "\t%5Blte%5D",
            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        };
}
