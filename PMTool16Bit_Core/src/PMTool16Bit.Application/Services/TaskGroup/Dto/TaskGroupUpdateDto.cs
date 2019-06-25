using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class TaskGroupUpdateDto : BaseUpdateDto
    {
        public int ProjectId { get; set; }

        [StringLength(EntityLength.GroupTaskName)]
        public string TaskGroupName { get; set; }

        [StringLength(EntityLength.ShortDescription)]
        public string ShortDescription { get; set; }
    }
}