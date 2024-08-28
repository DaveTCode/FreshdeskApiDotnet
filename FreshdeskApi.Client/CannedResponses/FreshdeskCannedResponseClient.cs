using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CannedResponses.Models;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Models;
using FreshdeskApi.Client.Pagination;

namespace FreshdeskApi.Client.CannedResponses;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskCannedResponseClient : IFreshdeskCannedResponseClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskCannedResponseClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <summary>
    /// Retrieve all details about a single canned response by id.
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_canned_response
    /// </summary>
    /// <param name="cannedResponseId">
    /// The unique identifier for the canned response.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full canned response details</returns>
    public async Task<CannedResponse> ViewCannedResponseAsync(
        long cannedResponseId,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<CannedResponse>(HttpMethod.Get, $"/api/v2/canned_responses/{cannedResponseId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// To view all canned responses in a folder.
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_canned_response_folders
    /// </summary>
    ///
    /// <param name="folderId">
    /// The unique identifier for the folder.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// Folder with id and title of the canned responses within it
    /// </returns>

    public async Task<CannedResponseFolder> ListCannedResponsesInFolderAsync(
        long folderId,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
           .ApiOperationAsync<CannedResponseFolder>(HttpMethod.Get, $"/api/v2/canned_response_folders/{folderId}", cancellationToken: cancellationToken)
           .ConfigureAwait(false);
    }

    /// <summary>
    /// To view all the details of canned responses in a folder.
    ///
    /// c.f. https://developers.freshdesk.com/api/#get_details_of_canned_responses_in_folders
    /// </summary>
    ///
    /// <param name="folderId">
    /// The unique identifier for the folder.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The detail of the canned responses contained in the folder.
    /// </returns>
    public async IAsyncEnumerable<CannedResponse> GetDetailedCannedResponsesInFolderAsync(
        long folderId,
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var response in _freshdeskClient
            .GetPagedResults<CannedResponse>($"/api/v2/canned_response_folders/{folderId}/responses", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return response;
        }
    }

}
