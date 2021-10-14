using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Xunit;
using Xunit.Sdk;

namespace FreshdeskApi.Client.UnitTests.Infrastructure
{
    public class JsonPropertiesTests
    {
        [Fact]
        public void ValidateJsonProperties()
        {
            var freshdeskClientType = typeof(IFreshdeskClient);

            var missingJsonProperty = new List<(Type classType, string propertyName)>();

            foreach (var type in freshdeskClientType.Assembly.GetTypes()
                .Where(x => x.IsClass)
                .Where(x => x.Namespace?.StartsWith(freshdeskClientType.Namespace!) == true)
                .Where(x => new[] { ".CommonModels", ".Models", ".Requests", }.Any(n => x.Namespace!.EndsWith(n)))
                .OrderBy(x => x.Namespace).ThenBy(x => x.Name)
            )
            {
                foreach (var propertyInfo in type.GetProperties()
                    .Where(x => x.CanRead)
                    .OrderBy(x => x.Name)
                )
                {
                    var jsonIgnoreAttribute = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();

                    if (jsonIgnoreAttribute != null)
                    {
                        continue;
                    }

                    var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();

                    if (jsonPropertyAttribute == null || string.IsNullOrEmpty(jsonPropertyAttribute.PropertyName))
                    {
                        missingJsonProperty.Add((type, propertyInfo.Name));
                    }
                }
            }

            if (missingJsonProperty.Any())
            {
                var notDefinedPropertiesMessage = string.Join("\n", missingJsonProperty.Select(x => $"{x.classType.Namespace}.{x.classType.Name}:{x.propertyName}"));

                throw new XunitException($"Properties without configured {typeof(JsonPropertyAttribute)}:\n{notDefinedPropertiesMessage}");
            }
        }
    }
}
