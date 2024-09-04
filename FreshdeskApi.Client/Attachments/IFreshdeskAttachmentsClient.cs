using System.Threading;
using System.Threading.Tasks;

namespace FreshdeskApi.Client.Attachments;

public interface IFreshdeskAttachmentsClient
{
    /// <summary>
    /// Delete an attachment by their id.
    ///
    /// c.f. https://developer.freshdesk.com/api/#delete_an_attachment
    /// </summary>
    ///
    /// <param name="agentId">
    /// The unique identifier for the attachment.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    Task DeleteAttachmentAsync(
        long agentId,
        CancellationToken cancellationToken = default);
}
