using Abp.AutoMapper;
using PMTool16Bit.Models;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(FileEntity))]
    public class FileEntityDto : BaseDto
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }
    }
}