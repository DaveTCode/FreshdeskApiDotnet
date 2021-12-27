using System.Threading;
using System.Threading.Tasks;

namespace FreshdeskApi.Client.Me
{
    public interface IFreshdeskMeClient
    {
        /// <summary>
        /// Retrieve all details about a current agent, identified by the API-key.
        ///
        /// c.f. https://developers.freshdesk.com/api/#me
        /// </summary>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The full agent information</returns>
        Task<Models.Me> ViewMeAsync(
            CancellationToken cancellationToken = default);
    }
}
