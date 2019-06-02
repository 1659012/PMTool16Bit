using Abp.Application.Services.Dto;
using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMTool16Bit.Services
{
    public class ProjectFilter : PagedAndSortedResultRequestDto
    {
        public long? ProjectOwnerId { get; set; }

        //[StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }
    }
}
