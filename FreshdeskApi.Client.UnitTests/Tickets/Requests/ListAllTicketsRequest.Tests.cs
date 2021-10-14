using FreshdeskApi.Client.Tickets.Requests;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.Tickets.Requests
{
    public class ListAllTicketsRequestTests
    {
        [Fact]
        public void TestBlankQueryStringGeneration()
        {
            var listAllTicketsRequest = new ListAllTicketsRequest();

            Assert.Equal("/api/v2/tickets", listAllTicketsRequest.UrlWithQueryString);
        }
    }
}
