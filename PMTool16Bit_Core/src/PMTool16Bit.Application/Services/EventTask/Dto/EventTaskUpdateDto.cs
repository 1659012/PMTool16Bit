using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class EventTaskUpdateDto : BaseUpdateDto
    {
        [StringLength(EntityLength.TaskName)]
        public string TaskName { get; set; }

        [StringLength(EntityLength.Description)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsMarked { get; set; }
        public bool IsCompleted { get; set; }

        [StringLength(EntityLength.IdLimit)]
        public string FileIds { get; set; }

        [Range(0, 3)]
        public int PriorityLevel { get; set; }

        public int TaskGroupId { get; set; }

        #region Reference

        public virtual List<EventTaskMemberDto> EventTaskMembers { get; set; }
        public virtual ICollection<CommentDto> Comments { get; set; }
        public virtual ICollection<TodoDto> Todos { get; set; }

        #endregion Reference
    }
}