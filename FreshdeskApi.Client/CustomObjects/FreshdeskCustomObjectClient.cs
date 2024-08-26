using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CustomObjects.Models;
using FreshdeskApi.Client.CustomObjects.Requests;
using FreshdeskApi.Client.Models;

namespace FreshdeskApi.Client.CustomObjects;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskCustomObjectClient(
    IFreshdeskHttpClient freshdeskClient
) : IFreshdeskCustomObjectClient
{
    /// <inheritdoc />
    public async Task<ListCustomObjectsResponse> ListCustomObjects(CancellationToken cancellationToken = default)
    {
        return await freshdeskClient
            .ApiOperationAsync<ListCustomObjectsResponse>(HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<CustomObject> GetCustomObject(string schemaId, CancellationToken cancellationToken = default)
    {
        return await freshdeskClient
            .ApiOperationAsync<CustomObject>(HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Record<T>> CreateRecord<T>(
        string schemaId, T recordData, CancellationToken cancellationToken = default
    )
    {
        var request = new CreateRecordRequest<T>(recordData);

        return await freshdeskClient
            .ApiOperationAsync<Record<T>, CreateRecordRequest<T>>(HttpMethod.Post,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/", request,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Record<T>> GetRecord<T>(
        string schemaId, string recordId, CancellationToken cancellationToken = default
    )
    {
        return await freshdeskClient
            .ApiOperationAsync<Record<T>>(HttpMethod.Get,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/{recordId}",
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Record<T>> UpdateRecord<T>(
        string schemaId, Record<T> record, CancellationToken cancellationToken = default
    )
    {
        return await freshdeskClient
            .ApiOperationAsync<Record<T>, Record<T>>(HttpMethod.Put,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/{record.DisplayId}", record,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteRecord(string schemaId, string recordId, CancellationToken cancellationToken = default)
    {
        await freshdeskClient
            .ApiOperationAsync<object>(HttpMethod.Delete,
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records/{recordId}",
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<T> ListAllRecordsAsync<T>(
        ListAllRecordsRequest request,
        string schemaId,
        IPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = request.GetQuery();
        await foreach (
            var product
            in freshdeskClient.GetPagedResults<T>(
                $"{IFreshdeskCustomObjectClient.UrlPrefix}/schemas/{schemaId}/records{query}",
                pagingConfiguration, EPagingMode.RecordContract, cancellationToken
            ).ConfigureAwait(false)
        )
        {
            yield return product;
        }
    }

    /// <inheritdoc />
    public async Task<int> GetCount<T>(string schemaId, CancellationToken cancellationToken = default)
    {
        var recordCount = await freshdeskClient
            .ApiOperationAsync<RecordCount>(HttpMethod.Get, $"/api/v2/custom_objects/{schemaId}/records/count",
                cancellationToken)
            .ConfigureAwait(false);

        return recordCount.Count;
    }
}
