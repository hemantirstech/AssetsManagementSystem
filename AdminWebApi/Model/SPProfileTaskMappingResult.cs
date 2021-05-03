using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    //[NotMapped]
    public class SPProfileTaskMappingResult
    {        
        public Nullable<long> MasterProfileTaskMappingId { get; set; }


        public Nullable<long> MasterProfileId { get; set; }
        public string ProfileTitle { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileDescription { get; set; }


        [Key]
        public Nullable<long> MasterFunctionId { get; set; }
        public string FunctionTitle { get; set; }
        public string FunctionLink { get; set; }
        public string FunctionIcon { get; set; }
        public string FunctionIconColour { get; set; }
        public Nullable<long> ParentMasterFunctionId { get; set; }
        public string ParentFunctionTitle { get; set; }
        public Nullable<long> Sequence { get; set; }


        public Nullable<bool> IsDelete { get; set; }


        public Nullable<bool> IsInsert { get; set; }


        public Nullable<bool> IsUpdate { get; set; }

        public Nullable<bool> IsSelect { get; set; }
        

        public Nullable<bool> IsActive { get; set; }
        

        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        
    }
}
