﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMTool16Bit.Authorization;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Roles.Dto;
using PMTool16Bit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace PMTool16Bit.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, UserFilter, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly FileService fileService;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IAbpSession session,
            FileService fileService)
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            this.fileService = fileService;
        }

        protected override IQueryable<User> CreateFilteredQuery(UserFilter input)
        {
            return Repository
                .GetAllIncluding(x => x.Roles)
                .WhereIf(input.UserName.IsNotNullOrEmpty(), p => p.UserName.Contains(input.UserName))
                .WhereIf(input.FullName.IsNotNullOrEmpty(), p => p.FullName.Contains(input.FullName))
                .WhereIf(input.EmailAdress.IsNotNullOrEmpty(), p => p.FullName.Contains(input.EmailAdress))
                //.WhereIf(input.RolesName.IsNotNullOrEmpty(),p=>p.Roles.Any(q=>q.U.ToString().Contains(input.RolesName)))
                ;
        }

        [AbpAllowAnonymous]
        public override async Task<UserDto> Create(CreateUserDto input)
        {
            //CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        //[AbpAllowAnonymous]
        public override async Task<UserDto> Update(UserDto input)
        {
            //CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);
            MapToEntity(input, user);
            var update = await Repository.UpdateAsync(user);
            CurrentUnitOfWork.SaveChanges();
            //MapToEntity(input, user);

            //CheckErrors(await _userManager.UpdateAsync(user));

            //if (input.RoleNames != null)
            //{
            //    CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            //}
            return await Get(input);
            //return await base.Update(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            //var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            //if (roles.Any())
            //{
            //    userDto.RoleNames = roles.ToArray();
            //}
            return userDto;
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = await Repository
                //.GetAllIncluding(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);

            //if (user == null)
            //{
            //    throw new EntityNotFoundException(typeof(User), id);
            //}

            return user;
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, UserFilter input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        //public async Task<bool> ChangePassword(ChangePasswordDto input)
        //{
        //    if (_abpSession.UserId == null)
        //    {
        //        throw new UserFriendlyException("Please log in before attemping to change password.");
        //    }
        //    long userId = _abpSession.UserId.Value;
        //    var user = await _userManager.GetUserByIdAsync(userId);
        //    var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
        //    if (loginAsync.Result != AbpLoginResultType.Success)
        //    {
        //        throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
        //    }
        //    if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
        //    {
        //        throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
        //    }
        //    user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
        //    CurrentUnitOfWork.SaveChanges();
        //    return true;
        //}

        public async Task<bool> ChangePassword(UpdatePasswordDto input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your current password did not correct. Please try again !");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        [AbpAllowAnonymous]
        public async Task<bool> ResetPassword(ResetPasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to reset password.");
            }
            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
            var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
            }
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }
            var roles = await _userManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("Only administrators may reset passwords.");
            }

            var user = await _userManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }

        #region Customize UserData

        [AbpAllowAnonymous]
        public async Task<UserDto> GetUserProfile(long id)
        {
            var user = await GetEntityByIdAsync(id);
            //var userDto = MapToEntityDto(user);
            //var permissionModelList = await _userManager.GetGrantedPermissionsAsync(user);
            //return userDto;
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                FullName = user.FullName,
                EmailAddress = user.EmailAddress,
                IsPublishProfile = user.IsPublishProfile,
                AvatarId = user.AvatarId,
                LastLoginTime = user.LastLoginTime
            };

            if (userDto.AvatarId != null)
            {
                var avatarId = userDto.AvatarId ?? default(int);
                userDto.AvatarUrl = fileService.GetFileUrl(avatarId).Result;
            }

            return userDto;
        }

        [AbpAllowAnonymous]
        public async Task UpdateLastLoginTime(long id, DateTime? date)
        {
            var user = await GetEntityByIdAsync(id);
            user.LastLoginTime = date;
            CurrentUnitOfWork.SaveChanges();
        }

        public async Task<List<UserDropdownDto>> GetDropdown()
        {
            return await Repository
                .GetAll()
                .OrderBy(p => p.Name)
                .Select(p => new UserDropdownDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    EmailAddress = p.EmailAddress
                })
                .ToListAsync();
        }

        public async Task<List<UserDropdownDto>> GetDropdownByKeyword(string keyword = null, int maxResultCount = 50)
        {
            if (keyword == null)
                return new List<UserDropdownDto>();

            keyword = keyword.ToLower();

            return await Repository
                .GetAll()
                .Where(p => p.FullName.ToLower().Contains(keyword)
                            || p.UserName.ToLower().Contains(keyword)
                            || p.EmailAddress.ToLower().Contains(keyword)
                      )
                .OrderBy(p => p.Name)
                .Select(p => new UserDropdownDto
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    EmailAddress = p.EmailAddress
                })
                .ToListAsync();
        }

        #endregion Customize UserData

        public long? GetCurrentUserId()
        {
            return _abpSession.UserId;
        }

        public string GetCurrentUserName()
        {
            var userId = _abpSession.UserId ?? default(long);
            if (userId == default(long))
                return "";
            var user = Repository.FirstOrDefault(userId);
            return user.Name;
        }

        //public List<int> GetRelatedProjectList()
        //{
        //    var userId = _abpSession.UserId ?? default(int);
        //    if (userId == default(int))
        //        return new List<int>();
        //    var list = Repository
        //        .GetAll()
        //        .Where(p => p.Id == userId)
        //        .Include(p => p.ProjectMembers)
        //        .Select(p => p.Id)
        //        .ToListAsync();
        //    return list;
        //}
    }
}