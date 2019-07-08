using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using PMTool16Bit.Roles.Dto;
using System;
using System.Threading.Tasks;

namespace PMTool16Bit.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        [AbpAllowAnonymous]
        Task<UserDto> GetUserProfile(long userId);

        [AbpAllowAnonymous]
        Task UpdateLastLoginTime(long id, DateTime? date);
        
        string GetCurrentUserName();

        long? GetCurrentUserId();
    }
}