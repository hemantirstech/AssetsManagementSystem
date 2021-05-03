using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterBranch
{
    [Serializable()]
    public class AddMasterBranchViewModel
    {
        [Key]
        public long MasterBranchId { get; set; }

        public string BranchCode { get; set; }

        [Required(ErrorMessage = "Enter Branch Title!")]
        [StringLength(100, MinimumLength = 3)]
        public string BranchTitle { get; set; }

        [Required(ErrorMessage = "Enter Branch ContactPerson!")]
        [StringLength(100, MinimumLength = 3)]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Select designation")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Designation")]
        public Nullable<long> MasterDesignationId { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateofRegistration { get; set; }

        public Nullable<long> MasterAddressTypeId { get; set; }

        [Required(ErrorMessage = "Enter Address!")]
        [StringLength(150, MinimumLength = 3)]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "Select Country")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Country")]
        public Nullable<long> MasterCountryId { get; set; }
        
        [Required(ErrorMessage = "Select State")]
        [Range(1, long.MaxValue, ErrorMessage = "Select State")]
        public Nullable<long> MasterStateId { get; set; }
        
        [Required(ErrorMessage = "Enter City!")]
        [StringLength(100, MinimumLength = 3)]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter Pincode!")]
        [StringLength(6, MinimumLength = 6)]
        public string PinCode { get; set; }

        
        [StringLength(10, MinimumLength = 10,ErrorMessage ="Enter Phone Number in Correct Format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter Mobile Number!")]
        [StringLength(10, MinimumLength = 10)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Enter Email!")]
        [EmailAddress]
        public string Email { get; set; }
        public string Fax { get; set; }
        public string URL { get; set; }

        [Required(ErrorMessage = "Enter Company!")]
        [Range(1, long.MaxValue, ErrorMessage = "Enter Company!")]
        public Nullable<long> MasterCompanyId { get; set; }
        
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }

        public List<ProductDetailResult> ProductDetailResultList { get; set; }
    }
}
