using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Role { get; set; }

        public Person ContactPerson { get; set; }
    }
}
