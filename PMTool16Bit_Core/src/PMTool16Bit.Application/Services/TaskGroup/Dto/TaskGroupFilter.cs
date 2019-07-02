using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class TaskGroupFilter : PagedAndSortedResultRequestDto
    {
        public int? ProjectId { get; set; }

        public string TaskGroupName { get; set; }

        public string ShortDescription { get; set; }
    }
}