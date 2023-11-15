using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FreshdeskApi.Client.Extensions;

public sealed class FreshdeskApiBuilder
{
    public IServiceCollection Services { get; }

    public FreshdeskApiBuilder(
        IServiceCollection services
    )
    {
        Services = services;
    }

    public FreshdeskApiBuilder Configure(
        Action<FreshdeskConfiguration> options
    )
    {
        Services.Configure(options);

        return this;
    }

    public FreshdeskApiBuilder OptionsBuilder(
        Action<OptionsBuilder<FreshdeskConfiguration>> optionsBuilder
    )
    {
        optionsBuilder(Services.AddOptions<FreshdeskConfiguration>());

        return this;
    }
}
