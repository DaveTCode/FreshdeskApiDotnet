using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Tickets.Models;
using Newtonsoft.Json;
using TiberHealth.Serializer.Attributes;

namespace FreshdeskApi.Client.Tickets.Requests
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class CreateOutboundEmailRequest : IRequestWithAttachment
    {
        public CreateOutboundEmailRequest(TicketStatus status, TicketPriority priority, string? subject, string description,
            string email, long emailConfigId, string? requesterName = null, string[]? ccEmails = null,
            Dictionary<string, object>? customFields = null, DateTimeOffset? dueBy = null,
            DateTimeOffset? firstResponseDueBy = null, long? groupId = null, string[]? tags = null,
            long? companyId = null, string? ticketType = null,
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

        /// Name of the requester
        [JsonProperty("name")]
        public string? RequesterName { get; }

        /// Email address of the requester. If no contact exists with this email address in Freshdesk, it will be added as a new contact.
        [JsonProperty("email")]
        public string? Email { get; }

        /// Subject of the ticket. The default Value is null.
        [JsonProperty("subject")]
        public string? Subject { get; }

        /// Helps categorize the ticket according to the different kinds of issues your support team deals with. The default Value is null.
        [JsonProperty("type")]
        public string? TicketType { get; }

        /// Status of the ticket. The default Value is 2.
        [JsonProperty("status")]
        public TicketStatus Status { get; }

        /// Priority of the ticket. The default value is 1.
        [JsonProperty("priority")]
        public TicketPriority Priority { get; }

        /// HTML content of the ticket.
        [JsonProperty("description")]
        public string? Description { get; }

        /// Key value pairs containing the names and values of custom fields.
        [JsonProperty("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; }

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

        /// Tags that have been associated with the ticket
        [JsonProperty("tags")]
        public string[]? Tags { get; }

        /// Email address added in the 'cc' field of the incoming ticket email
        [JsonProperty("cc_emails")]
        public string[]? CcEmails { get; }

        [JsonIgnore, Multipart(Name = "attachments")]
        public IEnumerable<FileAttachment>? Files { get; }

        public bool IsMultipartFormDataRequired() => Files != null && Files.Any();

        public override string ToString()
        {
            return $"{nameof(RequesterName)}: {RequesterName}, {nameof(Email)}: {Email}, {nameof(Subject)}: {Subject}, {nameof(TicketType)}: {TicketType}, {nameof(Status)}: {Status}, {nameof(Priority)}: {Priority}, {nameof(Description)}: {Description}, {nameof(CcEmails)}: {CcEmails}, {nameof(CustomFields)}: {CustomFields}, {nameof(DueBy)}: {DueBy}, {nameof(EmailConfigId)}: {EmailConfigId}, {nameof(FirstResponseDueBy)}: {FirstResponseDueBy}, {nameof(GroupId)}: {GroupId}, {nameof(Tags)}: {Tags}";
        }
    }
}
