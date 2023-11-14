using System;
using System.Collections.Generic;
using System.Linq;
using FreshdeskApi.Client.Tickets.Models;

namespace FreshdeskApi.Client.Tickets.Requests;

/// <summary>
/// When listing all tickets only certain filters can be applied, this
/// object restricts what searches can be done to reflect the API hard
/// limits.
///
/// c.f. https://developers.freshdesk.com/api/#list_all_tickets
/// </summary>
public class ListAllTicketsRequest
{
    private const string ListAllTicketsUrl = "/api/v2/tickets";

    internal string UrlWithQueryString { get; }

    public ListAllTicketsRequest(
        ListAllTicketsFilter? filter = null,
        long? requesterId = null,
        string? requesterEmail = null,
        long? companyId = null,
        DateTimeOffset? updatedSince = null,
        TicketIncludes? includes = default,
        TicketOrderBy? orderBy = default,
        TicketOrderDirection? orderDir = default)
    {
        var urlParams = new Dictionary<string, string?>
            {
                { "filter", filter?.QueryParameterValue() },
                { "requester_id", requesterId?.ToString() },
                { "email", requesterEmail },
                { "company_id", companyId?.ToString() },
                { "updated_since", updatedSince?.ToString("yyyy-MM-ddTHH:mm:ssZ") },
                { "include", includes?.ToString() },
                { "order_by", orderBy?.QueryParameterValue() },
                { "order_type", orderDir?.QueryParameterValue() }
            }.Where(x => x.Value != null)
            .Select(queryParam => $"{queryParam.Key}={Uri.EscapeDataString(queryParam.Value!)}")
            .ToList();

        UrlWithQueryString = ListAllTicketsUrl +
               (urlParams.Any() ? "?" + string.Join("&", urlParams) : "");
    }

    public override string ToString()
    {
        return $"{nameof(UrlWithQueryString)}: {UrlWithQueryString}";
    }
}

public enum ListAllTicketsFilter
{
    NewAnyMyOpen,
    Watching,
    Spam,
    Deleted
}

public enum TicketOrderBy
{
    CreatedAt,
    DueBy,
    UpdatedAt,
    Status
}

public enum TicketOrderDirection
{
    Ascending,
    Descending
}

internal static class TicketOrderExtensions
{
    internal static string QueryParameterValue(this ListAllTicketsFilter filter) => filter switch
    {
        ListAllTicketsFilter.NewAnyMyOpen => "new_any_my_open",
        ListAllTicketsFilter.Watching => "watching",
        ListAllTicketsFilter.Spam => "spam",
        ListAllTicketsFilter.Deleted => "deleted",
        _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null)
    };

    internal static string QueryParameterValue(this TicketOrderBy orderBy) => orderBy switch
    {
        TicketOrderBy.CreatedAt => "created_at",
        TicketOrderBy.DueBy => "due_by",
        TicketOrderBy.UpdatedAt => "updated_at",
        TicketOrderBy.Status => "status",
        _ => throw new ArgumentOutOfRangeException(nameof(orderBy), orderBy, null)
    };

    internal static string QueryParameterValue(this TicketOrderDirection orderDir) => orderDir switch
    {
        TicketOrderDirection.Ascending => "asc",
        TicketOrderDirection.Descending => "desc",
        _ => throw new ArgumentOutOfRangeException(nameof(orderDir), orderDir, null)
    };
}
