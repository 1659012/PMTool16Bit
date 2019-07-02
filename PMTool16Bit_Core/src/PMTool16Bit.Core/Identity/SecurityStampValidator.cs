using Abp.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.MultiTenancy;

namespace PMTool16Bit.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options,
            SignInManager signInManager,
            ISystemClock systemClock)
            : base(
                  options,
                  signInManager,
                  systemClock)
        {
        }
    }
}