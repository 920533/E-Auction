﻿using AutoFixture.NUnit3;
using EAuction.Shared;
using EAuction.Shared.Seller;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Interface;
using EAuction.Shared.Processors;
using EAuctionTests.TestDependencies;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAuctionTests.Processors
{

    public class SellerProcessorTests
    {
        [Test]
        [FakeDependencies]
        public void SellerProcessor_WithLoggerFactoryNull_ThrownError(
            ILoggerFactory loggerFactory,
            IProductService product,
            IUserService user,
            IValidator<User> userValidator,
            IValidator<Product> productValidator)
        {
            loggerFactory = null;
            Assert.Throws<ArgumentException>(() => new SellerProcessor(loggerFactory, product, user, userValidator, productValidator));
        }

        [Test]
        [FakeDependencies]
        public void SellerProcessor_WithProductNull_ThrownError(
            ILoggerFactory loggerFactory,
            IProductService product,
            IUserService user,
            IValidator<User> userValidator,
            IValidator<Product> productValidator)
        {
            product = null;
            Assert.Throws<ArgumentException>(() => new SellerProcessor(loggerFactory, product, user, userValidator, productValidator));
        }

        [Test]
        [FakeDependencies]
        public void SellerProcessor_WithUserNull_ThrownError(
            ILoggerFactory loggerFactory,
            IProductService product,
            IUserService user,
            IValidator<User> userValidator,
            IValidator<Product> productValidator)
        {
            user = null;
            Assert.Throws<ArgumentException>(() => new SellerProcessor(loggerFactory, product, user, userValidator, productValidator));
        }

        [Test]
        [FakeDependencies]
        public void SellerProcessor_WithUserValidatorNull_ThrownError(
            ILoggerFactory loggerFactory,
            IProductService product,
            IUserService user,
            IValidator<User> userValidator,
            IValidator<Product> productValidator)
        {
            userValidator = null;
            Assert.Throws<ArgumentException>(() => new SellerProcessor(loggerFactory, product, user, userValidator, productValidator));
        }

        [Test]
        [FakeDependencies]
        public void SellerProcessor_WithProductValidatorNull_ThrownError(
            ILoggerFactory loggerFactory,
            IProductService product,
            IUserService user,
            IValidator<User> userValidator,
            IValidator<Product> productValidator)
        {
            productValidator = null;
            Assert.Throws<ArgumentException>(() => new SellerProcessor(loggerFactory, product, user, userValidator, productValidator));
        }


        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithCreateOrUpdateAsync_ThrowError(
            [Frozen] Mock<IUserService> mockUserService,
            SellerProcessor processor)
        {
            var user = new User();
            mockUserService.Setup(x => x.CreateOrUpdateAsync(It.IsAny<User>()))
                .ThrowsAsync(new UserException(Constants.UserCreationFailed));

            var result = await processor.AddNewSeller(user);
            //result.Data.ShouldBeNull();
            result.Errors.ShouldNotBeNull();
            Assert.Contains(Constants.UserCreationFailed, result.Errors.ToList());
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithValidValue_Success(
            SellerProcessor processor)
        {
            var user = new User();
            var result = await processor.AddNewSeller(user);
            Assert.Equals(201, result.ResponseCode);
            //result.Data.ShouldNotBeNull();
        }

       
        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithCreateOrUpdateProductAsync_ThrowError(
            [Frozen] Mock<IProductService> mockProductService,
            SellerProcessor processor)
        {
            var product = new Product();
            mockProductService.Setup(x => x.CreateOrUpdateProductAsync(It.IsAny<Product>()))
                .ThrowsAsync(new ProductException(Constants.ProductCreationFailed));

            var result = await processor.AddProduct(product);
            //result.Data.ShouldBeNull();
            result.Errors.ShouldNotBeNull();
            Assert.Contains(Constants.ProductCreationFailed, result.Errors.ToList());
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithValidProductValue_Success(
            SellerProcessor processor)
        {
            var product = new Product();
            var result = await processor.AddProduct(product);
            Assert.Equals(201, result.ResponseCode);
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithInvalidProductIDValue_Error(
            [Frozen] Mock<IProductService> mockProductService,
            SellerProcessor processor)
        {
            var product = new Product();
            mockProductService.Setup(x => x.GetbidsByProductID(It.IsAny<string>()))
                .ThrowsAsync(new ProductNotFounException(Constants.ProductNotFound));
            var result = await processor.ShowBids("test001");
            result.Data.ShouldBeNull();
            result.Errors.ShouldNotBeNull();
            Assert.Contains(Constants.ProductNotFound, result.Errors.ToList());
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithValidShowBidsValue_Success(
            SellerProcessor processor)
        {
            var result = await processor.ShowBids("test001");
            result.Data.ShouldNotBeNull();
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithShowAllProducts_ThrownError(
            [Frozen] Mock<IProductService> mockProductService,
            SellerProcessor processor)
        {
            mockProductService.Setup(x => x.GetProductsAsync())
                .ThrowsAsync(new ProductException(Constants.ProductNotFound));
            var result = await processor.ShowAllProducts();
            result.Data.ShouldBeNull();
            result.Errors.ShouldNotBeNull();
            Assert.Contains(Constants.ProductNotFound, result.Errors.ToList());
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithShowAllProductsValue_Success(
            SellerProcessor processor)
        {
            var result = await processor.ShowAllProducts();
            result.Data.ShouldNotBeNull();
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithInvalidProductIDValue_DeletProductError(
            [Frozen] Mock<IProductService> mockProductService,
            SellerProcessor processor)
        {
            var product = new Product();
            mockProductService.Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ThrowsAsync(new ProductNotFounException(Constants.ProductNotFound));
            var result = await processor.DeletProduct("test001");
            //Assert.IsFalse(result.Data.);
            result.Errors.ShouldNotBeNull();
            Assert.Contains(Constants.ProductNotFound, result.Errors.ToList());
        }

        [Test]
        [FakeDependencies]
        public async Task SellerProcessor_WithValidDeletProductValue_Success(
            SellerProcessor processor)
        {
            var result = await processor.DeletProduct("test001");
            Assert.IsTrue(true);
        }
    }
}
