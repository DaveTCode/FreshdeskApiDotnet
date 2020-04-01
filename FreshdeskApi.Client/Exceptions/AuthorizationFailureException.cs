using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions
{
    /// <summary>
    /// Thrown whenever a 403 unauthorized message is hit whilst making an API
    /// call.
    ///
    /// c.f. https://developers.freshdesk.com/api/#introduction for details
    /// </summary>
    public class AuthorizationFailureException : FreshdeskApiException
    {
        public AuthorizationFailureException(HttpResponseMessage response) : base(response)
        {
        }
    }
}
