using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class EventTask : BaseAuditEntity
    {
        [StringLength(EnumLength.TaskName)]
        public string TaskName { get; set; }

        [StringLength(EnumLength.Description)]
        public string Description { get; set; }

        public int GroupTaskId { get; set; }

        #region Reference

        [ForeignKey(nameof(GroupTaskId))]
        public virtual GroupTask GroupTask { get; set; }

        public virtual ICollection<EventTaskMember> EventTaskMembers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        #endregion
    }
}