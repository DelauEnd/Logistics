using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Route : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("RouteId")]
        public Guid Id { get; set; }  

        [Required]
        [ForeignKey(nameof(Truck))]
        public Guid TruckId { get; set; }
        public Truck Truck { get; set; }

        [ForeignKey(nameof(Trailer))]
        public Guid? TrailerId { get; set; }
        public Trailer Trailer { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<Cargo> Cargoes { get; set; }

    }
}
