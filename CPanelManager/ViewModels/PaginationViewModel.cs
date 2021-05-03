using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels
{
    public class PaginationViewModel
    {
        public PaginationViewModel()
        {
            SearchCondition = "";
            IsDelete = false;
            IsInsert = false;
            IsUpdate = false;
            IsSelect = false;
        }

        public int TotalCount { get; set; }
        public int LimitStart { get; set; }
        public int LimitEnd { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public string SearchCondition { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsInsert { get; set; }
        public Nullable<bool> IsUpdate { get; set; }
        public Nullable<bool> IsSelect { get; set; }
    }
    
}
