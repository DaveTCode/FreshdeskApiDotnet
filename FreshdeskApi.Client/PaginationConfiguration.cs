using System;

namespace FreshdeskApi.Client
{
    public class PaginationConfiguration
    {
        public int StartingPage { get; set; } = 1;

        public int? PageSize { get; set; }

        public Action<int>? ProcessedPage { get; set; }
    }
}
