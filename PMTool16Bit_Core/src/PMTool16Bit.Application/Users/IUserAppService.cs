using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PMTool16Bit.Roles.Dto;

namespace PMTool16Bit.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<UserDto> GetUserProfile(long userId);
    }

}
