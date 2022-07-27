namespace EAuction.Shared
{
    public static class Constants
    {
        #region Execptions

        public const string BidPlaceError = "Cannot place this bid";
        public const string ProductNotFound = "Product not found";
        public const string InvalidProduct = "Please enter valid product name";
        public const string BidDateError = "Bid date should be in future";
        public const string ProductCannotDelete = "cannot delete the product";
        public const string UserNotFound = "User not found";
        public const string UserCreationFailed = "New user create failed";
        public const string ProductCreationFailed = "New product create failed";
        public const string ProductBidNotFound = "Product Bid not found";

        #endregion

        #region Model Validation

        public const string InvalidFirstName = "Please enter valid first name";
        public const string InvalidLastName = "Please enter valid last name";
        public const string InvalidPhoneNumber = "Please enter valid phone number";
        public const string InvalidEmail = "Please enter valid email";

        #endregion

        #region

        public const string NewBid = "New bid placed by {0} {1}";

        #endregion

        #region 

        public const string PhFormat = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        #endregion

        #region 
        public const string connectionString = "Endpoint=sb://sbns-eauction-prod.servicebus.windows.net/;SharedAccessKeyName=eauction-send-listen;SharedAccessKey=E/AhpJoUzygLMchkmInZpI26+A0UMEYfYf+2IBjLSx8=;EntityPath=sbt-eauction-prod";
        //public const string connectionString = "Endpoint=sb://sbns-eauction-prod.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=S5NPrghLLXSYumy3PGEDRV9BAPr4Yrb1KQhCtcrSQ5k=";
        public const string topicName = "sbt-eauction-prod";
        public const string subscriberName = "subs-eauction";

        #endregion
    }
}
