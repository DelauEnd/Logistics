using Entities.Models;
using System;
namespace Entities.DataTransferObjects
{
    public class CargoDto
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public DateTime? DepartureDate { get; set; }

        public DateTime? ArrivalDate { get; set; }

        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }
    }
}
