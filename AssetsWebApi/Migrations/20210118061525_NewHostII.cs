using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsWebApi.Migrations
{
    public partial class NewHostII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServicePassword",
                schema: "dbo",
                table: "ASMasterProductChild",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceURL",
                schema: "dbo",
                table: "ASMasterProductChild",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceUserName",
                schema: "dbo",
                table: "ASMasterProductChild",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicePassword",
                schema: "dbo",
                table: "ASMasterProductChild");

            migrationBuilder.DropColumn(
                name: "ServiceURL",
                schema: "dbo",
                table: "ASMasterProductChild");

            migrationBuilder.DropColumn(
                name: "ServiceUserName",
                schema: "dbo",
                table: "ASMasterProductChild");
        }
    }
}
