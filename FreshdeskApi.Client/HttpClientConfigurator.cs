using System;
using System.Net.Http;
using FreshdeskApi.Client.Extensions;

namespace FreshdeskApi.Client;

public static class HttpClientConfigurator
{
    [Obsolete("Use " + nameof(DependencyExtensions.ConfigureHttpClient) + " instead")]
    public static HttpClient ConfigureFreshdeskApi(
        this HttpClient httpClient, string freshdeskDomain, string apiKey
    ) => httpClient.ConfigureHttpClient(new FreshdeskConfiguration
    {
        FreshdeskDomain = freshdeskDomain,
        ApiKey = apiKey,
    });
}
