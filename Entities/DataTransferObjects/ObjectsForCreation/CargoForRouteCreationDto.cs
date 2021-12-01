using Entities.Enums;
using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CargoForRouteCreationDto
    {
        public Guid Id { get; set; } 

        public string Title { get; set; }

        public Guid CategoryId { get; set; }

        public Guid OrderId { get; set; }

        public Guid TypeId { get; set; }

        public EStatuses Status { get; set; }

        public DateTime? DepartureDate { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }
    }
}
