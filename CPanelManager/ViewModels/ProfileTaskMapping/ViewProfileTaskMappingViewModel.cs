﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CPanelManager.ViewModels.ProfileTaskMapping
{    
    public class ViewProfileTaskMappingViewModel
    {
        public ViewProfileTaskMappingViewModel()
        {
            DeleteColor = "red";
            DeleteIcon = "glyphicon glyphicon-remove";

            InsertColor = "red";
            InsertIcon = "glyphicon glyphicon-remove";

            UpdateColor = "red";
            UpdateIcon = "glyphicon glyphicon-remove";

            SelectColor = "red";
            SelectIcon = "glyphicon glyphicon-remove";

            ActiveColor = "red";
            ActiveIcon = "glyphicon glyphicon-remove";

        }

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


        public bool IsDelete { get; set; }
        public string DeleteColor { get; set; }
        public string DeleteIcon { get; set; }

        public bool IsInsert { get; set; }
        public string InsertColor { get; set; }
        public string InsertIcon { get; set; }

        public bool IsUpdate { get; set; }
        public string UpdateColor { get; set; }
        public string UpdateIcon { get; set; }

        public bool IsSelect { get; set; }
        public string SelectColor { get; set; }
        public string SelectIcon { get; set; }

        public bool IsActive { get; set; }
        public string ActiveColor { get; set; }
        public string ActiveIcon { get; set; }

        public Nullable<long> EnterById { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<long> ModifiedById { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public CPanelManager.Models.CommonFunction.Mode Mode { get; set; }
    }
}
