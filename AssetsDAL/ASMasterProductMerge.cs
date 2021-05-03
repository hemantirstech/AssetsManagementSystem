using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [NotMapped]
    public partial class ASMasterProductMerge
    {
        public ASMasterProduct ASMasterProduct { get; set;}
        public ASMasterProductChild ASMasterProductChild { get; set; }
        public ASMasterAssetsAssignment ASMasterAssetsAssignment { get; set; }

    }
}
