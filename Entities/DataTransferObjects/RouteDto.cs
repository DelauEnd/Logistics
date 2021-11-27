using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class RouteDto
    {
        public Guid Id { get; set; }

        public string TruckRegistrationNumber { get; set; }

        public string TrailerRegistrationNumber { get; set; }
    }
}
