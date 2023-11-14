using System;
using System.Net.Http;
using FreshdeskApi.Client.Extensions;

namespace FreshdeskApi.Client;

public static class HttpClientConfigurator
{
    [Obsolete("Use " + nameof(IocExtensions.ConfigureHttpClient) + " instead")]
    public static HttpClient ConfigureFreshdeskApi(
        this HttpClient httpClient, string freshdeskDomain, string apiKey
    ) => httpClient.ConfigureHttpClient(new IocExtensions.FreshdeskConfiguration
    {
        FreshdeskDomain = freshdeskDomain,
        ApiKey = apiKey,
    });
}
