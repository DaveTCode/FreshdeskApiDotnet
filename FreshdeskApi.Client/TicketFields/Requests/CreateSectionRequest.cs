using Newtonsoft.Json;

namespace FreshdeskApi.Client.TicketFields.Requests
{
    /// <summary>
    /// Contains the information to create a new section
    /// 
    /// c.f. https://developers.freshdesk.com/api/#create_section
    /// </summary>
    public class CreateSectionRequest
    {
        /// <summary>
        /// Display the name of the section
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; }

        /// <summary>
        /// Choice IDs for which the section to be displayed
        /// </summary>
        [JsonProperty("choice_ids")]
        public long[] ChoiceIds { get; }

        public CreateSectionRequest(string label, long[] choiceIds)
        {
            Label = label;
            ChoiceIds = choiceIds;
        }

        public override string ToString()
        {
            return $"{nameof(Label)}: {Label}, {nameof(ChoiceIds)}: {ChoiceIds}";
        }
    }
}
