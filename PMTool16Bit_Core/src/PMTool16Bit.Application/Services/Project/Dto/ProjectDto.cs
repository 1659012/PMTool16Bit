using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Users;
using System.Collections.Generic;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectDto : BaseAuditedDto
    {
        public long ProjectOwnerId { get; set; }

        //[StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }

        public string ShortDescription { get; set; }

        #region Reference

        //[ForeignKey(nameof(ProjectOwnerId))]
        public virtual UserSimpleDto ProjectOwner { get; set; }

        public virtual ICollection<TaskGroupDto> TaskGroups { get; set; }
        public ICollection<ProjectMemberDto> ProjectMembers { get; set; }

        #endregion Reference
    }
}