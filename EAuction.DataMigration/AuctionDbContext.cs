using Microsoft.EntityFrameworkCore;
using System;

namespace EAuction.DataMigration
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }


}
