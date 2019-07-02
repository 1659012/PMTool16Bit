using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class FileFilter : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }
    }
}