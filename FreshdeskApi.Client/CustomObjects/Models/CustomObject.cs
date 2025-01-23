using System;
using System.Diagnostics.CodeAnalysis;
using FreshdeskApi.Client.JsonConverters;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

/// <summary>
/// A custom object to create and bring in information specific to your business use-cases
///
/// c.f. https://developers.freshdesk.com/api/#custom-objects
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record CustomObject
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

    /// <summary>
    /// Prefix of the Schema
    /// </summary>
    [JsonProperty("prefix")]
    public string? Prefix { get; set; }

    /// <summary>
    /// Version of the Schema
    /// </summary>
    [JsonProperty("version")]
    public int? Version { get; set; }

    /// <summary>
    /// Deleted state of the Schema
    /// </summary>
    [JsonProperty("deleted")]
    public bool Deleted { get; set; }

    [JsonProperty("created_time")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime CreatedTime { get; set; }

    [JsonProperty("updated_time")]
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime UpdatedTime { get; set; }
}
