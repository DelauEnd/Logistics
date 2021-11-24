using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Sender { get; set; }

        public string Destination { get; set; }
    }
}
