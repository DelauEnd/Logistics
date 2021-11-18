using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models.OwnedModels
{
    [Owned]
    public class AccountInfo
    {
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}
