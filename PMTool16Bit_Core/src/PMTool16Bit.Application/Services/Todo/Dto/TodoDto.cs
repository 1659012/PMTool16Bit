using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(Todo))]
    public class TodoDto : BaseDto
    {
        public int EventTaskId { get; set; }

        [StringLength(EntityLength.TaskName)]
        public string TodoName { get; set; }

        public bool IsCompleted { get; set; }
    }
}