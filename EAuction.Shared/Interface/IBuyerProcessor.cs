using EAuction.Shared.Seller;
using EAuction.Shared.Models;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface IBuyerProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProcessorResponse<int>>PlaceBid(BuyerBid product);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="buyerEmailId"></param>
        /// <param name="newBidAmount"></param>
        /// <returns></returns>
        Task<ProcessorResponse<int>>UpdateBid(string ProductId, string buyerEmailId, decimal newBidAmount);
    }
}
