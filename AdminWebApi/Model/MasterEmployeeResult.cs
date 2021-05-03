using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    public class MasterEmployeeResult
    {
        [Key]
        public long MasterEmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public Nullable<long> MasterSalutationId { get; set; }
        public string SalutationTitle { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<long> Gender { get; set; }
        public string GenderTitle { get; set; }
        public string PANNo { get; set; }
        public string AadhaarNo { get; set; }
        public Nullable<long> MasterDesignationId { get; set; }
        public string DesignationTitle { get; set; }
        public Nullable<long> MasterDepartmentId { get; set; }
        public string DepartmentTitle { get; set; }
        public Nullable<long> ReportingHeadId { get; set; }
        public string ReportingHeadTitle { get; set; }
        public string ReportingHeadDesignationTitle { get; set; }
        public Nullable<long> MasterEmployeeTypeId { get; set; }
        public string EmployeeTypeTitle { get; set; }
        public Nullable<long> MasterTimeZoneId { get; set; }
        public string TimeZoneTitle { get; set; }
        public Nullable<long> MasterEmployeeStatusId { get; set; }
        public string EmployeeStatusTitle { get; set; }
        public Nullable<System.DateTime> DateOfLeavingOrganisation { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Nullable<long> MasterCountryId { get; set; }
        public string CountryTitle { get; set; }
        public Nullable<long> MasterStateId { get; set; }
        public string StateTitle { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public Nullable<long> MasterPaymentTypeId { get; set; }
        public string PaymentTypeTitle { get; set; }
        public string PaypalID { get; set; }
        public string PaypalLink { get; set; }
        public Nullable<bool> IsPaypalAccountVerified { get; set; }
        public Nullable<long> MasterBankAccountTypeId { get; set; }
        public string BankAccountTypeTitle { get; set; }
        public string BankName { get; set; }
        public string IFCSCode { get; set; }
        public string ShiftCode_RoutingNo_IBAN { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBranch { get; set; }
        public string BankCity { get; set; }
        public string BankAddress { get; set; }
        public string UploadBankDetail { get; set; }
        public Nullable<bool> IsBankAccountVerified { get; set; }
        public Nullable<long> MasterCompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyLogo { get; set; }

        public Nullable<long> MasterBranchId { get; set; }
        public string BranchTitle { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
