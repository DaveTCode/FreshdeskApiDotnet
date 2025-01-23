namespace FreshdeskApi.Client.Pagination;

public interface IPageBasedPaginationConfiguration : IPaginationConfiguration
{
    int StartingPage { get; }

    int? PageSize { get; }
}
