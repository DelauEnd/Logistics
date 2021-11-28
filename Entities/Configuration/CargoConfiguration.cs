using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class CargoConfiguration : DefaultGuids, IEntityTypeConfiguration<Cargo>
    {
        public async void Configure(EntityTypeBuilder<Cargo> builder)
        {
            AddInitialData(builder);
            await AddTriggers();
        }

        private async Task AddTriggers()
        {
            await DapperExecutor.ExecuteQueryAsync(Queries.OnCargoUpdate);
        }

        private void AddInitialData(EntityTypeBuilder<Cargo> builder)
        {
            builder.HasData
            (
                new Cargo
                {
                    Id = CargoGuid,
                    Title = "Initial Cargo",
                    CategoryId = CategoryGuid,
                    TypeId = TypeGuid,
                    DepartureDate = DateTime.Now,
                    ArrivalDate = DateTime.Now,
                    RouteId = RouteGuid,
                    OrderId = OrderGuid,
                    Status = EStatuses.PROCESSING,
                    Weight = 200,                           
                }
            );
            builder.OwnsOne(Cargo => Cargo.Dimensions).HasData(
                new
                {
                    CargoId = CargoGuid,
                    Height = 50d,
                    Width = 50d,
                    Length = 50d
                }
            );
        }
    }
}
