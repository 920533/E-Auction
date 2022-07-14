using Microsoft.EntityFrameworkCore.Migrations;

namespace EAuction.DataMigration.Migrations
{
    public partial class Proc_InsertOrUpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Proc_InsertOrUpdateProduct.sql");
            migrationBuilder.Sql(sqlToExecute);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Proc_InsertOrUpdateProductDrop.sql");
            migrationBuilder.Sql(sqlToExecute);
        }
    }
}
