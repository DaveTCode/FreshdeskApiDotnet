using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Companies.Models;

namespace FreshdeskApi.Client.Companies
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FreshdeskCompaniesClient
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
            return await _freshdeskClient.ApiOperationAsync<Company>(HttpMethod.Get, $"/api/v2/companies/{companyId}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// List all available companies
        ///
        /// c.f. https://developers.freshdesk.com/api/#list_all_companies
        /// </summary>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The full set of companies, this request is paged and iterating to the
        /// next entry may cause a new API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<Company> ListAllCompaniesAsync(
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var company in _freshdeskClient.GetPagedResults<Company>("/api/v2/companies", cancellationToken))
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
        /// <param name="unencodedQuery">
        /// The full query string in unencoded form.
        /// </param>
        ///
        /// <param name="cancellationToken"></param>
        ///
        /// <returns>
        /// The filtered set of companies, this request is paged and iterating 
        /// to the next entry may cause a new API call to get the next page.
        /// </returns>
        public async IAsyncEnumerable<Company> FilterCompaniesAsync(
            string unencodedQuery,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var encodedQuery = Uri.EscapeDataString(unencodedQuery);

            await foreach (var company in _freshdeskClient.GetPagedResults<Company>($"/api/v2/search/companies?query={encodedQuery}", cancellationToken))
            {
                yield return company;
            }
        }
    }
}
