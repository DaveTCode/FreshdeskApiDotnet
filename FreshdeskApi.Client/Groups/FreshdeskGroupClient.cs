using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Groups.Models;
using FreshdeskApi.Client.Groups.Requests;
using FreshdeskApi.Client.Models;
using FreshdeskApi.Client.Pagination;

namespace FreshdeskApi.Client.Groups;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskGroupClient : IFreshdeskGroupClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskGroupClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <summary>
    /// Retrieve all details about a single group by its id.
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_group
    /// </summary>
    /// <param name="groupId">
    /// The unique identifier for the group.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full group information</returns>
    public async Task<Group> ViewGroupAsync(
        long groupId,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Group>(HttpMethod.Get, $"/api/v2/groups/{groupId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// List all available groups
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_groups
    /// </summary>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of groups, this request is paged and iterating to the
    /// next entry may cause a new API call to get the next page.
    /// </returns>
    public async IAsyncEnumerable<Group> ListAllGroupsAsync(
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var group in _freshdeskClient
            .GetPagedResults<Group>("/api/v2/groups", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return group;
        }
    }

    /// <summary>
    /// Delete a group from the Freshdesk instance.
    ///
    /// Note:
    /// 1. Deleting a Group will only disband the group and not delete its members.
    /// 2. Deleted groups cannot be restored.
    ///
    /// c.f. https://developers.freshdesk.com/api/#delete_group
    /// </summary>
    ///
    /// <param name="groupId">
    /// The unique group identifier.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    public async Task DeleteGroupAsync(
        long groupId,
        CancellationToken cancellationToken = default)
    {
        await _freshdeskClient
            .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/groups/{groupId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a new group of agents
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_group
    /// </summary>
    ///
    /// <param name="request">
    /// The properties to set on the new group.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The newly created group</returns>
    public async Task<Group> CreateGroupAsync(
        CreateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _freshdeskClient
            .ApiOperationAsync<Group, CreateGroupRequest>(HttpMethod.Post, "/api/v2/groups", request, cancellationToken)
            .ConfigureAwait(false);
    }
}
