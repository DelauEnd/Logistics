using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class OrderConfiguration : DefaultGuids, IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ConfigureModel(builder);
            AddInitialData(builder);
        }

        private void ConfigureModel(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(Order => Order.Destination)
                          .WithMany(Customer => Customer.OrderDestination)
                          .HasForeignKey(Order => Order.DestinationId)
                          .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(Order => Order.Sender)
                          .WithMany(Customer => Customer.OrderSender)
                          .HasForeignKey(Order => Order.SenderId)
                          .OnDelete(DeleteBehavior.NoAction);
        }

        private void AddInitialData(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
             (
                new Order
                {
                    Id = OrderGuid,
                    UserId = LogistGuid,
                    DestinationId = CustomerGuid,
                    SenderId = CustomerGuid,
                    Status = Enums.Status.Processing,
                }
            );
        }

    }
}
