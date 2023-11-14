using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions;

/// <summary>
/// Thrown whenever a 401 auth failure message is hit whilst making an API
/// call.
///
/// c.f. https://developers.freshdesk.com/api/#introduction for details
/// </summary>
public class AuthenticationFailureException : FreshdeskApiException
{
    public AuthenticationFailureException(HttpResponseMessage response) : base(response)
    {
    }
}
