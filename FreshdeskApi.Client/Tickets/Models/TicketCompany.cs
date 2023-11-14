using Newtonsoft.Json;

namespace FreshdeskApi.Client.Tickets.Models;

/// <summary>
/// This class refers to the company against which a ticket was logged, it
/// does not contain the entire contents of the company details, those can
/// be retrieved using the specific company api call.
/// </summary>
public class TicketCompany
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
    }
}
