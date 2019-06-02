using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using PMTool16Bit.Services;
using System.ComponentModel.DataAnnotations;
using PMTool16Bit.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using PMTool16Bit.Authorization.Users;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(EventTaskMember))]
    public class EventTaskMemberDto
    {
        public int EventTaskId { get; set; }       
        public virtual EventTask EventTask { get; set; }
        public long MemberId { get; set; }        
        public virtual User Member { get; set; }

    }
}
