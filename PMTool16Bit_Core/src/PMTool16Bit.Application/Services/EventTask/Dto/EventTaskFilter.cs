using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class EventTaskFilter : PagedAndSortedResultRequestDto
    {        
        public string TaskName { get; set; }       
        public string Description { get; set; }
        public int? GroupTaskId { get; set; }
    }
}