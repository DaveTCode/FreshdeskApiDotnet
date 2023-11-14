using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Channel.Requests;
using FreshdeskApi.Client.Conversations.Models;
using FreshdeskApi.Client.Tickets.Models;

namespace FreshdeskApi.Client.Channel;

/// <summary>
/// The channel API is a restricted API used internally at Freshworks for
/// migrations, it is only accessible to accounts who have explicitly
/// agreed to use it.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskChannelApiClient : IFreshdeskChannelApiClient
{
    private readonly IFreshdeskHttpClient _client;

    public FreshdeskChannelApiClient(IFreshdeskHttpClient client)
    {
            _client = client;
        }

    public async Task<Ticket> CreateTicketAsync(
        ChannelCreateTicketRequest request,
        CancellationToken cancellationToken = default)
    {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _client
                .ApiOperationAsync<Ticket, ChannelCreateTicketRequest>(HttpMethod.Post, "/api/channel/v2/tickets", request, cancellationToken)
                .ConfigureAwait(false);
        }

    public async Task<ConversationEntry> CreateConversationAsync(
        long ticketId,
        ChannelCreateReplyRequest request,
        CancellationToken cancellationToken = default)
    {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _client
                .ApiOperationAsync<ConversationEntry, ChannelCreateReplyRequest>(HttpMethod.Post, $"/api/channel/v2/tickets/{ticketId}/reply", request, cancellationToken)
                .ConfigureAwait(false);
        }

    public async Task<ConversationEntry> CreateNoteAsync(
        long ticketId,
        ChannelCreateNoteRequest request,
        CancellationToken cancellationToken = default)
    {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _client
                .ApiOperationAsync<ConversationEntry, ChannelCreateNoteRequest>(HttpMethod.Post, $"/api/channel/v2/tickets/{ticketId}/notes", request, cancellationToken)
                .ConfigureAwait(false);
        }
}