namespace EAuction.Shared.Exceptions
{
    public class InternalServerException : AuctionException
    {
        public InternalServerException(string message,int code) : base(message,code) { }

        public static InternalServerException ForSystem(string message) => new InternalServerException(message,500);
    }
}
