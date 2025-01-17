using System.Collections.Generic;

namespace FreshdeskApi.Client.Tickets.Models;

/// <summary>
/// Optionally include extra data about a ticket on API calls which return one.
///
/// Each of these costs 1 extra API credit on a call which returns a
/// single ticket and 2 extra API credits on a call which returns a list
/// of tickets.
/// </summary>
public struct ListTicketIncludes
{
    /// <summary>
    /// Causes the full requester information to be loaded for a given
    /// ticket.
    /// </summary>
    public bool Requester;

    /// <summary>
    /// Causes statistics to be loaded for a given ticket.
    /// </summary>
    public bool Stats;

    /// <summary>
    /// Causes description and descriptiontext fields to be included when listing tickets.
    /// </summary>
    public bool Description;

    public override string ToString()
    {
        var includes = new List<string>();

        if (Requester) includes.Add("requester");
        if (Stats) includes.Add("stats");
        if (Description) includes.Add("description");

        return string.Join(',', includes);
    }
}
