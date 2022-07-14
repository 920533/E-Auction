using System;

namespace EAuction.Shared.Models
{
    public class ProductToBuyerDto
    {
        public long Id { get; set; }
        public string BuyerProductId { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

    }
}
