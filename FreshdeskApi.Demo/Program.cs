using FreshdeskApi.Client.Contacts;
using FreshdeskApi.Client.Contacts.Requests;
using FreshdeskApi.Client.Exceptions;
using FreshdeskApi.Client.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(static configurationBuilder => configurationBuilder
        .AddJsonFile("appsettings.json5", optional: false)
        .AddJsonFile("user.appsettings.json5", optional: false)
    )
    .ConfigureServices(static services => services
        .AddFreshdeskApiClient()
        .OptionsBuilder(optionsBuilder => optionsBuilder
            .BindConfiguration(configSectionPath: "Freshdesk")
            .ValidateOnStart()
        )
    )
    .Build();

using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(20));
var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{
    await using var serviceScope = host.Services.CreateAsyncScope();
    var freshdeskContactClient = serviceScope.ServiceProvider.GetRequiredService<IFreshdeskContactClient>();

    await foreach (
        var contact in freshdeskContactClient.ListAllContactsAsync(
            new ListAllContactsRequest(
                email: null,
                phone: null
            ),
            new PagingConfiguration(1, 2),
            cancellationToken: cancellationTokenSource.Token
        )
    )
    {
        logger.LogInformation("Found a contact {ContactName}", contact.Name);
    }
}
catch (InvalidFreshdeskRequest ex)
{
    await using var contentStream = await ex.Response.Content.ReadAsStreamAsync(cancellationTokenSource.Token);
    using var streamReader = new StreamReader(contentStream);
    var message = await streamReader.ReadToEndAsync();

    logger.LogError(ex, "List contacts failed with following message: {Message}", message);
}
finally
{
    host.Dispose();
}
