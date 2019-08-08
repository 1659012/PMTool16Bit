using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(EventTask))]
    public class EventTaskOrderDto : BaseDto
    {
        public int TaskOrder { get; set; }
    }
}