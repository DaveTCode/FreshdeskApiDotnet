using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CustomObjects.Models;
using FreshdeskApi.Client.CustomObjects.Requests;

namespace FreshdeskApi.Client.CustomObjects;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskCustomObjectClient : IFreshdeskCustomObjectClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskCustomObjectClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <summary>
    /// List all known custom objects type
    ///
    /// See: https://developers.freshdesk.com/api/#retrieve_existing_custom_objects
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The response lists all the existing Custom Object schemas along with the field names and their attributes such as field type, marked required or optional etc.</returns>
    public async Task<ListCustomObjectsResponse> ListCustomObjects(CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<ListCustomObjectsResponse>(HttpMethod.Get, $"/api/v2/custom_objects/schemas", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
    
    /// <summary>
    /// Retrieve all details about a single custom object by their id
    ///
    /// See: https://developers.freshdesk.com/api/#creating_a_custom_object
    /// </summary>
    /// <param name="schemaId">
    /// The unique identifier for the schema (Can be found in the URL when navigating on your freshdesk instance)
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>All details about the custom object</returns>
    public async Task<CustomObject> GetCustomObject(string schemaId, CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<CustomObject>(HttpMethod.Get, $"/api/v2/custom_objects/schemas/{schemaId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
    
    /// <summary>
    /// Create a new record for a specific schema.
    /// 
    /// 1. The field names and their attributes to construct the request body can be obtained using the Retrieve Custom Object API.
    /// 2. While creating a record with a Lookup field value, the ID of the Object should be specified as the Lookup field value.
    ///     - For Tickets, display_id should be used as the Lookup field value
    ///     - For Contacts, org_contact_id should be used as the Lookup field value.
    ///     - For Companies, org_company_id should be used as the Lookup field value
    ///     - For Custom Objects, id of the record should be used as the Lookup field value
    /// 
    /// See: https://developers.freshdesk.com/api/#create_a_new_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be created</param>
    /// <param name="recordData">The data to put in the record</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    /// <returns>The details of the created record</returns>
    public async Task<Record<T>> CreateRecord<T>(string schemaId, T recordData, CancellationToken cancellationToken = default)
    {
        var request = new CreateRecordRequest<T>(recordData);
        
        return await _freshdeskClient
            .ApiOperationAsync<Record<T>, CreateRecordRequest<T>>(HttpMethod.Post, $"/api/v2/custom_objects/schemas/{schemaId}/records/", request, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
    
    /// <summary>
    /// Retrieve all the details and data of a single record by their id
    ///
    /// See: https://developers.freshdesk.com/api/#retrieve_a_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be retrieved</param>
    /// <param name="recordId">The id of the record to retrieve</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    /// <returns></returns>
    /// <returns>The details of the record</returns>
    public async Task<Record<T>> GetRecord<T>(string schemaId, string recordId, CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Record<T>>(HttpMethod.Get, $"/api/v2/custom_objects/schemas/{schemaId}/records/{recordId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
    
    /// <summary>
    /// Update a record with new data
    /// 
    /// See: https://developers.freshdesk.com/api/#update_a_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="record">The record to update, with the new data</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    /// <returns>The details of the updated record</returns>
    public async Task<Record<T>> UpdateRecord<T>(string schemaId, Record<T> record, CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Record<T>, Record<T>>(HttpMethod.Put, $"/api/v2/custom_objects/schemas/{schemaId}/records/{record.DisplayId}", record, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Delete a record based on a record instance
    ///
    /// See: https://developers.freshdesk.com/api/#delete_a_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="record">The record to delete</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    public Task DeleteRecord<T>(string schemaId, Record<T> record, CancellationToken cancellationToken = default)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));

        if (record.DisplayId is null)
            throw new ArgumentNullException(nameof(record.DisplayId));
        
        return DeleteRecord(schemaId, record.DisplayId, cancellationToken);
    }


    /// <summary>
    /// Delete a record based on a record id
    ///
    /// See: https://developers.freshdesk.com/api/#delete_a_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="recordId">The id of the record to delete</param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteRecord(string schemaId, string recordId, CancellationToken cancellationToken = default)
    {
        await _freshdeskClient
            .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/custom_objects/schemas/{schemaId}/records/{recordId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve multitple records based on a filter.
    /// Multiple filters can be combined 
    ///
    /// Note:
    ///     1. By default, 20 records will be returned in a single query per page
    ///     2. A maximum of 100 records can be returned in a single query per page and the pages can only be fetched sequentially
    ///     3. For navigating to the next page, use the <see cref="GetNextRecordPage"/> method, using the result of this method as parameter of the other.
    ///     4. Similarly, for navigating to the previous page, use the <see cref="GetPreviousRecordPage"/> method, using the result of this method as parameter of the other.
    ///     5. For retrieving the total number of records , use the <see cref="GetCount"/> method, using the result of this method as parameter of the other.
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="request">The request to limit, filter and sort the list of record to retrieve</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    /// <returns>A page of record, which can be used to retrieve the next/previous page, as well as the count for the current request</returns>
    public async Task<RecordPage<T>> ListRecords<T>(string schemaId, RecordPageRequest request, CancellationToken cancellationToken = default)
    {
        var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        if (request.PageSize is { } pageSize)
        {
            queryString.Add("page_size", pageSize.ToString());
        }

        foreach (var filter in request.Filters ?? [])
        {
            queryString.Add(filter.QueryStringParameterName, filter.Value);
        }

        if (request.Sort is not null)
        {
            queryString.Add(request.Sort.QueryStringParameterName, request.Sort.QueryStringParameterValue);
        }

        return (await FetchRecordPage<T>($"schemas/{schemaId}/records?{queryString.ToString()}", cancellationToken))!;
    }

    /// <summary>
    /// Use the next link from the <see cref="currentPage"/> to retrieve the next page.
    /// The same PageSize, filter and sort will be used
    /// </summary>
    /// <param name="currentPage">The current record page</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data</typeparam>
    /// <returns>The next record page</returns>
    public Task<RecordPage<T>?> GetNextRecordPage<T>(RecordPage<T>? currentPage,CancellationToken cancellationToken = default)
        => FetchRecordPage<T>(currentPage?.Links?.Next?.Href, cancellationToken);
    
    /// <summary>
    /// Use the next link from the <see cref="currentPage"/> to retrieve the previous page.
    /// The same PageSize, filter and sort will be used
    /// </summary>
    /// <param name="currentPage">The current record page</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data</typeparam>
    /// <returns>The previous record page</returns>
    public Task<RecordPage<T>?> GetPreviousRecordPage<T>(RecordPage<T>? currentPage,CancellationToken cancellationToken = default)
        => FetchRecordPage<T>(currentPage?.Links?.Prev?.Href, cancellationToken);

    /// <summary>
    /// Use the count link from the <see cref="currentPage"/> to retrieve the count of record which can be retrieved for the current filter.
    /// </summary>
    /// <param name="currentPage">The current record page</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data</typeparam>
    /// <returns>The record count</returns>
    public async Task<int> GetCount<T>(RecordPage<T>? currentPage, CancellationToken cancellationToken = default)
    {
        if (currentPage is null)
            throw new ArgumentNullException(nameof(currentPage));

        if (currentPage?.Links?.Count?.Href is not { } href)
            throw new ArgumentNullException(nameof(currentPage.Links.Count.Href));
        
        var recordCount = await _freshdeskClient
            .ApiOperationAsync<RecordCount>(HttpMethod.Get, $"/api/v2/custom_objects/{href}", cancellationToken)
            .ConfigureAwait(false);

        return recordCount.Count;
    }

    private async Task<RecordPage<T>?> FetchRecordPage<T>(string? queryUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(queryUrl))
            return null;
        
        return await _freshdeskClient
            .ApiOperationAsync<RecordPage<T>>(HttpMethod.Get, $"/api/v2/custom_objects/{queryUrl}", cancellationToken)
            .ConfigureAwait(false);
    }

}
