using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Contacts.Requests;

/// <summary>
/// A class containing the fields passed back for the exported contacts.
///
/// Only enter the fields wanted from the exported contacts.
///
/// c.f. https://developers.freshdesk.com/api/#export_contact
/// </summary>
public class ContactsExportRequest
{
    /// <summary>
    /// Fields including default fields and custom fields, all of which are mandatory
    /// </summary>
    [JsonProperty("fields")]
    public Fields AllFields { get; }

    public class Fields
    {
        /// <summary>
        /// The default fields in Freshdesk
        /// </summary>
        [JsonProperty("default_fields")]
        public List<string> DefaultFields { get; }

        /// <summary>
        /// The custom fields in Freshdesk
        /// </summary>
        [JsonProperty("custom_fields")]
        public List<string> CustomFields { get; }

        public Fields(List<string> defaultFields, List<string> customFields)
        {
            DefaultFields = defaultFields;
            CustomFields = customFields;
        }

        public override string ToString()
        {
            return $"{nameof(DefaultFields)}: {DefaultFields}, {nameof(CustomFields)}: {CustomFields}";
        }
    }

    public ContactsExportRequest(List<string> defaultFields, List<string> customFields)
    {
        AllFields = new Fields(defaultFields, customFields);
    }

    public override string ToString()
    {
        return $"{nameof(AllFields)}: {AllFields}";
    }
}
