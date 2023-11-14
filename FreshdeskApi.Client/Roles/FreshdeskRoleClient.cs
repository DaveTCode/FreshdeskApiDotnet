using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Roles.Models;

namespace FreshdeskApi.Client.Roles;

/// <inheritdoc />
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskRoleClient : IFreshdeskRoleClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskRoleClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <inheritdoc />
    public async Task<Role> ViewRoleAsync(
        long roleId,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Role>(HttpMethod.Get, $"/api/v2/roles/{roleId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<Role> ListAllRolesAsync(
        IPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var role in _freshdeskClient
            .GetPagedResults<Role>("/api/v2/roles", pagingConfiguration, false, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return role;
        }
    }
}
