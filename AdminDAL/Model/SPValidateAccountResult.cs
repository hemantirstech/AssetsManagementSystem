using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL.Model
{

    
    //[NotMapped]
    public partial class SPValidateAccountResult
    {
        
        public long MasterLoginId { get; set; }

        public string MasterRegistrationTypeTitle { get; set; }
        //public Nullable<long> MasterRegistrationTypeId { get; set; }
        public long MasterRegistrationId { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }

        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsFirstLogin { get; set; }
        public Nullable<int> ValidationCount { get; set; }

        public Nullable<long> MasterEmployeeId { get; set; }
        public long MasterSalutationId { get; set; }        
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }        
        public Nullable<long> MasterCompanyId { get; set; }
        public string CompanyTitle { get; set; }
        public Nullable<long> MasterBranchId { get; set; }
        public string BranchTitle { get; set; }
        public long MasterProfileId { get; set; }
        public string ProfileTitle { get; set; }

        [Key]
        public long MasterFunctionId { get; set; }
        public string FunctionTitle { get; set; }
        public string FunctionLink { get; set; }
        public string FunctionIcon { get; set; }
        public string FunctionIconColour { get; set; }
        public long ParentMasterFunctionId { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsInsert { get; set; }
        public Nullable<bool> IsUpdate { get; set; }
        public Nullable<bool> IsSelect { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
    }
}
