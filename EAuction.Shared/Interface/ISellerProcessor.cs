using EAuction.Shared.Seller;
using EAuction.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface ISellerProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ProcessorResponse<int>> AddNewSeller(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProcessorResponse<int>> AddProduct(Product product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Task<ProcessorResponse<ProductBids>> ShowBids(string productID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ProcessorResponse<List<Product>>> ShowAllProducts();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Task<ProcessorResponse<int>> DeletProduct(string productID);
    }
}
