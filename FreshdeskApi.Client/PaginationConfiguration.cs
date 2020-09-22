using System;
using System.Threading.Tasks;

namespace FreshdeskApi.Client
{
    public sealed class PaginationConfiguration : IPaginationConfiguration
    {
        public PaginationConfiguration(
            int? pageSize = null,
            int startingPage = 1,
            Func<int, Task>? beforeProcessingPageAsync = null,
            Func<int, Task>? processedPageAsync = null
        )
        {
            PageSize = pageSize;
            StartingPage = startingPage;
            BeforeProcessingPageAsync = beforeProcessingPageAsync;
            ProcessedPageAsync = processedPageAsync;
        }

        public int StartingPage { get; }

        public int? PageSize { get; }

        public Func<int, Task>? BeforeProcessingPageAsync { get; }

        public Func<int, Task>? ProcessedPageAsync { get; }
    }
}
