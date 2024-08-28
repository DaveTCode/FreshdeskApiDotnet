using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FreshdeskApi.Client.Products.Models;

namespace FreshdeskApi.Client.Products;

public interface IFreshdeskProductClient
{
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
    Task<Product> ViewProductAsync(
        long productId,
        CancellationToken cancellationToken = default);

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
    IAsyncEnumerable<Product> ListAllProductsAsync(
        ListPaginationConfiguration? pagingConfiguration = null,
        CancellationToken cancellationToken = default);
}
