﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class CargoCategory : IEntity
    {
        [Key]
        [Column("CategoryId")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public List<Cargo> Cargoes { get; set; }
    }
}
