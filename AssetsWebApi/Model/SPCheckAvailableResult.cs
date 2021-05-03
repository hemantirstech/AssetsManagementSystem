using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsWebApi.Model
{
    //[NotMapped]
    public partial class SPCheckAvailableResult
    {
        [Key]
        public string NameAvailable { get; set; }
    }
}
