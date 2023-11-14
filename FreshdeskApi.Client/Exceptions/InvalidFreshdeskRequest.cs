using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions;

/// <summary>
/// Thrown whenever a 400 bad request is hit whilst making an API call.
///
/// Further details are probably in the Response object.
///
/// c.f. https://developers.freshdesk.com/api/#introduction for details
/// </summary>
public class InvalidFreshdeskRequest : FreshdeskApiException
{
    internal InvalidFreshdeskRequest(HttpResponseMessage response) : base(response)
    {
    }
}
