using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    
    public class MasterFunctionResult
    {      

        [Key]
        public long MasterFunctionId { get; set; }
        public string FunctionTitle { get; set; }
        public string FunctionLink { get; set; }
        public string FunctionIcon { get; set; }
        public string FunctionIconColour { get; set; }
        public Nullable<long> ParentMasterFunctionId { get; set; }
        public string ParentFunctionTitle { get; set; }
        public Nullable<long> Sequence { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }   
        
    }
}
