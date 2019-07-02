using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class CommentUpdateDto : BaseAuditedDto
    {
        public int EventTaskId { get; set; }

        [StringLength(EntityLength.CommentContent)]
        public string Content { get; set; }

        [StringLength(EntityLength.IdLimit)]
        public string FileIds { get; set; }
    }
}