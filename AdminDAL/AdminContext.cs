using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AdminDAL.Model;

namespace AdminDAL
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {
            //Database.SetInitializer<AdminContext>(new CreateDatabaseIfNotExists<AdminContext>());
        }

        public DbSet<ADMasterErrorLog> ADMasterErrorLogs { get; set; }
        public DbSet<ADGenCodeType> ADGenCodeTypes { get; set; }
        public DbSet<ADGenCodeMaster> ADGenCodeMasters { get; set; }
        public virtual DbSet<ADMasterAddressType> ADMasterAddressTypes { get; set; }
        public virtual DbSet<ADMasterBankAccountType> ADMasterBankAccountTypes { get; set; }
        public virtual DbSet<ADMasterBranch> ADMasterBranches { get; set; }
        public virtual DbSet<ADMasterBusinessVerticle> ADMasterBusinessVerticles { get; set; }
        public virtual DbSet<ADMasterCity> ADMasterCities { get; set; }
        public virtual DbSet<ADMasterColor> ADMasterColors { get; set; }
        public virtual DbSet<ADMasterCompany> ADMasterCompanies { get; set; }
        public virtual DbSet<ADMasterCompanyType> ADMasterCompanyTypes { get; set; }
        public virtual DbSet<ADMasterConfiguration> ADMasterConfigurations { get; set; }
        public virtual DbSet<ADMasterCountry> ADMasterCountries { get; set; }
        public virtual DbSet<ADMasterCurrency> ADMasterCurrencies { get; set; }
        public virtual DbSet<ADMasterDepartment> ADMasterDepartments { get; set; }
        public virtual DbSet<ADMasterDesignation> ADMasterDesignations { get; set; }
        public virtual DbSet<ADMasterEmployee> ADMasterEmployees { get; set; }       
        public virtual DbSet<ADMasterEmployeeType> ADMasterEmployeeTypes { get; set; }
        public virtual DbSet<ADMasterFinancialYear> ADMasterFinancialYears { get; set; }
        public virtual DbSet<ADMasterFunction> ADMasterFunctions { get; set; }
        public virtual DbSet<ADMasterGender> ADMasterGenders { get; set; }
        public virtual DbSet<ADMasterIndustryGroup> ADMasterIndustryGroups { get; set; }
        public virtual DbSet<ADMasterIndustrySubType> ADMasterIndustrySubTypes { get; set; }
        public virtual DbSet<ADMasterIndustryType> ADMasterIndustryTypes { get; set; }
        public virtual DbSet<ADMasterLogin> ADMasterLogins { get; set; }
        public virtual DbSet<ADMasterLoginType> ADMasterLoginTypes { get; set; }
        public virtual DbSet<ADMasterMailTemplate> ADMasterMailTemplates { get; set; }
        public virtual DbSet<ADMasterMessageType> ADMasterMessageTypes { get; set; }
        public virtual DbSet<ADMasterPaymentType> ADMasterPaymentTypes { get; set; }
        public virtual DbSet<ADMasterProfile> ADMasterProfiles { get; set; }
        public virtual DbSet<ADMasterRegion> ADMasterRegions { get; set; }
        public virtual DbSet<ADMasterRegisteredDevice> ADMasterRegisteredDevices { get; set; }
        public virtual DbSet<ADMasterReportingHead> ADMasterReportingHeads { get; set; }
        public virtual DbSet<ADMasterSalutation> ADMasterSalutations { get; set; }
        public virtual DbSet<ADMasterState> ADMasterStates { get; set; }
        public virtual DbSet<ADMasterStatus> ADMasterStatuss { get; set; }
        public virtual DbSet<ADMasterTax> ADMasterTaxes { get; set; }
        public virtual DbSet<ADMasterTimeZone> ADMasterTimeZones { get; set; }
        public virtual DbSet<ADMasterTypeOfDevice> ADMasterTypeOfDevices { get; set; }
        public virtual DbSet<ADMessageNotification> ADMessageNotifications { get; set; }
        public virtual DbSet<ADProfileTaskMapping> ADProfileTaskMappings { get; set; }
        public virtual DbSet<ADTransactionLogin> ADTransactionLogins { get; set; }

        public virtual DbSet<ADMasterEmployeeStatus> ADMasterEmployeeStatus { get; set; }
        public virtual DbSet<ADMasterRegistration> ADMasterRegistration { get; set; }
        public virtual DbSet<ADMasterRegistrationType> ADMasterRegistrationType { get; set; }
        public virtual DbSet<ADMasterVendor> ADMasterVendors { get; set; }
        public virtual DbSet<ADTransactionVendorFileUpload> ADTransactionVendorFileUploads { get; set; }

        //Execute Stored Procedure
        public virtual DbSet<SPValidateAccountResult> ValidateAccountResults { get; set; }
        public virtual List<SPValidateAccountResult> SP_ADValidateAccount(string UserName, string Password, string VerificationCode, string MacId, string SessionId, string Mode, int ModeVersion, string SearchCondition)
        {
            try
            {
                var _ValidateAccountResult = this.ValidateAccountResults.FromSqlRaw<SPValidateAccountResult>("Exec SP_ADValidateAccount {0},{1},{2},{3},{4},{5},{6},{7}", UserName, Password, VerificationCode, MacId, SessionId, Mode, ModeVersion, SearchCondition).ToList();
                return _ValidateAccountResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPDropDownFillResult> DropDownFillResults { get; set; }
        public virtual List<SPDropDownFillResult> SP_ADDropDownFillResult(string TableName, long MasterId, string Type)
        {
            try
            {
                var _DropDownFillResult = this.DropDownFillResults.FromSqlRaw<SPDropDownFillResult>("Exec SP_ADDropDownFill {0},{1},{2}", TableName, MasterId, Type).ToList();
                return _DropDownFillResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPCheckAvailableResult> CheckAvailableResults { get; set; }
        public virtual List<SPCheckAvailableResult> SP_ADCheckAvailableResult(string TableName, string NameAvailable, long NameId)
        {
            try
            {
                var _CheckAvailableResult = this.CheckAvailableResults.FromSqlRaw<SPCheckAvailableResult>("Exec SP_ADCheckAvailable {0},{1},{2}", TableName, NameAvailable, NameId).ToList();
                return _CheckAvailableResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPNextMasterIdResult> NextMasterIdResults { get; set; }

        public virtual List<SPNextMasterIdResult> SP_ADNextMasterIdResult(string TableName)
        {
            try
            {
                var _NextMasterIdResult = this.NextMasterIdResults.FromSqlRaw<SPNextMasterIdResult>("Exec SP_ADNextMasterId {0}", TableName).ToList();
                return _NextMasterIdResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPMaxTableMasterIdResult> MaxTableMasterIdResults { get; set; }

        public virtual List<SPMaxTableMasterIdResult> SP_ADMaxTableMasterIdResult(string TableName)
        {
            try
            {
                var _MaxMasterIdResult = this.MaxTableMasterIdResults.FromSqlRaw<SPMaxTableMasterIdResult>("Exec SP_ADMaxTableMasterId {0}", TableName).ToList();
                return _MaxMasterIdResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual DbSet<SPProfileTaskMappingResult> ProfileTaskMappingResults { get; set; }

        public virtual List<SPProfileTaskMappingResult> SP_ADProfileTaskMappingResults(long MasterProfileId)
        {
            try
            {
                var _ProfileTaskMappingResult = this.ProfileTaskMappingResults.FromSqlRaw<SPProfileTaskMappingResult>("Exec SP_ADProfileTaskMapping {0}", MasterProfileId).ToList();
                return _ProfileTaskMappingResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            //ADMasterCompany
            modelBuilder.Entity<ADMasterCompany>().HasData(new ADMasterCompany { MasterCompanyId = 1, CompanyTitle = "IRS TECHNOLOGIES", MobileNumber = "9999999999", Email = "support@irstechnologies.com", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADMasterBranch
            modelBuilder.Entity<ADMasterBranch>().HasData(new ADMasterBranch { MasterBranchId = 1, BranchTitle = "IRS TECHNOLOGIES Nagpur", MobileNumber = "9999999999", Email = "support@irstechnologies.com", MasterCompanyId = 1, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADMasterEmployee
            modelBuilder.Entity<ADMasterEmployee>().HasData(new ADMasterEmployee { MasterEmployeeId = 1, MasterSalutationId = 1, EmployeeName = "AMIT KUBADE", MobileNumber = "9999999999", Email = "superadmin@kritms.com", MasterBranchId = 1, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
                        
            //ADMasterLogin
            modelBuilder.Entity<ADMasterLogin>().HasData(new ADMasterLogin { MasterLoginId = 1, MasterRegistrationTypeId = 1, MasterRegistrationId = 1, MasterProfileId = 1, UserName = "superadmin@kritms.com", Password = "BQxfjG3oUEsTpNTUjOnKlA==", VerificationCode = "", IsVerified = true, IsFirstLogin = false, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADMasterProfile
            modelBuilder.Entity<ADMasterProfile>().HasData(new ADMasterProfile { MasterProfileId = 1, ProfileTitle = "Super Admin", ProfileDescription = "Super Admin", IsDelete = true,IsUpdate=true,IsInsert=true,IsSelect=true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterProfile>().HasData(new ADMasterProfile { MasterProfileId = 2, ProfileTitle = "Admin", ProfileDescription = "Admin", IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterProfile>().HasData(new ADMasterProfile { MasterProfileId = 3, ProfileTitle = "Employee", ProfileDescription = "Employee", IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADMasterFunction
            modelBuilder.Entity<ADMasterFunction>().HasData(new ADMasterFunction { MasterFunctionId = 1, FunctionTitle = "Dashboard", FunctionLink = "/Dashboard/Index", FunctionIcon = "fa fa-home", FunctionIconColour = "", ParentMasterFunctionId = 0, Sequence = 1, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterFunction>().HasData(new ADMasterFunction { MasterFunctionId = 2, FunctionTitle = "Configuration", FunctionLink = "", FunctionIcon = "fa fa-cog", FunctionIconColour = "", ParentMasterFunctionId = 0, Sequence = 2, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterFunction>().HasData(new ADMasterFunction { MasterFunctionId = 3, FunctionTitle = "Licence Agreement", FunctionLink = "/LicenceAggrement/ViewLicence", FunctionIcon = "", FunctionIconColour = "", ParentMasterFunctionId = 2, Sequence = 1, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterFunction>().HasData(new ADMasterFunction { MasterFunctionId = 4, FunctionTitle = "Master Function", FunctionLink = "/MasterFunction/Index", FunctionIcon = "fa fa-home", FunctionIconColour = "", ParentMasterFunctionId = 2, Sequence = 2, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterFunction>().HasData(new ADMasterFunction { MasterFunctionId = 5, FunctionTitle = "Master Profile", FunctionLink = "/MasterProfile/Index", FunctionIcon = "fa fa-cog", FunctionIconColour = "", ParentMasterFunctionId = 2, Sequence = 3, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterFunction>().HasData(new ADMasterFunction { MasterFunctionId = 6, FunctionTitle = "Task Mapping", FunctionLink = "//ProfileTaskMappings/Index", FunctionIcon = "", FunctionIconColour = "", ParentMasterFunctionId = 2, Sequence = 4, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADProfileTaskMapping
            modelBuilder.Entity<ADProfileTaskMapping>().HasData(new ADProfileTaskMapping { MasterProfileTaskMappingId = 1, MasterProfileId = 1, MasterFunctionId=1, IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADProfileTaskMapping>().HasData(new ADProfileTaskMapping { MasterProfileTaskMappingId = 2, MasterProfileId = 1, MasterFunctionId = 2, IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADProfileTaskMapping>().HasData(new ADProfileTaskMapping { MasterProfileTaskMappingId = 3, MasterProfileId = 1, MasterFunctionId = 3, IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADProfileTaskMapping>().HasData(new ADProfileTaskMapping { MasterProfileTaskMappingId = 4, MasterProfileId = 1, MasterFunctionId = 4, IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADProfileTaskMapping>().HasData(new ADProfileTaskMapping { MasterProfileTaskMappingId = 5, MasterProfileId = 1, MasterFunctionId = 5, IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADProfileTaskMapping>().HasData(new ADProfileTaskMapping { MasterProfileTaskMappingId = 6, MasterProfileId = 1, MasterFunctionId = 6, IsDelete = true, IsUpdate = true, IsInsert = true, IsSelect = true, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADMasterRegistrationType
            modelBuilder.Entity<ADMasterRegistrationType>().HasData(new ADMasterRegistrationType { MasterRegistrationTypeId = 1, MasterRegistrationTypeTitle = "Super Admin", MasterRegistrationCode = "SA", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterRegistrationType>().HasData(new ADMasterRegistrationType { MasterRegistrationTypeId = 2, MasterRegistrationTypeTitle = "Administrator", MasterRegistrationCode = "AD", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterRegistrationType>().HasData(new ADMasterRegistrationType { MasterRegistrationTypeId = 3, MasterRegistrationTypeTitle = "Employee", MasterRegistrationCode = "EM", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADMasterSalutation
            modelBuilder.Entity<ADMasterSalutation>().HasData(new ADMasterSalutation { MasterSalutationId = 1, SalutationTitle = "Mr.", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterSalutation>().HasData(new ADMasterSalutation { MasterSalutationId = 2, SalutationTitle = "Mrs.", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADMasterSalutation>().HasData(new ADMasterSalutation { MasterSalutationId = 3, SalutationTitle = "Dr.", IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
                       
            //ADGenCodeType
            modelBuilder.Entity<ADGenCodeType>().HasData(new ADGenCodeType { GenCodeTypeId = 1, GenCodeTypeTitle = "Company Type", GenCodeTypePrintDesc = "CT", GenCodeTypeDesc = "Company Type", Sequence = 1, IsActive = true, EnterById =1, EnterDate=DateTime.Now, ModifiedById =1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADGenCodeType>().HasData(new ADGenCodeType { GenCodeTypeId = 2, GenCodeTypeTitle = "Registration Type", GenCodeTypePrintDesc = "RT", GenCodeTypeDesc = "Registration Type", Sequence = 2, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADGenCodeType>().HasData(new ADGenCodeType { GenCodeTypeId = 3, GenCodeTypeTitle = "Login Type", GenCodeTypePrintDesc = "LT", GenCodeTypeDesc = "Login Type", Sequence = 3, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });
            modelBuilder.Entity<ADGenCodeType>().HasData(new ADGenCodeType { GenCodeTypeId = 4, GenCodeTypeTitle = "Bank Account Type", GenCodeTypePrintDesc = "BA", GenCodeTypeDesc = "Bank Account Type", Sequence = 4, IsActive = true, EnterById = 1, EnterDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

            //ADGenCodeMaster
            //modelBuilder.Entity<ADGenCodeMaster>().HasData(new ADGenCodeMaster { GenCodeMasterId=1, GenCodeMasterTitle= "Propritor", PrintDesc="", ADGenCodeType = 1, Sequence = 1, IsActive = true, EnterById = 1, EnterByDate = DateTime.Now, ModifiedById = 1, ModifiedDate = DateTime.Now });

        }

        //SqlParameter param = new SqlParameter("@FirstName", "Bill");

        
    }
}
