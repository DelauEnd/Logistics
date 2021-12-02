using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeautures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class CargoRepository : RepositoryBase<Cargo>, ICargoRepository
    {
        public CargoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCargo(Cargo cargo)
            => Create(cargo);

        public void DeleteCargo(Cargo cargo)
        {
            Delete(cargo);
        }

        public async Task<IEnumerable<Cargo>> GetAllCargoesAsync(CargoParameters parameters, bool trackChanges)
         => await FindAll(trackChanges)
            .Include(cargo => cargo.Type)
            .ApplyFilters(parameters)
            .Search(parameters.Search)
            .ToListAsync();

        public async Task<Cargo> GetCargoByIdAsync(Guid id, bool trackChanges)
            => await FindByCondition(cargo => cargo.Id == id, trackChanges)
            .Include(cargo => cargo.Type)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Cargo>> GetCargoesByOrderIdAsync(Guid id, CargoParameters parameters, bool trackChanges)
            => await FindByCondition(cargo => cargo.OrderId == id, trackChanges)
            .Include(cargo => cargo.Type)
            .ApplyFilters(parameters)
            .Search(parameters.Search)
            .ToListAsync();

        public async Task<IEnumerable<Cargo>> GetCargoesByRouteIdAsync(Guid? id, CargoParameters parameters, bool trackChanges)
            => await FindByCondition(cargo => cargo.RouteId == id, trackChanges)
            .Include(cargo => cargo.Type)
            .ApplyFilters(parameters)
            .Search(parameters.Search)
            .ToListAsync();

        public async Task MarkTheCargoToRouteAsync(Guid cargoId, Guid routeId)
        {
            var route = await FindByCondition(cargo => cargo.Id == cargoId, false).FirstOrDefaultAsync();
            route.RouteId = routeId;
            Update(route);
        }

        public void UpdateCargo(Cargo cargo)
        {
            Update(cargo);
        }
    }
}
