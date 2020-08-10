using System;
using System.Collections.Generic;
using System.Linq;
using FreshdeskApi.Client.Agents.Models;

namespace FreshdeskApi.Client.Agents.Requests
{
    /// <summary>
    /// Constructs a request which can filter the list of agents
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_agents
    /// </summary>
    public class ListAllAgentsRequest
    {
        private const string ListAllContactsUrl = "/api/v2/agents";

        internal string UrlWithQueryString { get; }

        public ListAllAgentsRequest(
            string? email = null,
            string? mobile = null,
            string? phone = null,
            AgentState? agentState = null)
        {
            var urlParams = new Dictionary<string, string?>
            {
                { "email", email },
                { "mobile", mobile },
                { "phone", phone },
                { "state", agentState?.GetQueryStringValue() }
            }.Where(x => x.Value != null)
                .Select(queryParam => $"{queryParam.Key}={Uri.EscapeDataString(queryParam.Value)}")
                .ToList();

            UrlWithQueryString = ListAllContactsUrl +
                   (urlParams.Any() ? "?" + string.Join("&", urlParams) : "");
        }

        public override string ToString()
        {
            return $"{nameof(UrlWithQueryString)}: {UrlWithQueryString}";
        }
    }
}
