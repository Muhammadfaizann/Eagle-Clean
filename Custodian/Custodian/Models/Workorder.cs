using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models
{
    public class Workorder
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public bool IsStarted { get; set; }
    }
}
