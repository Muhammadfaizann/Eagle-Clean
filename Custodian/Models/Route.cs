using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models
{
    public class Route : ObservableObject
    {
        public string type { get; set; }
        public string fid { get; set; }
        public string fn { get; set; }
        public string fac { get; set; }
        public string rte { get; set; }
        public string action { get; set; }
        public string sched { get; set; }
        public string desc { get; set; }
        public List<string> tasks { get; set; }
        public List<Task> taskList { get; set; }
        public string plannedTime { get; set; }
    }
    public class CompletedRoute
    {
        public string Title { get; set; }
        public bool IsOverTime { get; set; }
    }
    public class Task
    {
        public string Description { get; set; }
        public string PlannedTimeInMint { get; set; }
        public string PlannedTimeInSec { get; set; }
        public string ActualTimeInSec { get; set; }
        public bool IsCompleted { get; set; }
    }
}
