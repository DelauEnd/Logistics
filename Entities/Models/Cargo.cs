using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Cargo : IEntity
    {
        [Key]
        [Column("CargoId")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public EStatuses Status { get; set; } = EStatuses.PROCESSING;

        public DateTime? DepartureDate { get; set; }

        public DateTime? ArrivalDate { get; set; }

        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [Required]
        public Dimensions Dimensions { get; set; }

        [Required]
        [ForeignKey(nameof(CargoCategory))]
        public int CategoryId { get; set; }
        public CargoCategory Category { get; set; }

        [Required]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(Route))]
        public int? RouteId { get; set; }
        public Route Route { get; set; }
    }
}
