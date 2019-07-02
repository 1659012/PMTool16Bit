using AutoMapper;
using PMTool16Bit.Authorization.Users;

namespace PMTool16Bit.Users
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.ProjectMembers, opt => opt.Ignore())
                .ForMember(x => x.EventTaskMembers, opt => opt.Ignore())
                .ForMember(x => x.LastModifierUserId, opt => opt.Ignore())
                .ForMember(x => x.LastModifierUser, opt => opt.Ignore())
                ;

            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}