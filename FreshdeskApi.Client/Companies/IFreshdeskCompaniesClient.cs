using FreshdeskApi.Client.Companies.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreshdeskApi.Client.Companies
{
    public interface IFreshdeskCompaniesClient
    {
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
        Task<Company> ViewCompanyAsync(
            long companyId,
            CancellationToken cancellationToken = default);

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
        IAsyncEnumerable<Company> ListAllCompaniesAsync(
            IPaginationConfiguration? pagingConfiguration = null,
            CancellationToken cancellationToken = default);

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
        IAsyncEnumerable<Company> FilterCompaniesAsync(
            string encodedQuery,
            IPaginationConfiguration? pagingConfiguration = null,
            CancellationToken cancellationToken = default);
    }
}
