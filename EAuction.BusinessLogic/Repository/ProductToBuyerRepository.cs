using EAuction.Shared.Models;
using EAuction.Shared.Seller;
using EAuction.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace EAuction.BusinessLogic.Repository
{
    public class ProductToBuyerRepository : IProductToBuyerRepository
    {
        private readonly ILogger<ProductToBuyerRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IProductDataAccess _productDataAccess;
        public ProductToBuyerRepository(ILogger<ProductToBuyerRepository> logger, IMapper mapper, IProductDataAccess productDataAccess)
        {
            _logger = logger;
            _mapper = mapper;
            _productDataAccess = productDataAccess;
        }

        public async Task<int> CreateOrUpdateAsync(ProductToBuyer productToBuyer)
        {
            _logger.LogInformation("start product to buyer  details");
            productToBuyer.BuyerProductId = Guid.NewGuid().ToString();
            ProductToBuyerDto productToBuyerDto = _mapper.Map<ProductToBuyerDto>(productToBuyer);
            int successCode = await _productDataAccess.AddProductToBuyer(productToBuyerDto);
            return successCode;
        }

        public async Task<int> DeleteAsync(string ProductID, string userID)
        {
            _logger.LogInformation("start delete product to Buyer details");
            int successCode = await _productDataAccess.DeleteProductToBuyer(ProductID,userID);
            return successCode;
        }

        public async Task<List<ProductToBuyer>> GetBidByProductIDAsync(string productId)
        {
            ProductToBuyerDto productBuyerDto = await _productDataAccess.GetBidByProductIDAsync(productId);
           List<ProductToBuyer>receiveProductBuyer = _mapper.Map<List<ProductToBuyer>>(productBuyerDto);
            return receiveProductBuyer;
        }

        public async Task<ProductToBuyer> GetProductByUserIDAsync(string productId, string userID)
        {
            ProductToBuyerDto productBuyerDto = await _productDataAccess.GetProductByUserIDAsync(productId, userID);
            ProductToBuyer receiveProductBuyer = _mapper.Map<ProductToBuyer>(productBuyerDto);
            return receiveProductBuyer;
        }

        public async Task<List<ProductToBuyer>> GetProductsByBuyerIdAsync()
        {
            List<ProductToBuyerDto> productDto = await _productDataAccess.GetProductsByBuyerIdAsync();
            List<ProductToBuyer> receiveProduct = _mapper.Map<List<ProductToBuyer>>(productDto);
            return receiveProduct;
        }
    }
}
