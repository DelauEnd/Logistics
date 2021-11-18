using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Transport : IEntity
    {
        [Key]
        [Column("TransportId")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegistrationNumber { get; set; }

        [Range(0, double.MaxValue)]
        public double? LoadCapacity { get; set; }

        [Required]
        public Dimensions LimitLoad { get; set; }

        [Required]
        public Person Driver { get; set; }

        public List<Route> Route { get; set; }
    }
}
