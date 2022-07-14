namespace EAuction.Shared.Exceptions
{
    public class NotFoundException:AuctionException
    {
        public  NotFoundException(string message,int code) : base(message,code) { }

        public static NotFoundException ForClient(string message) => new NotFoundException(message, 404);
    }
}
