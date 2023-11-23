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

/// <summary>
/// An UpdateTicketRequest is an object which describes what updates are
/// required for a ticket. Any object set to null will be left unchanged.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class UpdateTicketRequest : IRequestWithAttachment, IRequestWithAdditionalMultipartFormDataContent
{
    private const string CustomFieldsName = "custom_fields";

    public UpdateTicketRequest(TicketStatus? status = null, TicketPriority? priority = null, TicketSource? source = null,
        string? description = null, string? requesterName = null, long? requesterId = null, string? email = null, string? facebookId = null,
        string? phoneNumber = null, string? twitterId = null, string? uniqueExternalId = null, long? responderId = null,
        Dictionary<string, string?>? customFields = null, DateTimeOffset? dueBy = null, long? emailConfigId = null,
        DateTimeOffset? firstResponseDueBy = null, long? groupId = null, long? productId = null, string[]? tags = null,
        long? companyId = null, string? subject = null, string? ticketType = null, IEnumerable<FileAttachment>? files = null)
    {
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
        Files = files;
    }

    /// Name of the requester
    [JsonProperty("name")]
    public string? RequesterName { get; }

    /// User ID of the requester. For existing contacts, the requester_id can be passed instead of the requester email.
    [JsonProperty("requester_id")]
    public long? RequesterId { get; }

    /// Email address of the requester. If no contact exists with this email address in Freshdesk, it will be added as a new contact.
    [JsonProperty("email")]
    public string? Email { get; }

    /// Facebook ID of the requester. If no contact exists with this facebook_id, then a new contact will be created.
    [JsonProperty("facebook_id")]
    public string? FacebookId { get; }

    /// Phone number of the requester. If no contact exists with this phone number in Freshdesk, it will be added as a new contact. If the phone number is set and the email address is not, then the name attribute is mandatory.
    [JsonProperty("phone")]
    public string? PhoneNumber { get; }

    /// Twitter handle of the requester. If no contact exists with this handle in Freshdesk, it will be added as a new contact.
    [JsonProperty("twitter_id")]
    public string? TwitterId { get; }

    /// External ID of the requester. If no contact exists with this external ID in Freshdesk, they will be added as a new contact.
    [JsonProperty("unique_external_id")]
    public string? UniqueExternalId { get; }

    /// Subject of the ticket. The default Value is null.
    [JsonProperty("subject")]
    public string? Subject { get; }

    /// Helps categorize the ticket according to the different kinds of issues your support team deals with. The default Value is null.
    [JsonProperty("type")]
    public string? TicketType { get; }

    /// Status of the ticket. The default Value is 2.
    [JsonProperty("status")]
    public TicketStatus? Status { get; }

    /// Priority of the ticket. The default value is 1.
    [JsonProperty("priority")]
    public TicketPriority? Priority { get; }

    /// HTML content of the ticket.
    [JsonProperty("description")]
    public string? Description { get; }

    /// ID of the agent to whom the ticket has been assigned
    [JsonProperty("responder_id")]
    public long? ResponderId { get; }

    /// Key value pairs containing the names and values of custom fields.
    [JsonProperty(CustomFieldsName)]
    [MultipartIgnore]
    public Dictionary<string, string?>? CustomFields { get; }

    /// Timestamp that denotes when the ticket is due to be resolved
    [JsonProperty("due_by")]
    public DateTimeOffset? DueBy { get; }

    /// ID of email config which is used for this ticket. (i.e., support@yourcompany.com/sales@yourcompany.com)
    /// If product_id is given and email_config_id is not given, product's primary email_config_id will be set
    [JsonProperty("email_config_id")]
    public long? EmailConfigId { get; }

    /// Timestamp that denotes when the first response is due
    [JsonProperty("fr_due_by")]
    public DateTimeOffset? FirstResponseDueBy { get; }

    /// ID of the group to which the ticket has been assigned. The default value is the ID of the group that is associated with the given email_config_id
    [JsonProperty("group_id")]
    public long? GroupId { get; }

    /// ID of the product to which the ticket is associated.
    /// It will be ignored if the email_config_id attribute is set in the request.
    [JsonProperty("product_id")]
    public long? ProductId { get; }

    /// The channel through which the ticket was created. The default value is 2.
    [JsonProperty("source")]
    public TicketSource? Source { get; }

    /// Tags that have been associated with the ticket
    [JsonProperty("tags")]
    public string[]? Tags { get; }

    /// Company ID of the requester. This attribute can only be set if the Multiple Companies feature is enabled (Estate plan and above)
    [JsonProperty("company_id")]
    public long? CompanyId { get; }

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
        return $"{nameof(RequesterName)}: {RequesterName}, {nameof(RequesterId)}: {RequesterId}, {nameof(Email)}: {Email}, {nameof(FacebookId)}: {FacebookId}, {nameof(PhoneNumber)}: {PhoneNumber}, {nameof(TwitterId)}: {TwitterId}, {nameof(UniqueExternalId)}: {UniqueExternalId}, {nameof(Subject)}: {Subject}, {nameof(TicketType)}: {TicketType}, {nameof(Status)}: {Status}, {nameof(Priority)}: {Priority}, {nameof(Description)}: {Description}, {nameof(ResponderId)}: {ResponderId}, {nameof(CustomFields)}: {CustomFields}, {nameof(DueBy)}: {DueBy}, {nameof(EmailConfigId)}: {EmailConfigId}, {nameof(FirstResponseDueBy)}: {FirstResponseDueBy}, {nameof(GroupId)}: {GroupId}, {nameof(ProductId)}: {ProductId}, {nameof(Source)}: {Source}, {nameof(Tags)}: {Tags}, {nameof(CompanyId)}: {CompanyId}";
    }
}
