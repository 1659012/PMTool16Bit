using Abp.AutoMapper;
using PMTool16Bit.Models;
using System;
using System.Collections.Generic;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(EventTask))]
    public class EventTaskSimpleDto : BaseDto
    {       
        public string TaskName { get; set; }
       
        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }
        
    }
}