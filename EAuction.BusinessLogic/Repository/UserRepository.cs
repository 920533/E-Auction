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
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IProductDataAccess _productDataAccess;
        public UserRepository(ILogger<UserRepository> logger, IMapper mapper, IProductDataAccess productDataAccess)
        {
            _logger = logger;
            _mapper = mapper;
            _productDataAccess = productDataAccess;
        }

        public async Task<int> CreateOrUpdateAsync(User user)
        {
            user.UserId = Guid.NewGuid().ToString();
            UserDto productDto = _mapper.Map<UserDto>(user);
            int successCode = await _productDataAccess.AddUser(productDto);
            return successCode;
        }

        public async Task<int> DeleteAsync(string UserId)
        {
            _logger.LogInformation("start delete user details");
            int successCode = await _productDataAccess.DeleteUser(UserId);
            return successCode;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            UserDto userDto = await _productDataAccess.GetUserByEmail(email);
            User receiveUser = _mapper.Map<User>(userDto);
            return receiveUser;
        }

        public async Task<User> GetUserByIDAsync(string userid)
        {
            UserDto userDto = await _productDataAccess.GetUserByID(userid);
            User receiveUser = _mapper.Map<User>(userDto);
            return receiveUser;
        }

        public async Task<List<User>> GetUSersAsync()
        {
            List<UserDto> userDto = await _productDataAccess.GetUsers();
            List<User> receiveUser = _mapper.Map<List<User>>(userDto);
            return receiveUser;
        }
    }
}
