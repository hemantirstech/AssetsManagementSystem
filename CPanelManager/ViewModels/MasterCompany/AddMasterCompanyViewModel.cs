using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterCompany
{
    [Serializable()]
    public class AddMasterCompanyViewModel
    {
        [Key]
        public long MasterCompanyId { get; set; }

        [Required(ErrorMessage = "Enter Company Title!")]
        [StringLength(100, MinimumLength = 3)]
        public string CompanyTitle { get; set; }

        [Required(ErrorMessage = "Enter Contact Person!")]
        [StringLength(100, MinimumLength = 3)]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Select Designation")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Designation")]
        public Nullable<long> MasterDesignationId { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateofRegistration { get; set; }

        [Required(ErrorMessage = "Select Company Type!")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Company Type!")]
        public Nullable<long> MasterCompanyTypeId { get; set; }
                
        public string RegistrationNumber { get; set; }       
        public string GSTNumber { get; set; }        
        public string PANNumber { get; set; }        
        public string TANNumber { get; set; }       
        public string SEZRegistrationNumber { get; set; }       
        public string SAC_Code { get; set; }
        public string LUT_AppliactionReference { get; set; }
        public string CompanyLogo { get; set; }
        public string ReportLogo { get; set; }

        [Required(ErrorMessage = "Select Master Currency!")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Master Currency!")]
        public Nullable<long> MasterCurrencyId { get; set; }

        [Required(ErrorMessage = "Select Master Time Zone!")]
        [Range(1, long.MaxValue, ErrorMessage = "Select Master Time Zone!")]
        public Nullable<long> MasterTimeZoneId { get; set; }

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

        
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter Phone Number!")]
        [StringLength(10, MinimumLength = 10)]
        public string MobileNumber { get; set; }


        [Required(ErrorMessage = "Enter Email!")]
        [EmailAddress]
        public string Email { get; set; }

        public string Fax { get; set; }
        public string URL { get; set; }
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
