using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels
{
    [Serializable()]
    public class ErrorMessageViewModel
    {
        public Exception Exception { get; set; }

        public string Controler { get; set; }

        public string ActionResult { get; set; }
    }
}
