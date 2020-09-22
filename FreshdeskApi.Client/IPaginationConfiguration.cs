using System;
using System.Threading.Tasks;

namespace FreshdeskApi.Client
{
    /// <summary>
    /// This object allows configuring of Freshdesk pagination
    /// e. g. restart from given page, change page size, ....  
    /// </summary>
    public interface IPaginationConfiguration
    {
        /// <summary>
        /// Page to start from
        /// </summary>
        int StartingPage { get; }

        /// <summary>
        /// Page size (see Freshdesk API documentation, there are different allowed page sizes)
        /// </summary>
        int? PageSize { get; }

        /// <summary>
        /// This event is invoked right after deserialization of the page
        /// </summary>
        Func<int, Task>? BeforeProcessingPageAsync { get; }

        /// <summary>
        /// This event is invoked after processing of all items on given page 
        /// </summary>
        Func<int, Task>? ProcessedPageAsync { get; }
    }
}
