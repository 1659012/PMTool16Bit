using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Roles.Dto;
using PMTool16Bit.Services;
using PMTool16Bit.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;


namespace PMTool16Bit.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users_View)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, UserFilter, UserCreateDto, UserDto>, IUserAppService
    {
        private readonly UserManager userManager;
        private readonly RoleManager roleManager;
        private readonly IRepository<Role> roleRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IEmailSender emailSender,
            //IPictureService pictureService,
            //INotificationService notificationService,
            IPasswordHasher<User> passwordHasher)
            : base(repository)
        {
            //_emailSender = emailSender;
            //_userManager = userManager;
            //_pictureService = pictureService;
            //_roleManager = roleManager;
            //_roleRepository = roleRepository;
            //_passwordHasher = passwordHasher;
            //_tokenBlacklistRepository = tokenBlacklistRepository;
            //this.notificationService = notificationService;
            //GetAllPermissionName = PermissionNames.UserViewPermission;
            //CreatePermissionName = PermissionNames.UserCreatePermission;
            //UpdatePermissionName = PermissionNames.UserUpdatePermission;
            //DeletePermissionName = PermissionNames.UserDeletePermission;
        }

        
    }
}
