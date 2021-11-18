using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Route : IEntity
    {
        [Key]
        [Column("RouteId")]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Transport))]
        public int TransportId { get; set; }
        public Transport Transport { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Cargo> Cargoes { get; set; }
    }
}
