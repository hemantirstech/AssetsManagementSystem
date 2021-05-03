using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsWebApi.Migrations
{
    public partial class NewHostIV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASTransactionProductHistory",
                schema: "dbo",
                columns: table => new
                {
                    TransactionProductHistoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterProductChildId = table.Column<long>(type: "bigint", nullable: false),
                    MasterSubscriptionTypeId = table.Column<long>(type: "bigint", nullable: true),
                    SubscriptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MasterSubscriptionVendorId = table.Column<long>(type: "bigint", nullable: true),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubscriptionExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadInvoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadWarretyCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASTransactionProductHistory", x => x.TransactionProductHistoryId);
                    table.ForeignKey(
                        name: "FK_ASTransactionProductHistory_ASMasterProductChild_MasterProductChildId",
                        column: x => x.MasterProductChildId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterProductChild",
                        principalColumn: "MasterProductChildId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASTransactionProductHistory_MasterProductChildId",
                schema: "dbo",
                table: "ASTransactionProductHistory",
                column: "MasterProductChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASTransactionProductHistory",
                schema: "dbo");
        }
    }
}
