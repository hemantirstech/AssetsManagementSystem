using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPanelManager.ViewModels.MasterTax
{
    [Serializable()]
    public class MasterTaxViewModel
    {
        [Key]
        public long MasterTaxId { get; set; }

        [Required(ErrorMessage = "Enter Tax Title!")]
        [StringLength(30, MinimumLength = 3)]
        public string TaxTitle { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> IsTaxPercentageAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> TaxValue { get; set; }
        public Nullable<System.DateTime> TaxStartDate { get; set; }
        public Nullable<System.DateTime> TaxEndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
