using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{
    //[NotMapped]
    public partial class SPNextMasterIdResult
    {
        [Key]
        public long MasterId { get; set; }
    }
}
