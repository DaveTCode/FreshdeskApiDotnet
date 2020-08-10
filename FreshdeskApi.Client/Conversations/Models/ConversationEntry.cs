using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

#pragma warning disable 8618

namespace FreshdeskApi.Client.Conversations.Models
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConversationEntry
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("body_text")]
        public string BodyText { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("incoming")]
        public bool Incoming { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("support_email")]
        public string SupportEmail { get; set; }

        [JsonProperty("source")]
        public ConversationSource Source { get; set; }

        [JsonProperty("category")]
        public long Category { get; set; }

        [JsonProperty("ticket_id")]
        public long TicketId { get; set; }

        [JsonProperty("to_emails")]
        public string[] ToEmails { get; set; }

        [JsonProperty("from_email")]
        public string FromEmail { get; set; }

        [JsonProperty("cc_emails")]
        public string[] CcEmails { get; set; }

        [JsonProperty("bcc_emails")]
        public string[] BccEmails { get; set; }

        [JsonProperty("email_failure_count")]
        public long? EmailFailureCount { get; set; }

        [JsonProperty("outgoing_failures")]
        public long? OutgoingFailures { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("source_additional_info")]
        public string SourceAdditionalInfo { get; set; }

        public override string ToString()
        {
            return $"{nameof(BodyText)}: {BodyText}, {nameof(Id)}: {Id}, {nameof(Incoming)}: {Incoming}, {nameof(Private)}: {Private}, {nameof(UserId)}: {UserId}, {nameof(SupportEmail)}: {SupportEmail}, {nameof(Source)}: {Source}, {nameof(Category)}: {Category}, {nameof(TicketId)}: {TicketId}, {nameof(ToEmails)}: {ToEmails}, {nameof(FromEmail)}: {FromEmail}, {nameof(CcEmails)}: {CcEmails}, {nameof(BccEmails)}: {BccEmails}, {nameof(EmailFailureCount)}: {EmailFailureCount}, {nameof(OutgoingFailures)}: {OutgoingFailures}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(SourceAdditionalInfo)}: {SourceAdditionalInfo}";
        }
    }
}
