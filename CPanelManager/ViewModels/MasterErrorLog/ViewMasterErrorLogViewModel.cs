using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterErrorLog
{
    [Serializable()]
    public class ViewMasterErrorLogViewModel
    {
        [Key]
        public long MasterErrorLogId { get; set; }
        public long MasterId { get; set; }
        public string SPName { get; set; }
        public string TableName { get; set; }
        public int ModeVersion { get; set; }
        public string ErrorMessage { get; set; }
        public int LineNumber { get; set; }
        public int StepComplete { get; set; }
        public DateTime EnterDate { get; set; }
        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
