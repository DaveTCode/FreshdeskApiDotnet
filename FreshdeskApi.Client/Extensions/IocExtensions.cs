using System;
using Microsoft.Extensions.DependencyInjection;

namespace FreshdeskApi.Client.Extensions
{
    public static class IocExtensions
    {
        public static IServiceCollection AddFreshdeskApiClient(this IServiceCollection serviceCollection, Action<FreshdeskConfiguration> options)
        {
            serviceCollection.AddHttpClient<IFreshdeskHttpClient, FreshdeskHttpClient>();
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
