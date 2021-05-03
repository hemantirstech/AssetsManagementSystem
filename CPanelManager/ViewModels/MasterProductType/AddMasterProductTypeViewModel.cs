using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterProductType
{
    [Serializable()]
    public class AddMasterProductTypeViewModel
    {
        [Key]
        public long MasterProductTypeId { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeTitle { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ProductTypeImage { get; set; }

        public long MasterSubCategoryId { get; set; }

        public string SubCategoryTitle { get; set; }

        public Nullable<long> Sequence { get; set; }
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
