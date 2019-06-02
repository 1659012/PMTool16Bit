using AutoMapper;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Users.Dto;

namespace PMTool16Bit.Services
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                //.ForMember(x => x.LastLoginTime, opt => opt.Ignore())
                ;

            CreateMap<UserCreateDto, User>();
            CreateMap<UserCreateDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
