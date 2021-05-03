using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsDAL
{
    [Table("ASMasterErrorLog", Schema = "dbo")]
    public partial class ASMasterErrorLog
    {        
        [Key]
        public long MasterErrorLogId { get; set; }
        public long MasterId { get; set; }
        public string SPName { get; set; }
        public string TableName { get; set; }
        public string Mode { get; set; }
        public int ModeVersion { get; set; }
        public string ErrorMessage { get; set; }
        public int LineNumber { get; set; }
        public int StepComplete { get; set; }
        public DateTime EnterDate { get; set; }
    }
}
