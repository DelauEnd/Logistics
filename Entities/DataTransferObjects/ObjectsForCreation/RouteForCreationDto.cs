using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class RouteForCreationDto
    {
        public Guid Id { get; set; }

        public Guid TruckId { get; set; }

        public Guid UserId { get; set; } 

        public Guid? TrailerId { get; set; }
    }
}
