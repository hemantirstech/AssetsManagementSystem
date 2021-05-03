using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterDepartment
{
    public class IndexMasterDepartmentViewModel : PaginationViewModel
    {
        public List<MasterDepartmentViewModel> MasterDepartmentList { get; set; }
    }
}
