using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.Products.Models;

/// <summary>
/// Refers to a product.
///
/// c.f. https://developers.freshdesk.com/api/#products_attributes
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Product
{
    /// <summary>
    /// Unique ID of the product
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the product
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Product creation timestamp
    /// </summary>
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Product updated timestamp
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(CreatedAt)}: {CreatedAt}, {nameof(UpdatedAt)}: {UpdatedAt}";
    }
}
