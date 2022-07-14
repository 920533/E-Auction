
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.Shared.Interface
{
    public interface IServiceBusMessageProducer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        Task<string> SendMessageAsync(string message);

    }
}
