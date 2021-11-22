using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.ObjectsForUpdate
{
    public class TruckForUpdateDto
    {
        public string RegistrationNumber { get; set; }

        public double LoadCapacity { get; set; }

        public Person Driver { get; set; }
    }
}
