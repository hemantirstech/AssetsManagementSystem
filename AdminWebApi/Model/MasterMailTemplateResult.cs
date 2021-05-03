﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    public class MasterMailTemplateResult
    {
        [Key]
        public long MasterMailTemplateId { get; set; }
        public string MailTemplateTitle { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string SMTPServer { get; set; }
        public Nullable<long> SMTPPort { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
    }
}
