using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Conversations.Models;
using FreshdeskApi.Client.Tickets.Models;
using FreshdeskApi.Client.Tickets.Requests;

namespace FreshdeskApi.Client.Tickets;

public interface IFreshdeskTicketClient
{
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
    Task<Ticket> ViewTicketAsync(
        long ticketId,
        TicketIncludes? includes = default,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<Ticket> ListAllTicketsAsync(
        ListAllTicketsRequest listAllTicketsRequest,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<Ticket> FilterTicketsAsync(
        string encodedQuery,
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    Task<Ticket> CreateTicketAsync(
        CreateTicketRequest createTicketRequest,
        CancellationToken cancellationToken = default);

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
    Task<Ticket> CreateOutboundEmailAsync(
        CreateOutboundEmailRequest createOutboundEmailRequest,
        CancellationToken cancellationToken = default);

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
    Task<Ticket> UpdateTicketAsync(
        long ticketId,
        UpdateTicketRequest updateTicketRequest,
        CancellationToken cancellationToken = default);

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
    Task DeleteTicketAsync(
        long ticketId,
        CancellationToken cancellationToken = default);

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
    Task RestoreTicketAsync(
        long ticketId,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<ConversationEntry> GetTicketConversationsAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<TimeEntry> GetTicketTimeEntriesAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<SatisfactionRating> GetTicketSatisfactionRatingsAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);


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
    Task<ArchivedTicket> ViewArchiveTicketAsync(
        long ticketId,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<ConversationEntry> GetArchiveTicketConversationsAsync(
        long ticketId,
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);
}
