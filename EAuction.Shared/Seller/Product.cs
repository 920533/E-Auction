
using EAuction.Shared.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace EAuction.Shared.Seller
{
    public class Product
    {

        public string ProductId { get; set; }
        [Required, MinLength(5), MaxLength(30)]
        public string ProductName { get; set; }
        public string ShortDeceription { get; set; }
        public string DetailedDeceription { get; set; }
        [Required]
        [EnumDataType(typeof(Category))]
        public Category Category { get; set; }
        public DateTime BidEndDate { get; set; }
        public decimal StartingPrice { get; set; }
    }
}
