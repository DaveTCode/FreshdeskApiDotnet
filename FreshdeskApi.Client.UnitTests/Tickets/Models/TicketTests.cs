using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.Tickets.Models;

public sealed class TicketTests
{
    [Fact]
    public void DeserializeTicketTest()
    {
        var stream = new MemoryStream();

        using (var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true))
        {
            writer.Write(ExampleTicketJson);
        }

        stream.Position = 0;

        using var reader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(reader);
        var serializer = new JsonSerializer();

        // Act
        var ticket = serializer.Deserialize<FreshdeskApi.Client.Tickets.Models.Ticket>(jsonReader);

        // Assert
        Assert.NotNull(ticket);
        Assert.Equal("This is a test ticket.", ticket.DescriptionText);
    }

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string ExampleTicketJson =
        """
        {
            "cc_emails": [],
            "fwd_emails": [],
            "reply_cc_emails": [],
            "ticket_cc_emails": [],
            "ticket_bcc_emails": [],
            "fr_escalated": false,
            "spam": false,
            "email_config_id": null,
            "group_id": null,
            "priority": 2,
            "requester_id": 36012621950,
            "responder_id": null,
            "source": 2,
            "company_id": null,
            "status": 2,
            "subject": "A test ticket",
            "support_email": null,
            "to_emails": null,
            "product_id": null,
            "id": 49548,
            "type": "Question",
            "due_by": null,
            "fr_due_by": null,
            "is_escalated": false,
            "description": "<div>This is a test ticket.</div>",
            "description_text": "This is a test ticket.",
            "custom_fields": {},
            "created_at": "2025-01-08T09:43:49Z",
            "updated_at": "2025-01-08T09:43:50Z",
            "tags": [
                "test-tag"
            ],
            "attachments": [],
            "form_id": 298,
            "nr_due_by": null,
            "nr_escalated": false
        }
        """;
}
