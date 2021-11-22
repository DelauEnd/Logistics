using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class RouteConfiguration : DefaultGuids, IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            AddInitialData(builder);
        }

        private void AddInitialData(EntityTypeBuilder<Route> builder)
        {
            builder.HasData
            (
                new Route
                {
                    Id = RouteGuid,
                    UserId = UserGuid,
                    TruckId = TruckGuid,                      
                }
            );
        }
    }
}
