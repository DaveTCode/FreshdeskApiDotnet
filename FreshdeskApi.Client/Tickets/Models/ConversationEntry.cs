using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FreshdeskApi.Client.Tickets.Models
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConversationEntry
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("body_text")]
        public string BodyText { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("incoming")]
        public bool Incoming { get; set; }

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }

        [JsonPropertyName("support_email")]
        public string SupportEmail { get; set; }

        [JsonPropertyName("source")]
        public TicketSource Source { get; set; }

        [JsonPropertyName("category")]
        public long Category { get; set; }

        [JsonPropertyName("ticket_id")]
        public long TicketId { get; set; }

        [JsonPropertyName("to_emails")]
        public string[] ToEmails { get; set; }

        [JsonPropertyName("from_email")]
        public string FromEmail { get; set; }

        [JsonPropertyName("cc_emails")]
        public string[] CcEmails { get; set; }

        [JsonPropertyName("bcc_emails")]
        public string[] BccEmails { get; set; }

        [JsonPropertyName("email_failure_count")]
        public long? EmailFailureCount { get; set; }

        [JsonPropertyName("outgoing_failures")]
        public long? OutgoingFailures { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{nameof(BodyText)}: {BodyText}, {nameof(Id)}: {Id}, {nameof(Incoming)}: {Incoming}, {nameof(Private)}: {Private}, {nameof(UserId)}: {UserId}, {nameof(SupportEmail)}: {SupportEmail}, {nameof(Source)}: {Source}, {nameof(Category)}: {Category}, {nameof(TicketId)}: {TicketId}, {nameof(ToEmails)}: {ToEmails}, {nameof(FromEmail)}: {FromEmail}, {nameof(CcEmails)}: {CcEmails}, {nameof(BccEmails)}: {BccEmails}, {nameof(EmailFailureCount)}: {EmailFailureCount}, {nameof(OutgoingFailures)}: {OutgoingFailures}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(SourceAdditionalInfo)}: {SourceAdditionalInfo}";
        }

        [JsonPropertyName("source_additional_info")]
        public string SourceAdditionalInfo { get; set; }
    }
}
