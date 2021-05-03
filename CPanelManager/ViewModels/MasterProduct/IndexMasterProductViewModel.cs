using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MasterProduct
{
    public class IndexMasterProductViewModel : PaginationViewModel
    {
        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public Nullable<long> MasterBrandId { get; set; }
        public string BrandTitle { get; set; }
        public List<MasterProductViewModel> MasterProductList { get; set; }
    }
}
