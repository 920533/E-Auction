using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EAuction.Shared.Enum;



namespace EAuction.Shared.Models
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public Category Category { get; set; }
        public string BidEndDate { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }

}
