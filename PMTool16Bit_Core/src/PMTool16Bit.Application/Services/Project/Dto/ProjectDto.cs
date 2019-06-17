using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;
using System.ComponentModel.DataAnnotations.Schema;
using PMTool16Bit.Users;

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
        public virtual UserDto ProjectOwner { get; set; }
        public virtual ICollection<TaskGroupDto> TaskGroups { get; set; }
        public ICollection<ProjectMemberDto> ProjectMembers { get; set; }
        #endregion
    }
}
