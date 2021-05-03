using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterLogin
{
    [Serializable()]
    public class MasterLoginViewModel
    {
        [Key]
        public long MasterLoginId { get; set; }

        public Nullable<long> MasterRegistrationTypeId { get; set; }
        public string RegistrationTypeTitle { get; set; }
        public Nullable<long> MasterRegistrationId { get; set; }
        public string RegistrationTitle { get; set; }
        public string SalutationTitle { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public string CompanyTitle { get; set; }
        public string BranchTitle { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<long> MasterProfileId { get; set; }
        public string ProfileTitle { get; set; }
        public string VerificationCode { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public Nullable<bool> IsFirstLogin { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
