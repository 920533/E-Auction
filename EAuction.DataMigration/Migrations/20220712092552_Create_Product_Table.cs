using Microsoft.EntityFrameworkCore.Migrations;

namespace EAuction.DataMigration.Migrations
{
    public partial class Create_Product_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Create_Product_Table.sql");
            migrationBuilder.Sql(sqlToExecute);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Create_Product_Table_Drop.sql");
            migrationBuilder.Sql(sqlToExecute);
        }
    }
}
