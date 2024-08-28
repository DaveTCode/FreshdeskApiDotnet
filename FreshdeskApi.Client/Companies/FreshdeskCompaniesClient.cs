using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Companies.Models;
using FreshdeskApi.Client.Companies.Requests;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Models;

namespace FreshdeskApi.Client.Companies;

/// <inheritdoc />
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskCompaniesClient : IFreshdeskCompaniesClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskCompaniesClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <inheritdoc />
    public async Task<Company> ViewCompanyAsync(
        long companyId,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Company>(HttpMethod.Get, $"/api/v2/companies/{companyId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<Company> ListAllCompaniesAsync(
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var company in _freshdeskClient
            .GetPagedResults<Company>("/api/v2/companies", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return company;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<Company> FilterCompaniesAsync(
        string encodedQuery,
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new PageBasedPaginationConfiguration();

        await foreach (var company in _freshdeskClient
            .GetPagedResults<Company>($"/api/v2/search/companies?query=\"{encodedQuery}\"", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return company;
        }
    }

    /// <inheritdoc />
    public async Task<ExportCsv> StartExportCompaniesAsync(
        CompaniesExportRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _freshdeskClient
            .ApiOperationAsync<ExportCsv, CompaniesExportRequest>(HttpMethod.Post, "/api/v2/companies/export", request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<ExportCsv> GetExportStatusAsync(
        ExportCsv export,
        CancellationToken cancellationToken = default)
    {
        if (export == null) throw new ArgumentNullException(nameof(export), "Export must not be null");
        if (export.Id == null) throw new ArgumentNullException(nameof(export.Id), "Export Id must not be null");

        return await _freshdeskClient
            .ApiOperationAsync<ExportCsv, ExportCsv>(HttpMethod.Get, $"/api/v2/companies/export/{export.Id}", export, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Company> UpdateCompanyAsync(
        long companyId,
        UpdateCompanyRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _freshdeskClient
            .ApiOperationAsync<Company, UpdateCompanyRequest>(HttpMethod.Put, $"/api/v2/companies/{companyId}", request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Company> CreateCompanyAsync(
        CreateCompanyRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

        return await _freshdeskClient
            .ApiOperationAsync<Company, CreateCompanyRequest>(HttpMethod.Post, $"/api/v2/companies", request, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteCompanyAsync(
        long companyId,
        CancellationToken cancellationToken = default)
    {
        await _freshdeskClient
            .ApiOperationAsync<object>(HttpMethod.Delete, $"/api/v2/companies/{companyId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}
