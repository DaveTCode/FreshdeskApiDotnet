using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace FreshdeskApi.Client.CustomObjects.Models;

/// <summary>
/// Field of a custom object
///
/// c.f. https://developers.freshdesk.com/api/#custom-objects
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CustomObjectField
{
    /// <summary>
    /// Name of the field
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Auto-generated unique identifier for a particular field
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }
    
    /// <summary>
    /// Fields in the Schema
    /// </summary>
    [JsonProperty("label")]
    public string? Label { get; set; }
    
    /// <summary>
    /// The default value is false. Should be set ‘true’ if you no longer intend to use this field. Can be restored by setting the value as false again
    /// </summary>
    [JsonProperty("deleted")]
    public string? Deleted { get; set; }
    
    /// <summary>
    /// Whether the given field is marked as filterable. The default value is false
    /// </summary>
    [JsonProperty("filterable")]
    public bool Filterable { get; set; }
    
    /// <summary>
    /// Hint to identify a particular field (Eg. This is a text field)
    /// </summary>
    [JsonProperty("hint")]
    public string? Hint { get; set; }
    
    /// <summary>
    /// Position of the field (in case of multiple fields in a schema)
    /// </summary>
    [JsonProperty("position")]
    public int Position { get; set; }
    
    /// <summary>
    /// To make the field mandatory while defining a custom object. The default value is false
    /// </summary>
    [JsonProperty("required")]
    public bool Required { get; set; }
    
    /// <summary>
    /// Type of the field. It can be any of the below supported field types
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }
    
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Label)}: {Label}, {nameof(Deleted)}: {Filterable}, {nameof(Hint)}: {Hint}, {nameof(Position)}: {Position}, {nameof(Required)}: {Required}, {nameof(Type)}: {Type}";
    }
}
