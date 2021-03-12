using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Companies.Models;
using FreshdeskApi.Client.Companies.Requests;

namespace FreshdeskApi.Client.Companies
{
    /// <inheritdoc />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskCompaniesClient : IFreshdeskCompaniesClient
    {
        private readonly FreshdeskClient _freshdeskClient;

        public FreshdeskCompaniesClient(FreshdeskClient freshdeskClient)
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
            IPaginationConfiguration? pagingConfiguration = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var company in _freshdeskClient.GetPagedResults<Company>("/api/v2/companies", pagingConfiguration, false, cancellationToken).ConfigureAwait(false))
            {
                yield return company;
            }
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<Company> FilterCompaniesAsync(
            string encodedQuery,
            IPaginationConfiguration? pagingConfiguration = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var company in _freshdeskClient.GetPagedResults<Company>($"/api/v2/search/companies?query=\"{encodedQuery}\"", pagingConfiguration, true, cancellationToken).ConfigureAwait(false))
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
                .ApiOperationAsync<ExportCsv>(HttpMethod.Post, "/api/v2/companies/export", request, cancellationToken)
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
                .ApiOperationAsync<ExportCsv>(HttpMethod.Get, $"/api/v2/companies/export/{export.Id}", export, cancellationToken)
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
                .ApiOperationAsync<Company>(HttpMethod.Put, $"/api/v2/companies/{companyId}", request, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Company> CreateCompanyAsync(
            CreateCompanyRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<Company>(HttpMethod.Post, $"/api/v2/companies", request, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
