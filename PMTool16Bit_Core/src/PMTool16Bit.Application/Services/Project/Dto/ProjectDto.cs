using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectDto : BaseAuditedDto
    {
        public int ProjectOwnerId { get; set; }

        //[StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }


        #region Reference  
        [ForeignKey(nameof(ProjectOwnerId))]
        public virtual UserDto ProjectOwner { get; set; }
        public virtual ICollection<GroupTask> EventTables { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
