using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.Dashboard
{
    public class DashboardAdminViewModel
    {
        public DateTime CurrentDateTime { get; set; }
        public string CompanyMasterName { get; set; }
        public string LastSuccessfullLogin { get; set; }
        public string LoginIP { get; set; }
        public string SessionId { get; set; }

        //public List<ProductDetailResult> ProductDetailResultList { get; set; }

        public List<ProductDetailResult> AssetsCategoryList { get; set; }
        public List<ProductDetailResult> AssetsSubCategoryList { get; set; }

        public long TotalAssetsInStock { get; set; }
        public long AssetsAssign { get; set; }
        public long AssetsInRepair { get; set; }

        public long TotalLaptopInStock { get; set; }
        public long LaptopAssign { get; set; }
        public long LaptopInRepair { get; set; }

        public long MicrosoftInStock { get; set; }
        public long MicrosoftAssign { get; set; }
        public long MicrosoftInExpire { get; set; }
    }
}
