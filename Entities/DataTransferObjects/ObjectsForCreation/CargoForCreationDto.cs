using Entities.Enums;
using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CargoForCreationDto
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid TypeId { get; set; }

        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }

        public Guid OrderId { get; set; }
    }
}
