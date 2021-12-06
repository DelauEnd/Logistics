using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForCreationDto
    {
        public Guid Id { get; set; }

        public UserRole Role { get; set; }

        public Person ContactPerson { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
