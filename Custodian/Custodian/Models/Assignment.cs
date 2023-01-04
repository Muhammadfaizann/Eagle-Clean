using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models
{
    public class Assignment
    {
        public string Title { get; set; }
        public bool IsStarted { get; set; }
    }
    public class CompletedAssignment
    {
        public string Title { get; set; }
        public bool IsOverTime { get; set; }
    }
}
