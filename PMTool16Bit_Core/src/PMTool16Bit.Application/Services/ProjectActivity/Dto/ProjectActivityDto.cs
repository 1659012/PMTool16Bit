using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using PMTool16Bit.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(ProjectActivity))]
    public class ProjectActivityDto: BaseDto
    {
        public int ProjectId { get; set; }

        [StringLength(EntityLength.ShortDescription)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }
    }
}