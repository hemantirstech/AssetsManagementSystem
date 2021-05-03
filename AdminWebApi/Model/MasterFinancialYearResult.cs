using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    public class MasterFinancialYearResult
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
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
