using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Roles.Models;

namespace FreshdeskApi.Client.Roles;

public interface IFreshdeskRoleClient
{
    /// <summary>
    /// Retrieve all details about a single role by its id.
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_role
    /// </summary>
    /// <param name="roleId">
    /// The unique identifier for the role.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full role information</returns>
    Task<Role> ViewRoleAsync(
        long roleId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List all available roles
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_roles
    /// </summary>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of roles, this request is paged and iterating to the
    /// next entry may cause a new API call to get the next page.
    /// </returns>
    IAsyncEnumerable<Role> ListAllRolesAsync(
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);
}
