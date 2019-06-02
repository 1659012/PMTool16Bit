using Abp.Application.Services.Dto;

namespace PMTool16Bit.Services
{
    public class UserFilter : PagedAndSortedResultRequestDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAdress { get; set; }
        public string RolesName { get; set; }
    }
}