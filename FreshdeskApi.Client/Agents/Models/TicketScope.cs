using System.Diagnostics.CodeAnalysis;

namespace FreshdeskApi.Client.Agents.Models;

/// <summary>
/// Ticket permission of an agent (1 -> Global Access, 2 -> Group Access,
/// 3 -> Restricted Access)
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public enum TicketScope
{
    GlobalAccess = 1,
    GroupAccess = 2,
    RestrictedAccess = 3
}
