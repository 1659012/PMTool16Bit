using Abp.Domain.Entities;
using Abp.Timing;
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public class ProjectActivity : BaseEntity
    {
        public int ProjectId { get; set; }

        [StringLength(EntityLength.ShortDescription)]     
        public string Description { get; set; }

        public DateTime CreationTime { get; set; } = Clock.Now;
        #region Reference
        //[ForeignKey(nameof(ProjectId))]
        //public virtual Project Project { get; set; }

        #endregion Reference
    }
}