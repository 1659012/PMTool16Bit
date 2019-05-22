using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PMTool16Bit.Roles.Dto;
using PMTool16Bit.Users.Dto;

namespace PMTool16Bit.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
