using Entities.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
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
        public string Password
        {
            private get
            {
                return Password;
            }
            set
            {
                Password = value;
                PasswordHashString = AuthenticationUtility.CalculateStringHash(value);
            }
        }

        [Required]
        [MaxLength(256)]
        public string PasswordHashString {get; private set; }
    }
}
