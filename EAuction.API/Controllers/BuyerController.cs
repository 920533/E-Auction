using EAuction.Shared.Enum;
using EAuction.Shared.Seller;
using EAuction.Shared.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EAuctionBuyer.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("e-auction-buyer/api/v{v:apiVersion}/[controller]")]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerProcessor _buyerProcessor;
        public BuyerController(IBuyerProcessor buyerProcessor)
        {
            if (buyerProcessor is null)
            {
                throw new ArgumentException(nameof(buyerProcessor));
            }
            _buyerProcessor = buyerProcessor;
        }

        [HttpPost]
        [Route("place-bid")]
        public async Task<IActionResult> PlaceBid([FromBody] BuyerBid product)
        {
            var result = await _buyerProcessor.PlaceBid(product);
            return result.ResponseCode switch
            {
                ResponseCode.Success => Ok(result),
                ResponseCode.Error => BadRequest(result),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Errors),
            };
        }

        [HttpPost]
        [Route("update-bid/{ProductId}/{buyerEmailId}/{newBidAmount}")]
        public async Task<IActionResult> UpdateBid(string ProductId, string buyerEmailId, decimal newBidAmount)
        {
            var result = await _buyerProcessor.UpdateBid(ProductId, buyerEmailId, newBidAmount);
            return result.ResponseCode switch
            {
                ResponseCode.Success => Ok(result),
                ResponseCode.Error => BadRequest(result),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, result.Errors),
            };
        }
    }
}

