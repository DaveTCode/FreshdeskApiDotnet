using System.Linq;
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
            if (body is CreateTicketRequest request)
            {
                return request.Files == null || !request.Files.Any();
            }

            return true;
        }
    }
}
