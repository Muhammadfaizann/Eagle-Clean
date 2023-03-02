using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models
{
    public class Config
    {
       public bool IsStartRouteButtonVisible { get; set; }
       public int Radius { get; set; }
       public string APIBaseURL { get; set; }
    }
}
