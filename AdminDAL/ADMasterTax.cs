using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterTax", Schema = "dbo")]
    public class ADMasterTax
    {
        [Key]
        public long MasterTaxId { get; set; }
        public string TaxTitle { get; set; }

        public Nullable<bool> IsTaxPercentageAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public Nullable<decimal> TaxValue { get; set; }
        public Nullable<System.DateTime> TaxStartDate { get; set; }
        public Nullable<System.DateTime> TaxEndDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
