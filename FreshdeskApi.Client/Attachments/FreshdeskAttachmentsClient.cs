using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreshdeskApi.Client.Attachments
{
    /// <summary>
    /// API endpoints specific to attachments.
    ///
    /// c.f. https://developers.freshdesk.com/api/#agents for further details
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskAttachmentsClient : IFreshdeskAttachmentsClient
    {
        private readonly IFreshdeskHttpClient _freshdeskClient;

        public FreshdeskAttachmentsClient(IFreshdeskHttpClient freshdeskClient)
        {
            _freshdeskClient = freshdeskClient;
        }

        /// <summary>
        /// Delete an attachment by their id.
        ///
        /// c.f. https://developer.freshdesk.com/api/#delete_an_attachment
        /// </summary>
        /// 
        /// <param name="attachmentId">
        /// The unique identifier for the attachment.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        public async Task DeleteAttachmentAsync(
            long attachmentId,
            CancellationToken cancellationToken = default)
        {
            await _freshdeskClient
                .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/attachments/{attachmentId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
