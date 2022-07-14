using EAuction.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{

    public interface IProductDataAccess
    {
        Task<int>AddProduct(ProductDto product);
        Task<ProductDto>ShowProduct(string productId);
        Task<List<ProductDto>> ShowAllProduct();
        Task<int>DeleteProduct(string productId);


        Task<int> AddProductToBuyer(ProductToBuyerDto product);
        Task<int> DeleteProductToBuyer(string productId, string userID);
        Task<ProductToBuyerDto> GetBidByProductIDAsync(string productId);
        Task<ProductToBuyerDto> GetProductByUserIDAsync(string productId, string userID);
        Task<List<ProductToBuyerDto>> GetProductsByBuyerIdAsync();



        Task<int> AddUser(UserDto product);
        Task<int> DeleteUser(string userId);
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserByID(string userId);
        Task<List<UserDto>> GetUsers();


    }
}