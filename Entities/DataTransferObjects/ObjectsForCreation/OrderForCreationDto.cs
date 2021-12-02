using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class OrderForCreationDto
    {
        public Guid Id { get; set; }

        public Guid SenderId { get; set; }

        public Guid DestinationId { get; set; }

        public Guid UserId { get; set; }
    }
}
