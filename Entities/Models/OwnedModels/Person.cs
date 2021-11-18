using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models
{
    [Owned]
    public class Person
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(30)]
        public string Patronymic { get; set; }

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
    }
}
