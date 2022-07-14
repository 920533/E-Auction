using EAuction.Shared.Seller;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface IUserService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> CreateOrUpdateAsync(User user);
    }
}
