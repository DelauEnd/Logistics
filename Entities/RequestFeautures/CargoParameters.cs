using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeautures
{
    public class CargoParameters : RequestParameters
    {
        public DateTime ArrivalDateFrom { get; set; }
        public DateTime ArrivalDateTo { get; set; }

        public DateTime DepartureDateFrom { get; set; }
        public DateTime DepartureDateTo { get; set; }
    }
}
