using System.Collections.Generic;
using FreshdeskApi.Client.CommonModels;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.Conversations.Requests;

/// <summary>
/// Defines the properties required when adding a note to a ticket.
///
/// c.f. https://developers.freshdesk.com/api/#add_note_to_a_ticket
/// </summary>
public class CreateNoteRequest
{
    /// <summary>
    /// Content of the note in HTML
    /// </summary>
    [JsonProperty("body")]
    public string BodyHtml { get; }

    /// <summary>
    /// Set to true if a particular note should appear as being created
    /// from outside (i.e., not through web portal). The default value is
    /// false
    /// </summary>
    [JsonProperty("incoming")]
    public bool? Incoming { get; }

    /// <summary>
    /// Email addresses of agents/users who need to be notified about this
    /// note.
    /// </summary>
    [JsonProperty("notify_emails")]
    public string[]? NotifyEmails { get; }

    /// <summary>
    /// Set to true if the note is private. The default value is true.
    /// </summary>
    [JsonProperty("private")]
    public bool? IsPrivate { get; }

    /// <summary>
    /// ID of the agent/user who is adding the note
    ///
    /// Defaults to the API user.
    /// </summary>
    [JsonProperty("user_id")]
    public long? UserId { get; }

    [JsonIgnore, Multipart(Name = "attachments")]
    public IEnumerable<FileAttachment>? Files { get; }

    public CreateNoteRequest(string bodyHtml, bool? incoming = null, string[]? notifyEmails = null, bool? isPrivate = null, long? userId = null,
        IEnumerable<FileAttachment>? files = null)
    {
        BodyHtml = bodyHtml;
        Incoming = incoming;
        NotifyEmails = notifyEmails;
        IsPrivate = isPrivate;
        UserId = userId;
        Files = files;
    }

    public override string ToString()
    {
        return $"{nameof(BodyHtml)}: {BodyHtml}, {nameof(Incoming)}: {Incoming}, {nameof(NotifyEmails)}: {NotifyEmails}, {nameof(IsPrivate)}: {IsPrivate}, {nameof(UserId)}: {UserId}";
    }
}
