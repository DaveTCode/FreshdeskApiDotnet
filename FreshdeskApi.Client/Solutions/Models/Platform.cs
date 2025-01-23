using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FreshdeskApi.Client.Solutions.Models;

/// <summary>
/// Platform on where articles are visible.
/// </summary>
[JsonConverter(typeof(StringEnumConverter), converterParameters: typeof(CamelCaseNamingStrategy))]
public enum Platform
{
    Web = 1,
    Ios = 2,
    Android = 3
}
