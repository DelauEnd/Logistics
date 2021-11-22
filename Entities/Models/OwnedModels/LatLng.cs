using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Models.OwnedModels
{
    [Owned]
    public class LatLng
    {
        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }
    }
}
