using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Order : IEntity
    {
        [Key]
        [Column("OrderId")]
        public int Id { get; set; }

        [Required]
        public EStatuses Status { get; set; } = EStatuses.PROCESSING;

        [Required]
        public int SenderId { get; set; }
        public Customer Sender { get; set; }

        [Required]
        public int DestinationId { get; set; }
        public Customer Destination { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Cargo> Cargoes { get; set; }
    }
}
