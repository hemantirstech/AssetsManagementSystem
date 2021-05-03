using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPanelManager.ViewModels.MasterProduct
{
    [NotMapped]
    public partial class ASMasterProductChildStatusViewModel
    {
        public long MasterProductId { get; set; }
        public long[] IsRepairs { get; set;}
        public long[] IsDeadAssets { get; set; }
        public long[] IsSaleProducts { get; set; }

    }
}
