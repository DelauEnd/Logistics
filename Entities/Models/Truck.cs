using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Truck : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("TruckId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegistrationNumber { get; set; }

        [Range(0, double.MaxValue)]
        public double? LoadCapacity { get; set; }

        public Guid? TransportedCargoTypeId { get; set; }
        public CargoType TransportedCargoType { get; set; }

        public Dimensions LimitLoad { get; set; }

        [Required]
        public Person Driver { get; set; }

        public List<Route> Routes { get; set; }
    }
}
