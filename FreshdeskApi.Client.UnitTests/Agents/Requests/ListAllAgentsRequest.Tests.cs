using FreshdeskApi.Client.Agents.Requests;
using Xunit;

namespace FreshdeskApi.Client.UnitTests.Agents.Requests;

public class ListAllAgentsRequestTests
{
    [Fact]
    public void TestBlankQueryStringGeneration()
    {
        var listAllAgentsRequest = new ListAllAgentsRequest();

        Assert.Equal("UrlWithQueryString: /api/v2/agents", listAllAgentsRequest.ToString());
    }
}