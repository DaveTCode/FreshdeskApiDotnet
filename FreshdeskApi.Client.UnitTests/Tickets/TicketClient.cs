using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Unicode;
using System.Threading.Tasks;
using FreshdeskApi.Client.Tickets;
using FreshdeskApi.Client.Tickets.Models;
using FreshdeskApi.Client.Tickets.Requests;
using Newtonsoft.Json;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.Tickets
{
    public class TicketClient
    {
        [Fact]
        public void TestMultipart()
        {
            var request = new CreateTicketWithFilesRequest(
                Client.Tickets.Models.TicketStatus.Open,
                Client.Tickets.Models.TicketPriority.Medium,
                Client.Tickets.Models.TicketSource.Email,
                "Test Description",
                new List<FileAttachment>(
                    new[]
                    {
                        new FileAttachment
                        {
                           Name = "Test 1",
                           FileBytes = System.Text.Encoding.Default.GetBytes("This is a test")
                        }
                    }),
                    email: "test@xyz.com"
                );

            var multiPart = FreshdeskClient.GetMultipartContent(request);
            Assert.NotNull(multiPart);
        }
    }
}
