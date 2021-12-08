using GMap.NET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Utility.ExcelHandler.RouteSheetAdditions
{
    public class RouteUnit
    {
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public Purpose Purpose { get; set; }
        public Guid Number { get; set; }
        public string Driver { get; set; }
        public PointLatLng LatLng { get; set; }
    }
}
