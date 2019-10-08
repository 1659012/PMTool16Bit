namespace PMTool16Bit.Users
{
    using Abp.Application.Services.Dto;

    public class UserFilter : PagedAndSortedResultRequestDto
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string EmailAdress { get; set; }

        public string RolesName { get; set; }
    }
}
