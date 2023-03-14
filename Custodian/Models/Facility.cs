using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodian.Models
{
    public class Facility
    {
            public string FacilityId { get; set; }
            public string GeoJson { get; set; }
            public string LocaleKey { get; set; }
            public string FinanceNumber { get; set; }
            public string Validated { get; set; }
            public string FacilityType { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Plus4 { get; set; }
            public string Tiemzone { get; set; }
            public string Region { get; set; }
            public string Division { get; set; }
            public string DistrictName { get; set; }
            public string LocationType { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string LocaleName { get; set; }
       
    }
}
