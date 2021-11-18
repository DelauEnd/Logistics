using Entities.Enums;
using Entities.Models.OwnedModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class User : IEntity
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }

        [Required]
        public EUserRole Role { get; set; }

        [Required]
        public Person ContactPerson { get; set; }

        [Required]
        public AccountInfo AccountInfo { get; set; }

        public List<Order> Orders { get; set; }
        public List<Route> Routes { get; set; }
    }
}
