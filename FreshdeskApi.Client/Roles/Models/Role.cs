using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Roles.Models;

/// <summary>
/// Refers to a role of agents (as specified in the
/// <see cref="Id"/> parameter).
///
/// c.f. https://developers.freshdesk.com/api/#view_role
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Role
{
    /// <summary>
    /// Unique ID of the role
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Name of the role
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the role
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    /// True if this is the default role
    /// </summary>
    [JsonProperty("default")]
    public bool Default { get; set; }

    /// Role creation timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Role updated timestamp
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    public override string ToString()
    {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Default)}: {Default}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}";
        }
}
