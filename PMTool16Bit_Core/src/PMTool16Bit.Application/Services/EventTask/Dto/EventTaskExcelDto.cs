using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PMTool16Bit.Services
{   
    public class EventTaskExcelDto
    {
        [DisplayName("Project name")]
        public string ProjectName { get; set; }        

        [DisplayName("Task Group name")]
        public string TaskGroupName { get; set; }

        [DisplayName("Task name")]
        public string TaskName { get; set; }

        [DisplayName("DueDate")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Member names")]
        public string MemberNames { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }       

        [DisplayName("Marked")]
        public bool IsMarked { get; set; }

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }       

        [DisplayName("Priority level")]
        public string PriorityLevel { get; set; }
    }
}
