using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PMTool16Bit.Services
{   
    public class EventTaskGanttDto
    {
        public string Id { get; set; }
        public string Name { get; set; }      
        public double? Start { get; set; }
        public double? End { get; set; }
        public string Assignee { get; set; }
        public string Dependency { get; set; }//Id
        public string Parent { get; set; }//Id
        public string Completed { get; set; }//0.00 to 1 (0% to 100%)
        public string PointWidth { get; set; }// if is parent (0 to 3)
    }
}
