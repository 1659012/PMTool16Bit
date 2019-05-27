using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class EventTableUpdateDto : BaseUpdateDto
    {
        public int ProjectId { get; set; }

        [StringLength(EnumLength.TableName)]
        public string TableName { get; set; }
    }
}