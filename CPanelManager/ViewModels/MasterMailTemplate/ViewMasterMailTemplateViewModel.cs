using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.MasterMailTemplate
{
    [Serializable()]
    public class ViewMasterMailTemplateViewModel
    {
        [Key]
        public long MasterMailTemplateId { get; set; }

        [Required(ErrorMessage = "Enter Mail Template Title!")]
        [StringLength(30, MinimumLength = 3)]
        public string MailTemplateTitle { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string SMTPServer { get; set; }
        public Nullable<long> SMTPPort { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
