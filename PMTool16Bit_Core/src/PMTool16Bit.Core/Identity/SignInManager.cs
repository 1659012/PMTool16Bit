using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Uow;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.MultiTenancy;

namespace PMTool16Bit.Identity
{
    public class SignInManager : AbpSignInManager<Tenant, Role, User>
    {
        public SignInManager(
            UserManager userManager,
            IHttpContextAccessor contextAccessor,
            UserClaimsPrincipalFactory claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IAuthenticationSchemeProvider schemes)
            : base(
                userManager,
                contextAccessor,
                claimsFactory,
                optionsAccessor,
                logger,
                unitOfWorkManager,
                settingManager,
                schemes)
        {
        }
    }
}