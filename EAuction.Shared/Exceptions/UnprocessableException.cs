namespace EAuction.Shared.Exceptions
{
    public class UnprocessableException:AuctionException
    {
        public UnprocessableException(string message) : base(message, 422)
        {
        }
    }
}
