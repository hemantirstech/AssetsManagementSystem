using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterBusinessVerticle
{
    public class IndexMasterBusinessVerticleViewModel : PaginationViewModel
    {
        public List<MasterBusinessVerticleViewModel> MasterBusinessVerticleList { get; set; }
    }
}
