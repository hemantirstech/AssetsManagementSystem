using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterProduct
{
    public class AddMasterProductMergeViewModel
    {       
        public AddMasterProductViewModel ASMasterProduct { get; set; }

        public AddMasterProductChildViewModel ASMasterProductChild { get; set; }

        public AddMasterAssetsAssignmentViewModel ASMasterAssetsAssignment { get; set; }
    }
}
