using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Groups.Models;

namespace FreshdeskApi.Client.Groups
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskGroupClient
    {
        private readonly FreshdeskClient _freshdeskClient;

        public FreshdeskGroupClient(FreshdeskClient freshdeskClient)
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
            return await _freshdeskClient.ApiOperationAsync<Group>(HttpMethod.Get, $"/api/v2/groups/{groupId}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// List all available groups
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_groups
        /// </summary>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The full set of groups, this request is paged and iterating to the
        /// next entry may cause a new API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<Group> ListAllGroupsAsync(
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var group in _freshdeskClient.GetPagedResults<Group>("/api/v2/groups", cancellationToken))
            {
                yield return group;
            }
        }
    }
}
