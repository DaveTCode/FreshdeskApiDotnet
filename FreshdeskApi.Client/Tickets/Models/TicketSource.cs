using System.Diagnostics.CodeAnalysis;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// Refers to the source of a ticket, i.e. where it was created from:
    ///
    /// https://developers.freshdesk.com/api/#tickets
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum TicketSource
    {
        Email = 1,
        Portal = 2,
        Phone = 3,
        Chat = 7,
        Mobihelp = 8,
        FeedbackWidget = 9,
        OutboundEmail = 10,
    }
}
