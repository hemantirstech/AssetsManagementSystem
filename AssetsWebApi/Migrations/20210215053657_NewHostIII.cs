using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsWebApi.Migrations
{
    public partial class NewHostIII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MasterCompanyOwnerId",
                schema: "dbo",
                table: "ASMasterProductChild",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MasterSubscriptionTypeId",
                schema: "dbo",
                table: "ASMasterProductChild",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterCompanyOwnerId",
                schema: "dbo",
                table: "ASMasterProductChild");

            migrationBuilder.DropColumn(
                name: "MasterSubscriptionTypeId",
                schema: "dbo",
                table: "ASMasterProductChild");
        }
    }
}
