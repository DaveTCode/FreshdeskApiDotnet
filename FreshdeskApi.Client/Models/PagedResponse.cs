using System.Collections.Generic;

namespace FreshdeskApi.Client.Models;

public record PagedResponse<T>(
    IReadOnlyCollection<T> Items,
    IReadOnlyCollection<string>? LinkHeaderValues
);
