using Entities.Models;
using Entities.Models.OwnedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public Person ContactPerson { get; set; }

        public LatLng Coordinates { get; set; }
    }
}
