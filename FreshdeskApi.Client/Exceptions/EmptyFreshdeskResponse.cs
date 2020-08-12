using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions
{
    /// <summary>
    /// Thrown whenever a 204 no content is hit whilst making an API call.
    /// </summary>
    public class EmptyFreshdeskResponse : FreshdeskApiException
    {
        internal EmptyFreshdeskResponse(HttpResponseMessage response) : base(response)
        {
        }
    }
}
