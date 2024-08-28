using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Extensions;
using FreshdeskApi.Client.Models;
using FreshdeskApi.Client.Pagination;
using FreshdeskApi.Client.Products.Models;

namespace FreshdeskApi.Client.Products;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FreshdeskProductClient : IFreshdeskProductClient
{
    private readonly IFreshdeskHttpClient _freshdeskClient;

    public FreshdeskProductClient(IFreshdeskHttpClient freshdeskClient)
    {
        _freshdeskClient = freshdeskClient;
    }

    /// <summary>
    /// Retrieve all details about a single product by its id.
    ///
    /// c.f. https://developers.freshdesk.com/api/#view_product
    /// </summary>
    /// <param name="productId">
    /// The unique identifier for the product.
    /// </param>
    ///
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>The full product information</returns>
    public async Task<Product> ViewProductAsync(
        long productId,
        CancellationToken cancellationToken = default)
    {
        return await _freshdeskClient
            .ApiOperationAsync<Product>(HttpMethod.Get, $"/api/v2/products/{productId}", cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// List all available products
    ///
    /// c.f. https://developers.freshdesk.com/api/#list_all_products
    /// </summary>
    ///
    /// <param name="pagingConfiguration"></param>
    /// <param name="cancellationToken"></param>
    ///
    /// <returns>
    /// The full set of products, this request is paged and iterating to the
    /// next entry may cause a new API call to get the next page.
    /// </returns>
    public async IAsyncEnumerable<Product> ListAllProductsAsync(
        ListPaginationConfiguration? pagingConfiguration = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        pagingConfiguration ??= new ListPaginationConfiguration();

        await foreach (var product in _freshdeskClient
            .GetPagedResults<Product>("/api/v2/products", pagingConfiguration, cancellationToken)
            .ConfigureAwait(false))
        {
            yield return product;
        }
    }
}
