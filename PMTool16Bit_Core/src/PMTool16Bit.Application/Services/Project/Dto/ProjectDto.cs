using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectDto : BaseAuditedDto
    {
        public int AdminId { get; set; }

        //[StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }


        #region Reference      
        public virtual ICollection<EventTable> EventTables { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        #endregion
    }
}
