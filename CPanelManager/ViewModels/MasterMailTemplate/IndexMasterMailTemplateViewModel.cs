using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterMailTemplate
{
    public class IndexMasterMailTemplateViewModel : PaginationViewModel
    {
        public List<MasterMailTemplateViewModel> MasterMailTemplateList { get; set; }
    }
}
