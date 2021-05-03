using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPanelManager.ViewModels.MessageNotification
{
    public class IndexMessageNotificationViewModel : PaginationViewModel
    {
        public List<MessageNotificationViewModel> MessageNotificationList { get; set; }
    }
}
