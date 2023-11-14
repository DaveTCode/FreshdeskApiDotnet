using Newtonsoft.Json;

namespace FreshdeskApi.Client.Tickets.Models;

/// <summary>
/// Refers to a single archived ticket within Freshdesk.
///
/// c.f. https://developers.freshdesk.com/api/#archive_tickets
/// </summary>
public class ArchivedTicket : Ticket
{
    [JsonProperty("archived")]
    public bool Archived { get; set; }

    public override string ToString()
    {
            return $"{base.ToString()}, {nameof(Archived)}: {Archived}";
        }
}
