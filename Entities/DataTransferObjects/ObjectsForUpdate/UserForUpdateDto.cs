using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForUpdateDto
    {
        public Guid Id { get; set; }

        public UserRole Role { get; set; }

        public Person ContactPerson { get; set; }
    }
}
