using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterCountry
{
    [Serializable()]
    public class ViewMasterCountryViewModel
    {
        [Key]
        public long MasterCountryId { get; set; }

        [Required(ErrorMessage = "Enter Country Title!")]
        [StringLength(30, MinimumLength = 3)]
        public string CountryTitle { get; set; }
        public string CountryCode { get; set; }
        public string CountryDialCode { get; set; }
        public string CountryFlag { get; set; }
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
