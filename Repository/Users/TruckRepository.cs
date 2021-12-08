using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class TruckRepository : RepositoryBase<Truck>, ITruckRepository
    {
        public TruckRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateTruck(Truck truck)
            => Create(truck);

        public void DeleteTruck(Truck truck)
            => Delete(truck);

        public async Task<IEnumerable<Truck>> GetAllTrucksAsync(bool trackChanges)
            => await FindAll(trackChanges)
            .Include(truck => truck.TransportedCargoType)
            .ToListAsync();

        public async Task<Truck> GetTruckByIdAsync(Guid id, bool trackChanges)
            => await FindByCondition(truck => truck.Id == id, trackChanges)
            .Include(truck => truck.TransportedCargoType)
            .SingleOrDefaultAsync();

        public async Task<Truck> GetTruckByRegistrationNumberAsync(string number, bool trackChanges)
            => await FindByCondition(truck => truck.RegistrationNumber == number, trackChanges)
            .Include(truck => truck.TransportedCargoType)
            .SingleOrDefaultAsync();

        public async Task<Truck> GetTruckByRouteIdAsync(Guid id, bool trackChanges)
        => await FindByCondition(truck => truck.Routes.Any(route => route.Id == id), trackChanges)
            .Include(truck => truck.TransportedCargoType)
            .SingleOrDefaultAsync();

        public void UpdateTruck(Truck truck)
            => Update(truck);
    }
}
