using System.Collections.Generic;
using System.Linq;
using FreshdeskApi.Client.Agents.Models;

namespace FreshdeskApi.Client.Agents.Requests
{
    public class ListAllAgentsRequest
    {
        private const string ListAllContactsUrl = "/api/v2/agents";

        private readonly string _email;
        private readonly string _mobile;
        private readonly string _phone;
        private readonly AgentState? _agentState;

        public ListAllAgentsRequest(
            string email = null,
            string mobile = null,
            string phone = null,
            AgentState? agentState = null)
        {
            _email = email;
            _mobile = mobile;
            _phone = phone;
            _agentState = agentState;
        }

        internal string GetUrl()
        {
            var urlParams = new List<string>
            {
                (_email != null) ? $"email={_email}" : null,
                (_mobile != null) ? $"mobile={_mobile}" : null,
                (_phone != null) ? $"phone={_phone}" : null,
                (_agentState != null) ? $"state={_agentState.Value.GetQueryStringValue()}" : null,
            }.Select(x => x != null).ToList();

            return ListAllContactsUrl +
                   (urlParams.Any() ? "?" + string.Join("&", urlParams) : "");
        }
    }
}
