using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels
{
    public class DashboardAdminViewModel
    {
        public DateTime CurrentDateTime { get; set; }
        public string CompanyMasterName { get; set; }
        public string LastSuccessfullLogin { get; set; }
        public string LoginIP { get; set; }
        public string SessionId { get; set; }
    }
}
