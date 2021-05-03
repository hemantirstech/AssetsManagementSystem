using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.Account
{
    [Serializable()]
    public class ProfileMenuRightsViewModel
    {
        public ProfileMenuRightsViewModel()
        {
            Read = true;
            Insert = true;
            Update = true;
            Delete = true;
        }

        public List<ValidateAccountViewModel> ValidateAccountViewModelList { get; set; }
        public string SearchCondition { get; set; }
        public Nullable<bool> Read { get; set; }
        public Nullable<bool> Insert { get; set; }
        public Nullable<bool> Update { get; set; }
        public Nullable<bool> Delete { get; set; }

        public string PayCalPeriod { get; set; }
        public string PayPeriod { get; set; }

        public long MasterLoginId { get; set; }
        public long MasterFinancialId { get; set; }
        public long MasterCompanyId { get; set; }
        public string MasterCompanyName { get; set; }

        public string SessionId { get; set; }

        public string LoginIP { get; set; }

        public string LastSuccessfullLogin { get; set; }
    }
}
