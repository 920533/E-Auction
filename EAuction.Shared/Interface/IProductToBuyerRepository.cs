using EAuction.Shared.Models;
using EAuction.Shared.Seller;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface IProductToBuyerRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<ProductToBuyer>> GetProductsByBuyerIdAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<List<ProductToBuyer>> GetBidByProductIDAsync(string productId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ProductToBuyer> GetProductByUserIDAsync(string productId, string UserID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<int> CreateOrUpdateAsync(ProductToBuyer productToBuyer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string ProductID, string userID);
    }
}
