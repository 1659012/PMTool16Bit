
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Models
{
  public  class Project: BaseAuditEntity
    {
        public int AdminId { get; set; }

        [StringLength(EnumLength.ProjectName)]
        public string ProjectName { get; set; }


        #region Reference      
        public virtual ICollection<EventTable> EventTables { get; set; }
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; }

        #endregion
    }
}
