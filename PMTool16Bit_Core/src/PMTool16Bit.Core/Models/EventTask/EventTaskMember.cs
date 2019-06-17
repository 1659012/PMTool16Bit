
using PMTool16Bit.Authorization.Users;
using PMTool16Bit.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMTool16Bit.Models
{
    public  class EventTaskMember: BaseEntity
    {
        public int EventTaskId { get; set; }

        [ForeignKey(nameof(EventTaskId))]
        public virtual EventTask EventTask { get; set; }

        public long MemberId { get; set; }

        //public bool IsMarked { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual User Member { get; set; }

    }
}
