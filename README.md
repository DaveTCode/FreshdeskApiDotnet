[![Build Status](https://dev.azure.com/DavidATyler/Freshdesk%20API/_apis/build/status/DaveTCode.FreshdeskApiDotnet?branchName=master)](https://dev.azure.com/DavidATyler/Freshdesk%20API/_build/latest?definitionId=3&branchName=master)

# Freshdesk API Client

This is a dotnet standard library providing a thin wrapper around the Freshdesk API as described here: https://developers.freshdesk.com/api.

At present this library requires .NET Standard 2.1 (for IAsyncEnumerable), if I get interest then I'll build a version of the library which
doesn't make use of that feature and is therefore available in .NET Standard 2.0 (or possibly lower)

## Usage

This library provides a single client class which can be created in one of two ways:

1. No existing HttpClient object (suitable for console applications)

```
using var freshdeskClient = new FreshdeskClient("https://mydomain.freshdesk.com", "APIKEY");
```

NOTE: Disposing the freshdeskClient will dispose the HttpClient object, as per https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/ you need to be careful when disposing HttpClient
objects. Broadly speaking, don't make and dispose lots of FreshdeskClient objects using this model.

2. Existing HttpClient object (suitable for asp.net applications or cases where you want more control over the HttpClient)

```
var freshdeskClient = new FreshdeskClient(myHttpClient);
```

NOTE: Typically you don't want to dispose the freshdesk client in this case.

### Examples

Get a single ticket, including the company information on the API response
```
using var freshdeskClient = new FreshdeskClient("https://mydomain.freshdesk.com", "APIKEY");
var ticket = await freshdeskClient.Tickets.ViewTicketAsync(ticketId: 12345, includes: new TicketIncludes { Company = true });
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
Roles|:x:
Groups|:heavy_check_mark:
Companies|:heavy_check_mark:
Canned Response Folders|:x:
Discussions|:x:
Solutions|:heavy_check_mark:
Surveys|:x:
Satisfaction Ratings|:x:
Field Service Management|:x:
Time Entries|:x:
Email Configs|:x:
Email Mailboxes|:x:
Products|:x:
Business Hours|:x:
Scenario Automations|:x:
SLA Policies|:x:
Settings|:x:

## Development

The library utilises C#8 features and therefore VS2019 or a suitable text editor are required for making changes.

Please feel free to send pull requests or raise Github issues.