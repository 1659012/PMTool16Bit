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
    [AutoMapFrom(typeof(EventTask))]
    public class EventTaskDto : BaseAuditedDto
    {
        //[StringLength(EnumLength.TaskName)]
        public string TaskName { get; set; }

        //[StringLength(EnumLength.Description)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsMarked { get; set; }

        public bool IsCompleted { get; set; }
       
        public string FileIds { get; set; }

        public int PriorityLevel { get; set; }

        public int TaskGroupId { get; set; }

        #region Reference
       
        public virtual TaskGroupDto GroupTask { get; set; }
        public virtual ICollection<EventTaskMemberDto> EventTaskMembers { get; set; }
        public virtual ICollection<CommentDto> Comments { get; set; }


        #endregion
    }
}
