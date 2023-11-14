using System.Diagnostics.CodeAnalysis;

namespace FreshdeskApi.Client.Tickets.Models;

/// <summary>
/// Refers to the priority of a ticket.
///
/// c.f. https://developers.freshdesk.com/api/#tickets
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public enum TicketPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Urgent = 4,
}
