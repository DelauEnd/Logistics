using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserWithLoginDto
    {
        public Guid Id { get; set; }

        public string Role { get; set; }

        public string Login { get; set; }

        public Person ContactPerson { get; set; }
    }
}
