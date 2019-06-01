using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class GroupTaskUpdateDto : BaseUpdateDto
    {
        public int ProjectId { get; set; }

        [StringLength(EnumLength.GroupTaskName)]
        public string GroupTaskName { get; set; }
    }
}