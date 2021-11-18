﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            ConfigureModel(builder);
            AddInitialData(builder);
        }

        private void ConfigureModel(EntityTypeBuilder<Transport> builder)
        {
            builder.HasIndex(transport
                => transport.RegistrationNumber).IsUnique(true);
        }

        private void AddInitialData(EntityTypeBuilder<Transport> builder)
        {
            builder.HasData
            (
                new Transport
                {
                    Id = 1,
                    LoadCapacity = 1000,
                    RegistrationNumber = "A000AA",
                }
            );

            builder.OwnsOne(Transport => Transport.Driver).HasData
            (
                new
                {
                    TransportId = 1,
                    Name = "Sasha",
                    Surname = "Trikorochki",
                    Patronymic = "Vitaljevich",
                    PhoneNumber = "19(4235)386-91-39"
                }
            );

            builder.OwnsOne(Transport => Transport.LimitLoad).HasData
            (
                new
                {
                    TransportId = 1,
                    Height = 50d,
                    Width = 50d,
                    Length = 50d
                }
            );
        }
    }
}
