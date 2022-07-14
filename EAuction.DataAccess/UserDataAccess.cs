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
    public class UserDataAccess : IUserDataAccess
    {
        private readonly ILogger<UserDataAccess> _logger;
        private readonly string _connectionString;

        public UserDataAccess(ILogger<UserDataAccess> logger,IOptions<ConnectionStrings> connectionString)
        {
            _logger = logger;
            _connectionString = connectionString.Value.DbConnection;
        }

        public async Task<int> AddUser(UserDto productDto)
        {
            try
            {
                _logger.LogInformation("Strat to execute add product procedure");
                using SqlConnection sqlConnection = new SqlConnection(_connectionString);
                string innerText = "";
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
