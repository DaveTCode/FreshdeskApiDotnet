using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Tickets.Models;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Requester
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("mobile")]
    public string? Mobile { get; set; }

    [JsonProperty("phone")]
    public string? Phone { get; set; }

    public override string ToString()
    {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(Mobile)}: {Mobile}, {nameof(Phone)}: {Phone}";
        }
}
