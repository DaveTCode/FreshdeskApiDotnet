namespace FreshdeskApi.Client.TicketFields.Requests
{
    /// <summary>
    /// Contains the information to update a section
    /// 
    /// c.f. https://developers.freshdesk.com/api/#create_section
    /// </summary>
    public class UpdateSectionRequest
    {
        /// <summary>
        /// Display the name of the section
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Choice IDs for which the section to be displayed
        /// </summary>
        public long[] ChoiceIds { get; }

        public UpdateSectionRequest(string label, long[] choiceIds)
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
