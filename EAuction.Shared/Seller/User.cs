﻿using System;

namespace EAuction.Shared.Seller
{
    public class User
    {

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }

}
