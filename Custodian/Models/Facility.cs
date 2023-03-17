using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            private string facilitytype;
            public string FacilityType {
                get
                { 
                    return this.facilitytype;
                }
                set
                {
                    facilitytype = value;
                    if (value == "ADMIN")
                        IsAdmin = true;
                    else if (value == "NET_FACIL")
                        IsNF = true;
                    else
                        IsPO = true;
                }
            } 
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
            public string DistanceInMiles { get; set; }

            public bool IsAdmin { get; set; } = false;
            public bool IsNF { get; set; } = false;
            public bool IsPO { get; set; } = false;
       
    }
}
