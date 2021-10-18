using System;
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
        public static IServiceCollection AddFreshdeskApiClient(this IServiceCollection serviceCollection, Action<FreshdeskConfiguration> options)
        {
            serviceCollection.Configure(options);

            return serviceCollection.AddFreshdeskApiClient();
        }

        public static IServiceCollection AddFreshdeskApiClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions();

            serviceCollection.AddHttpClient<IFreshdeskHttpClient, FreshdeskHttpClient>(static (serviceProvider, httpClient) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<FreshdeskConfiguration>>();

                var freshdeskConfiguration = options.Value;

                httpClient.BaseAddress = new Uri(freshdeskConfiguration.FreshdeskDomain, UriKind.Absolute);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.Default.GetBytes($"{freshdeskConfiguration.ApiKey}:X"))
                );
            });

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

        public class FreshdeskConfiguration
        {
            public string FreshdeskDomain { get; set; } = null!;

            public string ApiKey { get; set; } = null!;
        }
    }
}
