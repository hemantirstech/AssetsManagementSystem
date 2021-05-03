using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterPassword
{
    public class IndexMasterProductChildViewModel
    {

        [Key]
        public long MasterProductId { get; set; }
        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }
        public Nullable<long> MasterBrandId { get; set; }
        public string BrandTitle { get; set; }        
        public string ProductSKU { get; set; }
        public string ProductHSNCode { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductModel { get; set; }
        public string Manufacturer { get; set; }
        //Child Product
        public List<MasterProductAssignChildResult> ProductAssignChildList { get; set; }
    }
}
