namespace EAuction.Shared.Exceptions
{
    public class ConflictException:AuctionException
    {
        public ConflictException(string message) : base(message, 409)
        {
        }
    }
}
