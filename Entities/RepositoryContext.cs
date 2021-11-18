﻿using Entities.Configuration;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<CargoCategory> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }

        public RepositoryContext(DbContextOptions options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfigurations(modelBuilder);
        }

        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new CargoCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new CargoConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
