using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Conversations.Models;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// Refers to a single ticket within Freshdesk.
    ///
    /// c.f. https://developers.freshdesk.com/api/#tickets
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class Ticket
    {
        [JsonProperty("cc_emails")]
        public string[]? CcEmails { get; set; }

        [JsonProperty("fwd_emails")]
        public string[]? FwdEmails { get; set; }

        [JsonProperty("reply_cc_emails")]
        public string[]? ReplyCcEmails { get; set; }

        [JsonProperty("ticket_cc_emails")]
        public string[]? TicketCcEmails { get; set; }

        [JsonProperty("fr_escalated")]
        public bool FirstResponseTimeEscalated { get; set; }

        [JsonProperty("spam")]
        public bool Spam { get; set; }

        [JsonProperty("email_config_id")]
        public long? EmailConfigId { get; set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        [JsonProperty("priority")]
        public TicketPriority Priority { get; set; }

        [JsonProperty("requester_id")]
        public long RequesterId { get; set; }

        [JsonProperty("responder_id")]
        public long? ResponderId { get; set; }

        [JsonProperty("source")]
        public TicketSource Source { get; set; }

        [JsonProperty("company_id")]
        public long? CompanyId { get; set; }

        [JsonProperty("status")]
        public TicketStatus Status { get; set; }

        [JsonProperty("subject")]
        public string? Subject { get; set; }

        [JsonProperty("association_type")]
        public TicketAssociationType? AssociationType { get; set; }

        [JsonProperty("associated_ticket_list")]
        public long[]? AssociatedTicketList { get; set; }

        [JsonProperty("to_emails")]
        public string[]? ToEmails { get; set; }

        [JsonProperty("product_id")]
        public long? ProductId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("due_by")]
        public DateTimeOffset DueBy { get; set; }

        [JsonProperty("fr_due_by")]
        public DateTimeOffset FirstResponseDueBy { get; set; }

        [JsonProperty("is_escalated")]
        public bool IsEscalated { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("description_text")]
        public string? DescriptionText { get; set; }

        [JsonProperty("custom_fields")]
        public Dictionary<string, object?>? CustomFields { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("tags")]
        public string[]? Tags { get; set; }

        [JsonProperty("attachments")]
        public AttachmentResponse[]? Attachments { get; set; }

        [JsonProperty("source_additional_info")]
        public string? SourceAdditionalInfo { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonProperty("stats")]
        public TicketStats? Stats { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonProperty("requester")]
        public Requester? Requester { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonProperty("conversations")]
        public ConversationEntry[]? Conversations { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonProperty("company")]
        public TicketCompany? Company { get; set; }

        public override string ToString()
        {
            return $"{nameof(CcEmails)}: {CcEmails}, {nameof(FwdEmails)}: {FwdEmails}, {nameof(ReplyCcEmails)}: {ReplyCcEmails}, {nameof(TicketCcEmails)}: {TicketCcEmails}, {nameof(FirstResponseTimeEscalated)}: {FirstResponseTimeEscalated}, {nameof(Spam)}: {Spam}, {nameof(EmailConfigId)}: {EmailConfigId}, {nameof(GroupId)}: {GroupId}, {nameof(Priority)}: {Priority}, {nameof(RequesterId)}: {RequesterId}, {nameof(ResponderId)}: {ResponderId}, {nameof(Source)}: {Source}, {nameof(CompanyId)}: {CompanyId}, {nameof(Status)}: {Status}, {nameof(Subject)}: {Subject}, {nameof(AssociationType)}: {AssociationType}, {nameof(AssociatedTicketList)}: {AssociatedTicketList}, {nameof(ToEmails)}: {ToEmails}, {nameof(ProductId)}: {ProductId}, {nameof(Id)}: {Id}, {nameof(Type)}: {Type}, {nameof(DueBy)}: {DueBy}, {nameof(FirstResponseDueBy)}: {FirstResponseDueBy}, {nameof(IsEscalated)}: {IsEscalated}, {nameof(DescriptionText)}: {DescriptionText}, {nameof(CustomFields)}: {CustomFields}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(Tags)}: {Tags}, {nameof(Attachments)}: {Attachments}, {nameof(SourceAdditionalInfo)}: {SourceAdditionalInfo}, {nameof(Deleted)}: {Deleted}, {nameof(Stats)}: {Stats}, {nameof(Requester)}: {Requester}, {nameof(Conversations)}: {Conversations}, {nameof(Company)}: {Company}";
        }
    }
}
