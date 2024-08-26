using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Attachments;
using FreshdeskApi.Client.CannedResponses;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.CustomObjects;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Me;
using FreshdeskApi.Client.Products;
using FreshdeskApi.Client.Roles;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FreshdeskApi.Client.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection AddFreshdeskApiClient(
        this IServiceCollection serviceCollection,
        Action<FreshdeskConfiguration> options,
        Action<IHttpClientBuilder>? configureHttpClientBuilder = null
    ) => serviceCollection
        .AddFreshdeskApiClient(configureHttpClientBuilder)
        .Configure(options)
        .Services;

    public static FreshdeskApiBuilder AddFreshdeskApiClient(
        this IServiceCollection serviceCollection,
        Action<IHttpClientBuilder>? configureHttpClientBuilder = null
    )
    {
        serviceCollection.AddOptions();

        var httpClientBuilder = serviceCollection.AddHttpClient<IFreshdeskHttpClient, FreshdeskHttpClient>(ConfigureFreshdeskHttpClient);
        configureHttpClientBuilder?.Invoke(httpClientBuilder);

        return new FreshdeskApiBuilder(serviceCollection
            .AddScoped<IFreshdeskAgentClient, FreshdeskAgentClient>()
            .AddScoped<IFreshdeskAttachmentsClient, FreshdeskAttachmentsClient>()
            .AddScoped<IFreshdeskCannedResponseClient, FreshdeskCannedResponseClient>()
            .AddScoped<IFreshdeskChannelApiClient, FreshdeskChannelApiClient>()
            .AddScoped<IFreshdeskCompaniesClient, FreshdeskCompaniesClient>()
            .AddScoped<IFreshdeskContactClient, FreshdeskContactClient>()
            .AddScoped<IFreshdeskConversationsClient, FreshdeskConversationsClient>()
            .AddScoped<IFreshdeskCustomObjectClient, FreshdeskCustomObjectClient>()
            .AddScoped<IFreshdeskGroupClient, FreshdeskGroupClient>()
            .AddScoped<IFreshdeskMeClient, FreshdeskMeClient>()
            .AddScoped<IFreshdeskProductClient, FreshdeskProductClient>()
            .AddScoped<IFreshdeskRoleClient, FreshdeskRoleClient>()
            .AddScoped<IFreshdeskSolutionClient, FreshdeskSolutionClient>()
            .AddScoped<IFreshdeskTicketClient, FreshdeskTicketClient>()
            .AddScoped<IFreshdeskTicketFieldsClient, FreshdeskTicketFieldsClient>()
            .AddScoped<IFreshdeskClient, FreshdeskClient>()
        );
    }

    public static void ConfigureFreshdeskHttpClient(IServiceProvider serviceProvider, HttpClient httpClient)
    {
        var options = serviceProvider.GetRequiredService<IOptions<FreshdeskConfiguration>>();

        var freshdeskConfiguration = options.Value;

        httpClient.ConfigureHttpClient(freshdeskConfiguration);
    }

    public static HttpClient ConfigureHttpClient(this HttpClient httpClient, FreshdeskConfiguration freshdeskConfiguration)
    {
        var apiKey = string.IsNullOrWhiteSpace(freshdeskConfiguration.ApiKey)
            ? throw new ArgumentOutOfRangeException(nameof(freshdeskConfiguration.ApiKey), freshdeskConfiguration.ApiKey, "API Key can't be blank")
            : freshdeskConfiguration.ApiKey;
        var freshdeskDomain = string.IsNullOrWhiteSpace(freshdeskConfiguration.FreshdeskDomain)
            ? throw new ArgumentOutOfRangeException(nameof(freshdeskConfiguration.FreshdeskDomain), freshdeskConfiguration.FreshdeskDomain, "Freshdesk domain can't be blank")
            : freshdeskConfiguration.FreshdeskDomain;

        httpClient.BaseAddress = new Uri(freshdeskDomain, UriKind.Absolute);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(Encoding.Default.GetBytes($"{apiKey}:X"))
        );

        return httpClient;
    }
}
