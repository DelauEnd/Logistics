using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.ObjectsForUpdate
{
    public class CustomerForUpdateDto
    {
        public string Address { get; set; }

        public Person ContactPerson { get; set; }
    }
}
