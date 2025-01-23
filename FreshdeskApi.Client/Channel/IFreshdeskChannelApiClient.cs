using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Channel.Requests;
using FreshdeskApi.Client.Conversations.Models;
using FreshdeskApi.Client.Tickets.Models;

namespace FreshdeskApi.Client.Channel;

public interface IFreshdeskChannelApiClient
{
    Task<Ticket> CreateTicketAsync(
        ChannelCreateTicketRequest request,
        CancellationToken cancellationToken = default);

    Task<ConversationEntry> CreateConversationAsync(
        long ticketId,
        ChannelCreateReplyRequest request,
        CancellationToken cancellationToken = default);

    Task<ConversationEntry> CreateNoteAsync(
        long ticketId,
        ChannelCreateNoteRequest request,
        CancellationToken cancellationToken = default);
}
