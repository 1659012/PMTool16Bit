using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class Todo : BaseEntity
    {
        public int EventTaskId { get; set; }

        [StringLength(EntityLength.TaskName)]
        public string TodoName { get; set; }

        public bool IsCompleted { get; set; }

        #region Reference

        [ForeignKey(nameof(EventTaskId))]
        public virtual EventTask EventTask { get; set; }

        #endregion Reference
    }
}