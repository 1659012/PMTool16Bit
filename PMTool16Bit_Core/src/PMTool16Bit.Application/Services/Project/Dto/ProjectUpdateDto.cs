using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMTool16Bit.Services
{
    public class ProjectUpdateDto : BaseUpdateDto
    {
        public int AdminId { get; set; }

        [StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }
    }
}
