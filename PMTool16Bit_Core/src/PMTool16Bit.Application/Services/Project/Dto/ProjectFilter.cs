using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class ProjectFilter : PagedAndSortedResultRequestDto
    {
        public int? Id { get; set; }
        public long? ProjectOwnerId { get; set; }
        public string ProjectName { get; set; }
        public string ShortDescription { get; set; }
        public int? MemberId { get; set; }
    }
}