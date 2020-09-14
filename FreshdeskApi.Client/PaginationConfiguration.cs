using System;
using System.Threading.Tasks;

namespace FreshdeskApi.Client
{
    public class PaginationConfiguration
    {
        public int StartingPage { get; set; } = 1;

        public int? PageSize { get; set; }

        public Func<int, Task>? BeforeProcessingPage { get; set; }

        public Func<int, Task>? ProcessedPage { get; set; }
    }
}
