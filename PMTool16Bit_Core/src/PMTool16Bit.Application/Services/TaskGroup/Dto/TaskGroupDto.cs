using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(TaskGroup))]
    public class TaskGroupDto : BaseAuditedDto
    {
        public int ProjectId { get; set; }

        [StringLength(EntityLength.GroupTaskName)]
        public string TaskGroupName { get; set; }

        [StringLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }

        #region Reference

        //[ForeignKey(nameof(ProjectId))]
        //public virtual Project Project { get; set; }

        public virtual ICollection<EventTaskDto> EventTasks { get; set; }

        #endregion Reference
    }
}