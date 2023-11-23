using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Tickets.Models;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.Tickets.Requests;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CreateOutboundEmailRequest : IRequestWithAttachment, IRequestWithAdditionalMultipartFormDataContent
{
    private const string CustomFieldsName = "custom_fields";

    public CreateOutboundEmailRequest(TicketStatus status, TicketPriority priority, string subject, string description,
        string email, long emailConfigId, string? requesterName = null, string[]? ccEmails = null,
        Dictionary<string, object>? customFields = null, DateTimeOffset? dueBy = null,
        DateTimeOffset? firstResponseDueBy = null, long? groupId = null, string[]? tags = null,
        string? ticketType = null,
        IEnumerable<FileAttachment>? files = null)
    {
        Status = status;
        Priority = priority;
        RequesterName = requesterName;
        Email = email;
        Description = description;
        CcEmails = ccEmails;
        CustomFields = customFields;
        DueBy = dueBy;
        EmailConfigId = emailConfigId;
        FirstResponseDueBy = firstResponseDueBy;
        GroupId = groupId;
        Tags = tags;
        Subject = subject;
        TicketType = ticketType;
        Files = files;
    }

    /// <summary>
    /// Name of the requester.
    /// </summary>
    [JsonProperty("name")]
    public string? RequesterName { get; }

    /// <summary>
    /// Email address of the requester. If no contact exists with this email address in Freshdesk, it will be added as a new contact.
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; }

    /// <summary>
    /// Subject of the ticket. The default Value is <see langword="null"/>.
    ///
    /// Note:
    /// Call fails with <see langword="null"/> value.
    /// </summary>
    [JsonProperty("subject")]
    public string Subject { get; }

    /// <summary>
    /// Helps categorize the ticket according to the different kinds of issues your support team deals with. The default Value is <see langword="null"/>.
    /// </summary>
    [JsonProperty("type")]
    public string? TicketType { get; }

    /// <summary>
    /// Status of the ticket. The default Value is <see cref="TicketStatus.Closed"/>.
    /// </summary>
    [JsonProperty("status")]
    public TicketStatus Status { get; }

    /// <summary>
    /// Priority of the ticket. The default value is <see cref="TicketPriority.Low"/>.
    /// </summary>
    [JsonProperty("priority")]
    public TicketPriority Priority { get; }

    /// <summary>
    /// HTML content of the ticket.
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; }

    /// <summary>
    /// Email address added in the 'cc' field of the incoming ticket email.
    /// </summary>
    [JsonProperty("cc_emails")]
    public string[]? CcEmails { get; }

    /// <summary>
    /// Key value pairs containing the names and values of custom fields.
    /// </summary>
    [JsonProperty(CustomFieldsName)]
    [MultipartIgnore]
    public Dictionary<string, object>? CustomFields { get; }

    /// <summary>
    /// Timestamp that denotes when the ticket is due to be resolved.
    /// </summary>
    [JsonProperty("due_by")]
    public DateTimeOffset? DueBy { get; }

    /// <summary>
    /// ID of email config which is used for this ticket. (i.e., support@yourcompany.com/sales@yourcompany.com)
    /// </summary>
    [JsonProperty("email_config_id")]
    public long? EmailConfigId { get; }

    /// <summary>
    /// Timestamp that denotes when the first response is due.
    /// </summary>
    [JsonProperty("fr_due_by")]
    public DateTimeOffset? FirstResponseDueBy { get; }

    /// <summary>
    /// ID of the group to which the ticket has been assigned. The default value is the ID of the group that is associated with the given <see cref="EmailConfigId"/>.
    /// </summary>
    [JsonProperty("group_id")]
    public long? GroupId { get; }

    /// <summary>
    /// Tags that have been associated with the ticket.
    /// </summary>
    [JsonProperty("tags")]
    public string[]? Tags { get; }

    /// <summary>
    /// Ticket attachments. The total size of these attachments cannot exceed 20MB.
    /// </summary>
    [JsonIgnore, Multipart(Name = "attachments")]
    public IEnumerable<FileAttachment>? Files { get; }

    public bool IsMultipartFormDataRequired() => Files != null && Files.Any();

    public IEnumerable<(HttpContent HttpContent, string Name)> GetAdditionalMultipartFormDataContent()
    {
        foreach (var customField in CustomFields ?? [])
        {
            var key = $"{CustomFieldsName}[{customField.Key}]";
            var value = customField.Value?.ToString() ?? string.Empty;

            yield return (new StringContent(value), key);
        }
    }

    public override string ToString()
    {
        return $"{nameof(RequesterName)}: {RequesterName}, {nameof(Email)}: {Email}, {nameof(Subject)}: {Subject}, {nameof(TicketType)}: {TicketType}, {nameof(Status)}: {Status}, {nameof(Priority)}: {Priority}, {nameof(Description)}: {Description}, {nameof(CcEmails)}: {CcEmails}, {nameof(CustomFields)}: {CustomFields}, {nameof(DueBy)}: {DueBy}, {nameof(EmailConfigId)}: {EmailConfigId}, {nameof(FirstResponseDueBy)}: {FirstResponseDueBy}, {nameof(GroupId)}: {GroupId}, {nameof(Tags)}: {Tags}";
    }
}
