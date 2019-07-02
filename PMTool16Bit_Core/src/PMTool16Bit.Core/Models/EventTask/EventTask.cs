using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class EventTask : BaseAuditEntity
    {
        [StringLength(EntityLength.TaskName)]
        public string TaskName { get; set; }

        [StringLength(EntityLength.Description)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsMarked { get; set; }

        [StringLength(EntityLength.IdLimit)]
        public string FileIds { get; set; }

        public bool IsCompleted { get; set; }

        [Range(0, 3)]
        public int PriorityLevel { get; set; }

        public int TaskGroupId { get; set; }

        public int? TaskDependencyId { get; set; }

        #region Reference

        [ForeignKey(nameof(TaskGroupId))]
        public virtual TaskGroup TaskGroup { get; set; }

        public virtual ICollection<EventTaskMember> EventTaskMembers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Todo> Todos { get; set; }

        #endregion Reference
    }
}