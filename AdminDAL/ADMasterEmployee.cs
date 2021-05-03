//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterEmployee", Schema = "dbo")]
    public class ADMasterEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMasterEmployee()
        {
        }

        [Key]
        public long MasterEmployeeId { get; set; }
        public string EmployeeCode { get; set; }

        public Nullable<long> MasterSalutationId { get; set; }
        [ForeignKey("MasterSalutationId")]
        public virtual ADMasterSalutation ADMasterSalutation { get; set; }


        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<long> Gender { get; set; }
        public string PANNo { get; set; }
        public string AadhaarNo { get; set; }

        public Nullable<long> MasterDesignationId { get; set; }
        [ForeignKey("MasterDesignationId")]
        public virtual ADMasterDesignation ADMasterDesignation { get; set; }

        public Nullable<long> MasterDepartmentId { get; set; }
        [ForeignKey("MasterDepartmentId")]
        public virtual ADMasterDepartment ADMasterDepartment { get; set; }

        public Nullable<long> ReportingHeadId { get; set; }
        public Nullable<long> MasterEmployeeTypeId { get; set; }
        [ForeignKey("MasterEmployeeTypeId")]
        public virtual ADMasterEmployeeType ADMasterEmployeeType { get; set; }


        public Nullable<long> MasterTimeZoneId { get; set; }
        [ForeignKey("MasterTimeZoneId")]
        public virtual ADMasterTimeZone ADMasterTimeZone { get; set; }

        

        public Nullable<long> MasterEmployeeStatusId { get; set; }
        [ForeignKey("MasterEmployeeStatusId")]
        public virtual ADMasterEmployeeStatus ADMasterEmployeeStatus { get; set; }


        public Nullable<System.DateTime> DateOfLeavingOrganisation { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public Nullable<long> MasterCountryId { get; set; }
        [ForeignKey("MasterCountryId")]
        public virtual ADMasterCountry ADMasterCountry { get; set; }

        public Nullable<long> MasterStateId { get; set; }
        [ForeignKey("MasterStateId")]
        public virtual ADMasterState ADMasterState { get; set; }

        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        public Nullable<long> MasterPaymentTypeId { get; set; }
        [ForeignKey("MasterPaymentTypeId")]
        public virtual ADMasterPaymentType ADMasterPaymentType { get; set; }

        public string PaypalID { get; set; }
        public string PaypalLink { get; set; } 
        public Nullable<bool> IsPaypalAccountVerified { get; set; }

        public Nullable<long> MasterBankAccountTypeId { get; set; }
        [ForeignKey("MasterBankAccountTypeId")]
        public virtual ADMasterBankAccountType ADMasterBankAccountType { get; set; }

        public string BankName { get; set; }
        public string IFCSCode { get; set; }
        public string ShiftCode_RoutingNo_IBAN { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBranch { get; set; }
        public string BankCity { get; set; }
        public string BankAddress { get; set; }
        public string UploadBankDetail { get; set; }
        public Nullable<bool> IsBankAccountVerified { get; set; }

        public Nullable<long> MasterBranchId { get; set; }
        [ForeignKey("MasterBranchId")]
        public virtual ADMasterBranch ADMasterBranch { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
    }
}
