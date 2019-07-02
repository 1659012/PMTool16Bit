using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using PMTool16Bit.Users;
using System.ComponentModel.DataAnnotations;

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