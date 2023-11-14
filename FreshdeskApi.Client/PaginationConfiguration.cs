namespace FreshdeskApi.Client;

public sealed class PaginationConfiguration : IPaginationConfiguration
{
    /// <param name="startingPage">Page to start from. Default 1</param>
    /// <param name="pageSize">Page size, default unspecified</param>
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public PaginationConfiguration(
        int startingPage = 1,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    )
    {
            StartingPage = startingPage;
            PageSize = pageSize;
            BeforeProcessingPageAsync = beforeProcessingPageAsync;
            ProcessedPageAsync = processedPageAsync;
        }

    public int StartingPage { get; }

    public int? PageSize { get; }

    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; }
}
