using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;
using System.ComponentModel.DataAnnotations;
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Comment))]
    public class CommentDto : BaseAuditedDto
    {
        public int EventTaskId { get; set; }

        [StringLength(EnumLength.CommentContent)]
        public string Content { get; set; }
        public string FileIds { get; set; }

        //#region Reference
        //[ForeignKey(nameof(EventTaskId))]
        //public virtual EventTask EventTask { get; set; }
        //#endregion
    }
}
