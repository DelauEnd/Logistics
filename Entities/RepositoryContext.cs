using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<CargoType> Types { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public RepositoryContext(DbContextOptions options) 
            : base(options)
        {
            Database.EnsureCreated();
            ConfigureAdditionals();
        }

        private void ConfigureAdditionals()
        {
            TriggersConfiguration.Configure();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfigurations(modelBuilder);
        }


        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CargoTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TrailerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new TruckConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CargoConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
