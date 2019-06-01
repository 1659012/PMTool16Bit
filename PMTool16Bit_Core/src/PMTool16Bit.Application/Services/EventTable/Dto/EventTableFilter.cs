using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class EventTableFilter : PagedAndSortedResultRequestDto
    {
        public int? ProjectId { get; set; }
    
        public string TableName { get; set; }
    }
}