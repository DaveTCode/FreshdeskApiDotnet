using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CustomObjects.Models;
using FreshdeskApi.Client.CustomObjects.Requests;
using FreshdeskApi.Client.Pagination;

namespace FreshdeskApi.Client.CustomObjects;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IFreshdeskCustomObjectClient
{
    public const string UrlPrefix = "/api/v2/custom_objects";

    /// <summary>
    /// List all known custom objects type
    ///
    /// See: https://developers.freshdesk.com/api/#retrieve_existing_custom_objects
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The response lists all the existing Custom Object schemas along with the field names and their attributes such as field type, marked required or optional etc.</returns>
    Task<ListCustomObjectsResponse> ListCustomObjectsAsync(CancellationToken cancellationToken = default);

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
    Task<CustomObject> GetCustomObjectAsync(string schemaId, CancellationToken cancellationToken = default);

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
    Task<Record<T>> CreateRecordAsync<T>(string schemaId, T recordData, CancellationToken cancellationToken = default);

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
    Task<Record<T>> GetRecordAsync<T>(string schemaId, string recordId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a record with new data
    ///
    /// See: https://developers.freshdesk.com/api/#update_a_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="recordData">The record to update, with the new data</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    /// <returns>The details of the updated record</returns>
    Task<Record<T>> UpdateRecordAsync<T>(string schemaId, Record<T> recordData, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a record based on a record id
    ///
    /// See: https://developers.freshdesk.com/api/#delete_a_custom_object_record
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="recordId">The id of the record to delete</param>
    /// <param name="cancellationToken"></param>
    Task DeleteRecordAsync(string schemaId, string recordId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve all records based on a filter.
    /// Multiple filters can be combined
    ///
    /// Note:
    ///     1. By default, 20 records will be returned in a single query per page
    ///     2. A maximum of 100 records can be returned in a single query per page and the pages can only be fetched sequentially
    /// </summary>
    /// <param name="request">The request to filter and sort the list of record to retrieve</param>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="pagingConfiguration">The pagination configuration</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data. Must match the definition of the specified schemaId</typeparam>
    /// <returns>A page of record, which can be used to retrieve the next/previous page, as well as the count for the current request</returns>
    public IAsyncEnumerable<Record<T>> ListAllRecordsAsync<T>(
        ListAllRecordsRequest request,
        string schemaId,
        TokenBasedPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieve count of record in the schema
    /// </summary>
    /// <param name="schemaId">The schema in which the record must be updated</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">The type of the data</typeparam>
    /// <returns>The record count</returns>
    Task<int> GetCountAsync<T>(string schemaId, CancellationToken cancellationToken = default);
}
