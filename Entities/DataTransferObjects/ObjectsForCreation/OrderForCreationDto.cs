using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class OrderForCreationDto
    {
        public int SenderId { get; set; }

        public int DestinationId { get; set; }

        public IEnumerable<CargoForCreationDto> Cargoes { get; set; }
    }
}
