using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models
{
    public class MergeRecord
    {
        public string ver { get; set; }
        public string seq { get; set; }
        public string facilityid { get; set; }
        public string route { get; set; }
        public string startDate { get; set; }
        public string startTime { get; set; }
        public object endDate { get; set; }
        public object endTime { get; set; }
        public string startLatitude { get; set; }
        public string startLongitude { get; set; }
        public object endLatitude { get; set; }
        public object endLongitude { get; set; }
        public string employee { get; set; }
        public string startBarcode { get; set; }
        public object endBarcode { get; set; }
        public string estimatedTime { get; set; }
        public string actualTime { get; set; }
        public string status { get; set; }
        public List<string> tasksComplete { get; set; }
        public List<string> tasksIncomplete { get; set; }
        public List<string> pics { get; set; }

    }
}
