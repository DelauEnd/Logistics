using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class CargoTypeConfiguration : DefaultGuids, IEntityTypeConfiguration<CargoType>
    {
        public void Configure(EntityTypeBuilder<CargoType> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<CargoType> builder)
        {
            builder.HasData
            (
                new CargoType
                {
                    Id = TypeGuid,
                    Title = "Initial Type"
                }
            );
        }
    }
}
