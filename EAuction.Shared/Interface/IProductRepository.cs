using EAuction.Shared.Seller;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetProductsAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Task<Product> GetProductByIDAsync(string ProductID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<int> CreateOrUpdateAsync(Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(string ProductID);
    }
}
