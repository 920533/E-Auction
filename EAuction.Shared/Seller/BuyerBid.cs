﻿

namespace EAuction.Shared.Seller
{
    public class BuyerBid
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BuyerOrSeller { get; set; }
        public string ProductId { get; set; }
        public decimal BidAmount { get; set; }
    }
}
