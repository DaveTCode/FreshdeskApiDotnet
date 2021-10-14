using System.Net.Http;

namespace FreshdeskApi.Client.Exceptions
{
    public class GeneralApiException : FreshdeskApiException
    {
        public GeneralApiException(HttpResponseMessage response) : base(response)
        {
        }
    }
}
