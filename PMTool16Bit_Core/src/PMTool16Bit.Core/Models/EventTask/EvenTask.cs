using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class EvenTask : BaseAuditEntity
    {
        [StringLength(EnumLength.TaskName)]
        public string TaskName { get; set; }

        [StringLength(EnumLength.Description)]
        public string Description { get; set; }

        public int? EventTableId { get; set; }

        #region Reference

        [ForeignKey(nameof(EventTableId))]
        public virtual EventTable EventTable { get; set; }


        #endregion
    }
}