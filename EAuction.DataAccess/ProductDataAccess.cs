using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Options;
using EAuction.Shared.Interface;
using EAuction.Shared.Models;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Seller;

namespace EAuction.DataAccess
{
    public class ProductDataAccess : IProductDataAccess
    {
        private readonly ILogger<ProductDataAccess> _logger;
        private readonly string _connectionString;

        public ProductDataAccess(ILogger<ProductDataAccess> logger,IOptions<ConnectionStrings> connectionString)
        {
            _logger = logger;
            _connectionString = connectionString.Value.DbConnection;
        }

        public async Task<int>AddProduct(ProductDto productDto)
        {
            try
            {
                _logger.LogInformation("Strat to execute add product procedure");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "EXEC InsertOrUpdateProduct @ProductId,@ProductName,@ShortDeceription,@DetailedDeceription,@Category,@StartingPrice,@BidEndDate";
                int rowAffected = await sqlConnection.ExecuteAsync(innerText, productDto);
                return rowAffected;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ProductDto>ShowProduct(string productId)
        {
            try
            {
                _logger.LogInformation("start to execute get product");
                ProductDto productDto = new ProductDto();
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from Product where ProductId=@ProductId";
                IEnumerable<ProductDto> productFromDatabase = await sqlConnection.QueryAsync<ProductDto>
                    (innerText,
                    new
                    {
                        ProductId = productId
                    });
                List<ProductDto> productList = productFromDatabase.ToList();
                if (productList.Any())
                {
                    productDto = productList[0];
                }
                else
                {
                    throw new NotFoundException($"Could not find Product{ productId }", StatusCodes.Status404NotFound);
                }
                return productDto;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }


        public async Task<List<ProductDto>> ShowAllProduct()
        {
            try
            {
                _logger.LogInformation("start to execute get product");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from Product";
                IEnumerable<ProductDto> productFromDatabase = await sqlConnection.QueryAsync<ProductDto>
                    (innerText);
                List<ProductDto> productList = productFromDatabase.ToList();
                if (productList == null)
                {
                    throw new NotFoundException($"Could not find Product", StatusCodes.Status404NotFound);
                }
                return productList;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<int>DeleteProduct(string productId)
        {
            try
            {
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "delete from Product where ProductId=@ProductId";
                int rowAffected = await sqlConnection.ExecuteAsync(innerText, new { ProductId=productId});
                return rowAffected;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }



        public async Task<int> AddProductToBuyer(ProductToBuyerDto productDto)
        {
            try
            {
                _logger.LogInformation("Strat to execute add product to buyer");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "EXEC InsertOrUpdateProductBuyer @BuyerProductId,@ProductId,@UserId,@BidAmount";
                int rowAffected = await sqlConnection.ExecuteAsync(innerText, productDto);
                return rowAffected;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ProductToBuyerDto> GetBidByProductIDAsync(string productId)
        {
            try
            {
                _logger.LogInformation("start to execute get product to buyer");
                ProductToBuyerDto productToBuyerDto = new ProductToBuyerDto();
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from ProductBuyer where ProductId=@ProductId";
                IEnumerable<ProductToBuyerDto> producttoBuyerFromDatabase = await sqlConnection.QueryAsync<ProductToBuyerDto>
                    (innerText,
                    new
                    {
                        ProductId = productId
                    });
                List<ProductToBuyerDto> productToBuyerList = producttoBuyerFromDatabase.ToList();
                if (productToBuyerList.Any())
                {
                    productToBuyerDto = productToBuyerList[0];
                }
                else
                {
                    throw new NotFoundException($"Could not find ProductBuyer{ productId }", StatusCodes.Status404NotFound);
                }
                return productToBuyerDto;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<ProductToBuyerDto> GetProductByUserIDAsync(string productId, string userId)
        {
            try
            {
                _logger.LogInformation("start to execute get product");
                ProductToBuyerDto productToBuyerDto = new ProductToBuyerDto();
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from ProductBuyer where ProductId=@ProductId And userId=@UserId";
                IEnumerable<ProductToBuyerDto> productBuyerFromDatabase = await sqlConnection.QueryAsync<ProductToBuyerDto>
                    (innerText,
                    new
                    {
                        ProductId = productId,
                        UserId= userId
                    });
                List<ProductToBuyerDto> productToBuyerList = productBuyerFromDatabase.ToList();
                if (productToBuyerList.Any())
                {
                    productToBuyerDto = productToBuyerList[0];
                }
                else
                {
                    throw new NotFoundException($"Could not find ProductBuyer{ productId }", StatusCodes.Status404NotFound);
                }
                return productToBuyerDto;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<List<ProductToBuyerDto>> GetProductsByBuyerIdAsync()
        {
            try
            {
                _logger.LogInformation("start to execute get product");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from ProductBuyer";
                IEnumerable<ProductToBuyerDto> productFromDatabase = await sqlConnection.QueryAsync<ProductToBuyerDto>
                    (innerText);
                List<ProductToBuyerDto> productToBuyerList = productFromDatabase.ToList();
                if (productToBuyerList == null)
                {
                    throw new NotFoundException($"Could not find Buyer", StatusCodes.Status404NotFound);
                }
                return productToBuyerList;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<int> DeleteProductToBuyer(string productId, string userId)
        {
            try
            {
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "delete from ProductBuyer where ProductId=@ProductId and UserId=@UserId";
                int rowAffected = await sqlConnection.ExecuteAsync(innerText, new { ProductId = productId,UserId= userId });
                return rowAffected;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }


        public async Task<int> AddUser(UserDto productDto)
        {
            try
            {
                _logger.LogInformation("Strat to execute add product procedure");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "EXEC InsertOrUpdateUser @UserId,@FirstName,@LastName,@Address,@City,@State,@Pin,@Phone,@Email,@UserName,@Password,@UserType";
                int rowAffected = await sqlConnection.ExecuteAsync(innerText, productDto);
                return rowAffected;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<UserDto>GetUserByEmail(string email)
        {
            try
            {
                _logger.LogInformation("start to execute get user by email");
                UserDto productDto = new UserDto();
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from User where Email=@Email";
                IEnumerable<UserDto> userFromDatabase = await sqlConnection.QueryAsync<UserDto>
                    (innerText,
                    new
                    {
                       email = email
                    });
                List<UserDto> userList = userFromDatabase.ToList();
                if (userList.Any())
                {
                    productDto = userList[0];
                }
                else
                {
                    throw new NotFoundException($"Could not find email{ email }", StatusCodes.Status404NotFound);
                }
                return productDto;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<UserDto> GetUserByID( string userId)
        {
            try
            {
                _logger.LogInformation("start to execute get User by Id");
                UserDto userDto = new UserDto();
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from User where UserId=@UserId";
                IEnumerable<UserDto> userFromDatabase = await sqlConnection.QueryAsync<UserDto>
                    (innerText,
                    new
                    {
                        UserId = userId
                    });
                List<UserDto> userList = userFromDatabase.ToList();
                if (userList.Any())
                {
                    userDto = userList[0];
                }
                else
                {
                    throw new NotFoundException($"Could not find UserId{ userId }", StatusCodes.Status404NotFound);
                }
                return userDto;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<List<UserDto>> GetUsers()
        {
            try
            {
                _logger.LogInformation("start to execute get User");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = @"select * from User";
                IEnumerable<UserDto> userFromDatabase = await sqlConnection.QueryAsync<UserDto>
                    (innerText);
                List<UserDto> userList = userFromDatabase.ToList();
                if (userList == null)
                {
                    throw new NotFoundException($"Could not find Product", StatusCodes.Status404NotFound);
                }
                return userList;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<int> DeleteUser(string userId)
        {
            try
            {
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "delete from User where UserId=@UserId";
                int rowAffected = await sqlConnection.ExecuteAsync(innerText, new { UserId = userId });
                return rowAffected;
            }
            catch (Exception ex)
            {

                throw new InternalServerException(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
