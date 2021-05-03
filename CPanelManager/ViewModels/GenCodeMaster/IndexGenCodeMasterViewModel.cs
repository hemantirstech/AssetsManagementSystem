using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.GenCodeMaster
{
    public class IndexGenCodeMasterViewModel : PaginationViewModel
    {
        public List<GenCodeMasterViewModel> GenCodeMasterList { get; set; }
    }
}
