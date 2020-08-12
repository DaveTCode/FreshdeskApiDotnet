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
            PaginationConfiguration? pagingConfiguration = null,
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
            PaginationConfiguration? pagingConfiguration = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var company in _freshdeskClient.GetPagedResults<Company>($"/api/v2/search/companies?query=\"{encodedQuery}\"", pagingConfiguration, true, cancellationToken).ConfigureAwait(false))
            {
                yield return company;
            }
        }
    }
}
