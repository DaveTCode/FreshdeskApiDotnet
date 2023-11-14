using System;
using System.Collections.Generic;
using System.Linq;
using FreshdeskApi.Client.Contacts.Models;

namespace FreshdeskApi.Client.Contacts.Requests;

/// <summary>
/// Generates a URL which can filter the list of contacts
///
/// c.f. https://developers.freshdesk.com/api/#list_all_contacts
/// </summary>
public class ListAllContactsRequest
{
    private const string ListAllContactsUrl = "/api/v2/contacts";

    internal string UrlWithQueryString { get; }

    public ListAllContactsRequest(
        string? email = null,
        string? mobile = null,
        string? phone = null,
        long? companyId = null,
        ContactState? contactState = null,
        DateTimeOffset? updatedSince = null
    )
    {
            var urlParams = new Dictionary<string, string?>
            {
                { "email", email },
                { "mobile", mobile },
                { "phone", phone },
                { "company_id", companyId?.ToString() },
                { "state", contactState?.GetQueryStringValue() },
                { "_updated_since", updatedSince?.ToString("yyyy-MM-ddTHH:mm:ssZ") }
            }.Where(x => x.Value != null)
                .Select(queryParam => $"{queryParam.Key}={Uri.EscapeDataString(queryParam.Value!)}")
                .ToList();

            UrlWithQueryString = ListAllContactsUrl +
                   (urlParams.Any() ? "?" + string.Join("&", urlParams) : "");
        }

    public override string ToString()
    {
            return $"{nameof(UrlWithQueryString)}: {UrlWithQueryString}";
        }
}
