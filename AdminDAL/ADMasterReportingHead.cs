using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL
{
    [Table("ADMasterReportingHead", Schema = "dbo")]
    public class ADMasterReportingHead
    {
        [Key]
        public long MasterReportingHeadId { get; set; }
        public string ReportingHeadTitle { get; set; }
        public string ReportingDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
