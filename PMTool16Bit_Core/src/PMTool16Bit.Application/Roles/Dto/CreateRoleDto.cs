using Abp.Authorization.Roles;
using Abp.AutoMapper;
using PMTool16Bit.Authorization.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Roles.Dto
{
    [AutoMapTo(typeof(Role))]
    public class CreateRoleDto
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }

        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public List<string> Permissions { get; set; }
    }
}