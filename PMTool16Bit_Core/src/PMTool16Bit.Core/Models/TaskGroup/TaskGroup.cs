﻿
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class TaskGroup : BaseAuditEntity
    {
        public int ProjectId { get; set; }

        [StringLength(EnumLength.GroupTaskName)]
        public string TaskGroupName { get; set; }

        #region Reference

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        public virtual ICollection<EventTask> EventTasks { get; set; }

        #endregion
    }
}