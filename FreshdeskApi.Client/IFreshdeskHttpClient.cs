using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Models;

namespace FreshdeskApi.Client;

public interface IFreshdeskHttpClient
{
    long RateLimitRemaining { get; }

    long RateLimitTotal { get; }

    Task<T> ApiOperationAsync<T>(HttpMethod method, string url, CancellationToken cancellationToken = default)
        where T : new();

    Task<T> ApiOperationAsync<T, TBody>(HttpMethod method, string url, TBody? body, CancellationToken cancellationToken = default)
        where T : new()
        where TBody : class;

    IAsyncEnumerable<T> GetPagedResults<T>(
        string initialUrl,
        IPaginationConfiguration pagingConfiguration,
        CancellationToken cancellationToken = default);
}
