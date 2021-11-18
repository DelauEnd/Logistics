using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CargoForCreationDto
    {
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public DateTime DepartureDate { get; set; } = DateTime.Now;

        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }
    }
}
