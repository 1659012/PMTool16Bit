using Abp.Application.Services.Dto;
using System;

namespace PMTool16Bit.Services
{
    public class EventTaskFilter : PagedAndSortedResultRequestDto
    {        
        public string TaskName { get; set; }       
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }        
        public bool? IsMarked { get; set; }
        public bool? IsCompleted { get; set; }
        public int? TaskGroupId { get; set; }
    }
}