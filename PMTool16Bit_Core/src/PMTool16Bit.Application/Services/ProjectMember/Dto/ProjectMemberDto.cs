using Abp.AutoMapper;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using PMTool16Bit.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(ProjectMember))]
    public class ProjectMemberDto
    {
        public int ProjectId { get; set; }

        public long MemberId { get; set; }
       
        public virtual UserSimpleDto Member { get; set; }

        [StringLength(EntityLength.ProjectRole)]
        public string ProjectRole { get; set; }
    }
}
