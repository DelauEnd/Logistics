using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Trailer : IEntity
    {
        [Key]
        [Column("TrailerId")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegistrationNumber { get; set; }

        [Required]
        public Guid TransportedCargoTypeId { get; set; }
        public CargoType TransportedCargoType { get; set; }

        [Required]
        public Dimensions LimitLoad { get; set; }

        public List<Route> Routes { get; set; }
    }
}
