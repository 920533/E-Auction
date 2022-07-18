using AutoMapper;
using EAuction.BusinessLogic.Repository;
using EAuction.Shared.Interface;
using EAuction.Shared.Models;
using EAuction.Shared.Seller;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EAuction.Tests
{
    public class ProductBusinessLogicTest
    {
        private readonly IProductDataAccess _productDataAccess;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;
        private readonly ProductDto _sampleProductDto;
        private readonly Product _sampleProduct;
        public ProductBusinessLogicTest()
        {
            _sampleProductDto = new ProductDto()
            {
                ProductId = "1234",
                ProductName = "ABC",
                StartingPrice = 45
            };
            _sampleProduct = new Product()
            {
                ProductId = "1234",
                ProductName = "ABC",
                StartingPrice = 45
            };
            Mock<IProductDataAccess> mockDataAccess = new Mock<IProductDataAccess>();
            mockDataAccess.Setup(x => x.AddProduct(It.IsAny<ProductDto>())).Returns(Task.FromResult(1));
            mockDataAccess.Setup(x => x.AddProductToBuyer(It.IsAny<ProductToBuyerDto>())).Returns(Task.FromResult(1));
            _productDataAccess = mockDataAccess.Object;

            Mock<ILogger<ProductRepository>> mocklogger = new Mock<ILogger<ProductRepository>>();
            _logger = mocklogger.Object;

            Mock<IMapper> mockmapper = new Mock<IMapper>();
            mockmapper.Setup(x=>x.Map<Product>(It.IsAny<ProductDto>())).Returns(_sampleProduct);
            mockmapper.Setup(x=>x.Map<ProductDto>(It.IsAny<Product>())).Returns(_sampleProductDto);
            _mapper = mockmapper.Object;
        }

        [Fact]
        public async Task CreateProductSuccess()
        {
            ProductRepository productRepository = new ProductRepository(_logger,_mapper,_productDataAccess);
            int status = await productRepository.CreateOrUpdateAsync(_sampleProduct);
                Assert.Equal(status,1);
        }
    }
}
