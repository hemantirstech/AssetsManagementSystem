using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterGender
{
    public class IndexMasterGenderViewModel : PaginationViewModel
    {
        public List<MasterGenderViewModel> MasterGenderList { get; set; }
    }
}
