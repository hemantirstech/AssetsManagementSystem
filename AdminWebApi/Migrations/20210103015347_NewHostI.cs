using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminWebApi.Migrations
{
    public partial class NewHostI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ADErrorLog",
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
                    table.PrimaryKey("PK_ADErrorLog", x => x.MasterErrorLogId);
                });

            migrationBuilder.CreateTable(
                name: "ADGenCodeType",
                schema: "dbo",
                columns: table => new
                {
                    GenCodeTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenCodeTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenCodeTypePrintDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenCodeTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADGenCodeType", x => x.GenCodeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterAddressType",
                schema: "dbo",
                columns: table => new
                {
                    MasterAddressTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterAddressType", x => x.MasterAddressTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterBankAccountType",
                schema: "dbo",
                columns: table => new
                {
                    MasterBankAccountTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterBankAccountTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterBankAccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterBankAccountType", x => x.MasterBankAccountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterBusinessVerticle",
                schema: "dbo",
                columns: table => new
                {
                    MasterBusinessVerticleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessVerticleTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterBusinessVerticle", x => x.MasterBusinessVerticleId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterColor",
                schema: "dbo",
                columns: table => new
                {
                    MasterColorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterColor", x => x.MasterColorId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterCompanyType",
                schema: "dbo",
                columns: table => new
                {
                    MasterCompanyTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterCompanyType", x => x.MasterCompanyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterCountry",
                schema: "dbo",
                columns: table => new
                {
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryDialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterCountry", x => x.MasterCountryId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterDepartment",
                schema: "dbo",
                columns: table => new
                {
                    MasterDepartmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterDepartment", x => x.MasterDepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterEmployeeStatus",
                schema: "dbo",
                columns: table => new
                {
                    MasterEmployeeStatusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeStatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterEmployeeStatus", x => x.MasterEmployeeStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterEmployeeType",
                schema: "dbo",
                columns: table => new
                {
                    MasterEmployeeTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpTypCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterEmployeeType", x => x.MasterEmployeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterFinancialYear",
                schema: "dbo",
                columns: table => new
                {
                    MasterFinancialYearId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialYearDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YearEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CashBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearLocked = table.Column<bool>(type: "bit", nullable: true),
                    CurrentYear = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterFinancialYear", x => x.MasterFinancialYearId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterFunction",
                schema: "dbo",
                columns: table => new
                {
                    MasterFunctionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionIconColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMasterFunctionId = table.Column<long>(type: "bigint", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterFunction", x => x.MasterFunctionId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterGender",
                schema: "dbo",
                columns: table => new
                {
                    MasterGenderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gendercode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterGender", x => x.MasterGenderId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterIndustryGroup",
                schema: "dbo",
                columns: table => new
                {
                    MasterIndustryGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndustryGroupTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryGroupDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterIndustryGroup", x => x.MasterIndustryGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterLoginType",
                schema: "dbo",
                columns: table => new
                {
                    MasterLoginTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterLoginTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterLoginType", x => x.MasterLoginTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterMailTemplate",
                schema: "dbo",
                columns: table => new
                {
                    MasterMailTemplateId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailTemplateTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPort = table.Column<long>(type: "bigint", nullable: true),
                    SMTPUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterMailTemplate", x => x.MasterMailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterMessageType",
                schema: "dbo",
                columns: table => new
                {
                    MasterMessageTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterMessageType", x => x.MasterMessageTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterPaymentType",
                schema: "dbo",
                columns: table => new
                {
                    MasterPaymentTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterPaymentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterPaymentType", x => x.MasterPaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterProfile",
                schema: "dbo",
                columns: table => new
                {
                    MasterProfileId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsInsert = table.Column<bool>(type: "bit", nullable: true),
                    IsUpdate = table.Column<bool>(type: "bit", nullable: true),
                    IsSelect = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterProfile", x => x.MasterProfileId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterRegion",
                schema: "dbo",
                columns: table => new
                {
                    MasterRegionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterRegionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterRegion", x => x.MasterRegionId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterRegistrationType",
                schema: "dbo",
                columns: table => new
                {
                    MasterRegistrationTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterRegistrationTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterRegistrationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterRegistrationExpertType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterRegistrationType", x => x.MasterRegistrationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterReportingHead",
                schema: "dbo",
                columns: table => new
                {
                    MasterReportingHeadId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportingHeadTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterReportingHead", x => x.MasterReportingHeadId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterSalutation",
                schema: "dbo",
                columns: table => new
                {
                    MasterSalutationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalutationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalutationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterSalutation", x => x.MasterSalutationId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterTax",
                schema: "dbo",
                columns: table => new
                {
                    MasterTaxId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTaxPercentageAmount = table.Column<bool>(type: "bit", nullable: true),
                    TaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaxEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterTax", x => x.MasterTaxId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterTimeZone",
                schema: "dbo",
                columns: table => new
                {
                    MasterTimeZoneId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeZoneTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeZoneOffset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasDst = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterTimeZone", x => x.MasterTimeZoneId);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterTypeOfDevice",
                schema: "dbo",
                columns: table => new
                {
                    MasterTypeOfDeviceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfDeviceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfDeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterTypeOfDevice", x => x.MasterTypeOfDeviceId);
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
                name: "ProfileTaskMappingResults",
                columns: table => new
                {
                    MasterFunctionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterProfileTaskMappingId = table.Column<long>(type: "bigint", nullable: true),
                    MasterProfileId = table.Column<long>(type: "bigint", nullable: true),
                    ProfileTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionIconColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMasterFunctionId = table.Column<long>(type: "bigint", nullable: true),
                    ParentFunctionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsInsert = table.Column<bool>(type: "bit", nullable: true),
                    IsUpdate = table.Column<bool>(type: "bit", nullable: true),
                    IsSelect = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTaskMappingResults", x => x.MasterFunctionId);
                });

            migrationBuilder.CreateTable(
                name: "ValidateAccountResults",
                columns: table => new
                {
                    MasterFunctionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterLoginId = table.Column<long>(type: "bigint", nullable: false),
                    MasterRegistrationTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterRegistrationId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: true),
                    IsFirstLogin = table.Column<bool>(type: "bit", nullable: true),
                    ValidationCount = table.Column<int>(type: "int", nullable: true),
                    MasterEmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    MasterSalutationId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterBranchId = table.Column<long>(type: "bigint", nullable: true),
                    BranchTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterProfileId = table.Column<long>(type: "bigint", nullable: false),
                    ProfileTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionIconColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMasterFunctionId = table.Column<long>(type: "bigint", nullable: false),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsInsert = table.Column<bool>(type: "bit", nullable: true),
                    IsUpdate = table.Column<bool>(type: "bit", nullable: true),
                    IsSelect = table.Column<bool>(type: "bit", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidateAccountResults", x => x.MasterFunctionId);
                });

            migrationBuilder.CreateTable(
                name: "ADGenCodeMaster",
                schema: "dbo",
                columns: table => new
                {
                    GenCodeMasterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenCodeMasterTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrintDesc = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GenCodeTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADGenCodeMaster", x => x.GenCodeMasterId);
                    table.ForeignKey(
                        name: "FK_ADGenCodeMaster_ADGenCodeType_GenCodeTypeId",
                        column: x => x.GenCodeTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADGenCodeType",
                        principalColumn: "GenCodeTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterStatus",
                schema: "dbo",
                columns: table => new
                {
                    MasterStatusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterColorId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterStatus", x => x.MasterStatusId);
                    table.ForeignKey(
                        name: "FK_ADMasterStatus_ADMasterColor_MasterColorId",
                        column: x => x.MasterColorId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterColor",
                        principalColumn: "MasterColorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterCurrency",
                schema: "dbo",
                columns: table => new
                {
                    MasterCurrencyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencySymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ADMasterCountryMasterCountryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterCurrency", x => x.MasterCurrencyId);
                    table.ForeignKey(
                        name: "FK_ADMasterCurrency_ADMasterCountry_ADMasterCountryMasterCountryId",
                        column: x => x.ADMasterCountryMasterCountryId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCountry",
                        principalColumn: "MasterCountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterState",
                schema: "dbo",
                columns: table => new
                {
                    MasterStateId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterState", x => x.MasterStateId);
                    table.ForeignKey(
                        name: "FK_ADMasterState_ADMasterCountry_MasterCountryId",
                        column: x => x.MasterCountryId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCountry",
                        principalColumn: "MasterCountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterDesignation",
                schema: "dbo",
                columns: table => new
                {
                    MasterDesignationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ADMasterDepartmentMasterDepartmentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterDesignation", x => x.MasterDesignationId);
                    table.ForeignKey(
                        name: "FK_ADMasterDesignation_ADMasterDepartment_ADMasterDepartmentMasterDepartmentId",
                        column: x => x.ADMasterDepartmentMasterDepartmentId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterDepartment",
                        principalColumn: "MasterDepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterIndustryType",
                schema: "dbo",
                columns: table => new
                {
                    MasterIndustryTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterIndustryGroupId = table.Column<long>(type: "bigint", nullable: true),
                    IndustryTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterIndustryType", x => x.MasterIndustryTypeId);
                    table.ForeignKey(
                        name: "FK_ADMasterIndustryType_ADMasterIndustryGroup_MasterIndustryGroupId",
                        column: x => x.MasterIndustryGroupId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterIndustryGroup",
                        principalColumn: "MasterIndustryGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADProfileTaskMapping",
                schema: "dbo",
                columns: table => new
                {
                    MasterProfileTaskMappingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterProfileId = table.Column<long>(type: "bigint", nullable: true),
                    MasterFunctionId = table.Column<long>(type: "bigint", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsInsert = table.Column<bool>(type: "bit", nullable: true),
                    IsUpdate = table.Column<bool>(type: "bit", nullable: true),
                    IsSelect = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADProfileTaskMapping", x => x.MasterProfileTaskMappingId);
                    table.ForeignKey(
                        name: "FK_ADProfileTaskMapping_ADMasterFunction_MasterFunctionId",
                        column: x => x.MasterFunctionId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterFunction",
                        principalColumn: "MasterFunctionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADProfileTaskMapping_ADMasterProfile_MasterProfileId",
                        column: x => x.MasterProfileId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterProfile",
                        principalColumn: "MasterProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterLogin",
                schema: "dbo",
                columns: table => new
                {
                    MasterLoginId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterRegistrationTypeId = table.Column<long>(type: "bigint", nullable: true),
                    MasterRegistrationId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterProfileId = table.Column<long>(type: "bigint", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: true),
                    IsFirstLogin = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterLogin", x => x.MasterLoginId);
                    table.ForeignKey(
                        name: "FK_ADMasterLogin_ADMasterProfile_MasterProfileId",
                        column: x => x.MasterProfileId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterProfile",
                        principalColumn: "MasterProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterLogin_ADMasterRegistrationType_MasterRegistrationTypeId",
                        column: x => x.MasterRegistrationTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterRegistrationType",
                        principalColumn: "MasterRegistrationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterCity",
                schema: "dbo",
                columns: table => new
                {
                    MasterCityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterStateId = table.Column<long>(type: "bigint", nullable: true),
                    CityTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterCity", x => x.MasterCityId);
                    table.ForeignKey(
                        name: "FK_ADMasterCity_ADMasterState_MasterStateId",
                        column: x => x.MasterStateId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterState",
                        principalColumn: "MasterStateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterCompany",
                schema: "dbo",
                columns: table => new
                {
                    MasterCompanyId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterDesignationId = table.Column<long>(type: "bigint", nullable: true),
                    DateofRegistration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterCompanyTypeId = table.Column<long>(type: "bigint", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEZRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LUT_AppliactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    MasterTimeZoneId = table.Column<long>(type: "bigint", nullable: true),
                    MasterAddressTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: true),
                    MasterStateId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterCompany", x => x.MasterCompanyId);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterAddressType_MasterAddressTypeId",
                        column: x => x.MasterAddressTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterAddressType",
                        principalColumn: "MasterAddressTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterCompanyType_MasterCompanyTypeId",
                        column: x => x.MasterCompanyTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCompanyType",
                        principalColumn: "MasterCompanyTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterCountry_MasterCountryId",
                        column: x => x.MasterCountryId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCountry",
                        principalColumn: "MasterCountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterCurrency_MasterCurrencyId",
                        column: x => x.MasterCurrencyId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCurrency",
                        principalColumn: "MasterCurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterDesignation_MasterDesignationId",
                        column: x => x.MasterDesignationId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterDesignation",
                        principalColumn: "MasterDesignationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterState_MasterStateId",
                        column: x => x.MasterStateId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterState",
                        principalColumn: "MasterStateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterCompany_ADMasterTimeZone_MasterTimeZoneId",
                        column: x => x.MasterTimeZoneId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterTimeZone",
                        principalColumn: "MasterTimeZoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterVendor",
                schema: "dbo",
                columns: table => new
                {
                    MasterVendorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterDesignationId = table.Column<long>(type: "bigint", nullable: true),
                    DateofRegistration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterCompanyTypeId = table.Column<long>(type: "bigint", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEZRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LUT_AppliactionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    MasterTimeZoneId = table.Column<long>(type: "bigint", nullable: true),
                    MasterAddressTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: true),
                    MasterStateId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterVendor", x => x.MasterVendorId);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterAddressType_MasterAddressTypeId",
                        column: x => x.MasterAddressTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterAddressType",
                        principalColumn: "MasterAddressTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterCompanyType_MasterCompanyTypeId",
                        column: x => x.MasterCompanyTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCompanyType",
                        principalColumn: "MasterCompanyTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterCountry_MasterCountryId",
                        column: x => x.MasterCountryId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCountry",
                        principalColumn: "MasterCountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterCurrency_MasterCurrencyId",
                        column: x => x.MasterCurrencyId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCurrency",
                        principalColumn: "MasterCurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterDesignation_MasterDesignationId",
                        column: x => x.MasterDesignationId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterDesignation",
                        principalColumn: "MasterDesignationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterState_MasterStateId",
                        column: x => x.MasterStateId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterState",
                        principalColumn: "MasterStateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterVendor_ADMasterTimeZone_MasterTimeZoneId",
                        column: x => x.MasterTimeZoneId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterTimeZone",
                        principalColumn: "MasterTimeZoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterIndustrySubType",
                schema: "dbo",
                columns: table => new
                {
                    MasterIndustrySubTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterIndustryTypeId = table.Column<long>(type: "bigint", nullable: true),
                    IndustrySubTypeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustrySubTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustrySubTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterIndustrySubType", x => x.MasterIndustrySubTypeId);
                    table.ForeignKey(
                        name: "FK_ADMasterIndustrySubType_ADMasterIndustryType_MasterIndustryTypeId",
                        column: x => x.MasterIndustryTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterIndustryType",
                        principalColumn: "MasterIndustryTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterRegisteredDevice",
                schema: "dbo",
                columns: table => new
                {
                    MasterRegisteredDeviceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterLoginId = table.Column<long>(type: "bigint", nullable: true),
                    MacId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterTypeOfDeviceId = table.Column<long>(type: "bigint", nullable: true),
                    DeviceVerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeviceVerified = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterRegisteredDevice", x => x.MasterRegisteredDeviceId);
                    table.ForeignKey(
                        name: "FK_ADMasterRegisteredDevice_ADMasterLogin_MasterLoginId",
                        column: x => x.MasterLoginId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterLogin",
                        principalColumn: "MasterLoginId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterRegisteredDevice_ADMasterTypeOfDevice_MasterTypeOfDeviceId",
                        column: x => x.MasterTypeOfDeviceId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterTypeOfDevice",
                        principalColumn: "MasterTypeOfDeviceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterBranch",
                schema: "dbo",
                columns: table => new
                {
                    MasterBranchId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterDesignationId = table.Column<long>(type: "bigint", nullable: true),
                    DateofRegistration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterAddressTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: true),
                    MasterStateId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterBranch", x => x.MasterBranchId);
                    table.ForeignKey(
                        name: "FK_ADMasterBranch_ADMasterAddressType_MasterAddressTypeId",
                        column: x => x.MasterAddressTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterAddressType",
                        principalColumn: "MasterAddressTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterBranch_ADMasterCompany_MasterCompanyId",
                        column: x => x.MasterCompanyId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCompany",
                        principalColumn: "MasterCompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterBranch_ADMasterCountry_MasterCountryId",
                        column: x => x.MasterCountryId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCountry",
                        principalColumn: "MasterCountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterBranch_ADMasterDesignation_MasterDesignationId",
                        column: x => x.MasterDesignationId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterDesignation",
                        principalColumn: "MasterDesignationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterBranch_ADMasterState_MasterStateId",
                        column: x => x.MasterStateId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterState",
                        principalColumn: "MasterStateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterConfiguration",
                schema: "dbo",
                columns: table => new
                {
                    MasterConfigurationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    EnableNoActivity5Day = table.Column<bool>(type: "bit", nullable: true),
                    EnableLoginMACIdAuthentication = table.Column<bool>(type: "bit", nullable: true),
                    EnablePasswordResetByAdmin = table.Column<bool>(type: "bit", nullable: true),
                    EnableEmailVerification = table.Column<bool>(type: "bit", nullable: true),
                    EnableMobileVerification = table.Column<bool>(type: "bit", nullable: true),
                    SMTPServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPort = table.Column<long>(type: "bigint", nullable: true),
                    SMTPUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSSenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterConfiguration", x => x.MasterConfigurationId);
                    table.ForeignKey(
                        name: "FK_ADMasterConfiguration_ADMasterCompany_MasterCompanyId",
                        column: x => x.MasterCompanyId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCompany",
                        principalColumn: "MasterCompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADTransactionLogin",
                schema: "dbo",
                columns: table => new
                {
                    TransactionLoginId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterLoginId = table.Column<long>(type: "bigint", nullable: true),
                    LoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogoutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterRegisteredDeviceId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADTransactionLogin", x => x.TransactionLoginId);
                    table.ForeignKey(
                        name: "FK_ADTransactionLogin_ADMasterLogin_MasterLoginId",
                        column: x => x.MasterLoginId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterLogin",
                        principalColumn: "MasterLoginId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADTransactionLogin_ADMasterRegisteredDevice_MasterRegisteredDeviceId",
                        column: x => x.MasterRegisteredDeviceId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterRegisteredDevice",
                        principalColumn: "MasterRegisteredDeviceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterEmployee",
                schema: "dbo",
                columns: table => new
                {
                    MasterEmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterSalutationId = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<long>(type: "bigint", nullable: true),
                    PANNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AadhaarNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterDesignationId = table.Column<long>(type: "bigint", nullable: true),
                    MasterDepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    ReportingHeadId = table.Column<long>(type: "bigint", nullable: true),
                    MasterEmployeeTypeId = table.Column<long>(type: "bigint", nullable: true),
                    MasterTimeZoneId = table.Column<long>(type: "bigint", nullable: true),
                    MasterEmployeeStatusId = table.Column<long>(type: "bigint", nullable: true),
                    DateOfLeavingOrganisation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCountryId = table.Column<long>(type: "bigint", nullable: true),
                    MasterStateId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterPaymentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    PaypalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaypalLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaypalAccountVerified = table.Column<bool>(type: "bit", nullable: true),
                    MasterBankAccountTypeId = table.Column<long>(type: "bigint", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFCSCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftCode_RoutingNo_IBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadBankDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBankAccountVerified = table.Column<bool>(type: "bit", nullable: true),
                    MasterBranchId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterEmployee", x => x.MasterEmployeeId);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterBankAccountType_MasterBankAccountTypeId",
                        column: x => x.MasterBankAccountTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterBankAccountType",
                        principalColumn: "MasterBankAccountTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterBranch_MasterBranchId",
                        column: x => x.MasterBranchId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterBranch",
                        principalColumn: "MasterBranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterCountry_MasterCountryId",
                        column: x => x.MasterCountryId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCountry",
                        principalColumn: "MasterCountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterDepartment_MasterDepartmentId",
                        column: x => x.MasterDepartmentId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterDepartment",
                        principalColumn: "MasterDepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterDesignation_MasterDesignationId",
                        column: x => x.MasterDesignationId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterDesignation",
                        principalColumn: "MasterDesignationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterEmployeeStatus_MasterEmployeeStatusId",
                        column: x => x.MasterEmployeeStatusId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterEmployeeStatus",
                        principalColumn: "MasterEmployeeStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterEmployeeType_MasterEmployeeTypeId",
                        column: x => x.MasterEmployeeTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterEmployeeType",
                        principalColumn: "MasterEmployeeTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterPaymentType_MasterPaymentTypeId",
                        column: x => x.MasterPaymentTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterPaymentType",
                        principalColumn: "MasterPaymentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterSalutation_MasterSalutationId",
                        column: x => x.MasterSalutationId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterSalutation",
                        principalColumn: "MasterSalutationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterState_MasterStateId",
                        column: x => x.MasterStateId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterState",
                        principalColumn: "MasterStateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterEmployee_ADMasterTimeZone_MasterTimeZoneId",
                        column: x => x.MasterTimeZoneId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterTimeZone",
                        principalColumn: "MasterTimeZoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMessageNotification",
                schema: "dbo",
                columns: table => new
                {
                    MasterMessageNotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MasterMessageTypeId = table.Column<long>(type: "bigint", nullable: true),
                    MessageFrom = table.Column<long>(type: "bigint", nullable: true),
                    MessageTo = table.Column<long>(type: "bigint", nullable: true),
                    MessageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    IsSend = table.Column<bool>(type: "bit", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    ShareTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MasterCompanyId = table.Column<long>(type: "bigint", nullable: true),
                    MasterBranchId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMessageNotification", x => x.MasterMessageNotificationId);
                    table.ForeignKey(
                        name: "FK_ADMessageNotification_ADMasterBranch_MasterBranchId",
                        column: x => x.MasterBranchId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterBranch",
                        principalColumn: "MasterBranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMessageNotification_ADMasterCompany_MasterCompanyId",
                        column: x => x.MasterCompanyId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterCompany",
                        principalColumn: "MasterCompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMessageNotification_ADMasterMessageType_MasterMessageTypeId",
                        column: x => x.MasterMessageTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterMessageType",
                        principalColumn: "MasterMessageTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ADMasterRegistration",
                schema: "dbo",
                columns: table => new
                {
                    MasterRegistrationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterRegistrationTypeId = table.Column<long>(type: "bigint", nullable: true),
                    MasterEmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    MasterBDPId = table.Column<long>(type: "bigint", nullable: true),
                    MasterClientId = table.Column<long>(type: "bigint", nullable: true),
                    MasterResearchProfileId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    EnterById = table.Column<long>(type: "bigint", nullable: true),
                    EnterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMasterRegistration", x => x.MasterRegistrationId);
                    table.ForeignKey(
                        name: "FK_ADMasterRegistration_ADMasterEmployee_MasterEmployeeId",
                        column: x => x.MasterEmployeeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterEmployee",
                        principalColumn: "MasterEmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMasterRegistration_ADMasterRegistrationType_MasterRegistrationTypeId",
                        column: x => x.MasterRegistrationTypeId,
                        principalSchema: "dbo",
                        principalTable: "ADMasterRegistrationType",
                        principalColumn: "MasterRegistrationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADGenCodeType",
                columns: new[] { "GenCodeTypeId", "EnterById", "EnterDate", "GenCodeTypeDesc", "GenCodeTypePrintDesc", "GenCodeTypeTitle", "IsActive", "ModifiedById", "ModifiedDate", "Sequence" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(5450), "Company Type", "CT", "Company Type", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(6713), 1L },
                    { 2L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7399), "Registration Type", "RT", "Registration Type", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7416), 2L },
                    { 3L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7453), "Login Type", "LT", "Login Type", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7455), 3L },
                    { 4L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7476), "Bank Account Type", "BA", "Bank Account Type", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7477), 4L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterCompany",
                columns: new[] { "MasterCompanyId", "Address1", "Address2", "City", "CompanyLogo", "CompanyTitle", "ContactPerson", "DateofRegistration", "Email", "EnterById", "EnterDate", "Fax", "GSTNumber", "IsActive", "LUT_AppliactionReference", "MasterAddressTypeId", "MasterCompanyTypeId", "MasterCountryId", "MasterCurrencyId", "MasterDesignationId", "MasterStateId", "MasterTimeZoneId", "MobileNumber", "ModifiedById", "ModifiedDate", "PANNumber", "PhoneNumber", "PinCode", "RegistrationNumber", "ReportLogo", "SAC_Code", "SEZRegistrationNumber", "TANNumber", "URL" },
                values: new object[] { 1L, null, null, null, null, "IRS TECHNOLOGIES", null, null, "support@irstechnologies.com", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 155, DateTimeKind.Local).AddTicks(7009), null, null, true, null, null, null, null, null, null, null, null, "9999999999", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 156, DateTimeKind.Local).AddTicks(7575), null, null, null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterFunction",
                columns: new[] { "MasterFunctionId", "EnterById", "EnterDate", "FunctionIcon", "FunctionIconColour", "FunctionLink", "FunctionTitle", "IsActive", "ModifiedById", "ModifiedDate", "ParentMasterFunctionId", "Sequence" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(4872), "fa fa-home", "", "/Dashboard/Index", "Dashboard", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(5743), 0L, 1L },
                    { 2L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6426), "fa fa-cog", "", "", "Configuration", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6443), 0L, 2L },
                    { 3L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6480), "", "", "/LicenceAggrement/ViewLicence", "Licence Agreement", true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6481), 2L, 1L }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterProfile",
                columns: new[] { "MasterProfileId", "EnterById", "EnterDate", "IsActive", "IsDelete", "IsInsert", "IsSelect", "IsUpdate", "ModifiedById", "ModifiedDate", "ProfileCode", "ProfileDescription", "ProfileTitle" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(6717), true, true, true, true, true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(7841), null, "Super Admin", "Super Admin" },
                    { 2L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(8605), true, true, true, true, true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(8630), null, "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterRegistrationType",
                columns: new[] { "MasterRegistrationTypeId", "EnterById", "EnterDate", "IsActive", "MasterRegistrationCode", "MasterRegistrationExpertType", "MasterRegistrationTypeTitle", "ModifiedById", "ModifiedDate" },
                values: new object[] { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(6601), true, "SA", null, "Super Admin", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(7517) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterSalutation",
                columns: new[] { "MasterSalutationId", "EnterById", "EnterDate", "IsActive", "ModifiedById", "ModifiedDate", "SalutationCode", "SalutationTitle" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(746), true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(1558), null, "Mr." },
                    { 2L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2167), true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2186), null, "Mrs." },
                    { 3L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2219), true, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2221), null, "Dr." }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterBranch",
                columns: new[] { "MasterBranchId", "Address1", "Address2", "BranchCode", "BranchTitle", "City", "ContactPerson", "DateofRegistration", "Email", "EnterById", "EnterDate", "Fax", "IsActive", "MasterAddressTypeId", "MasterCompanyId", "MasterCountryId", "MasterDesignationId", "MasterStateId", "MobileNumber", "ModifiedById", "ModifiedDate", "PhoneNumber", "PinCode", "URL" },
                values: new object[] { 1L, null, null, null, "IRS TECHNOLOGIES Nagpur", null, null, null, "support@irstechnologies.com", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 158, DateTimeKind.Local).AddTicks(4777), null, true, null, 1L, null, null, null, "9999999999", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 158, DateTimeKind.Local).AddTicks(5613), null, null, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterLogin",
                columns: new[] { "MasterLoginId", "EnterById", "EnterDate", "IsActive", "IsFirstLogin", "IsVerified", "MasterProfileId", "MasterRegistrationId", "MasterRegistrationTypeId", "ModifiedById", "ModifiedDate", "Password", "UserName", "VerificationCode" },
                values: new object[] { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(8768), true, false, true, 1L, 1L, 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(9646), "P@ssword", "admin@email.com", "" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                columns: new[] { "MasterProfileTaskMappingId", "EnterById", "EnterDate", "IsActive", "IsDelete", "IsInsert", "IsSelect", "IsUpdate", "MasterFunctionId", "MasterProfileId", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(880), true, true, true, true, true, 1L, 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(1685) },
                    { 2L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2376), true, true, true, true, true, 2L, 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2391) },
                    { 3L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2429), true, true, true, true, true, 3L, 1L, 1L, new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2431) }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterEmployee",
                columns: new[] { "MasterEmployeeId", "AadhaarNo", "Address1", "Address2", "BankAccountNumber", "BankAddress", "BankBranch", "BankCity", "BankName", "City", "DateOfBirth", "DateOfJoining", "DateOfLeavingOrganisation", "Email", "EmployeeCode", "EmployeeName", "EnterById", "EnterDate", "Gender", "IFCSCode", "IsActive", "IsBankAccountVerified", "IsPaypalAccountVerified", "MasterBankAccountTypeId", "MasterBranchId", "MasterCountryId", "MasterDepartmentId", "MasterDesignationId", "MasterEmployeeStatusId", "MasterEmployeeTypeId", "MasterPaymentTypeId", "MasterSalutationId", "MasterStateId", "MasterTimeZoneId", "MobileNumber", "ModifiedById", "ModifiedDate", "PANNo", "PaypalID", "PaypalLink", "PhoneNumber", "PinCode", "ReportingHeadId", "ShiftCode_RoutingNo_IBAN", "UploadBankDetail" },
                values: new object[] { 1L, null, null, null, null, null, null, null, null, null, null, null, null, "support@irstechnologies.com", null, "IRS TECHNOLOGIES", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(42), null, null, true, null, null, null, 1L, null, null, null, null, null, null, 1L, null, null, "9999999999", 1L, new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(896), null, null, null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_ADGenCodeMaster_GenCodeTypeId",
                schema: "dbo",
                table: "ADGenCodeMaster",
                column: "GenCodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterBranch_MasterAddressTypeId",
                schema: "dbo",
                table: "ADMasterBranch",
                column: "MasterAddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterBranch_MasterCompanyId",
                schema: "dbo",
                table: "ADMasterBranch",
                column: "MasterCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterBranch_MasterCountryId",
                schema: "dbo",
                table: "ADMasterBranch",
                column: "MasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterBranch_MasterDesignationId",
                schema: "dbo",
                table: "ADMasterBranch",
                column: "MasterDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterBranch_MasterStateId",
                schema: "dbo",
                table: "ADMasterBranch",
                column: "MasterStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCity_MasterStateId",
                schema: "dbo",
                table: "ADMasterCity",
                column: "MasterStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterAddressTypeId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterAddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterCompanyTypeId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterCompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterCountryId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterCurrencyId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterDesignationId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterStateId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCompany_MasterTimeZoneId",
                schema: "dbo",
                table: "ADMasterCompany",
                column: "MasterTimeZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterConfiguration_MasterCompanyId",
                schema: "dbo",
                table: "ADMasterConfiguration",
                column: "MasterCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterCurrency_ADMasterCountryMasterCountryId",
                schema: "dbo",
                table: "ADMasterCurrency",
                column: "ADMasterCountryMasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterDesignation_ADMasterDepartmentMasterDepartmentId",
                schema: "dbo",
                table: "ADMasterDesignation",
                column: "ADMasterDepartmentMasterDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterBankAccountTypeId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterBankAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterBranchId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterCountryId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterDepartmentId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterDesignationId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterEmployeeStatusId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterEmployeeStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterEmployeeTypeId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterEmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterPaymentTypeId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterPaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterSalutationId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterSalutationId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterStateId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterEmployee_MasterTimeZoneId",
                schema: "dbo",
                table: "ADMasterEmployee",
                column: "MasterTimeZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterIndustrySubType_MasterIndustryTypeId",
                schema: "dbo",
                table: "ADMasterIndustrySubType",
                column: "MasterIndustryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterIndustryType_MasterIndustryGroupId",
                schema: "dbo",
                table: "ADMasterIndustryType",
                column: "MasterIndustryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterLogin_MasterProfileId",
                schema: "dbo",
                table: "ADMasterLogin",
                column: "MasterProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterLogin_MasterRegistrationTypeId",
                schema: "dbo",
                table: "ADMasterLogin",
                column: "MasterRegistrationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterRegisteredDevice_MasterLoginId",
                schema: "dbo",
                table: "ADMasterRegisteredDevice",
                column: "MasterLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterRegisteredDevice_MasterTypeOfDeviceId",
                schema: "dbo",
                table: "ADMasterRegisteredDevice",
                column: "MasterTypeOfDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterRegistration_MasterEmployeeId",
                schema: "dbo",
                table: "ADMasterRegistration",
                column: "MasterEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterRegistration_MasterRegistrationTypeId",
                schema: "dbo",
                table: "ADMasterRegistration",
                column: "MasterRegistrationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterState_MasterCountryId",
                schema: "dbo",
                table: "ADMasterState",
                column: "MasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterStatus_MasterColorId",
                schema: "dbo",
                table: "ADMasterStatus",
                column: "MasterColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterAddressTypeId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterAddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterCompanyTypeId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterCompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterCountryId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterCurrencyId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterDesignationId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterStateId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMasterVendor_MasterTimeZoneId",
                schema: "dbo",
                table: "ADMasterVendor",
                column: "MasterTimeZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMessageNotification_MasterBranchId",
                schema: "dbo",
                table: "ADMessageNotification",
                column: "MasterBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMessageNotification_MasterCompanyId",
                schema: "dbo",
                table: "ADMessageNotification",
                column: "MasterCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMessageNotification_MasterMessageTypeId",
                schema: "dbo",
                table: "ADMessageNotification",
                column: "MasterMessageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ADProfileTaskMapping_MasterFunctionId",
                schema: "dbo",
                table: "ADProfileTaskMapping",
                column: "MasterFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_ADProfileTaskMapping_MasterProfileId",
                schema: "dbo",
                table: "ADProfileTaskMapping",
                column: "MasterProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ADTransactionLogin_MasterLoginId",
                schema: "dbo",
                table: "ADTransactionLogin",
                column: "MasterLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_ADTransactionLogin_MasterRegisteredDeviceId",
                schema: "dbo",
                table: "ADTransactionLogin",
                column: "MasterRegisteredDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADErrorLog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADGenCodeMaster",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterBusinessVerticle",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterCity",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterConfiguration",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterFinancialYear",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterGender",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterIndustrySubType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterLoginType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterMailTemplate",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterRegion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterRegistration",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterReportingHead",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterTax",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterVendor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMessageNotification",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADProfileTaskMapping",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADTransactionLogin",
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
                name: "ProfileTaskMappingResults");

            migrationBuilder.DropTable(
                name: "ValidateAccountResults");

            migrationBuilder.DropTable(
                name: "ADGenCodeType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterIndustryType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterEmployee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterColor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterMessageType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterFunction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterRegisteredDevice",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterIndustryGroup",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterBankAccountType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterBranch",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterEmployeeStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterEmployeeType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterPaymentType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterSalutation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterTypeOfDevice",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterCompany",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterProfile",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterRegistrationType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterAddressType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterCompanyType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterCurrency",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterDesignation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterState",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterTimeZone",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterDepartment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ADMasterCountry",
                schema: "dbo");
        }
    }
}
