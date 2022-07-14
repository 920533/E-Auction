using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using EAuction.Shared.Seller;
using EAuction.Shared.Interface;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EAuctionTests.TestDependencies
{
    public class FakeDependencies : AutoDataAttribute
    {
        public FakeDependencies() : base(CreateFixture)
        {

        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            var productService = fixture.Freeze<Mock<IProductService>>();
            productService.Setup(x => x.CreateOrUpdateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(1);
            productService.Setup(x => x.GetbidsByProductID(It.IsAny<string>()))
                .ReturnsAsync(new ProductBids());
            productService.Setup(x => x.GetProductsAsync())
                .ReturnsAsync(new List<Product>());
            productService.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(1);
            productService.Setup(x => x.AddBidForProductAsync(It.IsAny<BuyerBid>()))
                .ReturnsAsync(1);
            productService.Setup(x => x.UpdateBidForProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()))
               .ReturnsAsync(1);

            var userService = fixture.Freeze<Mock<IUserService>>();
            userService.Setup(x => x.CreateOrUpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(1);

            var mockUserValidator = new Mock<IValidator<User>>();
            mockUserValidator.Setup(x => x.ValidateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            fixture.Register(() => mockUserValidator);


            var mockProductValidator = new Mock<IValidator<Product>>();
            mockProductValidator.Setup(x => x.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            fixture.Register(() => mockProductValidator);

            var mockUserRepository = new Mock<IUserRepository>();

            int usermodel = 1;

            mockUserRepository.Setup(x => x.CreateOrUpdateAsync(It.IsAny<User>()))
                .Callback((int userInput) =>
                {
                    if (userInput == null)
                    {
                        userInput = 0;//Guid.NewGuid().ToString();
                    }
                   
                    usermodel = userInput;

                })
                .ReturnsAsync(() =>
                {
                    return usermodel;
                });

            mockUserRepository.Setup(x => x.GetUserByIDAsync(It.IsAny<string>()))
                .ReturnsAsync(new User());
            fixture.Register(() => mockUserRepository);

            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository.Setup(x => x.GetProductByIDAsync(It.IsAny<string>()))
                .ReturnsAsync(new Product());

            int productModel = 1;

            mockProductRepository.Setup(x => x.CreateOrUpdateAsync(It.IsAny<Product>()))
                .Callback((int productInput) =>
                {
                    if (productInput == null)
                    {
                        productInput = 0;//Guid.NewGuid().ToString();
                    }
                    //else
                    //{
                    //    productInput.BidEndDate = DateTime.Now.AddDays(30);
                    //}
                    productModel = productInput;

                })
                .ReturnsAsync(() =>
                {
                    return productModel;
                });


            fixture.Register(() => mockProductRepository);

            var mockProductToBuyerRepository = new Mock<IProductToBuyerRepository>();
            mockProductToBuyerRepository.Setup(x => x.CreateOrUpdateAsync(It.IsAny<ProductToBuyer>()))
                .ReturnsAsync(1);

            mockProductToBuyerRepository.Setup(x => x.GetBidByProductIDAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<ProductToBuyer>() { new ProductToBuyer(){
                    CreatedDate= DateTime.Now.AddDays(2)
                } });

            fixture.Register(() => mockProductToBuyerRepository);

            fixture.Customize(new AutoMoqCustomization());

            return fixture;
        }
    }
}
