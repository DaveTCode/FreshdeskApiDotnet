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
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskCompaniesClient : IFreshdeskCompaniesClient
    {
        private readonly FreshdeskClient _freshdeskClient;

        public FreshdeskCompaniesClient(FreshdeskClient freshdeskClient)
        {
            _freshdeskClient = freshdeskClient;
        }

        /// <summary>
        /// Retrieve all details about a single company by its id.
        ///
        /// c.f. https://developers.freshdesk.com/api/#view_company
        /// </summary>
        /// <param name="companyId">
        /// The unique identifier for the company.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The full company information</returns>
        public async Task<Company> ViewCompanyAsync(
            long companyId,
            CancellationToken cancellationToken = default)
        {
            return await _freshdeskClient
                .ApiOperationAsync<Company>(HttpMethod.Get, $"/api/v2/companies/{companyId}", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// List all available companies
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_companies
        /// </summary>
        ///
        /// <param name="pagingConfiguration"></param>
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The full set of companies, this request is paged and iterating to the
        /// next entry may cause a new API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<Company> ListAllCompaniesAsync(
            IPaginationConfiguration? pagingConfiguration = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var company in _freshdeskClient.GetPagedResults<Company>("/api/v2/companies", pagingConfiguration, false, cancellationToken).ConfigureAwait(false))
            {
                yield return company;
            }
        }

        /// <summary>
        /// Filter the full set of companies with a filter of the form:
        ///
        /// (company_field:integer OR company_field:'string') AND company_field:boolean
        ///
        /// c.f. https://developers.freshdesk.com/api/#filter_companies
        /// </summary>
        /// 
        /// <param name="encodedQuery">
        /// The full query string with params encoded properly.
        ///
        /// Will be appended with ?query="encodedQuery" so don't enclose in quotes.
        /// </param>
        ///
        /// <param name="pagingConfiguration"></param>
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The filtered set of companies, this request is paged and iterating 
        /// to the next entry may cause a new API call to get the next page.
        /// </returns>
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

        /// <summary>
        /// Export companies with the specified information
        /// </summary>
        ///
        /// <param name="request">
        /// All the fields of the companies to be retrieved.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The ExportCsv object which contains an ID to be used</returns>
        public async Task<ExportCsv> StartExportContactsAsync(
            CompaniesExportRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request must not be null");

            return await _freshdeskClient
                .ApiOperationAsync<ExportCsv>(HttpMethod.Post, "/api/v2/companies/export", request, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Check if an export has completed
        /// </summary>
        ///
        /// <param name="export">
        /// The export being checked.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The ExportCsv object which contains an export url to be used</returns>
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


        /// <summary>
        /// Update a company with new details.
        ///
        /// c.f. https://developers.freshdesk.com/api/#update_company
        /// </summary>
        /// 
        /// <param name="companyId">
        /// The unique identifier for the company.
        /// </param>
        ///
        /// <param name="request">
        /// The details about the company to update.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>The newly updated company</returns>
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
    }
}
