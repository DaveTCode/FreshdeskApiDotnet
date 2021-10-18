using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Conversations.Models;
using FreshdeskApi.Client.Conversations.Requests;

namespace FreshdeskApi.Client.Conversations
{
    public interface IFreshdeskConversationsClient
    {
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
        Task<ConversationEntry> CreateReplyAsync(
            long ticketId,
            CreateReplyRequest request,
            CancellationToken cancellationToken = default);

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
        Task<ConversationEntry> CreateNoteAsync(
            long ticketId,
            CreateNoteRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a conversation entry (either reply or note).
        ///
        /// c.f. https://developers.freshdesk.com/api/#delete_conversation
        /// </summary>
        /// 
        /// <param name="conversationEntryId">The entry to delete</param>
        ///
        /// <param name="cancellationToken"></param>
        Task DeleteConversationEntryAsync(
            long conversationEntryId,
            CancellationToken cancellationToken = default);

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
        Task<ConversationEntry> UpdateNoteAsync(
            long conversationEntryId,
            UpdateNoteRequest request,
            CancellationToken cancellationToken = default);
    }
}
