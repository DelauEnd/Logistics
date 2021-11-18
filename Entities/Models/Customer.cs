﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Customer : IEntity
    {
        [Key]
        [Column("CustomerId")]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public Person ContactPerson { get; set; }

        public List<Order> OrderSender { get; set; }
        public List<Order> OrderDestination { get; set; }
    }
}