using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FreshdeskApi.Client.Agents;
using FreshdeskApi.Client.Channel;
using FreshdeskApi.Client.Companies;
using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Conversations;
using FreshdeskApi.Client.Groups;
using FreshdeskApi.Client.Solutions;
using FreshdeskApi.Client.TicketFields;
using FreshdeskApi.Client.Tickets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FreshdeskApi.Client.Extensions
{
    public static class IocExtensions
    {
        public static IServiceCollection AddFreshdeskApiClient(
            this IServiceCollection serviceCollection,
            Action<FreshdeskConfiguration> options,
            Action<IHttpClientBuilder>? configureHttpClientBuilder = null
        )
        {
            serviceCollection.Configure(options);

            return serviceCollection.AddFreshdeskApiClient(configureHttpClientBuilder);
        }

        public static IServiceCollection AddFreshdeskApiClient(
            this IServiceCollection serviceCollection,
            Action<IHttpClientBuilder>? configureHttpClientBuilder = null
        )
        {
            serviceCollection.AddOptions();

            var httpClientBuilder = serviceCollection.AddHttpClient<IFreshdeskHttpClient, FreshdeskHttpClient>(ConfigureFreshdeskHttpClient);
            configureHttpClientBuilder?.Invoke(httpClientBuilder);

            serviceCollection.AddScoped<IFreshdeskTicketClient, FreshdeskTicketClient>();
            serviceCollection.AddScoped<IFreshdeskContactClient, FreshdeskContactClient>();
            serviceCollection.AddScoped<IFreshdeskGroupClient, FreshdeskGroupClient>();
            serviceCollection.AddScoped<IFreshdeskAgentClient, FreshdeskAgentClient>();
            serviceCollection.AddScoped<IFreshdeskCompaniesClient, FreshdeskCompaniesClient>();
            serviceCollection.AddScoped<IFreshdeskSolutionClient, FreshdeskSolutionClient>();
            serviceCollection.AddScoped<IFreshdeskTicketFieldsClient, FreshdeskTicketFieldsClient>();
            serviceCollection.AddScoped<IFreshdeskConversationsClient, FreshdeskConversationsClient>();
            serviceCollection.AddScoped<IFreshdeskChannelApiClient, FreshdeskChannelApiClient>();

            serviceCollection.AddScoped<IFreshdeskClient, FreshdeskClient>();

            return serviceCollection;
        }

        public static void ConfigureFreshdeskHttpClient(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            var options = serviceProvider.GetRequiredService<IOptions<FreshdeskConfiguration>>();

            var freshdeskConfiguration = options.Value;

            httpClient.BaseAddress = new Uri(freshdeskConfiguration.FreshdeskDomain, UriKind.Absolute);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.Default.GetBytes($"{freshdeskConfiguration.ApiKey}:X"))
            );
        }

        public class FreshdeskConfiguration
        {
            public string FreshdeskDomain { get; set; } = null!;

            public string ApiKey { get; set; } = null!;
        }
    }
}
