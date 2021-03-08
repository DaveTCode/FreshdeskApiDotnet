using System.Collections.Generic;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

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

        [JsonIgnore, Multipart(Name = "attachments")]
        public IEnumerable<FileAttachment>? Files { get; }

        public UpdateNoteRequest(string bodyHtml, IEnumerable<FileAttachment>? files = null)
        {
            BodyHtml = bodyHtml;
            Files = files;
        }

        public override string ToString()
        {
            return $"{nameof(BodyHtml)}: {BodyHtml}";
        }
    }
}
