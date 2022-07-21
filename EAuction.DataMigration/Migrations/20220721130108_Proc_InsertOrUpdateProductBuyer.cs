using Microsoft.EntityFrameworkCore.Migrations;

namespace EAuction.DataMigration.Migrations
{
    public partial class Proc_InsertOrUpdateProductBuyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Proc_InsertOrUpdateProductBuyer.sql");
            migrationBuilder.Sql(sqlToExecute);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Proc_InsertOrUpdateProductBuyerDrop.sql");
            migrationBuilder.Sql(sqlToExecute);
        }
    }
}
