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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("CargoId")]
        public Guid Id { get; set; }

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
        public Guid CategoryId { get; set; }
        public CargoCategory Category { get; set; }

        [Required]
        [ForeignKey(nameof(CargoType))]
        public Guid TypeId { get; set; }
        public CargoType Type { get; set; }

        [Required]
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey(nameof(Route))]
        public Guid? RouteId { get; set; }
        public Route Route { get; set; }
    }
}
