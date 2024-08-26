using FreshdeskApi.Client;

namespace FreshdeskApi.Demo;

public sealed record PagingConfiguration(
    int? StartingPage,
    int? PageSize
) : IPaginationConfiguration
{
    public string? StartingToken => null;

    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; } = null;
    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; } = null;
}
