using EAuction.Shared.Enum;
using EAuction.Shared.Seller;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Helpers;
using EAuction.Shared.Interface;
using EAuction.Shared.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAuction.Shared.Processors
{
    public class SellerProcessor : ISellerProcessor
    {

        private readonly IUserService _userService;
        private readonly IProductService _product;
        private readonly ILogger _logger;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<Product> _productValidator;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="product"></param>
        /// <param name="userService"></param>
        /// <param name="userValidator"></param>
        /// <param name="productValidator"></param>
        public SellerProcessor(ILoggerFactory loggerFactory, IProductService product, IUserService userService, IValidator<User> userValidator, IValidator<Product> productValidator)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentException(nameof(loggerFactory));
            }

            if (product is null)
            {
                throw new ArgumentException(nameof(product));
            }

            if (userService is null)
            {
                throw new ArgumentException(nameof(userService));
            }

            if (userValidator is null)
            {
                throw new ArgumentException(nameof(userValidator));
            }

            if (productValidator is null)
            {
                throw new ArgumentException(nameof(productValidator));
            }

            _logger = loggerFactory.CreateLogger<SellerProcessor>();
            _product = product;
            _userService = userService;
            _userValidator = userValidator;
            _productValidator = productValidator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ProcessorResponse<int>> AddNewSeller(User user)
        {
            try
            {
                var validate = await _userValidator.ValidateAsync(user);
                if (!validate.IsValid)
                {
                    var errorList = ValidatorExtension.ValidationErrorExtract(validate);
                    var error = ResponseHelper.Error<int>(ResponseCode.Error, errorList.ToArray());
                    return error;
                }
                _logger.LogInformation($"Register New seller: {user.FirstName}");
                var result = await _userService.CreateOrUpdateAsync(user);
                var success = ResponseHelper.Success<int>(result);
                return success;
            }
            catch (UserException ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                var error = ResponseHelper.Error<int>(ResponseCode.Error, ex.Message);
                return error;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ProcessorResponse<int>>AddProduct(Product product)
        {
            try
            {
                var validate = await _productValidator.ValidateAsync(product);

                if (!validate.IsValid)
                {
                    var errorList = ValidatorExtension.ValidationErrorExtract(validate);
                    var error = ResponseHelper.Error<int>(ResponseCode.Error, errorList.ToArray());
                    return error;
                }

                _logger.LogInformation($"Adding New product: {product.ProductName}");
                var result = await _product.CreateOrUpdateProductAsync(product);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public async Task<ProcessorResponse<ProductBids>>ShowBids(string productID)
        {
            try
            {
                _logger.LogInformation($"Showing all bids under product: {productID}");
                var result = await _product.GetbidsByProductID(productID);
                var success = ResponseHelper.Success<ProductBids>(result);
                return success;
            }
            catch (ProductNotFounException ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                var error = ResponseHelper.Error<ProductBids>(ResponseCode.Error, ex.Message);
                return error;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ProcessorResponse<List<Product>>>ShowAllProducts()
        {
            try
            {
                var result = await _product.GetProductsAsync();
                var success = ResponseHelper.Success<List<Product>>(result);
                return success;
            }
            catch (ProductException ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                var error = ResponseHelper.Error<List<Product>>(ResponseCode.Error, ex.Message);
                return error;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public async Task<ProcessorResponse<int>>DeletProduct(string productID)
        {
            try
            {
                var result = await _product.DeleteAsync(productID);
                var success = ResponseHelper.Success<int>(result);
                return success;
            }
            catch (ProductNotFounException ex)
            {
                _logger.LogInformation($"Exception : {ex}");
                var error = ResponseHelper.Error<int>(ResponseCode.Error, ex.Message);
                return error;
            }
        }
    }
}
