using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class GroupTaskFilter : PagedAndSortedResultRequestDto
    {
        public int? ProjectId { get; set; }
    
        public string GroupTaskName { get; set; }
    }
}