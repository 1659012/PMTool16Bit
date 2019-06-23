using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;
using System.ComponentModel.DataAnnotations;
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(TaskGroup))]
    public class TaskGroupDto : BaseAuditedDto
    {
        public int ProjectId { get; set; }

        [StringLength(EntityLength.GroupTaskName)]
        public string TaskGroupName { get; set; }

        #region Reference

        //[ForeignKey(nameof(ProjectId))]
        //public virtual Project Project { get; set; }

        public virtual ICollection<EventTaskDto> EventTasks { get; set; }

        #endregion
    }
}
