using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

/// <summary>
/// A custom object to create and bring in information specific to your business use-cases
///
/// c.f. https://developers.freshdesk.com/api/#custom-objects
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CustomObject
{
    /// <summary>
    /// Name of the Schema
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Auto-generated unique identifier for the Schema
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Fields in the Schema
    /// </summary>
    [JsonProperty("fields")]
    public CustomObjectField[]? Fields { get; set; }

    /// <summary>
    /// A short description of the Schema
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}";
    }
}
