using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Groups.Models;
using FreshdeskApi.Client.Groups.Requests;

namespace FreshdeskApi.Client.Groups;

public interface IFreshdeskGroupClient
{
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
    Task<Group> ViewGroupAsync(
        long groupId,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<Group> ListAllGroupsAsync(
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    Task DeleteGroupAsync(
        long groupId,
        CancellationToken cancellationToken = default);

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
    Task<Group> CreateGroupAsync(
        CreateGroupRequest request,
        CancellationToken cancellationToken = default);
}
