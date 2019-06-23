using Abp.AutoMapper;
using PMTool16Bit.Models;
using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMTool16Bit.Services
{
    [AutoMapFrom(typeof(FileEntity))]
    public class FileEntityDto : BaseDto
    {       
        public string Name { get; set; }        
       
        public string Type { get; set; }
        
        public string Path { get; set; }
    }
}
