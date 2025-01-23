using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions;

public class ResourceConflictException : FreshdeskApiException
{
    public ResourceConflictException(HttpResponseMessage response) : base(response)
    {
    }
}
