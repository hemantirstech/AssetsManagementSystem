using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDAL.Model
{
    //[NotMapped]
    public partial class SPDropDownFillResult
    {
        [Key]
        public long MasterId { get; set; }
        public string MasterName { get; set; }
    }
}
