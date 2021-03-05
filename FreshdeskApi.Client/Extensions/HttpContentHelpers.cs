using System.Linq;
using FreshdeskApi.Client.Contacts.Requests;
using FreshdeskApi.Client.Conversations.Requests;
using FreshdeskApi.Client.Tickets.Requests;

namespace FreshdeskApi.Client.Extensions
{
    public static class HttpContentHelpers
    {
        /// <summary>
        /// Determines if the request should be serialized as JSON or a MultiPart Form Data
        /// </summary>
        /// <param name="body">Body object to be serialized</param>
        /// <returns>Boolean indicating if the body should be serialized as JSON.</returns>
        internal static bool SerializeAsJson(this object body)
        {
            if (body is CreateTicketRequest createTicketRequest)
            {
                return createTicketRequest.Files == null || !createTicketRequest.Files.Any();
            }

            if (body is UpdateTicketRequest updateTicketRequest)
            {
                return updateTicketRequest.Files == null || !updateTicketRequest.Files.Any();
            }

            if (body is ContactCreateRequest contactCreateRequest)
            {
                return contactCreateRequest.Avatar == null;
            }

            if (body is UpdateContactRequest updateContactRequest)
            {
                return updateContactRequest.Avatar == null;
            }

            if (body is CreateReplyRequest createReplyRequest)
            {
                return createReplyRequest.Files == null || !createReplyRequest.Files.Any();
            }

            if (body is UpdateNoteRequest updateNoteRequest)
            {
                return updateNoteRequest.Files == null || !updateNoteRequest.Files.Any();
            }

            return true;
        }
    }
}
