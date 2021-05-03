using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsWebApi.Migrations
{
    public partial class NewHostI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ASMasterBrand",
                schema: "dbo",
                columns: table => new
                {
                    MasterBrandId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterBrand", x => x.MasterBrandId);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterCategory",
                schema: "dbo",
                columns: table => new
                {
                    MasterCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCategoryType = table.Column<long>(type: "bigint", nullable: true),
                    MasterCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterCategory", x => x.MasterCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterErrorLog",
                schema: "dbo",
                columns: table => new
                {
                    MasterErrorLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<long>(type: "bigint", nullable: false),
                    SPName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeVersion = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    StepComplete = table.Column<int>(type: "int", nullable: false),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterErrorLog", x => x.MasterErrorLogId);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterProductSize",
                schema: "dbo",
                columns: table => new
                {
                    MasterProductSizeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSizeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterProductSize", x => x.MasterProductSizeId);
                });

            migrationBuilder.CreateTable(
                name: "CheckAvailableResults",
                columns: table => new
                {
                    NameAvailable = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckAvailableResults", x => x.NameAvailable);
                });

            migrationBuilder.CreateTable(
                name: "DropDownFillResults",
                columns: table => new
                {
                    MasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropDownFillResults", x => x.MasterId);
                });

            migrationBuilder.CreateTable(
                name: "MaxTableMasterIdResults",
                columns: table => new
                {
                    MasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaxTableMasterIdResults", x => x.MasterId);
                });

            migrationBuilder.CreateTable(
                name: "NextMasterIdResults",
                columns: table => new
                {
                    MasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextMasterIdResults", x => x.MasterId);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterSubCategory",
                schema: "dbo",
                columns: table => new
                {
                    MasterSubCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterSubCategory", x => x.MasterSubCategoryId);
                    table.ForeignKey(
                        name: "FK_ASMasterSubCategory_ASMasterCategory_MasterCategoryId",
                        column: x => x.MasterCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterCategory",
                        principalColumn: "MasterCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterProductType",
                schema: "dbo",
                columns: table => new
                {
                    MasterProductTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTypeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterSubCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterProductType", x => x.MasterProductTypeId);
                    table.ForeignKey(
                        name: "FK_ASMasterProductType_ASMasterSubCategory_MasterSubCategoryId",
                        column: x => x.MasterSubCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterSubCategory",
                        principalColumn: "MasterSubCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterProduct",
                schema: "dbo",
                columns: table => new
                {
                    MasterProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterSubCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    MasterBrandId = table.Column<long>(type: "bigint", nullable: true),
                    ProductSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductHSNCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductBarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductMainImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalDisclamer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SafetyWarning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepreciatePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ReorderLevel = table.Column<int>(type: "int", nullable: true),
                    CountryOfOrigin = table.Column<long>(type: "bigint", nullable: true),
                    ProductTaxCode = table.Column<long>(type: "bigint", nullable: true),
                    ProductCurrency = table.Column<long>(type: "bigint", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ASMasterProductSizeMasterProductSizeId = table.Column<long>(type: "bigint", nullable: true),
                    ASMasterProductTypeMasterProductTypeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterProduct", x => x.MasterProductId);
                    table.ForeignKey(
                        name: "FK_ASMasterProduct_ASMasterBrand_MasterBrandId",
                        column: x => x.MasterBrandId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterBrand",
                        principalColumn: "MasterBrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASMasterProduct_ASMasterProductSize_ASMasterProductSizeMasterProductSizeId",
                        column: x => x.ASMasterProductSizeMasterProductSizeId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterProductSize",
                        principalColumn: "MasterProductSizeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASMasterProduct_ASMasterProductType_ASMasterProductTypeMasterProductTypeId",
                        column: x => x.ASMasterProductTypeMasterProductTypeId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterProductType",
                        principalColumn: "MasterProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASMasterProduct_ASMasterSubCategory_MasterSubCategoryId",
                        column: x => x.MasterSubCategoryId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterSubCategory",
                        principalColumn: "MasterSubCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterProductChild",
                schema: "dbo",
                columns: table => new
                {
                    MasterProductChildId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductChildSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductChildTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerPartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionType = table.Column<int>(type: "int", nullable: true),
                    ConditionNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductColour = table.Column<long>(type: "bigint", nullable: true),
                    MasterProductSizeId = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepreciatePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MasterVendorId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeadAssets = table.Column<bool>(type: "bit", nullable: true),
                    IsTimeToSaleProduct = table.Column<bool>(type: "bit", nullable: true),
                    IsSaleProduct = table.Column<bool>(type: "bit", nullable: true),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductQty = table.Column<long>(type: "bigint", nullable: true),
                    ProductQtyUnit = table.Column<long>(type: "bigint", nullable: true),
                    NumberOfItemIncludeInProduct = table.Column<int>(type: "int", nullable: true),
                    ItemPackageQuantity = table.Column<int>(type: "int", nullable: true),
                    IterationOfWarranty = table.Column<int>(type: "int", nullable: true),
                    WarrantyStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarrantyExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterBranchId = table.Column<long>(type: "bigint", nullable: true),
                    MasterEmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterProductChild", x => x.MasterProductChildId);
                    table.ForeignKey(
                        name: "FK_ASMasterProductChild_ASMasterProduct_MasterProductId",
                        column: x => x.MasterProductId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterProduct",
                        principalColumn: "MasterProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASMasterProductChild_ASMasterProductSize_MasterProductSizeId",
                        column: x => x.MasterProductSizeId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterProductSize",
                        principalColumn: "MasterProductSizeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ASMasterAssetsAssignment",
                schema: "dbo",
                columns: table => new
                {
                    MasterAssetsAssignmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetsAssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterProductChildId = table.Column<long>(type: "bigint", nullable: false),
                    MasterEmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    IsAssetsDeAssign = table.Column<bool>(type: "bit", nullable: true),
                    AssetsDeAssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeAssignReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterLocationId = table.Column<long>(type: "bigint", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASMasterAssetsAssignment", x => x.MasterAssetsAssignmentId);
                    table.ForeignKey(
                        name: "FK_ASMasterAssetsAssignment_ASMasterProductChild_MasterProductChildId",
                        column: x => x.MasterProductChildId,
                        principalSchema: "dbo",
                        principalTable: "ASMasterProductChild",
                        principalColumn: "MasterProductChildId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterAssetsAssignment_MasterProductChildId",
                schema: "dbo",
                table: "ASMasterAssetsAssignment",
                column: "MasterProductChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProduct_ASMasterProductSizeMasterProductSizeId",
                schema: "dbo",
                table: "ASMasterProduct",
                column: "ASMasterProductSizeMasterProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProduct_ASMasterProductTypeMasterProductTypeId",
                schema: "dbo",
                table: "ASMasterProduct",
                column: "ASMasterProductTypeMasterProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProduct_MasterBrandId",
                schema: "dbo",
                table: "ASMasterProduct",
                column: "MasterBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProduct_MasterSubCategoryId",
                schema: "dbo",
                table: "ASMasterProduct",
                column: "MasterSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProductChild_MasterProductId",
                schema: "dbo",
                table: "ASMasterProductChild",
                column: "MasterProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProductChild_MasterProductSizeId",
                schema: "dbo",
                table: "ASMasterProductChild",
                column: "MasterProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterProductType_MasterSubCategoryId",
                schema: "dbo",
                table: "ASMasterProductType",
                column: "MasterSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ASMasterSubCategory_MasterCategoryId",
                schema: "dbo",
                table: "ASMasterSubCategory",
                column: "MasterCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASMasterAssetsAssignment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterErrorLog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CheckAvailableResults");

            migrationBuilder.DropTable(
                name: "DropDownFillResults");

            migrationBuilder.DropTable(
                name: "MaxTableMasterIdResults");

            migrationBuilder.DropTable(
                name: "NextMasterIdResults");

            migrationBuilder.DropTable(
                name: "ASMasterProductChild",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterProduct",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterBrand",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterProductSize",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterProductType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterSubCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ASMasterCategory",
                schema: "dbo");
        }
    }
}
