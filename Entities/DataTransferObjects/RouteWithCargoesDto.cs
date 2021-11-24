using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class RouteWithCargoesDto
    {
        public Guid Id { get; set; }

        public string TruckRegistrationNumber { get; set; }

        public List<CargoDto> Cargoes { get; set; }
    }
}
