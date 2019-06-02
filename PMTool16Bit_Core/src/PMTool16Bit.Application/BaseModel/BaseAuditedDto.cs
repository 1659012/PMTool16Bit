using System;
using Abp.Domain.Entities.Auditing;

namespace PMTool16Bit.Services
{
    public class BaseAuditedDto : BaseDto, IAudited
    {
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
