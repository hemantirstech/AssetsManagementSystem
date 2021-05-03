using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPanelManager.ViewModels.MasterAssetsAssignment
{
    [Serializable()]
    public class PrintMasterAssetsAssignmentViewModel
    {
        public long MasterAssetsAssignmentId { get; set; }

        [Required(ErrorMessage = "Select branch")]
        [Range(1, long.MaxValue, ErrorMessage = "Select branch")]
        public Nullable<long> MasterBranchId { get; set; }
        public string BranchTitle { get; set; }

        public long MasterProductChildId { get; set; }
        public string ProductChildTitle { get; set; }

        //====================================================================================================================================
        public Nullable<long> MasterCategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public Nullable<long> MasterSubCategoryId { get; set; }
        public string SubCategoryTitle { get; set; }

        public Nullable<long> MasterBrandId { get; set; }
        public string BrandTitle { get; set; }
        
        public long MasterProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductChildSKU { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public Nullable<long> MasterEmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<long> MasterVendorId { get; set; }
        public string VendorTitle { get; set; }

        public List<MasterAssetsAssignmentViewModel> MasterAssetsAssignmentList { get; set; }
        public List<MasterAssetsAssignmentViewModel> MasterAssetsNotAssignmentList { get; set; }
        public AssetsAssignEmployeeViewModel AssetsAssignEmployee { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
