using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Conversations.Models;
using FreshdeskApi.Client.Conversations.Requests;

namespace FreshdeskApi.Client.Conversations;

/// <summary>
/// API endpoints specific to creating/updating conversations on Freshdesk
/// tickets.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskConversationsClient : IFreshdeskConversationsClient
{
    private readonly IFreshdeskHttpClient _client;

    public FreshdeskConversationsClient(IFreshdeskHttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Create a reply to a ticket.
    ///
    /// c.f. https://developers.freshdesk.com/api/#reply_ticket
    /// </summary>
    /// 
    /// <param name="ticketId">
    /// The ticket to add the reply to.
    /// </param>
    ///
    /// <param name="request">
    /// Defines the set of properties to set on the reply.
    /// </param>
    /// 
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full conversation entry</returns>
    public async Task<ConversationEntry> CreateReplyAsync(
        long ticketId,
        CreateReplyRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _client
            .ApiOperationAsync<ConversationEntry, CreateReplyRequest>(HttpMethod.Post, $"/api/v2/tickets/{ticketId}/reply", request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Add a public/private note to a ticket.
    ///
    /// c.f. https://developers.freshdesk.com/api/#add_note_to_a_ticket
    /// </summary>
    /// 
    /// <param name="ticketId">
    /// The ticket to add the reply to.
    /// </param>
    ///
    /// <param name="request">
    /// Defines the set of properties to set on the note.
    /// </param>
    /// 
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full conversation entry</returns>
    public async Task<ConversationEntry> CreateNoteAsync(
        long ticketId,
        CreateNoteRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _client
            .ApiOperationAsync<ConversationEntry, CreateNoteRequest>(HttpMethod.Post, $"/api/v2/tickets/{ticketId}/notes", request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a conversation entry (either reply or note).
    ///
    /// c.f. https://developers.freshdesk.com/api/#delete_conversation
    /// </summary>
    /// 
    /// <param name="conversationEntryId">The entry to delete</param>
    ///
    /// <param name="cancellationToken"></param>
    public async Task DeleteConversationEntryAsync(
        long conversationEntryId,
        CancellationToken cancellationToken = default)
    {
        await _client
            .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/conversations/{conversationEntryId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Update an existing public/private note.
    ///
    /// c.f. https://developers.freshdesk.com/api/#update_conversation
    /// </summary>
    /// 
    /// <param name="conversationEntryId">
    /// The unique identifier for the note 
    /// </param>
    ///
    /// <param name="request">
    /// Defines the fields to update on the note.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The updated note</returns>
    public async Task<ConversationEntry> UpdateNoteAsync(
        long conversationEntryId,
        UpdateNoteRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _client
            .ApiOperationAsync<ConversationEntry, UpdateNoteRequest>(HttpMethod.Put, $"/api/v2/conversations/{conversationEntryId}", request, cancellationToken)
            .ConfigureAwait(false);
    }
}
