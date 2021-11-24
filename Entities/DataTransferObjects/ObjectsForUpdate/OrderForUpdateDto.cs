using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.ObjectsForUpdate
{
    public class OrderForUpdateDto
    {
        public Guid SenderId { get; set; }

        public Guid DestinationId { get; set; }

        public string Status { get; set; }
    }
}
