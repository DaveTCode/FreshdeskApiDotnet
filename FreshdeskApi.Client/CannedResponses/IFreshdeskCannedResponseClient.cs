using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CannedResponses.Models;

namespace FreshdeskApi.Client.CannedResponses;

public interface IFreshdeskCannedResponseClient
{

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
    Task<CannedResponse> ViewCannedResponseAsync(
        long cannedResponseId,
        CancellationToken cancellationToken = default);

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

    Task<CannedResponseFolder> ListCannedResponsesInFolderAsync(
        long folderId,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<CannedResponse> GetDetailedCannedResponsesInFolderAsync(
        long folderId,
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);
}
