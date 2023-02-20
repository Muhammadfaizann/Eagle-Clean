using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models.ServerModels
{
    public class RouteModel
    {
        public string FacilityId { get; set; } 
        public string RouteName { get; set; } 
        public string Json { get; set; } 
        public string Description { get; set; }

    }
    public class RouteDetailModel
    {
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public List<StepModel> Steps { get; set; }
    }

    public class StepModel 
    { 
        public string Description { get; set; } 
        public int TimeInMins { get; set; } 
    }
}
