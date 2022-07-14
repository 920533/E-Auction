using EAuction.Shared;
using EAuction.Shared.Enum;
using EAuction.Shared.Seller;
using EAuction.Shared.Exceptions;
using EAuction.Shared.Interface;
using System;
using System.Threading.Tasks;

namespace EAuction.Shared.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            if (userRepository is null)
            {
                throw new ArgumentException(nameof(userRepository));
            }

            _userRepository = userRepository;
        }

        public async Task<int> CreateOrUpdateAsync(User user)
        {
            if (user.UserId != null)
            {
                var existingUser = await _userRepository.GetUserByIDAsync(user.UserId);

                if (existingUser == null)
                {
                    throw new UserNotFounException(Constants.UserNotFound);
                }

                var updateduser = await _userRepository.CreateOrUpdateAsync(user);
                return updateduser;
            }
            else
            {
                var newUser = await _userRepository.CreateOrUpdateAsync(user);
                //newUser.UserType = newUser.UserType == "1" ? UserType.Seller.ToString() : UserType.Buyer.ToString();
                return newUser;
            }
        }
    }
}
