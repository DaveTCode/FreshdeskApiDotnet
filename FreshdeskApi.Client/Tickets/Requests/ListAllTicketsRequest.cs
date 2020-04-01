using System;
using System.Collections.Generic;
using FreshdeskApi.Client.Tickets.Models;

namespace FreshdeskApi.Client.Tickets.Requests
{
    /// <summary>
    /// When listing all tickets only certain filters can be applied, this
    /// object restricts what searches can be done to reflect the API hard
    /// limits.
    /// </summary>
    public class ListAllTicketsRequest
    {
        private readonly ListAllTicketsFilter? _filter;
        private readonly long? _requesterId;
        private readonly string _requesterEmail;
        private readonly long? _companyID;
        private readonly DateTimeOffset? _updatedSince;
        private readonly TicketIncludes? _includes;
        private readonly TicketOrderBy? _orderBy;
        private readonly TicketOrderDirection? _orderDir;

        public ListAllTicketsRequest(
            ListAllTicketsFilter filter,
            long? requesterId = null,
            string requesterEmail = null,
            long? companyID = null,
            DateTimeOffset? updatedSince = null,
            TicketIncludes? includes = default,
            TicketOrderBy? orderBy = default,
            TicketOrderDirection? orderDir = default)
        {
            _filter = filter;
            _requesterId = requesterId;
            _requesterEmail = requesterEmail;
            _companyID = companyID;
            _updatedSince = updatedSince;
            _includes = includes;
            _orderBy = orderBy;
            _orderDir = orderDir;
        }

        internal string ToQueryString()
        {
            var queryParts = new List<string>();

            if (_filter.HasValue) queryParts.Add($"filter={_filter.Value.QueryParameterValue()}");
            if (_requesterId.HasValue) queryParts.Add($"requesterId={_requesterId.Value}");
            if (_requesterEmail != null) queryParts.Add($"email={_requesterEmail}");
            if (_companyID.HasValue) queryParts.Add($"company_id={_companyID.Value}");
            if (_updatedSince.HasValue) queryParts.Add($"updated_since={_updatedSince.Value:yyyy-MM-ddTHH:mm:ssZ}");
            if (_orderBy.HasValue) queryParts.Add($"order_by={_orderBy.Value.QueryParameterValue()}");
            if (_orderDir.HasValue) queryParts.Add($"order_type={_orderDir.Value.QueryParameterValue()}");

            var queryString = "?" + string.Join("&", queryParts);

            if (_includes.HasValue) queryString += _includes.Value.ToString();

            return queryString;
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
}
