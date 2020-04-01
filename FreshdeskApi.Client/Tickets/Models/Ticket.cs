using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("cc_emails")]
        public string[] CcEmails { get; set; }

        [JsonPropertyName("fwd_emails")]
        public string[] FwdEmails { get; set; }

        [JsonPropertyName("reply_cc_emails")]
        public string[] ReplyCcEmails { get; set; }

        [JsonPropertyName("ticket_cc_emails")]
        public string[] TicketCcEmails { get; set; }

        [JsonPropertyName("fr_escalated")]
        public bool FirstResponseTimeEscalated { get; set; }

        [JsonPropertyName("spam")]
        public bool Spam { get; set; }

        [JsonPropertyName("email_config_id")]
        public long? EmailConfigId { get; set; }

        [JsonPropertyName("group_id")]
        public long? GroupId { get; set; }

        [JsonPropertyName("priority")]
        public TicketPriority Priority { get; set; }

        [JsonPropertyName("requester_id")]
        public long RequesterId { get; set; }

        [JsonPropertyName("responder_id")]
        public long? ResponderId { get; set; }

        [JsonPropertyName("source")]
        public TicketSource Source { get; set; }

        [JsonPropertyName("company_id")]
        public long? CompanyId { get; set; }

        [JsonPropertyName("status")]
        public TicketStatus Status { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("association_type")]
        public TicketAssociationType? AssociationType { get; set; }

        [JsonPropertyName("associated_ticket_list")]
        public long[] AssociatedTicketList { get; set; }

        [JsonPropertyName("to_emails")]
        public string[] ToEmails { get; set; }

        [JsonPropertyName("product_id")]
        public long? ProductId { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("due_by")]
        public DateTimeOffset DueBy { get; set; }

        [JsonPropertyName("fr_due_by")]
        public DateTimeOffset FirstResponseDueBy { get; set; }

        [JsonPropertyName("is_escalated")]
        public bool IsEscalated { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("description_text")]
        public string DescriptionText { get; set; }

        [JsonPropertyName("custom_fields")]
        public Dictionary<string, string> CustomFields { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }

        [JsonPropertyName("attachments")]
        public object[] Attachments { get; set; }

        [JsonPropertyName("source_additional_info")]
        public string SourceAdditionalInfo { get; set; }

        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonPropertyName("stats")]
        public TicketStats Stats { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonPropertyName("requester")]
        public Requester Requester { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonPropertyName("conversations")]
        public ConversationEntry[] Conversations { get; set; }

        /// <summary>
        /// Optional include, excluded by default and will therefore be null.
        /// </summary>
        [JsonPropertyName("company")]
        public TicketCompany Company { get; set; }

        public override string ToString()
        {
            return $"{nameof(CcEmails)}: {CcEmails}, {nameof(FwdEmails)}: {FwdEmails}, {nameof(ReplyCcEmails)}: {ReplyCcEmails}, {nameof(TicketCcEmails)}: {TicketCcEmails}, {nameof(FirstResponseTimeEscalated)}: {FirstResponseTimeEscalated}, {nameof(Spam)}: {Spam}, {nameof(EmailConfigId)}: {EmailConfigId}, {nameof(GroupId)}: {GroupId}, {nameof(Priority)}: {Priority}, {nameof(RequesterId)}: {RequesterId}, {nameof(ResponderId)}: {ResponderId}, {nameof(Source)}: {Source}, {nameof(CompanyId)}: {CompanyId}, {nameof(Status)}: {Status}, {nameof(Subject)}: {Subject}, {nameof(AssociationType)}: {AssociationType}, {nameof(AssociatedTicketList)}: {AssociatedTicketList}, {nameof(ToEmails)}: {ToEmails}, {nameof(ProductId)}: {ProductId}, {nameof(Id)}: {Id}, {nameof(Type)}: {Type}, {nameof(DueBy)}: {DueBy}, {nameof(FirstResponseDueBy)}: {FirstResponseDueBy}, {nameof(IsEscalated)}: {IsEscalated}, {nameof(DescriptionText)}: {DescriptionText}, {nameof(CustomFields)}: {CustomFields}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(Tags)}: {Tags}, {nameof(Attachments)}: {Attachments}, {nameof(SourceAdditionalInfo)}: {SourceAdditionalInfo}, {nameof(Deleted)}: {Deleted}, {nameof(Stats)}: {Stats}, {nameof(Requester)}: {Requester}, {nameof(Conversations)}: {Conversations}, {nameof(Company)}: {Company}";
        }
    }
}
