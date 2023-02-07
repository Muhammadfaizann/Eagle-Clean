using Custodian.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Helpers
{
    public class Utils
    {
        public static Assignment activeAssigment { get; set; }
        public static ObservableCollection<Assignment> ongoingAssigments = new ObservableCollection<Assignment>
        {
            new Assignment { Title = "Route 005", IsStarted = false },
            new Assignment { Title = "Route 006", IsStarted = false },
            new Assignment { Title = "Route 007", IsStarted = false },
            new Assignment { Title = "Route 008", IsStarted = true },
            new Assignment { Title = "Route 009", IsStarted = false },
        };
        public static ObservableCollection<CompletedAssignment> completedAssigments = new ObservableCollection<CompletedAssignment>
        {
            new CompletedAssignment { Title = "Route 001", IsOverTime = true },
            new CompletedAssignment { Title = "Route 002", IsOverTime = false },
            new CompletedAssignment { Title = "Route 003", IsOverTime = false },
            new CompletedAssignment { Title = "Route 004", IsOverTime = true },
        };
    }
}
