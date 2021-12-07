using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class TruckConfiguration : DefaultGuids, IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            ConfigureModel(builder);
            if (Configuration.ShouldAddInitialData())
                AddInitialData(builder);
        }

        private void ConfigureModel(EntityTypeBuilder<Truck> builder)
        {
            builder.HasIndex(truck
                => truck.RegistrationNumber).IsUnique(true);
        }

        private void AddInitialData(EntityTypeBuilder<Truck> builder)
        {
            builder.HasData
            (
                new Truck
                {
                    Id = TruckGuid,
                    LoadCapacity = 1000,
                    RegistrationNumber = "A000AA"
                }
            );

            builder.OwnsOne(Truck => Truck.Driver).HasData
            (
                new
                {
                    TruckId = TruckGuid,
                    Name = "Sasha",
                    Surname = "Trikorochki",
                    Patronymic = "Vitaljevich",
                    PhoneNumber = "19(4235)386-91-39"
                }
            );

            builder.OwnsOne(Truck => Truck.LimitLoad).HasData
            (
                new
                {
                    TruckId = TruckGuid,
                    Height = 50d,
                    Width = 50d,
                    Length = 50d
                }
            );
        }
    }
}
