﻿using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Project))]
    public class ProjectCreateDto : BaseUpdateDto
    {
        public long ProjectOwnerId { get; set; }

        [StringLength(EntityLength.ProjectName)]
        public string ProjectName { get; set; }

        [StringLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }

        #region Reference

        public List<ProjectMemberDto> ProjectMembers { get; set; }

        #endregion Reference
    }
}