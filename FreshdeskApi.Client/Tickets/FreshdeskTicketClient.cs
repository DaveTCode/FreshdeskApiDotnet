using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Conversations.Models;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Models;
using FreshdeskApi.Client.Pagination;
using FreshdeskApi.Client.Tickets.Models;
using FreshdeskApi.Client.Tickets.Requests;

namespace FreshdeskApi.Client.Tickets;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskTicketClient : IFreshdeskTicketClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskTicketClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <summary>
    /// Get all information about a single ticket by <see cref="ticketId"/>
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_a_ticket
    /// </summary>
    ///
    /// <param name="ticketId">
    /// The unique identifier of the ticket.
    /// </param>
    ///
    /// <param name="includes">
    /// A set of optional includes which will consume extra API credits to
    /// retrieve more linked information about the ticket.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full ticket information</returns>
    public async Task<Ticket> ViewTicketAsync(
        long ticketId,
        TicketIncludes? includes = default,
        CancellationToken cancellationToken = default)
    {
        var url = $"/api/v2/tickets/{ticketId}";

        if (includes.HasValue)
        {
            url += $"?include={includes}";
        }

        return await _freshdeskClient
            .ApiOperationAsync<Ticket>(HttpMethod.Get, url, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// View all tickets applying the required filters.
    ///
    /// Note that this is a terrible API with a limit of 300 pages (and
    /// further pages take exponentially longer to return!
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_tickets
    /// </summary>
    ///
    /// <param name="listAllTicketsRequest">
    /// A request object with required filters filled in.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// A list of matching tickets as an <seealso cref="IAsyncEnumerable{Ticket}"/>,
    /// therefore if there are multiple pages of results iterating to the next item
    /// may cause an API call.
    /// </returns>
    public async IAsyncEnumerable<Ticket> ListAllTicketsAsync(
        ListAllTicketsRequest listAllTicketsRequest,
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var ticket in _freshdeskClient
            .GetPagedResults<Ticket>(listAllTicketsRequest.UrlWithQueryString, pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return ticket;
        }
    }

    /// <summary>
    /// Filter all tickets according to the query language described in the
    /// link below.
    ///
    /// c.f. https://developers.freshdesk.com/api/#filter_tickets
    /// </summary>
    ///
    /// <param name="encodedQuery">
    /// An encoded query string of the form
    /// (ticket_field:integer or ticket_field:'string') AND ticket_field:boolean
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// A list of matching tickets as an <seealso cref="IAsyncEnumerable{Ticket}"/>,
    /// therefore if there are multiple pages of results iterating to the next item
    /// may cause an API call.
    /// </returns>
    public async IAsyncEnumerable<Ticket> FilterTicketsAsync(
        string encodedQuery,
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new PageBasedPaginationConfiguration();

        await foreach (var ticket in _freshdeskClient
            .GetPagedResults<Ticket>($"/api/v2/search/tickets?query={encodedQuery}", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return ticket;
        }
    }

    /// <summary>
    /// Create a new ticket in Freshdesk.
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_ticket
    /// </summary>
    ///
    /// <param name="createTicketRequest">
    /// Describes the values of all the fields in the new ticket.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The newly created ticket with its ID included.
    /// </returns>
    public async Task<Ticket> CreateTicketAsync(
        CreateTicketRequest createTicketRequest,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Ticket, CreateTicketRequest>(HttpMethod.Post, "/api/v2/tickets", createTicketRequest, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a new outbound email in Freshdesk.
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_outbound_email
    /// </summary>
    ///
    /// <param name="createOutboundEmailRequest">
    /// Describes the values of all the fields in the new outbound email.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The newly created ticket with its ID included.
    /// </returns>
    public async Task<Ticket> CreateOutboundEmailAsync(
        CreateOutboundEmailRequest createOutboundEmailRequest,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Ticket, CreateOutboundEmailRequest>(HttpMethod.Post, "/api/v2/tickets/outbound_email", createOutboundEmailRequest, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a new ticket in Freshdesk.
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_ticket
    /// </summary>
    ///
    /// <param name="ticketId">
    /// The unique identifier for the ticket to be updated.
    /// </param>
    ///
    /// <param name="updateTicketRequest">
    /// Describes the values of all the fields in the new ticket.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The updated ticket with all the new values as seen by the API.
    /// </returns>
    public async Task<Ticket> UpdateTicketAsync(
        long ticketId,
        UpdateTicketRequest updateTicketRequest,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Ticket, UpdateTicketRequest>(HttpMethod.Put, $"/api/v2/tickets/{ticketId}", updateTicketRequest, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete the ticket with id <see cref="ticketId"/>
    ///
    /// c.f. https://developers.freshdesk.com/api/#delete_a_ticket
    /// </summary>
    ///
    /// <param name="ticketId">
    /// An existing ticket in the Freshdesk instance.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    public async Task DeleteTicketAsync(
        long ticketId,
        CancellationToken cancellationToken = default)
    {
        await _freshdeskClient
            .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/tickets/{ticketId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Restore a previously deleted ticket.
    ///
    /// c.f. https://developers.freshdesk.com/api/#restore_a_ticket
    /// </summary>
    ///
    /// <param name="ticketId">
    /// An existing ticket in the Freshdesk instance.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    public async Task RestoreTicketAsync(
        long ticketId,
        CancellationToken cancellationToken = default)
    {
        await _freshdeskClient
            .ApiOperationAsync<object>(HttpMethod.Put, $"/api/v2/tickets/{ticketId}/restore", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Get the full set of conversation entries for a given ticket.
    ///
    /// This can be consumed by using:
    /// <code>
    /// await foreach (var conversationEntry in GetTicketConversations(ticketId))
    /// {
    ///   // Do something
    /// }
    /// </code>
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_ticket_notes
    /// </summary>
    ///
    /// <param name="ticketId">
    /// An existing ticket in the Freshdesk instance.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// An iterable of conversation entries, since the results
    /// from the API can be paged this is async and iterating to the next
    /// entry may cause another API call if there are several pages.
    /// </returns>
    public async IAsyncEnumerable<ConversationEntry> GetTicketConversationsAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var conversationEntry in _freshdeskClient
            .GetPagedResults<ConversationEntry>($"/api/v2/tickets/{ticketId}/conversations", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return conversationEntry;
        }
    }

    /// <summary>
    /// Get the full set of time entries for a given ticket.
    ///
    /// This can be consumed by using:
    /// <code>
    /// await foreach (var timeEntry in GetTicketTimeEntries(ticketId))
    /// {
    ///   // Do something
    /// }
    /// </code>
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_ticket_timeentries
    /// </summary>
    ///
    /// <param name="ticketId">
    /// An existing ticket in the Freshdesk instance.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// An iterable of time entries, since the results
    /// from the API can be paged this is async and iterating to the next
    /// entry may cause another API call if there are several pages.
    /// </returns>
    public async IAsyncEnumerable<TimeEntry> GetTicketTimeEntriesAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var timeEntry in _freshdeskClient
            .GetPagedResults<TimeEntry>($"/api/v2/tickets/{ticketId}/time_entries", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return timeEntry;
        }
    }

    /// <summary>
    /// Get the full set of satisfaction ratings for a given ticket.
    ///
    /// This can be consumed by using:
    /// <code>
    /// await foreach (var satisfactionRating in GetTicketSatisfactionRatings(ticketId))
    /// {
    ///   // Do something
    /// }
    /// </code>
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_ticket_satisfaction_ratings
    /// </summary>
    ///
    /// <param name="ticketId">
    /// An existing ticket in the Freshdesk instance.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// An iterable of satisfaction ratings, since the results
    /// from the API can be paged this is async and iterating to the next
    /// entry may cause another API call if there are several pages.
    /// </returns>
    public async IAsyncEnumerable<SatisfactionRating> GetTicketSatisfactionRatingsAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var satisfactionRating in _freshdeskClient
            .GetPagedResults<SatisfactionRating>($"/api/v2/tickets/{ticketId}/satisfaction_ratings", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return satisfactionRating;
        }
    }


    /// <summary>
    /// Get all information about a single archive ticket by <see cref="ticketId"/>
    ///
    /// c.f. https://developers.freshdesk.com/api/#archive_tickets
    /// </summary>
    ///
    /// <param name="ticketId">
    /// The unique identifier of the archive ticket.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full ticket information</returns>
    public async Task<ArchivedTicket> ViewArchiveTicketAsync(
        long ticketId,
        CancellationToken cancellationToken = default)
    {
        var url = $"/api/v2/tickets/archived/{ticketId}";

        return await _freshdeskClient
            .ApiOperationAsync<ArchivedTicket>(HttpMethod.Get, url, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Get the full set of conversation entries for a given archive ticket.
    ///
    /// This can be consumed by using:
    /// <code>
    /// await foreach (var conversationEntry in GetArchiveTicketConversations(ticketId))
    /// {
    ///   // Do something
    /// }
    /// </code>
    ///
    /// c.f. https://developers.freshdesk.com/api/#archive_tickets
    /// </summary>
    ///
    /// <param name="ticketId">
    /// An existing archive ticket in the Freshdesk instance.
    /// </param>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// An iterable of conversation entries, since the results
    /// from the API can be paged this is async and iterating to the next
    /// entry may cause another API call if there are several pages.
    /// </returns>
    public async IAsyncEnumerable<ConversationEntry> GetArchiveTicketConversationsAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var conversationEntry in _freshdeskClient
            .GetPagedResults<ConversationEntry>($"/api/v2/tickets/archived/{ticketId}/conversations", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return conversationEntry;
        }
    }
}
