using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMTool16Bit.Services
{
    public class ProjectUpdateDto : BaseUpdateDto
    {
        public long ProjectOwnerId { get; set; }

        [StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }


        #region Reference
        public virtual List<GroupTaskDto> GroupTasks { get; set; }

        public List<ProjectMemberDto> ProjectMembers { get; set; }
        #endregion
    }
}
