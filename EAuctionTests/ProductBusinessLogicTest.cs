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
        private readonly ILogger<ProductToBuyerRepository> _loggerbuyer;
        private readonly ProductDto _sampleProductDto;
        private readonly Product _sampleProduct;
        private readonly ProductToBuyerDto _sampleProductToBuyerDto;
        private readonly ProductToBuyer _sampleProductToBuyer;
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

            _sampleProductToBuyerDto = new ProductToBuyerDto()
            {
                ProductId = "1234",
                BuyerProductId = "567"
            };
            _sampleProductToBuyer = new ProductToBuyer()
            {
                ProductId = "1234",
                BuyerProductId = "567"
            };
            Mock<IProductDataAccess> mockDataAccess = new Mock<IProductDataAccess>();
            mockDataAccess.Setup(x => x.AddProduct(It.IsAny<ProductDto>())).Returns(Task.FromResult(1));
            mockDataAccess.Setup(x => x.AddProductToBuyer(It.IsAny<ProductToBuyerDto>())).Returns(Task.FromResult(1));
            _productDataAccess = mockDataAccess.Object;

            Mock<ILogger<ProductRepository>> mocklogger = new Mock<ILogger<ProductRepository>>();
            _logger = mocklogger.Object;

            Mock<ILogger<ProductToBuyerRepository>> mockloggerbuyer = new Mock<ILogger<ProductToBuyerRepository>>();
            _loggerbuyer = mockloggerbuyer.Object;

            Mock<IMapper> mockmapper = new Mock<IMapper>();
            mockmapper.Setup(x=>x.Map<Product>(It.IsAny<ProductDto>())).Returns(_sampleProduct);
            mockmapper.Setup(x=>x.Map<ProductDto>(It.IsAny<Product>())).Returns(_sampleProductDto);
            _mapper = mockmapper.Object;

            Mock<IMapper> mockmapperbuyer= new Mock<IMapper>();
            mockmapperbuyer.Setup(x => x.Map<ProductToBuyer>(It.IsAny<ProductToBuyerDto>())).Returns(_sampleProductToBuyer);
            mockmapperbuyer.Setup(x => x.Map<ProductToBuyerDto>(It.IsAny<ProductToBuyer>())).Returns(_sampleProductToBuyerDto);
            _mapper = mockmapperbuyer.Object;
        }

        [Fact]
        public async Task CreateProductSuccess()
        {
            ProductRepository productRepository = new ProductRepository(_logger,_mapper,_productDataAccess);
            int status = await productRepository.CreateOrUpdateAsync(_sampleProduct);
                Assert.Equal(status,1);
        }

        [Fact]
        public async Task CreateProductToBuyerSuccess()
        {

            ProductToBuyerRepository productRepository = new ProductToBuyerRepository(_loggerbuyer, _mapper, _productDataAccess);
            int status = await productRepository.CreateOrUpdateAsync(_sampleProductToBuyer);
            Assert.Equal(status, 1);
        }
    }
}
