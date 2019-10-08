using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace PMTool16Bit.Authorization
{
    public class PMTool16BitAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            #region Project
            //var projectPermission = context.CreatePermission(PermissionNames.ProjectPermission, L("Project"));
            //projectPermission.CreateChildPermission(PermissionNames.ProjectDeletePermission, L("Delete project"));
            var projectDeletePermission = context.CreatePermission(PermissionNames.ProjectDeletePermission, L("Delete project"));
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PMTool16BitConsts.LocalizationSourceName);
        }
    }
}