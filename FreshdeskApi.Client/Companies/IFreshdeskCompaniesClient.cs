using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.CommonModels;
using FreshdeskApi.Client.Companies.Models;
using FreshdeskApi.Client.Companies.Requests;
using FreshdeskApi.Client.Pagination;

namespace FreshdeskApi.Client.Companies;

// compile with: -doc:DocFileName.xml
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
        ListPaginationConfiguration? pagingConfiguration = null,
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
    /// <param name="pagingConfiguration">NOTE: The PageSize can't be configured for this api</param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The filtered set of companies, this request is paged and iterating 
    /// to the next entry may cause a new API call to get the next page.
    /// </returns>
    IAsyncEnumerable<Company> FilterCompaniesAsync(
        string encodedQuery,
        PageBasedPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);

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
    /// <returns>Returns an ExportCsv which can be passed to GetExportStatusAsync to monitor the status of the export asynchronously</returns>
    Task<ExportCsv> StartExportCompaniesAsync(
        CompaniesExportRequest request,
        CancellationToken cancellationToken = default);

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
    Task<ExportCsv> GetExportStatusAsync(
        ExportCsv export,
        CancellationToken cancellationToken = default);


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
    Task<Company> UpdateCompanyAsync(
        long companyId,
        UpdateCompanyRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a company with new details.
    ///
    /// c.f. https://developers.freshdesk.com/api/#create_company
    /// </summary>
    ///
    /// <param name="request">
    /// The details about the company to update.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The newly updated company</returns>
    Task<Company> CreateCompanyAsync(
        CreateCompanyRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a company.
    ///
    /// c.f. https://developers.freshdesk.com/api/#delete_company
    /// </summary>
    ///
    /// <param name="companyId">
    /// The company to delete.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    Task DeleteCompanyAsync(
        long companyId,
        CancellationToken cancellationToken = default);
}
