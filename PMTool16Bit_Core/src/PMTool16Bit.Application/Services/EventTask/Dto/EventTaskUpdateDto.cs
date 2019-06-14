using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class EventTaskUpdateDto : BaseUpdateDto
    {
        [StringLength(EnumLength.TaskName)]
        public string TaskName { get; set; }

        [StringLength(EnumLength.Description)]
        public string Description { get; set; }

        public int TaskGroupId { get; set; }

        #region Reference

        public virtual List<EventTaskMemberDto> EventTaskMembers { get; set; }

        #endregion
    }
}