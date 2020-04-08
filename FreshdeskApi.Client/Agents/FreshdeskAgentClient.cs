using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Agents.Models;
using FreshdeskApi.Client.Agents.Requests;

namespace FreshdeskApi.Client.Agents
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskAgentClient
    {
        private readonly FreshdeskClient _freshdeskClient;

        public FreshdeskAgentClient(FreshdeskClient freshdeskClient)
        {
            _freshdeskClient = freshdeskClient;
        }

        /// <summary>
        /// Retrieve all details about a single agent by their id.
        ///
        /// c.f. https://developers.freshdesk.com/api/#view_agent
        /// </summary>
        /// <param name="agentId">
        /// The unique identifier for the agent.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The full agent information</returns>
        public async Task<Agent> ViewAgentAsync(
            long agentId,
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<Agent>(HttpMethod.Get, $"/api/v2/agents/{agentId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Filter the list of agents according to a set of predefined filters.
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_agents
        /// </summary>
        /// 
        /// <param name="request">
        /// A <seealso cref="ListAllAgentsRequest"/> object which contains
        /// the filters that we want to apply. By default will include all
        /// agents.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The full set of agents matching the filters supplied, this
        /// request is paged and iterating to the next entry may cause a new
        /// API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<Agent> ListAllAgentsAsync(
            ListAllAgentsRequest request,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var agent in _freshdeskClient.GetPagedResults<Agent>(request.UrlWithQueryString, false, cancellationToken).ConfigureAwait(false))
            {
                yield return agent;
            }
        }

        /// <summary>
        /// Downgrade an agent to a contact by their id.
        ///
        /// c.f. https://developers.freshdesk.com/api/#delete_agent
        /// </summary>
        /// 
        /// <param name="agentId">
        /// The unique identifier for the agent.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        public async Task DeleteAgentAsync(
            long agentId,
            CancellationToken cancellationToken = default)
        {
            await _freshdeskClient
                .ApiOperationAsync<string>(HttpMethod.Delete, $"/api/v2/agents/{agentId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
