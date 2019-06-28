using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PMTool16Bit.Services
{   
    public class EventTaskExcelDto
    {
        [DisplayName("Task name")]
        public string TaskName { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("DueDate")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Marked")]
        public bool IsMarked { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        //public string FileIds { get; set; }

        [DisplayName("Priority level")]
        public string PriorityLevel { get; set; }

        [DisplayName("Task Group name")]
        public string TaskGroupName { get; set; }

        [DisplayName("Project name")]
        public string ProjectName { get; set; }

        [DisplayName("Member names")]
        public string MemberNames { get; set; }

        //#region Reference

        //public virtual TaskGroupDto GroupTask { get; set; }
        //public virtual ICollection<EventTaskMemberDto> EventTaskMembers { get; set; }
        //public virtual ICollection<CommentDto> Comments { get; set; }


        //#endregion
    }
}
