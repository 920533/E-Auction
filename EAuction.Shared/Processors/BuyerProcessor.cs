using EAuction.Shared.Enum;
using EAuction.Shared.Seller;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Helpers;
using EAuction.Shared.Interface;
using EAuction.Shared.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EAuction.Shared.Processors
{
    public class BuyerProcessor : IBuyerProcessor
    {
        private readonly IProductService _product;
        private readonly ILogger _logger;

        public BuyerProcessor(ILoggerFactory loggerFactory, IProductService product)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentException(nameof(loggerFactory));
            }

            if (product is null)
            {
                throw new ArgumentException(nameof(product));
            }

            _logger = loggerFactory.CreateLogger<SellerProcessor>();
            _product = product;
        }

        public async Task<ProcessorResponse<int>>PlaceBid(BuyerBid product)
        {
            try
            {
                var result = await _product.AddBidForProductAsync(product);
                var success = ResponseHelper.Success<int>(result);
                return success;
            }
            catch (ProductException ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                var error = ResponseHelper.Error<int>(ResponseCode.Error, ex.Message);
                return error;
            }
        }

        public async Task<ProcessorResponse<int>>UpdateBid(string ProductId, string buyerEmailId, decimal newBidAmount)
        {
            try
            {
                var result = await _product.UpdateBidForProduct(ProductId, buyerEmailId, newBidAmount);
                var success = ResponseHelper.Success<int>(result);
                return success;
            }
            catch (ProductException ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                var error = ResponseHelper.Error<int>(ResponseCode.Error, ex.Message);
                return error;
            }
        }
    }
}
