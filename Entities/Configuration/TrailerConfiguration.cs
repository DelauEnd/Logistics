using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class TrailerConfiguration : DefaultGuids, IEntityTypeConfiguration<Trailer>
    {
        public void Configure(EntityTypeBuilder<Trailer> builder)
        {
            ConfigureModel(builder);
            AddInitialData(builder);
        }

        private void ConfigureModel(EntityTypeBuilder<Trailer> builder)
        {
            builder.HasIndex(truck
                => truck.RegistrationNumber).IsUnique(true);
        }

        private void AddInitialData(EntityTypeBuilder<Trailer> builder)
        {
            builder.HasData
            (
                new Trailer
                {
                    Id = TrailerGuid,
                    RegistrationNumber = "A06AAA",
                    TransportedCargoTypeId = TypeGuid,              
                }
            );

            builder.OwnsOne(Trailer => Trailer.LimitLoad).HasData
            (
                new
                {
                    TrailerId = TrailerGuid,
                    Height = 50d,
                    Width = 50d,
                    Length = 50d
                }
            );
        }
    }
}
