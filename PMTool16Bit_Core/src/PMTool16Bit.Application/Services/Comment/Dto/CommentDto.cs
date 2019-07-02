using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Comment))]
    public class CommentDto : BaseAuditedDto
    {
        public int EventTaskId { get; set; }

        [StringLength(EntityLength.CommentContent)]
        public string Content { get; set; }

        public string FileIds { get; set; }

        //#region Reference
        //[ForeignKey(nameof(EventTaskId))]
        //public virtual EventTask EventTask { get; set; }
        //#endregion
    }
}