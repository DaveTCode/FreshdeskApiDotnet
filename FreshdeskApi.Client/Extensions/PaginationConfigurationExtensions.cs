using System;

namespace FreshdeskApi.Client.Extensions;

public static class PaginationConfigurationExtensions
{
    public static void GuardTokenBasedPagination(this IPaginationConfiguration? paginationConfiguration)
    {
        if (paginationConfiguration?.StartingPage.HasValue is true)
        {
            throw new ArgumentException(
                $"You have passed pagination with {nameof(IPaginationConfiguration.StartingPage)} set. This method uses token based pagination.",
                nameof(paginationConfiguration)
            );
        }
    }

    public static void GuardPageBasedPagination(this IPaginationConfiguration? paginationConfiguration)
    {
        if (paginationConfiguration?.StartingToken is not null)
        {
            throw new ArgumentException(
                $"You have passed pagination with {nameof(IPaginationConfiguration.StartingToken)} set. This method uses page based pagination.",
                nameof(paginationConfiguration)
            );
        }
    }
}
