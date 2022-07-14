using EAuction.Shared.Models;
using EAuction.Shared.Seller;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetProductsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<int> CreateOrUpdateProductAsync(Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string ProductID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<int> AddBidForProductAsync(BuyerBid product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Task<ProductBids> GetbidsByProductID(string ProductID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="buyerEmail"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<int> UpdateBidForProduct(string ProductID, string buyerEmail, decimal amount);
    }
}
