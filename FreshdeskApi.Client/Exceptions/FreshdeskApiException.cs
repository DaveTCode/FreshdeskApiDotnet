using System;
using System.IO;
using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions;

/// <summary>
/// Thrown whenever an unexpected failure status code is hit talking to the
/// Freshdesk API.
///
/// Will always contain <see cref="HttpResponseMessage"/> with the
/// request/response details.
/// </summary>
public abstract class FreshdeskApiException : Exception, IDisposable
{
    /// <summary>
    /// The HTTP response which Freshdesk returned, will often contain
    /// useful diagnostics about the failure.
    /// </summary>
    public HttpResponseMessage Response { get; }

    public string ResponseMessage { get; }
    
    internal FreshdeskApiException(HttpResponseMessage response)
    {
        try
        {
            using var messageStream = new StreamReader(response.Content.ReadAsStream());
            ResponseMessage = messageStream.ReadToEnd();
        }
        catch (Exception) { /* not important move on */ }

        Response = response;
    }

    public void Dispose()
    {
        Response.Dispose();
    }
}
