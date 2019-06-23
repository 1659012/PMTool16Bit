using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;
using System.ComponentModel.DataAnnotations;
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Services{
    
    public class CommentUpdateDto : BaseAuditedDto
    {
        public int EventTaskId { get; set; }

        [StringLength(EntityLength.CommentContent)]
        public string Content { get; set; }

        [StringLength(EntityLength.IdLimit)]
        public string FileIds { get; set; }
    }
}
