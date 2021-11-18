﻿using Entities.Models;
using System;
namespace Entities.DataTransferObjects
{
    public class CargoDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public DateTime DepartureDate { get; set; } = DateTime.Now;

        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        public double Weight { get; set; }

        public Dimensions Dimensions { get; set; }
    }
}
