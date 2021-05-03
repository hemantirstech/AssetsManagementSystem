using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterAssetsAssignment
{
    [Serializable()]
    public class ViewMasterAssetsAssignmentViewModel
    {
        [Key]
        public long MasterAssetsAssignmentId { get; set; }
        public Nullable<System.DateTime> AssetsAssignmentDate { get; set; }
        public long MasterProductChildId { get; set; }
        public string ProductChildTitle { get; set; }

        public Nullable<long> MasterEmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public Nullable<bool> IsAssetsDeAssign { get; set; }

        public Nullable<System.DateTime> AssetsDeAssignmentDate { get; set; }
        public string DeAssignReason { get; set; }

        public Nullable<long> MasterLocationId { get; set; }
        public string LocationName { get; set; }
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
