using PMTool16Bit.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMTool16Bit.Models
{
    public class FileEntity : BaseEntity
    {
        [StringLength(EntityLength.FileName)]
        public string Name { get; set; }
        
        [StringLength(EntityLength.FileType)]
        public string Type { get; set; }

        [Required]
        [StringLength(EntityLength.FilePath)]
        public string Path { get; set; }
    }
}
