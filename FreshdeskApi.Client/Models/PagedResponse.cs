using System.Collections.Generic;
using FreshdeskApi.Client.Attributes;

namespace FreshdeskApi.Client.Models;

[IgnoreJsonValidation]
public record PagedResponse<T>(
    IReadOnlyCollection<T> Items,
    IReadOnlyCollection<string>? LinkHeaderValues
);
