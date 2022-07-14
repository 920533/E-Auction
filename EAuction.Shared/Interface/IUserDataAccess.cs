using EAuction.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{

    public interface IUserDataAccess
    {
        Task<int> AddUser(UserDto product);
        Task<int> DeleteUser(string userId);
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserByID(string userId);
        Task<List<UserDto>> GetUsers();

    }
}