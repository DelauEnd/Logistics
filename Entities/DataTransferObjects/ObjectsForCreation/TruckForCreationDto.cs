using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class TruckForCreationDto
    {
        public Guid Id { get; set; }

        public string RegistrationNumber { get; set; }

        public double? LoadCapacity { get; set; }

        public Person Driver { get; set; }

        public Dimensions LimitLoad { get; set; }

        public Guid? TransportedCargoTypeId { get; set; }
    }
}
