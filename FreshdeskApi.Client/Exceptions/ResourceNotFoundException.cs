using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions
{
    /// <summary>
    /// Thrown whenever a 404 resource not found message is hit whilst making
    /// an API call.
    ///
    /// c.f. https://developers.freshdesk.com/api/#introduction for details
    /// </summary>
    public class ResourceNotFoundException : FreshdeskApiException
    {
        public ResourceNotFoundException(HttpResponseMessage response) : base(response)
        {
        }
    }
}
