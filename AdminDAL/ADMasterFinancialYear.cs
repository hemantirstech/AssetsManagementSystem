using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterFinancialYear", Schema = "dbo")]
    public class ADMasterFinancialYear
    {
        [Key]
        public long MasterFinancialYearId { get; set; }
        public string FinancialYearDescription { get; set; }
        public Nullable<System.DateTime> YearStartDate { get; set; }
        public Nullable<System.DateTime> YearEndDate { get; set; }
        public string CashBook { get; set; }
        public Nullable<bool> YearLocked { get; set; }
        public Nullable<bool> CurrentYear { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
