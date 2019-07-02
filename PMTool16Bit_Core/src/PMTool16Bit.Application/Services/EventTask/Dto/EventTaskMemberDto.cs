using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Users;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(EventTaskMember))]
    public class EventTaskMemberDto
    {
        public int EventTaskId { get; set; }

        //public virtual EventTask EventTask { get; set; }
        public long MemberId { get; set; }

        public virtual UserSimpleDto Member { get; set; }
        //public bool IsMarked { get; set; }
    }
}