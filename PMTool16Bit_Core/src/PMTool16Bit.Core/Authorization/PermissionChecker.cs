using Abp.Authorization;
using PMTool16Bit.Authorization.Roles;
using PMTool16Bit.Authorization.Users;

namespace PMTool16Bit.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}