using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{
    //[NotMapped]
    public partial class SPDropDownFillResult
    {
        [Key]
        public long MasterId { get; set; }
        public string MasterName { get; set; }
    }
}
