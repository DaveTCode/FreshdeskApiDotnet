using System.Diagnostics.CodeAnalysis;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// Refers to the status of a ticket
    ///
    /// https://developers.freshdesk.com/api/#tickets
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum TicketStatus
    {
        Open = 2,
        Pending = 3,
        Resolved = 4,
        Closed = 5,
    }
}
