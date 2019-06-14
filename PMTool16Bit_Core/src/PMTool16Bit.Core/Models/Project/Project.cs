
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
  public  class Project: BaseAuditEntity
    {
        public long ProjectOwnerId { get; set; }

        [StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }

        [StringLength(EnumLength.ShortDescription)]
        public string ShortDescription { get; set; }




        #region Reference  
        [ForeignKey(nameof(ProjectOwnerId))]
        public virtual User ProjectOwner { get; set; }
        public virtual ICollection<TaskGroup> TaskGroups { get; set; }
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        #endregion
    }
}
