using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterAssetsAssignment
{
    public class IndexMasterAssetsAssignmentViewModel : PaginationViewModel
    {
        public List<MasterAssetsAssignmentViewModel> MasterAssetsAssignmentList { get; set; }
        public List<AssetsAssignEmployeeViewModel> AssetsAssignEmployeeList { get; set; }
        
    }
}
