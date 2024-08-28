using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Agents.Models;
using FreshdeskApi.Client.Agents.Requests;

namespace FreshdeskApi.Client.Agents;

public interface IFreshdeskAgentClient
{
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
    Task<Agent> ViewAgentAsync(
        long agentId,
        CancellationToken cancellationToken = default);

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
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of agents matching the filters supplied, this
    /// request is paged and iterating to the next entry may cause a new
    /// API call to get the next page.
    /// </returns>
    IAsyncEnumerable<Agent> ListAllAgentsAsync(
        ListAllAgentsRequest request,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    Task DeleteAgentAsync(
        long agentId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Create an agent and the underlying contact.
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_agent
    /// </summary>
    /// 
    /// <param name="request">
    /// The request object containing the information to set on the agent
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The newly populated agent</returns>
    Task<Agent> CreateAgentAsync(
        CreateAgentRequest request,
        CancellationToken cancellationToken = default);
}
