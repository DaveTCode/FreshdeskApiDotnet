using System.Collections.Generic;
using System.Linq;
using FreshdeskApi.Client.CommonModels;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.Conversations.Requests
{
    /// <summary>
    /// Set the properties required for creating a new reply to a ticket.
    ///
    /// c.f. https://developers.freshdesk.com/api/#reply_ticket
    /// </summary>
    public class CreateReplyRequest : IRequestWithAttachment
    {
        /// <summary>
        /// Content of the note in HTML format
        /// </summary>
        [JsonProperty("body")]
        public string BodyHtml { get; }

        /// <summary>
        /// The email address from which the reply is sent. By default the
        /// global support email will be used.
        /// </summary>
        [JsonProperty("from_email")]
        public string? FromEmail { get; }

        /// <summary>
        /// ID of the agent who is adding the note.
        ///
        /// By default will use the API user.
        /// </summary>
        [JsonProperty("user_id")]
        public long? UserId { get; }

        /// <summary>
        /// Email address added in the 'cc' field of the outgoing ticket email.
        /// </summary>
        [JsonProperty("cc_emails")]
        public string[]? CcEmails { get; }

        /// <summary>
        /// Email address added in the 'bcc' field of the outgoing ticket
        /// email.
        /// </summary>
        [JsonProperty("bcc_emails")]
        public string[]? BccEmails { get; }

        [JsonIgnore, Multipart(Name = "attachments")]
        public IEnumerable<FileAttachment>? Files { get; }

        public CreateReplyRequest(string bodyHtml, string? fromEmail = null, long? userId = null, string[]? ccEmails = null, string[]? bccEmails = null,
            IEnumerable<FileAttachment>? files = null)
        {
            BodyHtml = bodyHtml;
            FromEmail = fromEmail;
            UserId = userId;
            CcEmails = ccEmails;
            BccEmails = bccEmails;
            Files = files;
        }

        public bool IsMultipartFormDataRequired() => Files != null && Files.Any();

        public override string ToString()
        {
            return $"{nameof(BodyHtml)}: {BodyHtml}, {nameof(FromEmail)}: {FromEmail}, {nameof(UserId)}: {UserId}, {nameof(CcEmails)}: {CcEmails}, {nameof(BccEmails)}: {BccEmails}";
        }
    }
}
