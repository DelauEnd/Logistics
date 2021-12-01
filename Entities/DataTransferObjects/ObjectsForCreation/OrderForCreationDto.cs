using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class OrderForCreationDto
    {
        public Guid SenderId { get; set; }

        public Guid DestinationId { get; set; }

        public IEnumerable<CargoForRouteCreationDto> Cargoes { get; set; }
    }
}
