using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebApi.Model
{
    public class MasterIndustryTypeResult
    {
        [Key]
        public long MasterIndustryTypeId { get; set; }
        public Nullable<long> MasterIndustryGroupId { get; set; }
        public string IndustryTypeTitle { get; set; }
        public string IndustryTypeCode { get; set; }
        public string IndustryTypeDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }
        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
