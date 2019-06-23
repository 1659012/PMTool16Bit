using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class Comment : BaseAuditEntity
    {
        public int EventTaskId { get; set; }

        [StringLength(EntityLength.CommentContent)]
        public string Content { get; set; }

        [StringLength(EntityLength.IdLimit)]
        public string FileIds { get; set; }
        #region Reference
        [ForeignKey(nameof(EventTaskId))]
        public virtual EventTask EventTask { get; set; }
        #endregion
    }
}