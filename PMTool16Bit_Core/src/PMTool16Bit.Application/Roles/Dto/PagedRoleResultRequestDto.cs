using Abp.Application.Services.Dto;

namespace PMTool16Bit.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

