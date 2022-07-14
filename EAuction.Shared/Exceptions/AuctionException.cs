using System;

namespace EAuction.Shared.Exceptions
{
    public class AuctionException:Exception
    {
        // In order to differentiate between client exception and system exception 
        public readonly int Code;

        public AuctionException(string message, int code) : base(message)
        {
            Code = code;
        }
        
    }
}
