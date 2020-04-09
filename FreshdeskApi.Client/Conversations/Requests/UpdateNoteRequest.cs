namespace FreshdeskApi.Client.Conversations.Requests
{
    /// <summary>
    /// Defines an update request on a private/public note.
    ///
    /// c.f. https://developers.freshdesk.com/api/#update_conversation
    /// </summary>
    public class UpdateNoteRequest
    {
        /// <summary>
        /// Content of the note in HTML
        /// </summary>
        public string BodyHtml { get; }

        public UpdateNoteRequest(string bodyHtml)
        {
            BodyHtml = bodyHtml;
        }

        public override string ToString()
        {
            return $"{nameof(BodyHtml)}: {BodyHtml}";
        }
    }
}
