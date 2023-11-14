using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Tickets.Models;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.Tickets.Requests;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CreateTicketRequest : IRequestWithAttachment
{
    public CreateTicketRequest(TicketStatus status, TicketPriority priority, TicketSource source, string description, string? requesterName = null,
        long? requesterId = null, string? email = null, string? facebookId = null, string? phoneNumber = null,
        string? twitterId = null, string? uniqueExternalId = null, long? responderId = null, string[]? ccEmails = null,
        Dictionary<string, object>? customFields = null, DateTimeOffset? dueBy = null, long? emailConfigId = null,
        DateTimeOffset? firstResponseDueBy = null, long? groupId = null, long? productId = null, string[]? tags = null,
        long? companyId = null, string? subject = null, string? ticketType = null, long? parentTicketId = null,
        IEnumerable<FileAttachment>? files = null)
    {
            if (!requesterId.HasValue && email == null && facebookId == null && phoneNumber == null && twitterId == null && uniqueExternalId == null)
            {
                throw new ArgumentException("You must set at least one of requesterId, email, facebookId, phoneNumber, twitterId, uniqueExternalId to denote the requester");
            }

            Status = status;
            Priority = priority;
            Source = source;
            RequesterName = requesterName;
            Email = email;
            FacebookId = facebookId;
            PhoneNumber = phoneNumber;
            TwitterId = twitterId;
            UniqueExternalId = uniqueExternalId;
            Description = description;
            RequesterId = requesterId;
            ResponderId = responderId;
            CcEmails = ccEmails;
            CustomFields = customFields;
            DueBy = dueBy;
            EmailConfigId = emailConfigId;
            FirstResponseDueBy = firstResponseDueBy;
            GroupId = groupId;
            ProductId = productId;
            Tags = tags;
            CompanyId = companyId;
            Subject = subject;
            TicketType = ticketType;
            ParentTicketId = parentTicketId;
            Files = files;
        }

    /// <summary>
    /// Name of the requester.
    /// </summary>
    [JsonProperty("name")]
    public string? RequesterName { get; }

    /// <summary>
    /// User ID of the requester. For existing contacts, the requester_id can be passed instead of the requester email.
    /// </summary>
    [JsonProperty("requester_id")]
    public long? RequesterId { get; }

    /// <summary>
    /// Email address of the requester. If no contact exists with this email address in Freshdesk, it will be added as a new contact.
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; }

    /// <summary>
    /// Facebook ID of the requester. If no contact exists with this facebook_id, then a new contact will be created.
    /// </summary>
    [JsonProperty("facebook_id")]
    public string? FacebookId { get; }

    /// <summary>
    /// Phone number of the requester. If no contact exists with this phone number in Freshdesk, it will be added as a new contact. If the phone number is set and the email address is not, then the name attribute is mandatory.
    /// </summary>
    [JsonProperty("phone")]
    public string? PhoneNumber { get; }

    /// <summary>
    /// Twitter handle of the requester. If no contact exists with this handle in Freshdesk, it will be added as a new contact.
    /// </summary>
    [JsonProperty("twitter_id")]
    public string? TwitterId { get; }

    /// <summary>
    /// External ID of the requester. If no contact exists with this external ID in Freshdesk, they will be added as a new contact.
    /// </summary>
    [JsonProperty("unique_external_id")]
    public string? UniqueExternalId { get; }

    /// <summary>
    /// Subject of the ticket. The default Value is <see langword="null"/>.
    /// </summary>
    [JsonProperty("subject")]
    public string? Subject { get; }

    /// <summary>
    /// Helps categorize the ticket according to the different kinds of issues your support team deals with. The default Value is <see langword="null"/>.
    /// </summary>
    [JsonProperty("type")]
    public string? TicketType { get; }

    /// <summary>
    /// Requires child/parent relationships to be enabled on your instance.
    /// </summary>
    [JsonProperty("parent_id")]
    public long? ParentTicketId { get; }

    /// <summary>
    /// Status of the ticket. The default Value is <see cref="TicketStatus.Open"/>.
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
    public string Description { get; }

    /// <summary>
    /// ID of the agent to whom the ticket has been assigned.
    /// </summary>
    [JsonProperty("responder_id")]
    public long? ResponderId { get; }

    /// <summary>
    /// Email address added in the 'cc' field of the incoming ticket email.
    /// </summary>
    [JsonProperty("cc_emails")]
    public string[]? CcEmails { get; }

    /// <summary>
    /// Key value pairs containing the names and values of custom fields.
    /// </summary>
    [JsonProperty("custom_fields")]
    public Dictionary<string, object>? CustomFields { get; }

    /// <summary>
    /// Timestamp that denotes when the ticket is due to be resolved.
    /// </summary>
    [JsonProperty("due_by")]
    public DateTimeOffset? DueBy { get; }

    /// <summary>
    /// ID of email config which is used for this ticket. (i.e., support@yourcompany.com/sales@yourcompany.com)
    /// If <see cref="ProductId"/> is given and <see cref="EmailConfigId"/> is not given, product's primary <see cref="EmailConfigId"/> will be set.
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
    /// ID of the product to which the ticket is associated.
    /// It will be ignored if the <see cref="EmailConfigId"/> attribute is set in the request.
    /// </summary>
    [JsonProperty("product_id")]
    public long? ProductId { get; }

    /// <summary>
    /// The channel through which the ticket was created. The default value is <see cref="TicketSource.Portal"/>.
    /// </summary>
    [JsonProperty("source")]
    public TicketSource Source { get; }

    /// <summary>
    /// Tags that have been associated with the ticket.
    /// </summary>
    [JsonProperty("tags")]
    public string[]? Tags { get; }

    /// <summary>
    /// Company ID of the requester. This attribute can only be set if the Multiple Companies feature is enabled (Estate plan and above).
    /// </summary>
    [JsonProperty("company_id")]
    public long? CompanyId { get; }

    /// <summary>
    /// Ticket attachments. The total size of these attachments cannot exceed 20MB.
    /// </summary>
    [JsonIgnore, Multipart(Name = "attachments")]
    public IEnumerable<FileAttachment>? Files { get; }

    public bool IsMultipartFormDataRequired() => Files != null && Files.Any();

    public override string ToString()
    {
            return $"{nameof(RequesterName)}: {RequesterName}, {nameof(RequesterId)}: {RequesterId}, {nameof(Email)}: {Email}, {nameof(FacebookId)}: {FacebookId}, {nameof(PhoneNumber)}: {PhoneNumber}, {nameof(TwitterId)}: {TwitterId}, {nameof(UniqueExternalId)}: {UniqueExternalId}, {nameof(Subject)}: {Subject}, {nameof(TicketType)}: {TicketType}, {nameof(ParentTicketId)}: {ParentTicketId}, {nameof(Status)}: {Status}, {nameof(Priority)}: {Priority}, {nameof(Description)}: {Description}, {nameof(ResponderId)}: {ResponderId}, {nameof(CcEmails)}: {CcEmails}, {nameof(CustomFields)}: {CustomFields}, {nameof(DueBy)}: {DueBy}, {nameof(EmailConfigId)}: {EmailConfigId}, {nameof(FirstResponseDueBy)}: {FirstResponseDueBy}, {nameof(GroupId)}: {GroupId}, {nameof(ProductId)}: {ProductId}, {nameof(Source)}: {Source}, {nameof(Tags)}: {Tags}, {nameof(CompanyId)}: {CompanyId}";
        }
}