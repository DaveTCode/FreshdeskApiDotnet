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

    /// <param name="startingToken">Token to start from</param>
    /// <param name="pageSize">Page size, default unspecified</param>
    /// <param name="beforeProcessingPageAsync">Hook before page is processed, optional</param>
    /// <param name="processedPageAsync">Hook after page is processed, optional</param>
    public PaginationConfiguration(
        string? startingToken,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    )
    {
        StartingToken = startingToken;
        PageSize = pageSize;
        BeforeProcessingPageAsync = beforeProcessingPageAsync;
        ProcessedPageAsync = processedPageAsync;
    }

    public static PaginationConfiguration CreatePagination(
        int startingPage = 1,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    ) => new(
        startingPage: startingPage,
        pageSize,
        beforeProcessingPageAsync,
        processedPageAsync
    );

    public static PaginationConfiguration CreateTokenPagination(
        string? startingToken = null,
        int? pageSize = null,
        IPaginationConfiguration.ProcessPageDelegate? beforeProcessingPageAsync = null,
        IPaginationConfiguration.ProcessPageDelegate? processedPageAsync = null
    ) => new(
        startingToken: startingToken,
        pageSize,
        beforeProcessingPageAsync,
        processedPageAsync
    );

    public int? StartingPage { get; }

    public string? StartingToken { get; }

    public int? PageSize { get; }

    public IPaginationConfiguration.ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    public IPaginationConfiguration.ProcessPageDelegate? ProcessedPageAsync { get; }
}
