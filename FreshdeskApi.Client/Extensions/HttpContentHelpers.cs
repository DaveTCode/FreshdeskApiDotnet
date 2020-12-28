using System;
using System.Linq;
using FreshdeskApi.Client.Tickets.Requests;

namespace FreshdeskApi.Client
{
    public static class HttpContentHelpers
    {
        /// <summary>
        /// Determines if the request should be serialized as JSON or a MultiPart Form Data
        /// </summary>
        /// <param name="body">Body object to be serialized</param>
        /// <returns>Boolean indicating if the body should be serialized as JSON.</returns>
        internal static bool SerializeAsJson(this object body) =>
            !(body is CreateTicketRequest request) ||
            (request.Files == null || request.Files.Count() == 0);

    }
}
