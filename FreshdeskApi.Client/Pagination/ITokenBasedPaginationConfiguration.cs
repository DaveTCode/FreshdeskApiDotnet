namespace FreshdeskApi.Client.Pagination;

public interface ITokenBasedPaginationConfiguration : IPaginationConfiguration
{
    string? StartingToken { get; }

    int? PageSize { get; }
}
