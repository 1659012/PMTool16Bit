using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Users
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; } = true;

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public DateTime CreationTime { get; set; }

        public string[] RoleNames { get; set; }

        public int? AvatarId { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsPublishProfile { get; set; }

        #region reference

        public virtual ICollection<ProjectMemberDto> ProjectMembers { get; set; }
        public virtual ICollection<EventTaskMemberDto> EventTaskMembers { get; set; }

        #endregion reference
    }
}