using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Companies.Requests
{
    /// <summary>
    /// A class containing the fields passed back for the exported companies.
    ///
    /// Only enter the fields wanted from the exported companies.
    /// 
    /// c.f. https://developers.freshdesk.com/api/#export_company
    /// </summary>
    public class CompaniesExportRequest
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

        public CompaniesExportRequest(List<string> defaultFields, List<string> customFields)
        {
            AllFields = new Fields(defaultFields, customFields);
        }

        public override string ToString()
        {
            return $"{nameof(AllFields)}: {AllFields}";
        }
    }
}
