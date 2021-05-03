using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels
{
    [Serializable()]
    public class GlobalMessageViewModel
    {
        public GlobalMessageViewModel()
        {
            //0: None, 1:Big, 2:Small, 3:x-Small
            GlobalMessageType = 0;
        }

        public int GlobalMessageMode { get; set; }
        public int GlobalMessageType { get; set; }
        public string GlobalMessageTitle { get; set; }
        public string GlobalMessageText { get; set; }
        public string GlobalMessageTextI { get; set; }
        public string GlobalMessageDate { get; set; }
        public string GlobalMessageTime { get; set; }
        public int GlobalMessageColor { get; set; }
        public int GlobalMessageIcon { get; set; }


        public int ErrorMessageMode { get; set; }
        public string ErrorMessageTitle { get; set; }
        public string ErrorMessageText { get; set; }
        public string ErrorMessageTextI { get; set; }
        public string ErrorMessageDate { get; set; }
        public string ErrorMessageTime { get; set; }
        public int ErrorMessageColor { get; set; }
        public int ErrorMessageIcon { get; set; }
    }
}
