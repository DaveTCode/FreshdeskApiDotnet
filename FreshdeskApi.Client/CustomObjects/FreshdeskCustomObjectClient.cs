using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CustomObjects.Models;
using FreshdeskApi.Client.CustomObjects.Requests;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Models;

namespace FreshdeskApi.Client.CustomObjects;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskCustomObjectClient(
    IFreshdeskHttpClient freshdeskClient
) : IFreshdeskCustomObjectClient
{
    /// <inheritdoc />
    public async Task<ListCustomObjectsResponse> ListCustomObjectsAsync(CancellationToken cancellationToken = default)
    {
        return await freshdeskClient
            .ApiOperationAsync<ListCustomObjectsResponse>(
                HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas",
                cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<CustomObject> GetCustomObjectAsync(string schemaId, CancellationToken cancellationToken = default)
    {
        return await freshdeskClient
            .ApiOperationAsync<CustomObject>(
                HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}",
                cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Record<T>> CreateRecordAsync<T>(
        string schemaId, T recordData, CancellationToken cancellationToken = default
    )
    {
        var request = new CreateRecordRequest<T>(recordData);

        return await freshdeskClient
            .ApiOperationAsync<Record<T>, CreateRecordRequest<T>>(
                HttpMethod.Post,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/",
                request,
                cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Record<T>> GetRecordAsync<T>(
        string schemaId, string recordId, CancellationToken cancellationToken = default
    )
    {
        return await freshdeskClient
            .ApiOperationAsync<Record<T>>(
                HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/{recordId}",
                cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Record<T>> UpdateRecordAsync<T>(
        string schemaId, Record<T> record, CancellationToken cancellationToken = default
    )
    {
        return await freshdeskClient
            .ApiOperationAsync<Record<T>, Record<T>>(
                HttpMethod.Put,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/{record.DisplayId}",
                record,
                cancellationToken: cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteRecordAsync(string schemaId, string recordId, CancellationToken cancellationToken = default)
    {
        await freshdeskClient
            .ApiOperationAsync<object>(
                HttpMethod.Delete,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/{recordId}",
                cancellationToken
            ).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<Record<T>> ListAllRecordsAsync<T>(
        ListAllRecordsRequest request,
        string schemaId,
        TokenBasedPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new TokenBasedPaginationConfiguration();
        
        await foreach (
            var product
            in freshdeskClient.GetPagedResults<Record<T>>(
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records{request.GetQuery()}",
                pagingConfiguration, cancellationToken
            ).ConfigureAwait(false)
        )
        {
            yield return product;
        }
    }

    /// <inheritdoc />
    public async Task<int> GetCountAsync<T>(string schemaId, CancellationToken cancellationToken = default)
    {
        var recordCount = await freshdeskClient
            .ApiOperationAsync<RecordCount>(
                HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/count",
                cancellationToken
            ).ConfigureAwait(false);

        return recordCount.Count;
    }
}
