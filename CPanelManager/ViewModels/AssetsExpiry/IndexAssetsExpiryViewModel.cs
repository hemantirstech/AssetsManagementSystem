using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.AssetsExpiry
{
    public class IndexAssetsExpiryViewModel:PaginationViewModel
    {
        public List<AssetsExpiryViewModel> AssetsExpiryViewModelList { get; set; }

        public Nullable<long> MasterCategoryType { get; set; }
    }
}
