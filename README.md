[![Build Status](https://github.com/DaveTCode/FreshdeskApiDotnet/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/DaveTCode/FreshdeskApiDotnet/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/Freshdesk.Api.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Freshdesk.Api/)
[![MyGet](https://img.shields.io/myget/freshdesk-api-dotnet/vpre/Freshdesk.Api?label=MyGet)](https://www.myget.org/feed/freshdesk-api-dotnet/package/nuget/Freshdesk.Api)


# Freshdesk API Client

This is a dotnet standard library providing a thin wrapper around the Freshdesk API as described here: https://developers.freshdesk.com/api.

At present this library requires .NET Standard 2.1 (for IAsyncEnumerable), if I get interest then I'll build a version of the library which
doesn't make use of that feature and is therefore available in .NET Standard 2.0 (or possibly lower)

## Usage

This library provides a single client class which can be created in one of several ways:

1. No existing HttpClient object (suitable for console applications)

```csharp
using var freshdeskHttpClient = FreshdeskHttpClient.Create("https://mydomain.freshdesk.com", "APIKEY");
var freshdeskClient = FreshdeskClient.Create(freshdeskHttpClient);
```

NOTE: Disposing the freshdeskClient will dispose the HttpClient object, as per https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ you need to be careful when disposing HttpClient
objects. Broadly speaking, don't make and dispose lots of FreshdeskClient objects using this model.

2. Existing HttpClient object (suitable for asp.net applications or cases where you want more control over the HttpClient)

```csharp
var freshdeskHttpClient = new FreshdeskHttpClient(myHttpClient);
var freshdeskClient = FreshdeskClient.Create(freshdeskHttpClient);
```

NOTE: Typically you don't want to dispose the freshdesk client in this case.

3. Using `Microsoft.Extensions.DependencyInjection`

```csharp
using FreshdeskApi.Client.Extensions;

serviceCollection
    .AddFreshdeskApiClient()
    .Configure(options => {
      options.FreshdeskDomain = "https://<mydomain>.freshdesk.com";
      options.ApiKey = "APIKEY"; 
    });

...

container.GetRequiredService<IFreshdeskClient>();
container.GetRequiredService<IFreshdeskTicketClient>();
container.GetRequiredService<IFreshdesk...Client>();
```

### Examples

Get a single ticket, including the company information on the API response
```csharp
using var freshdeskHttpClient = new FreshdeskHttpClient("https://mydomain.freshdesk.com", "APIKEY");
var freshdeskTicketClient = new FreshdeskTicketClient(freshdeskHttpClient);
var ticket = await freshdeskTicketClient.ViewTicketAsync(
  ticketId: 12345, 
  includes: new TicketIncludes { Company = true }
);
```

## API Coverage

Not all of the Freshdesk API is covered, this table illustrates the current status of coverage by this library. Pull requests to add additional features are welcome.

**API Area**|**Coverage**
:-----:|:-----:
Tickets|:heavy_check_mark:
Ticket Fields|:heavy_check_mark:
Conversations|:heavy_check_mark:
Contacts|:heavy_check_mark:
Agents|:heavy_check_mark:
Skills|:x:
Roles|:heavy_check_mark:
Groups|:heavy_check_mark:
Companies|:heavy_check_mark:
Canned Responses|:heavy_check_mark: (read only)
Discussions|:x:
Solutions|:heavy_check_mark:
Surveys|:x:
Satisfaction Ratings|:x:
Field Service Management|:x:
Time Entries|:x:
Email Configs|:x:
Email Mailboxes|:x:
Products|:heavy_check_mark:
Business Hours|:x:
Scenario Automations|:x:
SLA Policies|:x:
Settings|:x:
Custom Objects|:heavy_check_mark:

## Development

The library utilises C#11 features and therefore VS2022 or a suitable text editor are required for making changes.

Please feel free to send pull requests or raise Github issues.
