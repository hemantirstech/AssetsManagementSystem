using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterCity
{
    [Serializable()]
    public class AddMasterCityViewModel
    {
        [Key]
        public long MasterCityId { get; set; }

        //Foreign Key
        [Required(ErrorMessage = "Enter State!")]
        [Range(1, long.MaxValue, ErrorMessage = "Error State!")]
        public Nullable<long> MasterStateId { get; set; }

        [Required(ErrorMessage = "Enter City Title!")]
        [StringLength (30, MinimumLength =3)]
        public string CityTitle { get; set; }
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
