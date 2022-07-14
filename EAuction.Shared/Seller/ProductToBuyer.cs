using System;

namespace EAuction.Shared.Seller
{
    public class ProductToBuyer
    {

        public string BuyerProductId { get; set; }
        public string ProductId { get; set; }
        public string UserID { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? Updateddate { get; set; }

    }
}
