using System;
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

        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Due date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Assigned members")]
        public string AssignedMembers { get; set; }       

        [DisplayName("Completed")]
        public bool IsCompleted { get; set; }

        [DisplayName("Priority level")]
        public string PriorityLevel { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}