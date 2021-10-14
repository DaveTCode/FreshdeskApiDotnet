using System.Diagnostics.CodeAnalysis;

namespace FreshdeskApi.Client.Tickets.Models
{
    /// <summary>
    /// A ticket may optionally have an association type, if that is set it
    /// should be one of the following values.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum TicketAssociationType
    {
        Parent = 1,
        Child = 2,
        Tracker = 3,
        Related = 4
    }
}
