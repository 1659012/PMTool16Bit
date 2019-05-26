
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class EventTable : BaseAuditEntity
    {
        public int ProjectId { get; set; }

        [StringLength(EnumLength.TableName)]
        public string TableName { get; set; }

        #region Reference

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
      
        #endregion
    }
}
