using EAuction.Shared.Models;
using EAuction.Shared.Seller;
using EAuction.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace EAuction.BusinessLogic.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IProductDataAccess _productDataAccess;
        public ProductRepository(ILogger<ProductRepository> logger, IMapper mapper, IProductDataAccess productDataAccess)
        {
            _logger = logger;
            _mapper = mapper;
            _productDataAccess = productDataAccess;
        }

        public async Task<int> CreateOrUpdateAsync(Product product)
        {
                product.ProductId = Guid.NewGuid().ToString();
                ProductDto productDto = _mapper.Map<ProductDto>(product);
                int successCode = await _productDataAccess.AddProduct(productDto);
                return successCode;
        }

        public async Task<int> DeleteAsync(string ProductID)
        {
            _logger.LogInformation("start delete product details");
            int successCode = await _productDataAccess.DeleteProduct(ProductID);
            return successCode;
        }

        public async Task<Product> GetProductByIDAsync(string ProductID)
        {
            ProductDto productDto = await _productDataAccess.ShowProduct(ProductID);
            Product receiveProduct = _mapper.Map<Product>(productDto);
            return receiveProduct;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<ProductDto> productDto = await _productDataAccess.ShowAllProduct();
            List<Product> receiveProduct = _mapper.Map<List<Product>>(productDto);
            return receiveProduct;
        }
    }
}
