using Microsoft.EntityFrameworkCore.Migrations;

namespace EAuction.DataMigration.Migrations
{
    public partial class Create_User_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Create_User_Table.sql");
            migrationBuilder.Sql(sqlToExecute);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sqlToExecute = MigrationHelper.GetEmbeddedSql("Create_User_Table_Drop.sql");
            migrationBuilder.Sql(sqlToExecute);
        }
    }
}
