using Abp.AutoMapper;
using PMTool16Bit.Models;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(EventTask))]
    public class EventTaskDropdownDto
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public int TaskGroupId { get; set; }

        public string TaskGroupName { get; set; }
    }
}