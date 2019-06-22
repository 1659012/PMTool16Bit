using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using PMTool16Bit.Models;

namespace PMTool16Bit.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public override bool IsActive { get; set; } = true;
        public int? AvatarId { get; set; }
        public bool IsPublishProfile { get; set; } = true;

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }

        #region reference
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }
        public virtual ICollection<EventTaskMember> EventTaskMembers { get; set; }

        #endregion
    }
}
