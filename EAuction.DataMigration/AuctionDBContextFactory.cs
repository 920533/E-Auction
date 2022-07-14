using EAuction.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using System.Reflection;

namespace EAuction.DataMigration
{
    public class AuctionDBContextFactory : IDesignTimeDbContextFactory<AuctionDbContext>
    {
        public AuctionDbContext CreateDbContext(string[] args)
        {
            var optionbuilder = new DbContextOptionsBuilder<AuctionDbContext>();
            optionbuilder.UseSqlServer("Server=CTSDOTNET455;Initial Catalog=EAuction920533;User ID=sa;Password=pass@word1;");
            return new AuctionDbContext(optionbuilder.Options);
        }
    }
}
