using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PMTool16Bit.Roles.Dto;

namespace PMTool16Bit.Services
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, UserFilter, UserCreateDto, UserDto>
    {
        //Task<ListResultDto<RoleDto>> GetRoles();
        //Task Logout(string token);
        //Task<UserIdentifier[]> GetUserIdentifierByRoles(params string[] roles);
        //List<BaseDropdownDto> GetUserNameByIds(string ids);
        //Task<UserDto> GetProfile(long userId);     
    }
}
