using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreshdeskApi.Client.Me;

/// <inheritdoc />
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskMeClient : IFreshdeskMeClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskMeClient(IFreshdeskHttpClient freshdeskClient)
    {
            _freshdeskClient = freshdeskClient;
        }

    /// <inheritdoc />
    public async Task<Models.Me> ViewMeAsync(
        CancellationToken cancellationToken = default)
    {
            return await _freshdeskClient
                .ApiOperationAsync<Models.Me>(HttpMethod.Get, $"/api/v2/agents/me", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
}