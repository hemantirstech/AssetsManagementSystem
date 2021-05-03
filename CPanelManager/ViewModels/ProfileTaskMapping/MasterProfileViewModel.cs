using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.ProfileTaskMapping
{
    [Serializable()]
    public class MasterProfileViewModel 
    {
        [Key]
        public long MasterProfileId { get; set; }        
        public string ProfileTitle { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileDescription { get; set; }

        public Nullable<bool> IsDelete { get; set; }
        public string DeleteColor { get; set; }
        public string DeleteIcon { get; set; }

        public Nullable<bool> IsInsert { get; set; }
        public string InsertColor { get; set; }
        public string InsertIcon { get; set; }

        public Nullable<bool> IsUpdate { get; set; }
        public string UpdateColor { get; set; }
        public string UpdateIcon { get; set; }

        public Nullable<bool> IsSelect { get; set; }
        public string SelectColor { get; set; }
        public string SelectIcon { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }        
    }
}
