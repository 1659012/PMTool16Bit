using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectCreateDto : BaseUpdateDto
    {
        public long ProjectOwnerId { get; set; }

        [StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }


        #region Reference

        //public List<ProjectMemberDto> ProjectMembers { get; set; }
        #endregion
    }
}
