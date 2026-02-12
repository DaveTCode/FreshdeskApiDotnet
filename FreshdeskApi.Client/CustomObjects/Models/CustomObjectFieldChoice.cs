using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

/// <summary>
/// Choice for a dropdown field of a custom object
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record CustomObjectFieldChoice
{
    /// <summary>
    /// Id of the choice
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// Actual value of the choice
    /// </summary>
    [JsonProperty("value")]
    public required string Value { get; set; }

    /// <summary>
    /// Position of the choice
    /// </summary>
    [JsonProperty("position")]
    public int Position { get; set; }
}
