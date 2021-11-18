using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects.ObjectsForUpdate
{
    public class OrderForUpdateDto
    {
        public int SenderId { get; set; }

        public int DestinationId { get; set; }

        public string Status { get; set; }
    }
}
