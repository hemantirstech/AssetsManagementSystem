using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminWebApi.Migrations
{
    public partial class NewHostII : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(1933), new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3170) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3778), new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3794) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3832), new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3834) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 4L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3854), new DateTime(2021, 1, 3, 8, 8, 21, 553, DateTimeKind.Local).AddTicks(3855) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterBranch",
                keyColumn: "MasterBranchId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 548, DateTimeKind.Local).AddTicks(4644), new DateTime(2021, 1, 3, 8, 8, 21, 548, DateTimeKind.Local).AddTicks(5455) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterCompany",
                keyColumn: "MasterCompanyId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 545, DateTimeKind.Local).AddTicks(8275), new DateTime(2021, 1, 3, 8, 8, 21, 546, DateTimeKind.Local).AddTicks(8365) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterEmployee",
                keyColumn: "MasterEmployeeId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 548, DateTimeKind.Local).AddTicks(9644), new DateTime(2021, 1, 3, 8, 8, 21, 549, DateTimeKind.Local).AddTicks(427) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(1771), new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(2977) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3612), new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3628) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3667), new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3669) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterFunction",
                columns: new[] { "MasterFunctionId", "EnterById", "EnterDate", "FunctionIcon", "FunctionIconColour", "FunctionLink", "FunctionTitle", "IsActive", "ModifiedById", "ModifiedDate", "ParentMasterFunctionId", "Sequence" },
                values: new object[,]
                {
                    { 4L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3691), "fa fa-home", "", "/MasterFunction/Index", "Master Function", true, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3693), 2L, 2L },
                    { 6L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3742), "", "", "//ProfileTaskMappings/Index", "Task Mapping", true, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3743), 2L, 4L },
                    { 5L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3713), "fa fa-cog", "", "/MasterProfile/Index", "Master Profile", true, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(3714), 2L, 3L }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterLogin",
                keyColumn: "MasterLoginId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 549, DateTimeKind.Local).AddTicks(8010), new DateTime(2021, 1, 3, 8, 8, 21, 549, DateTimeKind.Local).AddTicks(8801) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterProfile",
                keyColumn: "MasterProfileId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 550, DateTimeKind.Local).AddTicks(5803), new DateTime(2021, 1, 3, 8, 8, 21, 550, DateTimeKind.Local).AddTicks(6636) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterProfile",
                keyColumn: "MasterProfileId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 550, DateTimeKind.Local).AddTicks(7283), new DateTime(2021, 1, 3, 8, 8, 21, 550, DateTimeKind.Local).AddTicks(7301) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterProfile",
                columns: new[] { "MasterProfileId", "EnterById", "EnterDate", "IsActive", "IsDelete", "IsInsert", "IsSelect", "IsUpdate", "ModifiedById", "ModifiedDate", "ProfileCode", "ProfileDescription", "ProfileTitle" },
                values: new object[] { 3L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 550, DateTimeKind.Local).AddTicks(7345), true, true, true, true, true, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 550, DateTimeKind.Local).AddTicks(7346), null, "Employee", "Employee" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterRegistrationType",
                keyColumn: "MasterRegistrationTypeId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(3266), new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(4066) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADMasterRegistrationType",
                columns: new[] { "MasterRegistrationTypeId", "EnterById", "EnterDate", "IsActive", "MasterRegistrationCode", "MasterRegistrationExpertType", "MasterRegistrationTypeTitle", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 2L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(4676), true, "AD", null, "Administrator", 1L, new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(4692) },
                    { 3L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(4729), true, "EM", null, "Employee", 1L, new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(4731) }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterSalutation",
                keyColumn: "MasterSalutationId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(7402), new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(8197) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterSalutation",
                keyColumn: "MasterSalutationId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(8806), new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(8827) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterSalutation",
                keyColumn: "MasterSalutationId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(8863), new DateTime(2021, 1, 3, 8, 8, 21, 552, DateTimeKind.Local).AddTicks(8865) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(7685), new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(8474) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9106), new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9123) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9162), new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9164) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                columns: new[] { "MasterProfileTaskMappingId", "EnterById", "EnterDate", "IsActive", "IsDelete", "IsInsert", "IsSelect", "IsUpdate", "MasterFunctionId", "MasterProfileId", "ModifiedById", "ModifiedDate" },
                values: new object[] { 4L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9186), true, true, true, true, true, 4L, 1L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9188) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                columns: new[] { "MasterProfileTaskMappingId", "EnterById", "EnterDate", "IsActive", "IsDelete", "IsInsert", "IsSelect", "IsUpdate", "MasterFunctionId", "MasterProfileId", "ModifiedById", "ModifiedDate" },
                values: new object[] { 5L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9208), true, true, true, true, true, 5L, 1L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9209) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                columns: new[] { "MasterProfileTaskMappingId", "EnterById", "EnterDate", "IsActive", "IsDelete", "IsInsert", "IsSelect", "IsUpdate", "MasterFunctionId", "MasterProfileId", "ModifiedById", "ModifiedDate" },
                values: new object[] { 6L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9268), true, true, true, true, true, 6L, 1L, 1L, new DateTime(2021, 1, 3, 8, 8, 21, 551, DateTimeKind.Local).AddTicks(9270) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADMasterProfile",
                keyColumn: "MasterProfileId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADMasterRegistrationType",
                keyColumn: "MasterRegistrationTypeId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADMasterRegistrationType",
                keyColumn: "MasterRegistrationTypeId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(5450), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(6713) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7399), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7416) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7453), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7455) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADGenCodeType",
                keyColumn: "GenCodeTypeId",
                keyValue: 4L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7476), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(7477) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterBranch",
                keyColumn: "MasterBranchId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 158, DateTimeKind.Local).AddTicks(4777), new DateTime(2021, 1, 3, 7, 23, 46, 158, DateTimeKind.Local).AddTicks(5613) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterCompany",
                keyColumn: "MasterCompanyId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 155, DateTimeKind.Local).AddTicks(7009), new DateTime(2021, 1, 3, 7, 23, 46, 156, DateTimeKind.Local).AddTicks(7575) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterEmployee",
                keyColumn: "MasterEmployeeId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(42), new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(896) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(4872), new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(5743) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6426), new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6443) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterFunction",
                keyColumn: "MasterFunctionId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6480), new DateTime(2021, 1, 3, 7, 23, 46, 161, DateTimeKind.Local).AddTicks(6481) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterLogin",
                keyColumn: "MasterLoginId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(8768), new DateTime(2021, 1, 3, 7, 23, 46, 159, DateTimeKind.Local).AddTicks(9646) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterProfile",
                keyColumn: "MasterProfileId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(6717), new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(7841) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterProfile",
                keyColumn: "MasterProfileId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(8605), new DateTime(2021, 1, 3, 7, 23, 46, 160, DateTimeKind.Local).AddTicks(8630) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterRegistrationType",
                keyColumn: "MasterRegistrationTypeId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(6601), new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(7517) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterSalutation",
                keyColumn: "MasterSalutationId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(746), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(1558) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterSalutation",
                keyColumn: "MasterSalutationId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2167), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2186) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADMasterSalutation",
                keyColumn: "MasterSalutationId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2219), new DateTime(2021, 1, 3, 7, 23, 46, 163, DateTimeKind.Local).AddTicks(2221) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 1L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(880), new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(1685) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 2L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2376), new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2391) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "ADProfileTaskMapping",
                keyColumn: "MasterProfileTaskMappingId",
                keyValue: 3L,
                columns: new[] { "EnterDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2429), new DateTime(2021, 1, 3, 7, 23, 46, 162, DateTimeKind.Local).AddTicks(2431) });
        }
    }
}
