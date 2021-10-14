using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FreshdeskApi.Client
{
    public static class HttpClientConfigurator
    {
        public static HttpClient ConfigureFreshdeskApi(this HttpClient httpClient, string freshdeskDomain, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentOutOfRangeException(nameof(apiKey), apiKey, "API Key can't be blank");
            if (string.IsNullOrWhiteSpace(freshdeskDomain)) throw new ArgumentOutOfRangeException(nameof(freshdeskDomain), freshdeskDomain, "Freshdesk domain can't be blank");

            httpClient.BaseAddress = new Uri(freshdeskDomain, UriKind.Absolute);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes($"{apiKey}:X")));

            return httpClient;
        }
    }
}
