using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class TaskGroupUpdateDto : BaseUpdateDto
    {
        public int ProjectId { get; set; }

        [StringLength(EnumLength.GroupTaskName)]
        public string TaskGroupName { get; set; }
    }
}