using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.ObjectsForUpdate
{
    public class TransportForUpdateDto
    {
        public string RegistrationNumber { get; set; }

        public double LoadCapacity { get; set; }

        public Person Driver { get; set; }
    }
}
