using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreshdeskApi.Client;

/// <summary>
/// This object allows configuring of Freshdesk pagination
/// e. g. restart from given page, change page size, ....
/// </summary>
public interface IPaginationConfiguration
{
    public Dictionary<string, string> BuildInitialPageParameters();

    public Dictionary<string, string>? BuildNextPageParameters<T>(int page, IReadOnlyCollection<T> newData, string link);
    
    
    public delegate Task ProcessPageDelegate(int page, string url, CancellationToken? cancellationToken = default);

    /// <summary>
    /// This event is invoked right after deserialization of the page
    /// </summary>
    ProcessPageDelegate? BeforeProcessingPageAsync { get; }

    /// <summary>
    /// This event is invoked after processing of all items on given page
    /// </summary>
    ProcessPageDelegate? ProcessedPageAsync { get; }
}
